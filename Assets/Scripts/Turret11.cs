using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 炮塔脚本，控制炮塔攻击
public class Turret : MonoBehaviour {

	// 在攻击范围内的敌人
	private List<GameObject> enemies = new List<GameObject>();
	public float attackRateTime = 1.0f; // 攻击间隔
	private float timer = 0; // 计时器
	public GameObject bulletPrefab; // 子弹预制体
	public Transform firePosition; // 炮塔发射口位置
	public Transform head; // 炮塔头部
	public bool isUseLaser = false; // 是否使用激光炮塔
	public float damageRate = 70; // 激光炮塔攻击伤害 1秒70伤害
	public LineRenderer laserRenderer; // 激光渲染器
	public GameObject laserEffect; // 激光攻击特效
	
	void Start()
	{
		// 炮塔刚实例化就能开始攻击，让 timer >= attackRateTime 成立
		timer = attackRateTime;
	}

	void Update()
	{
		// 炮塔对准敌人
		if (enemies.Count > 0 && enemies[0] != null)
		{
			Vector3 targetPosition = enemies[0].transform.position;
			targetPosition.y = head.position.y;
			head.LookAt(targetPosition);
		}

		// 是否是激光炮塔
		if (isUseLaser)
		{
			Debug.Log ("piu piu  " + gameObject.name + "\n");
			if (enemies.Count > 0)
			{
				// 如果目标已经被杀死或者已经到达终点，则移除集合
				if (enemies[0] == null)
				{
					UpdateEnemys();
				}
				// 清理了空元素后，再次判断是否还有敌人能够被攻击
				if (enemies.Count > 0)
				{
					if (laserRenderer.enabled == false)
					{
						laserRenderer.enabled = true;
						laserEffect.SetActive(true);
					}
					// 激光攻击目标
					laserRenderer.SetPositions(new Vector3[]{firePosition.position, enemies[0].transform.position});
					laserEffect.transform.position = enemies[0].transform.position;
					laserEffect.transform.LookAt(new Vector3(transform.position.x, enemies[0].transform.position.y, transform.position.z));
					// 造成持续伤害
					enemies[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
				}
			}
			else
			{
				// 可以攻击状态
				laserRenderer.enabled = false;
				laserEffect.SetActive(false);
			}
			
		}


		else 
		{
			
			// 普通炮塔，计时器递增
			timer += Time.deltaTime;
			// 有存在的地方，并且计时器大于攻击间隔就重置，并调用攻击方法
			if (enemies.Count > 0 && timer >= attackRateTime)
			{
				// 定时器清空
				Debug.Log ("pou pou  " + gameObject.name + "\n");
				timer = 0;
				Attack();
			}
		}

	}

	// 进入攻击范围
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			enemies.Add(other.gameObject);
		}
	}

	// 离开攻击范围 - 如果炮塔范围笼罩终点，会没有移除敌人
	void OnTriggerExit(Collider other)
	{
		// 敌人离开攻击范围，移除集合
		if (other.tag == "Enemy")
		{
			enemies.Remove(other.gameObject);
		}
	}

	// 攻击敌人
	void Attack()
	{
		if (enemies[0] == null)
		{
			UpdateEnemys();
		}
		if (enemies.Count > 0)
		{
			GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
			bullet.GetComponent<Bullet>().SetTarget(enemies[0].transform);
		}
		else
		{
			timer = attackRateTime;
		}
		
	}

	// 更新敌人集合 - 移除已经被杀死或者到达终点的敌人
	void UpdateEnemys()
	{
		// 存储所有为空的元素
		List<int> emptyIndexList = new List<int>();
		for (int i = 0; i < enemies.Count; i++)
		{
			if (enemies[i] == null)
			{
				emptyIndexList.Add(i);
			}
		}
		// 移除空元素
		for (int i = 0; i < emptyIndexList.Count; i++)
		{
			enemies.RemoveAt(emptyIndexList[i] - i);
		}
	}

}
