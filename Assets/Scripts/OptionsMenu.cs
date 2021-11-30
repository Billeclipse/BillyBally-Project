using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

	public Slider volumeSlider;
	public Button accelerometerButton;
	public Button joystickButton;
	private MusicManager musicManager;
	
	void Start()
	{
		musicManager = FindObjectOfType<MusicManager>();
		SetDefaults();
	}
	
	void Update()
	{
		if (musicManager)
		{
			musicManager.SetVolume(volumeSlider.value);
		}			
	}

	public void SaveAndGoBack()
	{
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
		PlayerPrefsManager.SavePreferences();
	}

	public void SetDefaults()
	{
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		if (PlayerPrefsManager.GetJoystickEnabled())
		{
			joystickButton.interactable = false;
			accelerometerButton.interactable = true;
		}
		else
		{
			joystickButton.interactable = true;
			accelerometerButton.interactable = false;
		}
	}

	public void SetJoystick(bool enable)
	{
		PlayerPrefsManager.SetJoystickEnabled(enable);
	}
}
