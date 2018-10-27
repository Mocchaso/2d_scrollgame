using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public Rigidbody2D rigidbody2D;
	public LayerMask groundlayer; // 接地判定で使用するレイヤーマスク
	private float jump_force; // ジャンプ時に加える力
	private float jump_vy_limit; // ジャンプ中のy方向の速度制限(Spaceキー長押しでより高くジャンプさせる時に使用)
	private float run_force; // 走り始めに加える力
	private float run_speed; // 走っている間の速度
	private float run_threshold; // 速度切り替え判定のための閾値
	private bool isGround; // 接地状態を管理するbool変数
	private string state; // プレイヤーの状態管理
	private float state_effect; // 状態に応じて横移動速度を変えるための係数
	private int key; // 左右キーの入力管理
	public static bool isLeft; // プレイヤーオブジェクトが左を向いているか(openbookの例ではファイアボールの向きを見ている)
	private bool prev_isLeft; // 1フレーム前のプレイヤーの向きを保存(Playerオブジェクトのスプライト反転に使用)
	public GameObject fireball; // ファイアボールを管理するGameObject
	

	// Use this for initialization
	void Start () {
		rigidbody2D.fixedAngle = true; // PlayerのRotationを固定
		jump_force = 1000f;
		jump_vy_limit = 50f;
		run_force = 30f;
		run_speed = 8f;
		run_threshold = 12f;
		isGround = true;
		state = "IDLE"; // 待機状態
		state_effect = 1f;
		key = 0; // 左右どちらにも入力されていない状態
		isLeft = false;
		prev_isLeft = false;
	}
	
	// Update is called once per frame
	void Update () {
		GetInputKey();
		ChangeState();
		Move();
		ShootFireBall();
	}

	void GetInputKey() {
		// 左右キーの入力を取得して保存する
		key = 0;
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			key = -1;
			if (isGround) { // 接地していれば向き変更
				prev_isLeft = isLeft; // 向きの状態を変える前に保存しておく
				isLeft = true;
				ReversePlayerSprite();
			}
		}
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			key = 1;
			if (isGround) { // 接地していれば向き変更
				prev_isLeft = isLeft; // 向きの状態を変える前に保存しておく
				isLeft = false;
				ReversePlayerSprite();
			}
		}
	}

	void ChangeState() {
		// 接地しているかどうかを調べる
		isGround = Physics2D.Linecast(transform.position, transform.position - transform.up * 1.2f, groundlayer);

		if (isGround) { // 接地している場合
			if (key != 0) { // 走行中
				state = "RUN";
				state_effect = 1f;
			} else { // 待機状態
				state = "IDLE";
				state_effect = 1f;
			}
		} else { // 空中にいる場合
			if (rigidbody2D.velocity.y > 0) { // 上昇中
				state = "JUMP";
				state_effect = 0.5f;
			} else if (rigidbody2D.velocity.y < 0) { // 下降中
				state = "FALL";
				state_effect = 0.5f;
			}
		}
	}

	void Move() {
		// 接地している時にSpaceキー押下でジャンプ
		// 実装予定：押す長さによって高さが変わる
		if (isGround && Input.GetKeyDown(KeyCode.Space)) {
			rigidbody2D.AddForce(Vector2.up * jump_force);
		}

		// 左右の移動
		float speed_x = rigidbody2D.velocity.x;
		if (Mathf.Abs(speed_x) < run_threshold) { // 一定の速度に達するまではAddforceで力を加える
			// 未入力の場合はkey=0のため移動しない
			rigidbody2D.AddForce(transform.right * key * run_force * state_effect);
		} else { // transform.positionを直接書き換えて同一速度で移動する
			transform.position += new Vector3 (run_speed * Time.deltaTime * key * state_effect, 0, 0);
		}
	}

	void ReversePlayerSprite() {
		if (isLeft != prev_isLeft) { // 現在のプレイヤーの向きが1フレーム前と異なっていたら
			Vector3 now_scale = transform.localScale;
			now_scale.x *= -1;
			transform.localScale = now_scale;
		}
	}

	void ShootFireBall() {
		// fキー押下でファイアボールを撃つ
		if (Input.GetKeyDown("f")) {
			Instantiate(fireball, transform.position, transform.rotation);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Enemy") { // ぶつかったオブジェクトの判別
			GameManager.damaged++;
		} else if (collision.gameObject.tag == "GameOver") {
			// 落下したらゲームオーバー
			GameManager.isGameOver = true;
		}
	}

	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.gameObject.tag == "Goal") { // ぶつかったオブジェクトの判別
			GameManager.isGoaled = true;
		}
	}
}