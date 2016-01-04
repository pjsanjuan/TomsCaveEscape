using UnityEngine;
using System.Collections;

public class BulletAttack : MonoBehaviour {

	public int damage;
	public float bulletSpeed;

	PlayerAttack playerAttack;
	float bulletTimer;
	float bulletTimerMax = 2f;

	void Awake () {
		playerAttack = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttack> ();
		Rigidbody bulletRB = GetComponent<Rigidbody> ();
		Vector3 direction = playerAttack.GetShotDirection ();

		bulletRB.AddForce (direction.normalized * bulletSpeed);
	}


	void Update () {
		bulletTimer += Time.deltaTime;
		if (bulletTimer > bulletTimerMax) {
			Destroy (this.gameObject);
		}
	}


	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Enemy")) {
			EnemyHealth enemyHealth = other.GetComponent <EnemyHealth> ();
			enemyHealth.TakeDamage(damage);
		}


		if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Spawn") && !other.gameObject.CompareTag("Aggro") && !other.gameObject.CompareTag("Spawn Point")) {
			Destroy (this.gameObject);
		}
	}

}

