using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmmoManager : MonoBehaviour {
	
	public PlayerAttack playerAttack;
	
	Text text;
	
	// Use this for initialization
	void Awake () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerAttack.reloading) {
			text.text = "Reloading";
		} else {
			text.text = "Ammo: " + playerAttack.ammoCount + " / " + playerAttack.ammoStore;
		}
	}
}
