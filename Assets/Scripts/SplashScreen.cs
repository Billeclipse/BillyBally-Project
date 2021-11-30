using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	public float autoLoadNextLevelAfter;

	// Use this for initialization
	void Start () {
		Invoke("AutoLoadNextLevel", autoLoadNextLevelAfter);
	}	

	private void AutoLoadNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
