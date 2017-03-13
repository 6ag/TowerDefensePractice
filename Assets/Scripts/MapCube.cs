using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 地图cube，可以放置炮塔
public class MapCube : MonoBehaviour {

	[HideInInspector] // [HideInInspector]可以隐藏这个public属性显示在inspector面板
	public GameObject turretGo; // 当前cube下的炮塔，如果为空，则表示当前位置没有炮塔
	public GameObject buildEffect; // 构建炮塔的特效预制体
	private Renderer cubeRenderer; // 渲染器

	void Start()
	{
		cubeRenderer = GetComponent<Renderer>();
	}
	
	// 构建炮塔
	public void BuildTurret(GameObject turretPrefab) 
	{
		// 实例化炮塔
		turretGo = GameObject.Instantiate(turretPrefab, transform.position, Quaternion.identity);
		// 构建炮塔的尘土特效
		GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
		Destroy(effect, 1);
	}

	void OnMouseEnter()
	{
		// 当前位置没有炮塔，鼠标没有在UI上。则改变渲染器颜色
		if (turretGo == null && !EventSystem.current.IsPointerOverGameObject())
		{
			cubeRenderer.material.color = Color.red;
		}
	}

	void OnMouseExit()
	{
		// 鼠标移开后恢复颜色
		cubeRenderer.material.color = Color.white;
	}

}
