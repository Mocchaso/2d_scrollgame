using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start () {
		rigidbody2D.fixedAngle = true;
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.AddForce(Vector3.right * -50); // ついでに敵を歩かせる
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "FireBall") { // ぶつかったオブジェクトの判別
			Destroy(gameObject);
		}
	}
}
