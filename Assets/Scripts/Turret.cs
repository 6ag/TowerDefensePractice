using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 炮塔脚本，控制炮塔攻击
public class Turret : MonoBehaviour {

	// 在攻击范围内的敌人
	public List<GameObject> enemies = new List<GameObject>();
	public float attackRateTime = 1.0f; // 攻击间隔
	private float timer = 0; // 计时器
	public GameObject bulletPrefab; // 子弹预制体
	public Transform firePosition; // 炮塔发射口位置
	
	void Start()
	{
		// 炮塔刚实例化就能开始攻击，让 timer >= attackRateTime 成立
		timer = attackRateTime;
	}

	void Update()
	{
		// 计时器递增
		timer += Time.deltaTime;
		// 有存在的地方，并且计时器大于攻击间隔就重置，并调用攻击方法
		if (enemies.Count > 0 && timer >= attackRateTime)
		{
			// 定时器清空
			timer = 0;
			Attack();
		}

		if (enemies.Count > 0)
		{

		}
	}

	// 进入攻击范围
	void OnTriggerEnter(Collider other)
	{
		// 敌人进入攻击范围，加入集合
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
		// 实例化子弹，子弹位置和方向于炮塔枪口一致
		GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
		// 给子弹设置攻击目标
		bullet.GetComponent<Bullet>().SetTarget(enemies[0].transform);
	}

}
