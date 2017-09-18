using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject endUI; // 结束UI
	public Text endMessage; // 游戏结束提示信息
	public static GameManager instance;
	private EnemySpawner enemySpawner; // 敌人孵化器
    public BulletPrototype bulletPrototype;
    public GameObject missile, bullet;
	void Awake()
	{
		instance = this;
		enemySpawner = GetComponent<EnemySpawner>();
        bulletPrototype = new BulletPrototype(bullet,missile);
	}

	public void Win()
	{
		endUI.SetActive(true);
		endMessage.text = "胜 利";
	}

	public void Failed()
	{
		endUI.SetActive(true);
		endMessage.text = "失 败";
		// 停止生成敌人
		enemySpawner.Stop();
	}

	// 重玩
	public void OnButtonRetryDown()
	{
		// 重新加载游戏场景
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	// 菜单
	public void OnButtonMenuDown()
	{
		// 加载菜单场景
		SceneManager.LoadScene(0);
	}

}
