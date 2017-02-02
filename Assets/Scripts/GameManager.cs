using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Sinletone<GameManager> {

	private float _timeRemaining;

	public float TimeRemaining {
		get {return _timeRemaining;}
		set {_timeRemaining = value;}
	}

	private int _numCoins = 0;

	public int NumCoins {
		get {return _numCoins;}
		set {_numCoins = value;}
	}

	private float _playerHealth;

	public float PlayerHealth {
		get {return _playerHealth;}
		set {_playerHealth = value;}
	}

	private float maxTime = 5 * 60; // in seconds

	private int maxHealth = 3;

	private bool isInvulnerable = false;

	void OnEnable(){
		DamagePlayerEvent.OnDamagePlayer += DecrementPLayerHealth;
	}

	void OnDisable(){
		DamagePlayerEvent.OnDamagePlayer -= DecrementPLayerHealth;
	}

	// Use this for initialization
	void Start () {
		ResetStats();
	}
	
	// Update is called once per frame
	void Update () {
		TimeRemaining -= Time.deltaTime;

		if (TimeRemaining <= 0.0f) {
			Restart ();
		}
	}

	private void DecrementPLayerHealth(GameObject player) {

		if (isInvulnerable) {
			return;
		}

		StartCoroutine (InvulnerableDelay ());

		PlayerHealth--;

		if (PlayerHealth <= 0) {
			Restart ();
		}
	}

	private void ResetStats (){
		TimeRemaining = maxTime;
		PlayerHealth = maxHealth;
	}

	private void Restart (){
		Scene scene = SceneManager.GetActiveScene(); 
		SceneManager.LoadScene(scene.name);
		ResetStats();
	}

	private IEnumerator InvulnerableDelay(){
		isInvulnerable = true;
		yield return new WaitForSeconds (1.0f);
		isInvulnerable = false;
	}

	public float GetPlayerHealthPercentage (){
		return PlayerHealth / (float)maxHealth; 
	}
}
