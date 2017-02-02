using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	private const int FULL_ROUND = 360;

	[SerializeField]
	private float rotateSpeed = 1.0f; // rotations per second

	[SerializeField]
	private float floatSpeed = 0.5f; // up and down per second

	[SerializeField]
	private float movementDistance = 0.5f; // maximum distance of move up and down

	private float startingY;

	void OnTriggerEnter (Collider collider){
		Debug.Log ("OnTriggerEnter with name = " + collider.gameObject.name + ", tag = " + collider.gameObject.tag);
		if (collider.gameObject.name == "Player") {
			Pickup ();
		}
	}

	// Use this for initialization
	void Start () {
		startingY = transform.position.y;
		transform.Rotate (transform.up, Random.Range (0f, 360f));
		StartCoroutine (Spin ());
		StartCoroutine (Float ());
	}

	void Pickup ()	{
		GameManager.Instance.NumCoins++;
		Destroy (gameObject);
	}
	
	private IEnumerator Spin(){
		while (true) {
			transform.Rotate (transform.up, Time.deltaTime * rotateSpeed * FULL_ROUND);
			yield return 0;
		}
	}

	private IEnumerator Float(){
		while (true) {
			var newY = startingY + movementDistance * Mathf.Sin (Time.realtimeSinceStartup * floatSpeed * 2 * Mathf.PI);
			transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
			yield return 0;
		}
	}
}
