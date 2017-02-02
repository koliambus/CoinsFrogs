using UnityEngine;

public class EnemyDeathMessage : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag("Player")) {
			gameObject.SendMessageUpwards ("OnDeath");
		}
	}
}
