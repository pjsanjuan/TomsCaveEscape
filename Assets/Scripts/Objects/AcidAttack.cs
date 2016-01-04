using UnityEngine;
using System.Collections;

public class AcidAttack : MonoBehaviour {

	public int damage;
	public float acidSpeed;

	float acidTimer;
	float acidTimerMax = 2f;



	void Update () {
		acidTimer += Time.deltaTime;
		if (acidTimer > acidTimerMax) {
			Destroy (this.gameObject);
		}

	}


	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			PlayerHealth playerHealth = other.GetComponent <PlayerHealth> ();
			playerHealth.TakeDamage(damage);
		}


		if (!other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Spawn") && !other.gameObject.CompareTag("Aggro") && !other.gameObject.CompareTag("Spawn Point")) {
			print(other.tag);
			Destroy (this.gameObject);

		}
	}

}
