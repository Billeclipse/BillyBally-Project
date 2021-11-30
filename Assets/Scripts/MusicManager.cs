using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;

	private AudioSource audioSource;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		//Debug.Log("Don't Destroy on load: " + name);
	}

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefsManager.GetMasterVolume();
	}

	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		//Debug.Log("Level Loaded");
		//Debug.Log(scene.name);
		//Debug.Log(mode);
		AudioClip thisLevelMusic = levelMusicChangeArray[scene.buildIndex];
		//If there's some music attached.
		if (thisLevelMusic)
		{
			audioSource.clip = thisLevelMusic;
			audioSource.loop = true;
			audioSource.volume = PlayerPrefsManager.GetMasterVolume();
			audioSource.Play();
		}
	}

	public void SetVolume(float volume)
	{
		audioSource.volume = volume;
	}
}
