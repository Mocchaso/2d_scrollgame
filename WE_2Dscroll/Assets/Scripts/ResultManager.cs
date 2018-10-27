using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour {
	private Text result_message; // リザルト画面に表示するテキスト

	// Use this for initialization
	void Start () {
		result_message = GameObject.Find("ResultMessage").GetComponent<Text>();
		if (GameManager.isGameOver) {
			result_message.text = "Game Over...";
		} else if (GameManager.isGoaled) {
			result_message.text = "Game Cleared!";
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Spaceキー押下でタイトル画面に遷移
		if (Input.GetKey(KeyCode.Space)) {
			SceneManager.LoadScene("Title");
		}
	}
}
