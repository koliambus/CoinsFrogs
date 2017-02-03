using UnityEngine;

/// <summary>
///     <para>Event called when Player take damage</para>
/// </summary>
public class DamagePlayerEvent : MonoBehaviour {

	public delegate void DamagePlayerAction(GameObject player);
	public static event DamagePlayerAction OnDamagePlayer;

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag("Player")) {
			if (OnDamagePlayer != null) {
				OnDamagePlayer (collider.gameObject);
			}
		}
	}
}
