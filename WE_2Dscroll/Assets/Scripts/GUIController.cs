using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour {
	public Text guitext;
	public static int damaged = 0; // 受けたダメージ(初期値0)
	public int lifemax = 3; // ライフの上限
	public static bool isGoaled = false;

	// Use this for initialization
	void Start () {
		damaged = 0;
		lifemax = 3;
		isGoaled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isGoaled) {
			guitext.text = "GOAL!";
		} else if(lifemax - damaged >= 0) {
			guitext.text = "LIFE: " + (lifemax - damaged);
		} else {
			SceneManager.LoadScene("Main");
		}
	}
}
