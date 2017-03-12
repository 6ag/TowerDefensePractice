using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敌人路径点
public class Waypoints : MonoBehaviour {

	public static Transform[] positions; // 所有路径点

	void Awake()
	{
		positions = new Transform[transform.childCount];
		for (int i = 0; i < positions.Length; i++) 
		{
			positions[i] = transform.GetChild(i);
		}
	}
}
