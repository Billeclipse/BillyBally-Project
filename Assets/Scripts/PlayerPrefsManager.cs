using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager {
	const string MASTER_VOLUME_KEY = "master_volume";
	const string HIGH_SCORE = "high_score";
	const string JOYSTICK_ENABLED = "joystick_enable";
	const string ENABLE_TUTORIAL = "enable_tutorial";

	public static void SavePreferences()
	{
		PlayerPrefs.Save();
	}

	public static void SetMasterVolume(float volume)
	{
		if (volume > 0f && volume < 1f)
		{
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		}
		else
		{
			Debug.LogError("Master volume out of range!");
		}
	}

	public static float GetMasterVolume()
	{
		if (!PlayerPrefs.HasKey(MASTER_VOLUME_KEY))
		{ 
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, 1);
		}
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}

	public static void SetHighScore(float high_score)
	{
		PlayerPrefs.SetFloat(HIGH_SCORE, high_score);
	}

	public static float GetHighScore()
	{
		if (!PlayerPrefs.HasKey(HIGH_SCORE))
		{
			return -1f;
		}
		else
		{
			return PlayerPrefs.GetFloat(HIGH_SCORE);
		}					
	}

	public static void ResetHighScore()
	{
		PlayerPrefs.DeleteKey(HIGH_SCORE);
	}

	public static void SetJoystickEnabled(bool enabled)
	{
		if (enabled)
		{
			PlayerPrefs.SetInt(JOYSTICK_ENABLED, 0);
		}
		else
		{
			PlayerPrefs.SetInt(JOYSTICK_ENABLED, 1);
		}		
	}

	public static bool GetJoystickEnabled()
	{
		if (PlayerPrefs.HasKey(JOYSTICK_ENABLED))
		{
			if(PlayerPrefs.GetInt(JOYSTICK_ENABLED) == 0)
			{
				return true;
			}else
			{
				return false;
			}
		}
		else
		{
			PlayerPrefs.SetInt(JOYSTICK_ENABLED, 0);
			return true; 
		}
	}

	public static void SetEnableTutorial(bool enabled)
	{
		if (enabled)
		{
			PlayerPrefs.SetInt(ENABLE_TUTORIAL, 0);
		}
		else
		{
			PlayerPrefs.SetInt(ENABLE_TUTORIAL, 1);
		}
	}

	public static bool GetEnableTutorial()
	{
		if (PlayerPrefs.HasKey(ENABLE_TUTORIAL))
		{
			if (PlayerPrefs.GetInt(ENABLE_TUTORIAL) == 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			PlayerPrefs.SetInt(ENABLE_TUTORIAL, 0);
			return true;
		}
	}
}
