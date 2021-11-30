using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

	public Transform playerTransform;

	private void LateUpdate()
	{
		Vector3 new_position = playerTransform.position;
		new_position.y = transform.position.y;
		transform.position = new_position;
	}
}
