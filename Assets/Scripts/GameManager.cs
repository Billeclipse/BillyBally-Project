using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{	
	public int max_intels = 10;	
	public Vector3 max_spawn_radius;
	public Vector3 min_spawn_radius;
	public GameObject intel_prefab;
	public GameObject intel_spawner;	
	public TextMeshProUGUI intelCountText;

	public int max_minutes = 60;
	public TextMeshProUGUI timerText;
	public TextMeshProUGUI newHighScoreText;
	public TextMeshProUGUI currentScoreText;
	public GameObject highScoreMenu;	
	public GameObject joystickController;
	public GameObject standardUI;
	public GameObject tutorialMenu;

	public PlayerController player;
	public Animator SpaceshipAnimator;

	private float start_time;
	private float delta_time;
	private bool is_game_ended = false;	

	void Start()
	{
		for(int i=0; i<max_intels; i++)
		{
			SpawnIntel();
		}
		start_time = Time.time;

		if (PlayerPrefsManager.GetEnableTutorial())
		{
			tutorialMenu.SetActive(true);
			standardUI.SetActive(false);
			Time.timeScale = 0f;
		}
		else
		{
			tutorialMenu.SetActive(false);
			standardUI.SetActive(true);
		}
	}
	
	private void FixedUpdate()
	{
		if (intelCountText)
		{
			int intels = intel_spawner.transform.childCount;
			if(intels == 0)
			{
				intelCountText.color = Color.red;
				intelCountText.SetText(" GO TO SPACESHIP!");
			}
			else
			{
				intelCountText.SetText(" Intels: {0}", intels);
			}			
		}

		if (timerText && !is_game_ended)
		{
			delta_time = Time.time - start_time;			
			timerText.SetText(DeltaTimeToText(delta_time));
		}
		
		if(joystickController && !is_game_ended)
			joystickController.SetActive(PlayerPrefsManager.GetJoystickEnabled());
	}

	private void SpawnIntel()
	{
		float random_x = Random.Range(min_spawn_radius.x, max_spawn_radius.x);
		float random_z = Random.Range(min_spawn_radius.z, max_spawn_radius.z);
		Vector3 new_pos = new Vector3(random_x, min_spawn_radius.y, random_z);
		GameObject intel = Instantiate(intel_prefab) as GameObject;
		intel.transform.parent = intel_spawner.transform;
		intel.transform.position = intel_prefab.transform.position + new_pos;
		intel.transform.rotation = intel_prefab.transform.rotation;
		//Debug.Log("Intels spawned: " + intel_spawner.transform.childCount);
	}
	
	public void EndLevel()
	{
		if (!is_game_ended)
		{
			if (PlayerPrefsManager.GetHighScore() == -1f || delta_time < PlayerPrefsManager.GetHighScore())
			{
				PlayerPrefsManager.SetHighScore(delta_time);
				PlayerPrefsManager.SavePreferences();
				if (newHighScoreText)
				{
					currentScoreText.gameObject.SetActive(false);
					newHighScoreText.gameObject.SetActive(true);
					newHighScoreText.SetText("NEW \nHIGH SCORE ! \n" + DeltaTimeToText(delta_time));
				}
			}
			else
			{
				newHighScoreText.gameObject.SetActive(false);
				currentScoreText.SetText(timerText.text);
				currentScoreText.gameObject.SetActive(true);
			}

			is_game_ended = true;
			player.SetCanMove(false);
			player.SetCanZoom(false);
			SpaceshipAnimator.SetBool("end_game", true);

			standardUI.SetActive(false);
			timerText.gameObject.SetActive(true);
			highScoreMenu.SetActive(true);
		}				
	}

	public string DeltaTimeToText(float time)
	{
		int minutes = ((int)time / 60);
		float seconds = (time % 60);
		if (minutes >= max_minutes)
		{
			EndLevel();
		}
		return (minutes.ToString() + ":" + seconds.ToString("f2"));
	}

	public int GetIntelCount()
	{
		return intel_spawner.transform.childCount;
	}

	public void TurnTutorialOff()
	{
		PlayerPrefsManager.SetEnableTutorial(false);
		tutorialMenu.SetActive(false);
		standardUI.SetActive(true);
		Time.timeScale = 1f;
	}
}
