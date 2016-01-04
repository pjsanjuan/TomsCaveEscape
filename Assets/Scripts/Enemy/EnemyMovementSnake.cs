using UnityEngine;
using System.Collections;

public class EnemyMovementSnake : MonoBehaviour
{
	Transform player;               // Reference to the player's position.
	//PlayerHealth playerHealth;      // Reference to the player's health.
	//EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	public float range = 5f;
	bool enableMods = false;
	EnemyShoot enemyShoot;



	void Awake ()
	{
		enemyShoot = GetComponentInChildren <EnemyShoot> ();
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//playerHealth = player.GetComponent <PlayerHealth> ();
		//enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
		nav.enabled = false;
		enemyShoot.enabled = false;
	}

	void OnTriggerEnter(Collider other){
		//nav.enabled = true;
		if (other.tag == "Player"){
			enableMods = true;
		}
	}


	void Update ()
	{
		// If the enemy and the player have health left...
		//if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
		//{
		// ... set the destination of the nav mesh agent to the player.
		float dist = Vector3.Distance(player.position, transform.position);

		//print ("Distance to player:" + dist);

		if (enableMods == true && dist < range) {
			nav.enabled = false;
			if (enemyShoot != null){
				enemyShoot.enabled = true;
			} else {
				Destroy (this.gameObject);
			}
		} else if (enableMods == true && dist > range) {
			nav.enabled = true;
			if (enemyShoot != null){
				enemyShoot.enabled = false;
				nav.SetDestination (player.position);
			} else {
				Destroy (this.gameObject);
			}
		} 

		if (player != null) {
			transform.LookAt (player);
		}

		/*if (dist < range) {
			nav.enabled = false;
		} else {
			nav.enabled = true;
		}*/



		//}
		// Otherwise...
		//else
		//{
		// ... disable the nav mesh agent.
		//nav.enabled = false;
		//}
	} 
}
