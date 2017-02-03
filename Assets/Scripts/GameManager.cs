using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


/// <summary>
///     <para>Manages game flow</para>
/// </summary>
public class GameManager : Sinletone<GameManager> {

	private float _timeRemaining;

    /// <summary>
    ///     <para>Remaining time of round</para>
    /// </summary>
	public float TimeRemaining {
		get {return _timeRemaining;}
		set {_timeRemaining = value;}
	}

	private int _numCoins = 0;

    /// <summary>
    ///     <para>Number of gathered coins</para>
    /// </summary>
	public int NumCoins {
		get {return _numCoins;}
		set {_numCoins = value;}
	}

    private float _playerHealth;

    /// <summary>
    ///     <para>Player health in units</para>
    /// </summary>
    public float PlayerHealth {
		get {return _playerHealth;}
		set {_playerHealth = value;}
	}

    /// <summary>
    ///     <para>Maximum time of round</para>
    /// </summary>
    private float maxTime = 5 * 60; // in seconds

    /// <summary>
    ///     <para>Maximum player health in units</para>
    /// </summary>
	private int maxHealth = 3;

    /// <summary>
    ///     <para>Player invulnerability</para>
    /// </summary>
	private bool isInvulnerable = false;

	void OnEnable(){
		DamagePlayerEvent.OnDamagePlayer += DecrementPLayerHealth;
	}

	void OnDisable(){
		DamagePlayerEvent.OnDamagePlayer -= DecrementPLayerHealth;
	}

	void Start () {
		ResetStats();
	}
	
	void Update () {
		TimeRemaining -= Time.deltaTime;

	    // restart round if time is off
		if (TimeRemaining <= 0.0f) {
			Restart ();
		}
	}

    /// <summary>
    ///     <para>Player decrement health on DamagePlayerEvent</para>
    /// </summary>
    /// <param name="player"></param>
	private void DecrementPLayerHealth(GameObject player) {

		if (isInvulnerable) {
			return;
		}

		StartCoroutine (InvulnerableDelay ());

		PlayerHealth--;

        // restart on death
		if (PlayerHealth <= 0) {
			Restart ();
		}
	}

    /// <summary>
    ///     <para>Resets game stats</para>
    /// </summary>
	private void ResetStats (){
		TimeRemaining = maxTime;
		PlayerHealth = maxHealth;
	}

    /// <summary>
    ///  <para>Scene restart</para>
    /// </summary>
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
