using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject player; // プレイヤーのオブジェクト
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// カメラに、プレイヤーを常に追従させる
		Vector3 player_pos = player.transform.position;
		transform.position = new Vector3(player_pos.x + 4.602f, player_pos.y + 3f, player_pos.z - 7.88f);
	}
}
