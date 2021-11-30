using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	public Vector3 offset;
	public PlayerController player;

	public bool followingPlayer = true;
	
	void FixedUpdate () {
		if (player && followingPlayer)
		{
			float pos_x = player.transform.position.x;
			float pos_y = player.transform.position.y;
			float pos_z = player.transform.position.z;
			transform.position = new Vector3(pos_x, pos_y, pos_z) + offset;
		}
	}
}
