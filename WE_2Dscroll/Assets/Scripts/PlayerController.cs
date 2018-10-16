using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	private float run_force;
	private float max_run_speed;
	public float dy; // y方向の移動量(ジャンプの高さ)
	private bool grounded;
	public Rigidbody2D rigidbody2D;
	public LayerMask groundlayer;
	public GameObject fireball;
	public static bool isLeft; // プレイヤーオブジェクトが左を向いているか(openbookの例ではファイアボールの向きを見ている)

	// Use this for initialization
	void Start () {
		run_force = 50;
		max_run_speed = 10;
		dy = 1500f;
		grounded = false;
		rigidbody2D.fixedAngle = true; // PlayerのRotationを固定
		isLeft = false;
	}
	
	// Update is called once per frame
	void Update () {
		// 接地しているかどうかを調べる
		grounded = Physics2D.Linecast(transform.position, transform.position - transform.up * 1.2f, groundlayer);
		if (Input.GetKeyDown(KeyCode.Space) && grounded) { // 接地している状態でジャンプした
			rigidbody2D.AddForce(Vector2.up * dy);
		}

		float run_speed_tmp = Mathf.Abs(rigidbody2D.velocity.x); // 現在のx方向の速度
		if (Input.GetKey(KeyCode.LeftArrow)) { // 左に移動中
			if (!isLeft && grounded) { // 右を向いていたら&接地していたら
				// Playerオブジェクトのスプライトを反転
				Vector3 nowScale = transform.localScale;
				nowScale.x *= -1;
				transform.localScale = nowScale;
				isLeft = true;
			}
			if (run_speed_tmp < max_run_speed) { // スピードの上限に達していなければ
				// 移動するスピードを求めて左向きにAddForce
				// -> openbookの例題ではvelocityに再代入していたが、ジャンプ中に移動できないため変更
				rigidbody2D.AddForce(transform.right * (-1) * run_force);
			} else {
				// transform.positionを直接更新することで等速に左へ移動させる
				transform.position += new Vector3(run_speed_tmp * Time.deltaTime * (-1), 0, 0);
			}
		}
		if (Input.GetKey(KeyCode.RightArrow)) { // 右に移動中
			if (isLeft && grounded) { // 左を向いていたら&接地していたら
				// Playerオブジェクトのスプライトを反転
				Vector3 nowScale = transform.localScale;
				nowScale.x *= -1;
				transform.localScale = nowScale;
				isLeft = false;
			}
			if (run_speed_tmp < max_run_speed) { // スピードの上限に達していなければ
				// 移動するスピードを求めて右向きにAddForce
				rigidbody2D.AddForce(transform.right * run_force);
			} else {
				// transform.positionを直接更新することで等速に右へ移動させる
				transform.position += new Vector3(run_speed_tmp * Time.deltaTime, 0, 0);
			}
		}

		if (Input.GetKeyDown("f")) {
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