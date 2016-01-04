using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {

	AudioSource enemyHiss;

	Animator anim;

	public Transform origin;
	public Rigidbody bullet;
	Transform target;
	public float fireRate;
	public float spread; //amount to scatter bullet targets by

	EnemyMovement enemyMovement;
	float fireTimer; //time since the player last shot
	Vector3 shotDirection;

	void Start () {
		enemyMovement = GetComponent<EnemyMovement> ();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		anim = GetComponent <Animator> ();
		enemyHiss = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		fireTimer += Time.deltaTime;

		if (fireTimer > fireRate) {
			anim.SetTrigger ("Attack");
			enemyHiss.Play ();
			Shoot ();
		}
	}

	void Shoot () {
		fireTimer = 0;

		shotDirection = target.position - transform.position;

		shotDirection.x += Random.Range (-spread, spread);
		shotDirection.y += Random.Range (-spread, spread);
		shotDirection.z += Random.Range (-spread, spread);

		Rigidbody AcidRb = (Rigidbody)Instantiate (bullet, origin.position, Quaternion.LookRotation(shotDirection));

		AcidRb.AddForce (shotDirection.normalized * 200f);

	}

	public Vector3 GetShotDirection () {
		return shotDirection;
	}

}
