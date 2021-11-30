using TMPro;
using UnityEngine;

public class HighScoreMenu : MonoBehaviour {

	public TextMeshProUGUI highScoreText;

	private void Update()
	{
		if(PlayerPrefsManager.GetHighScore() != -1)
		{
			highScoreText.SetText(DeltaTimeToString(PlayerPrefsManager.GetHighScore()));
		}
		else
		{
			highScoreText.SetText("--:--.--");
		}	
	}

	public void ResetScore()
	{
		PlayerPrefsManager.ResetHighScore();
	}

	private string DeltaTimeToString(float time)
	{
		int minutes = ((int)time / 60);
		float seconds = (time % 60);
		return (minutes.ToString() + ":" + seconds.ToString("f2"));
	}
}
