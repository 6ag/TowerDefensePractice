using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

	// 开始游戏
	public void OnStartGame()
	{
		// 加载编号为1的场景
		SceneManager.LoadScene(1);
	}

	// 退出游戏
	public void OnExitGame()
	{
		// 退出游戏
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

}
