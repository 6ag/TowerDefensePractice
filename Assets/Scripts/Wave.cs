using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 保存每一波敌人生成所需要的属性
[System.Serializable] // 可以被序列化，就是能显示在Inspector面板上
public class Wave {
	public GameObject enemyPrefab; // 敌人预制体
	public int count; // 一波敌人的数量
	public float rate; // 每个敌人生成的间隔
}
