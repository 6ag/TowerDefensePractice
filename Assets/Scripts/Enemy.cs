using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 一个Enemy就是一个敌人
public class Enemy : MonoBehaviour {

	public float speed = 10; // 移动速度
	private Transform[] positions; // 所有路径点
	private int index = 0; // 当前正在像哪个点移动

	void Start() 
	{
		// 获取路径点数组
		positions = Waypoints.positions;
	}
	
	void Update() 
	{
		Move();
	}

	// 怪物移动处理
	void Move() 
	{
		// 防止越界
		if (index > positions.Length - 1) return;
		// 目标点 - 自身点 = 自身点到目标点的方向
		transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
		// 如果目标点和自身点的位置小于0.2m，则开始像下个目标点移动
		if (Vector3.Distance(positions[index].position, transform.position) < 0.2) 
		{
			index++;
		}
		// 最后一次执行，index会大于数组最大角标
		if (index > positions.Length - 1) 
		{
			// 当前敌人已经到达目的地
			ReachDestination();
		}
	}

	// 敌人到达目的地
	void ReachDestination() 
	{
		// 敌人到达目的地后，销毁当前游戏物体
		GameObject.Destroy(gameObject);
	}

	// 敌人销毁
	void OnDestroy()
	{
		// 敌人销毁的时候，将存活计数器-1
		EnemySpawner.CountEnemyAlive--;
	}

}
