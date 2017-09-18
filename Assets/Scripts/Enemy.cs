using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 一个Enemy就是一个敌人
public class Enemy : MonoBehaviour {

	private float totalHp; // 保存总血量，用于计算血条进度。因为hp会递减
	public float hp = 150; // 敌人血量
	public Slider hpSlider; // 血条
	public float speed = 10; // 移动速度
	public GameObject explosionEffectPrefab; // 敌人死亡爆炸特效
	private Transform[] positions; // 所有路径点
	private int index = 0; // 当前正在像哪个点移动-
	void Start() 
	{
		totalHp = hp;
		positions = Waypoints.positions;
	}
	
	void Update() 
	{
		Move();
	}

	void Move() 
	{
		if (index > positions.Length - 1) return;
		transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
		if (Vector3.Distance(positions[index].position, transform.position) < 0.2) 
		{
			index++;
		}
		if (index > positions.Length - 1) 
		{
			ReachDestination();
		}
	}

    public void resetar(Vector3 position)
    {
        gameObject.transform.position = position;
        hp = totalHp;
        hpSlider.value = 1;
        index = 0;
       
    }

    // 敌人到达目的地
    void ReachDestination() 
	{
		// 敌人到达目的地后，销毁当前游戏物体
		GameObject.Destroy(gameObject);
        // 游戏失败
        Debug.Log(this);
		GameManager.instance.Failed();
	}

	// 敌人销毁
	void OnDestroy()
	{
		// 敌人销毁的时候，将存活计数器-1
		EnemySpawner.CountEnemyAlive--;
	}

	// 受到伤害
	public void TakeDamage(float damage)
	{
		if (hp <= 0)
		{
			return;
		}
		hp -= damage;
		hpSlider.value = hp / totalHp;
		if (hp <= 0)
		{
			Die();
		}
	}

	// 敌人死亡
	void Die()
	{
		GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
		Destroy(effect, 1.5f);
        OnDestroy();
        EnemySpawner.Reuse(gameObject);
	//	Destroy(gameObject);
	}

}
