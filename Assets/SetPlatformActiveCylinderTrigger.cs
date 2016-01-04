using UnityEngine;
using System.Collections;

public class SetPlatformActiveCylinderTrigger : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if(GetComponent<Renderer>().material.color == Color.green)
        {
            transform.parent.GetChild(1).gameObject.SetActive(true);
        }
    }
}
