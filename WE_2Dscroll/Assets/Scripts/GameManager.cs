using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public Text time_text; // 残り時間を表示するテキスト
	public Text life_text; // 残りライフを表示するテキスト
	private int lifemax; // ライフの上限
	private int tmp_time; // 現在の残り時間を記録する変数
	private float countdown_timer; // 1秒ごとに残り時間の表示を変えるための変数
	public static int damaged; // 受けたダメージ(初期値0)
	public static bool isGameOver; // ゲームオーバーのフラグ
	public static bool isGoaled; // ゲームクリアのフラグ

	// Use this for initialization
	void Start () {
		lifemax = 3;
		tmp_time = 3; // 初期値は制限時間となる
		time_text.text = "TIME: " + tmp_time + " s"; // 残り時間の表示
		countdown_timer = 1f;
		damaged = 0;
		isGameOver = false;
		isGoaled = false;
	}
	
	// Update is called once per frame
	void Update () {
		// 残り時間のカウントダウン
		countdown_timer -= Time.deltaTime;
		if (countdown_timer <= 0) {
			tmp_time -= 1; // 残り時間を1秒減らす
			time_text.text = "TIME: " + tmp_time + " s"; // 残り時間の表示を更新
			if (tmp_time == 0) {
				// 残り時間が0になったらゲームオーバー
				isGameOver = true;
			}
			countdown_timer = 1f; // タイマーのリセット
		}

		// 残りライフの表示を更新
		if (lifemax - damaged >= 0) {
			life_text.text = "LIFE: " + (lifemax - damaged);
		} else {
			// 残りライフが0になったらゲームオーバー
			isGameOver = true;
		}

		if (isGameOver || isGoaled) {
			// ゲームオーバー or ゲームクリア時にリザルト画面に遷移
			SceneManager.LoadScene("Result");
		}
	}
}
