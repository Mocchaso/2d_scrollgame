using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private float speed = 10;
	private bool grounded = false;
	public Rigidbody2D rigidbody2D;
	public LayerMask groundlayer;

	// Use this for initialization
	void Start () {
		// PlayerのRotationを固定
		rigidbody2D.fixedAngle = true;		
	}
	
	// Update is called once per frame
	void Update () {
		// 接地しているかどうかを調べる
		grounded = Physics2D.Linecast(transform.position, transform.position - transform.up * 1.2f, groundlayer);
		if (Input.GetKeyDown("space") && grounded) {
			// ジャンプ
			rigidbody2D.AddForce(Vector2.up * 30000f);
		}

		// 左右のキー入力を受け取る
		float x = Input.GetAxisRaw("Horizontal");

		// 移動する向きを求める
		Vector2 direction = new Vector2(x, 0).normalized;

		// 移動するスピードを求めて代入する
		rigidbody2D.velocity = direction * speed;
	}
}