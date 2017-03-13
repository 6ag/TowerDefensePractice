using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 炮台数据类
[System.Serializable] // 可序列化
public class TurretData {
	public GameObject turretPrefab; // 基础版预制体
	public int cost; // 基础版价格
	public GameObject turretUpgradedPrefab; // 加强版预制体
	public int costUpgraded; // 升级的价格
}

// 炮塔类型枚举
public enum TurretType {
	LaserTurret, // 激光炮塔
	MissileTurret, // 炮弹炮塔
	StandardTurret // 标准炮塔
}
