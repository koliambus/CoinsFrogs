using UnityEngine;
using System.Collections;

/// <summary>
///     <para>Controlles Coin lifecycle</para>
/// </summary>
public class Coin : MonoBehaviour {

	private const int FULL_ROUND = 360;

    /// <summary>
    ///     <para>rotations per second</para>
    /// </summary>
	[SerializeField]
	private float rotateSpeed = 1.0f;

    /// <summary>
    ///     <para>up and down per second</para>
    /// </summary>
    [SerializeField]
	private float floatSpeed = 0.5f;

    /// <summary>
    ///     <para>maximum distance of move up and down</para>
    /// </summary>
    [SerializeField]
	private float movementDistance = 0.5f;

    /// <summary>
    ///     <para>start floating point</para>
    /// </summary>
    private float startingY;

	void OnTriggerEnter (Collider collider){
		Debug.Log ("OnTriggerEnter with name = " + collider.gameObject.name + ", tag = " + collider.gameObject.tag);
		if (collider.gameObject.name == "Player") {
			Pickup ();
		}
	}

	/// <summary>
	///     <para>start floating and rotate</para>
	/// </summary>
	void Start () {
		startingY = transform.position.y;
		transform.Rotate (transform.up, Random.Range (0f, 360f));
		StartCoroutine (Spin ());
		StartCoroutine (Float ());
	}

    /// <summary>
    ///     <para>Calls when user pick up the coin</para>
    /// </summary>
    void Pickup ()	{
		GameManager.Instance.NumCoins++;
		Destroy (gameObject);
	}

    /// <summary>
    ///     <para>Spin coroutine</para>
    /// </summary>
	private IEnumerator Spin(){
		while (true) {
			transform.Rotate (transform.up, Time.deltaTime * rotateSpeed * FULL_ROUND);
			yield return 0;
		}
	}

    /// <summary>
    ///     <para>Float coroutine</para>
    /// </summary>
    /// <returns></returns>
	private IEnumerator Float(){
		while (true) {
			var newY = startingY + movementDistance * Mathf.Sin (Time.realtimeSinceStartup * floatSpeed * 2 * Mathf.PI);
			transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
			yield return 0;
		}
	}
}
