using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 控制摄像机
public class ViewController : MonoBehaviour {
	
	public float translateSpeed = 25; // 视角移动速度
	public float scaleSpeed = 500; // 视角缩放速度

	void Update () 
	{
		// 方向按键控制视角前后左右移动
		float h = Input.GetAxis("Horizontal") * translateSpeed;
		float v = Input.GetAxis("Vertical") * translateSpeed;

		// 鼠标滑轮控制视角的远近
		float mouse = Input.GetAxis("Mouse ScrollWheel") * scaleSpeed;

		// 视角按照世界坐标系统，这样不受自身旋转影响
		transform.Translate(new Vector3(h, mouse, v) * Time.deltaTime, Space.World);
	}
}
