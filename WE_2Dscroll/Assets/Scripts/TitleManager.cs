﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Spaceキー押下でゲーム画面に遷移
		if (Input.GetKey(KeyCode.Space)) {
			SceneManager.LoadScene("Main");
		}
	}
}
