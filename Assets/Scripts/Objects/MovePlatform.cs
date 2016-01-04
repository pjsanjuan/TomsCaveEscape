using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.GetComponent<MoveX>().enabled = true;
            transform.GetChild(1).gameObject.GetComponent<MoveX>().enabled = true;
            transform.GetChild(2).gameObject.GetComponent<MoveX>().enabled = true;
        }
    }
}
