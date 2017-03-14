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
	private MapCube selectedMapCube; // 当前选择的炮塔所在的cube，选择炮塔会显示或隐藏升级UI
	public Text moneyText; // 显示金钱的文本
	public Animator moneyAnimator; // 金钱动画状态机
	private int money = 1000; // 金钱
	public GameObject upgradeCanvas; // 升级炮塔的画布UI
	public Button upgradeButton; // 升级按钮
	public Animator upgradeCanvasAnimator; // 炮塔升级画布的状态机
	
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
						if (money >= selectedTurretData.cost)
						{
							// 金钱数量变化
							ChangeMoney(-selectedTurretData.cost);
							// 创建炮塔
							mapCube.BuildTurret(selectedTurretData);
						}
						else 
						{
							// TODO钱不够，给一个提示
							moneyAnimator.SetTrigger("Flicker");
						}
					}
					else if (mapCube.turretGo != null)
					{
						if (mapCube == selectedMapCube && upgradeCanvas.activeInHierarchy)
						{
							// 选择的是同一个炮塔，并且炮塔上已经显示了升级UI
							StartCoroutine(HideUpgradeUI());
						}
						else
						{
							// 已经有了炮塔，传递炮塔位置和是否已经升级 
							ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
						}
						// 记录当前选择的炮塔
						selectedMapCube = mapCube;
					}
				}
			}
		}
	}

	// 选择了激光炮塔
	public void OnLaserSelected(bool isOn) 
	{
		if (isOn)
		{
			selectedTurretData = laserTurretData;
		}
	}

	// 选择了炮弹炮塔
	public void OnMissileSelected(bool isOn) 
	{
		if (isOn)
		{
			selectedTurretData = missileTurretData;
		}
	}

	// 选择了标准炮塔
	public void OnStandardSelected(bool isOn)
	{
		if (isOn)
		{
			selectedTurretData = standardTurretData;
		}
	}

	// 显示炮塔升级UI
	void ShowUpgradeUI(Vector3 position, bool isDisableUpgrade)
	{
		// 停止上个隐藏动画 - 不起作用
		StopCoroutine(HideUpgradeUI());
		// 每次激活都先禁用，这样动画才能正常显示
		upgradeCanvas.SetActive(false);

		// 激活炮塔升级UI
		upgradeCanvas.SetActive(true);
		// upgradeCanvas全场就只有一个对象，每次显示的时候都给他设置位置。
		upgradeCanvas.transform.position = position;
		// 升级按钮是否禁用，如果已经升级或者钱不够就禁用
		upgradeButton.interactable = !isDisableUpgrade;
	}

	// 隐藏炮塔升级UI
	IEnumerator HideUpgradeUI()
	{
		upgradeCanvasAnimator.SetTrigger("Hide");
		// 0.5秒后禁用UI
		yield return new WaitForSeconds(0.5f);
		// 隐藏炮塔升级UI
		upgradeCanvas.SetActive(false);
	}

	// 点击了升级按钮
	public void OnUpgradeButtonDown()
	{
		// 如果点击的cube下没有炮塔，则可以创建
		if (money >= selectedMapCube.turretData.costUpgraded)
		{
			// 金钱数量变化
			ChangeMoney(-selectedTurretData.costUpgraded);
			// 对cube上的炮塔进行升级
			selectedMapCube.UpgradeTurret();
			// 隐藏UI
			StartCoroutine(HideUpgradeUI());
		}
		else 
		{
			// TODO钱不够，给一个提示
			moneyAnimator.SetTrigger("Flicker");
		}
	}
	
	// 点击了拆除按钮
	public void OnDestroyButtonDown()
	{
		// 拆除cube上的炮塔
		selectedMapCube.DestroyTurret();
		// 隐藏UI
		StartCoroutine(HideUpgradeUI());
	}

}
