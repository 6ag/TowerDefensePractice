using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 子弹AI - 实例化出来就去攻击敌人
public class Bullet : MonoBehaviour {

	public int damage = 50; // 子弹伤害
	public float speed = 40; // 子弹发射速度
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
}
