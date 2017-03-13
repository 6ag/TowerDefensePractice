using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 子弹AI - 实例化出来就去攻击敌人
public class Bullet : MonoBehaviour {

	public int damage = 50; // 子弹伤害
	public float speed = 40; // 子弹发射速度
	public GameObject explosionEffectPrefab; // 子弹碰到敌人的爆炸效果预制体
	private Transform target; // 攻击目标

	// 实例化子弹后需要给定攻击目标
	public void SetTarget(Transform target)
	{
		this.target = target;
	}

	void Update() 
	{
		// 子弹指向攻击目标
		transform.LookAt(target.position);
		// 向攻击目标发射
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}

	// 子弹碰撞检测
	void OnTriggerEnter(Collider other)
	{
		// 如果攻击到的是敌人
		if (other.tag == "Enemy")
		{
			// 让敌人掉血
			other.GetComponent<Enemy>().TakeDamage(damage);
			// 爆炸效果
			GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
			// 销毁子弹
			Destroy(gameObject);
		}
	}

}
