using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 地图cube，可以放置炮塔
public class MapCube : MonoBehaviour {

	[HideInInspector] // [HideInInspector]可以隐藏这个public属性显示在inspector面板
	public GameObject turretGo; // 当前cube下的炮塔，如果为空，则表示当前位置没有炮塔
	[HideInInspector] // [HideInInspector]可以隐藏这个public属性显示在inspector面板
	public bool isUpgraded = false; // 炮塔是否已经升级过
	public GameObject buildEffect; // 构建炮塔的特效预制体
	private Renderer cubeRenderer; // 渲染器
	public TurretData turretData; // 当前cube下的炮塔数据

	void Start()
	{
		cubeRenderer = GetComponent<Renderer>();
	}
	
	// 构建炮塔
	public void BuildTurret(TurretData turretData) 
	{
		// 让当前cube持有炮塔的数据，方便对cube上的炮塔升级
		this.turretData = turretData;
		// 每次构建炮塔都重置升级标识
		isUpgraded = false;
		// 实例化炮塔
		turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
		// 构建炮塔的尘土特效
		GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
		Destroy(effect, 1.5f);
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

	// 升级当前cube下的炮塔
	public void UpgradeTurret()
	{
		// 已经升级过
		if (isUpgraded)
		{
			return;
		}

		Destroy(turretGo);
		// 升级炮塔后修改标识
		isUpgraded = true;
		// 实例化加强版的炮塔
		turretGo = GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
		// 升级炮塔的尘土特效
		GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
		Destroy(effect, 1.5f);
	}

	// 拆除炮塔
	public void DestroyTurret()
	{
		Destroy(turretGo);
		isUpgraded = false;
		turretGo = null;
		turretData = null;
	}

}
