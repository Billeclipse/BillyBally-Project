using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour {

	public GameObject loadingScreen;
	public Slider loadingSlider;
	public TextMeshProUGUI progressText;

	public void LoadNextSceneAsync()
	{
		StartCoroutine(LoadAsynchronously());
	}

	public void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public static void LoadMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void QuitGame()
	{
		Application.Quit();
	}


	IEnumerator LoadAsynchronously()
	{		
		AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

		if (loadingScreen && loadingSlider)
		{
			loadingScreen.SetActive(true);
			while (!operation.isDone)
			{
				float progress = Mathf.Clamp01(operation.progress / .9f);
				loadingSlider.value = progress;
				progressText.SetText(progress * 100 + "%");
				//Debug.Log(progress);
				yield return null;
			}
		}		
	}
}
