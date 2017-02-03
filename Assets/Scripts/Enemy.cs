using UnityEngine;
using System.Collections;

/// <summary>
///     <para>Enemy lifecycle controller</para>
/// </summary>
public class Enemy : MonoBehaviour {

    /// <summary>
    ///     <para>rotation speed to aim, in degrees per second</para>
    /// </summary>
	[SerializeField]
	private float rotationSpeed = 180;

    /// <summary>
    ///     <para>Movement speed to aim, in units per second</para>
    /// </summary>
	[SerializeField]
	private float movementSpeed = 1f;

    /// <summary>
    ///     <para>Minimun radius for collider triggering</para>
    /// </summary>
	[SerializeField]
	private float meshRadius = 1f; // In units

	[SerializeField]
	private Animation animationComponent;

	private IEnumerator turnTowardsPlayerCoroutine;
	private IEnumerator moveTowardsPlayerCoroutine;

    /// <summary>
    ///     <para>Ignore colliding on death animation</para>
    /// </summary>
	private bool isDead;

    /// <summary>
    ///     <para>Starts pursuit when Player is near</para>
    /// </summary>
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player") && !isDead)
		{
			float playerDistance = Vector3.Distance(collider.transform.position, transform.position);

			// Ignore trigger events from the inner colliders
			if (playerDistance >= 2f * meshRadius)
			{
				turnTowardsPlayerCoroutine = TurnTowardsPlayer(collider.transform);
				moveTowardsPlayerCoroutine = MoveTowardsPlayer(collider.transform);
				StartCoroutine(turnTowardsPlayerCoroutine);
				StartCoroutine(moveTowardsPlayerCoroutine);
			}
		}
	}

    /// <summary>
    ///     <para>On enemy death event</para>
    /// </summary>
	void OnDeath(){
		if (isDead) {
			return;
		}

		isDead = true;

		animationComponent.Play ("Death");

		StopCoroutine(turnTowardsPlayerCoroutine);
		StopCoroutine(moveTowardsPlayerCoroutine);

		Destroy (gameObject, animationComponent["Death"].length);
	}

    /// <summary>
    ///     <para>Stop pursuit when Player is too far</para>
    /// </summary>
    /// <param name="collider"></param>
	void OnTriggerExit(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			float playerDistance = Vector3.Distance(collider.transform.position, transform.position);
			
			// Ignore trigger events from the inner colliders
			if (playerDistance >= 2f * meshRadius)
			{
				StopCoroutine(turnTowardsPlayerCoroutine);
				StopCoroutine(moveTowardsPlayerCoroutine);
			}
		}
	}

    /// <summary>
    ///     <para>Turning towards player</para>
    /// </summary>
    /// <param name="player">Player position</param>
	private IEnumerator TurnTowardsPlayer(Transform player)
	{
		while (true)
		{
			Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);
			targetRotation.x = 0f;
			targetRotation.z = 0f;

			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
			yield return 0;
		}
	}

    /// <summary>
    ///     <para>Moving toward player</para>
    /// </summary>
    /// <param name="player">Player position</param>
	private IEnumerator MoveTowardsPlayer(Transform player)
	{
		while (true)
		{
			Vector3 playerDirection = transform.position - player.position;
			playerDirection.y = 0;
			playerDirection = playerDirection.normalized;

			Vector3 deltaMovement = playerDirection * movementSpeed * Time.deltaTime;

            transform.position -= deltaMovement;

			yield return 0;
		}
	}
}
