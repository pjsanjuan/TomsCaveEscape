using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	Transform player;               // Reference to the player's position.
	//PlayerHealth playerHealth;      // Reference to the player's health.
	EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	public float AggroRange = 5f;




	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponentInChildren <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
		nav.enabled = false;
	}


	void Update ()
	{
		//Calculate distance from players position to enemy's position
		float dist = Vector3.Distance(player.position, transform.position);


		//Player is inside aggro range
		if (dist < AggroRange) {
			nav.enabled = true;
			nav.SetDestination (player.position);
		}   

		if (enemyHealth == null) {
			Destroy (this.gameObject);
		}


	} 
}
