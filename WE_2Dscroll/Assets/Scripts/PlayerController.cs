using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	private float run_speed = 8;
	private bool grounded = false;
	public Rigidbody2D rigidbody2D;
	public LayerMask groundlayer;
	public GameObject fireball;
	public static bool isLeft = false;

	// Use this for initialization
	void Start () {
		// PlayerのRotationを固定
		rigidbody2D.fixedAngle = true;		
	}
	
	// Update is called once per frame
	void Update () {
		// 左右のキー入力を受け取る
		float x = Input.GetAxisRaw("Horizontal");

		// 移動する向きを求める
		Vector2 direction = new Vector2(x, 0).normalized;

		// 移動するスピードを求めて代入する
		rigidbody2D.velocity = direction * run_speed;

		// 接地しているかどうかを調べる
		grounded = Physics2D.Linecast(transform.position, transform.position - transform.up * 1.2f, groundlayer);

		if (Input.GetKeyDown("space") && grounded) {
			rigidbody2D.AddForce(Vector2.up * 50000f); // ジャンプ
		}
		if (Input.GetKeyDown("l")) {
			isLeft = true;
			Instantiate(fireball, transform.position, transform.rotation);
		}
		if (Input.GetKeyDown("r")) {
			isLeft = false;
			Instantiate(fireball, transform.position, transform.rotation);
		}

		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Enemy") { // ぶつかったオブジェクトの判別
			GUIController.damaged++;
		} else if (collision.gameObject.tag == "GameOver") {
			// 落下したらゲームオーバー、同じシーンをリロードする
			SceneManager.LoadScene("Main");
		}
	}

	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.gameObject.tag == "Goal") { // ぶつかったオブジェクトの判別
			GUIController.isGoaled = true;
		}
	}
}