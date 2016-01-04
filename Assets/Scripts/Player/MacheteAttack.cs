using UnityEngine;
using System.Collections;

public class MacheteAttack : MonoBehaviour {



	public int damage = 50;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy") {
			EnemyHealth enemyHealth = other.GetComponent <EnemyHealth> ();
			enemyHealth.TakeDamage (damage);
		}

	}


}
