using UnityEngine;
using System.Collections;

public class TurnOffTorches : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            transform.parent.GetChild(0).gameObject.SetActive(false);
            transform.parent.GetChild(1).gameObject.SetActive(false);
            transform.parent.GetChild(2).gameObject.SetActive(false);
            transform.parent.GetChild(3).gameObject.SetActive(false);
            transform.parent.GetChild(4).gameObject.SetActive(false);
            transform.parent.GetChild(5).gameObject.SetActive(false);
        }
    }
}