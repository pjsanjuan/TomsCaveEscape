using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	//public AudioClip deathClip;

	Animator anim;
	//AudioSource enemyAudio;
	EnemyMovement enemyMovement;
	//EnemyAttack enemyAttack;
	bool isDead;
	bool damaged; 
	float despawnTimer = 0;
	float despawnTimerMax = 2;


	void Awake () {
		anim = GetComponent <Animator> ();
		//enemyAudio = GetComponent <AudioSource> ();
		enemyMovement = GetComponentInParent <EnemyMovement> ();
		//enemyAttack = GetComponent <EnemyAttack> ();
		currentHealth = startingHealth;
	}


	void Update ()
	{
		damaged = false; //leaving damaged here in case we need it for anything. At the moment we don't
		
		if (isDead) {
			despawnTimer += Time.deltaTime;
			if (despawnTimer > despawnTimerMax) {
				Destroy(this.gameObject);
			}
		}
	}
	
	
	public void TakeDamage (int amount)
	{
		print ("Enemy Took Damage");
		damaged = true;
		
		currentHealth -= amount;

		// enemyAudio.Play ();
		
		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}

	
	void Death ()
	{
		isDead = true;
		
		// enemyAttack.DisableEffects ();
		
		anim.SetTrigger ("IsDead");
		
		// enemyAudio.clip = deathClip;
		// enemyAudio.Play ();
		
//		enemyMovement.enabled = false;
		//enemyAttack.enabled = false;
		
		
		
	}
}
