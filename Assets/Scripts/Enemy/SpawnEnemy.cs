using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	public GameObject enemyToSpawn;
	
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			Spawn ();
		}
	}

	void Spawn () {
		Instantiate (enemyToSpawn, transform.position, transform.rotation);

		Destroy (this.gameObject);
	}
}
