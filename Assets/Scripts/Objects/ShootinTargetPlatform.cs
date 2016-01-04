using UnityEngine;
using System.Collections;

public class ShootinTargetPlatform : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = Color.green;
            transform.parent.parent.GetChild(1).gameObject.SetActive(true);
        }
    }
}
