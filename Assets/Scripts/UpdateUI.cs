using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour {

	[SerializeField]
	private Text timerLabel;

	[SerializeField]
	private Text coinsLabel;

	[SerializeField]
	private Text healthLabel;

	// Update is called once per frame
	void Update () {
		timerLabel.text = FormatTime(GameManager.Instance.TimeRemaining);
		coinsLabel.text = GameManager.Instance.NumCoins.ToString ();
		healthLabel.text = FormatHealth(GameManager.Instance.GetPlayerHealthPercentage ());
	}

	private string FormatTime(float seconds) {
		return string.Format ("{0}:{1:00}", Mathf.FloorToInt (seconds / 60), Mathf.FloorToInt (seconds % 60));
	}

	private string FormatHealth(float health) {
		return string.Format ("{0}%", Mathf.RoundToInt (health * 100));
	}
}
