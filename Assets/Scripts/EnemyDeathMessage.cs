using UnityEngine;

/// <summary>
///     <para>Event called when Player kills Enemy</para>
/// </summary>
public class EnemyDeathMessage : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag("Player")) {
			gameObject.SendMessageUpwards ("OnDeath");
		}
	}
}
