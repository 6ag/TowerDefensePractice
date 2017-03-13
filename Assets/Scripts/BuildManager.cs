using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 建造炮塔管理类
public class BuildManager : MonoBehaviour {

	public TurretData laserTurretData; // 激光炮塔数据
	public TurretData missileTurretData; // 炮弹炮塔数据
	public TurretData standardTurretData; // 标准炮塔数据
	private TurretData selectedTurretData; // 当前选择的炮塔数据，将要建造
	public Text moneyText; // 显示金钱的文本
	public Animator moneyAnimator; // 金钱动画状态机
	private int money = 1000; // 金钱
	
	// 金钱发生变化
	void ChangeMoney(int change)
	{
		money += change;
		// 修改金钱UI
		moneyText.text = "¥ " + money;
	}
	
	void Update() 
	{
		// 鼠标左键按下
		if (Input.GetMouseButtonDown(0))
		{
			// 鼠标是否点击到UI上 - 如果是手机上，则需要判断触摸
			if (EventSystem.current.IsPointerOverGameObject() == false)
			{
				// 发射射线
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				// 射线检测，参数：射线、碰撞信息、最大距离、检测的层。返回是否碰撞到
				bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
				if (isCollider)
				{
					// 获取到点击的Cube
					MapCube mapCube = hit.collider.gameObject.GetComponent<MapCube>();
					// 已经选择了一个默认的炮塔类型，并且点击的位置的炮塔还没有创建
					if (selectedTurretData != null && mapCube.turretGo == null)
					{
						// 如果点击的cube下没有炮塔，则可以创建
						if (money > selectedTurretData.cost)
						{
							// 金钱数量变化
							ChangeMoney(-selectedTurretData.cost);
							// 创建炮塔
							mapCube.BuildTurret(selectedTurretData.turretPrefab);
						}
						else 
						{
							// TODO钱不够，给一个提示
							moneyAnimator.SetTrigger("Flicker");
						}
					}
					else if (mapCube.turretGo != null)
					{
						// TODO已经有了炮塔，判断是否需要升级 
						
					}
				}
			}
		}
	}

	public void OnLaserSelected(bool isOn) 
	{
		if (isOn)
		{
			selectedTurretData = laserTurretData;
		}
	}

	public void OnMissileSelected(bool isOn) 
	{
		if (isOn)
		{
			selectedTurretData = missileTurretData;
		}
	}

	public void OnStandardSelected(bool isOn)
	{
		if (isOn)
		{
			selectedTurretData = standardTurretData;
		}
	}

}
