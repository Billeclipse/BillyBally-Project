using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shredder : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Intel"))
		{
			//Debug.Log("Intel Collided");
			Destroy(other.gameObject);
		}else if (other.gameObject.CompareTag("Player"))
		{
			//Debug.Log("Player Collided");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

}
