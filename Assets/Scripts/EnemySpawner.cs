using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敌人孵化器
public class EnemySpawner : MonoBehaviour {

	public Wave[] waves; // 每个元素 保存每一波敌人生成所需要的属性，有多少个元素就生成多少波敌人
	public Transform START; // 生成敌人的位置
	public float waveRate = 0.2f; // 上一波敌人死干净后重新生成一波敌人的时间间隔
	public static int CountEnemyAlive = 0; // 存活的敌人数量
	private Coroutine coroutine; // 协程

	void Start()
	{
		coroutine = StartCoroutine(SpawnEnemy());
	}

	// 生成敌人
	IEnumerator SpawnEnemy() 
	{
		foreach (Wave wave in waves)
		{
			for (int i = 0; i < wave.count; i++) 
			{
				// 生成敌人
				GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
				// 每次生成敌人都让敌人存活计数器+1,并在敌人死亡的时候-1
				CountEnemyAlive++;
				// 每一波最后一个敌人生成后无需暂停
				if (i != wave.count - 1) 
				{
					// 每个敌人生成间隔
					yield return new WaitForSeconds(wave.rate);
				}
			}
			while (CountEnemyAlive > 0)
			{
				// 如果还有敌人没有死，就一直等待
				yield return 0;
			}
			// 上一波敌人死干净后重新生成一波敌人的时间间隔
			yield return new WaitForSeconds(waveRate);
		}

		while (CountEnemyAlive > 0)
		{
			yield return 0;
		}
		// 没有敌人存活，游戏胜利
		GameManager.instance.Win();
	}

	// 停止生成敌人
	public void Stop()
	{
		StopCoroutine(coroutine);
	}

}
