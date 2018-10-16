using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {
	public float ball_speed = 0;
	public Rigidbody2D rigidbody2D;

	// Use this for initialization
	IEnumerator Start () {
		if (!PlayerController.isLeft) { // Playerが向いている向き：左
			ball_speed = 1000;
		} else { // Playerが向いている向き：右
			ball_speed = -1000;
			// ファイアボールのスプライト付きオブジェクトを左右反転させる
			Vector3 nowScale = transform.localScale;
			nowScale.x *= -1;
			transform.localScale = nowScale;
		}

		rigidbody2D.AddForce(Vector3.right * ball_speed);
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
