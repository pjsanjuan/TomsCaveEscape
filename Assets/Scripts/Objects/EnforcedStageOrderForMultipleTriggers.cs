using UnityEngine;
using System.Collections;

public class EnforcedStageOrderForMultipleTriggers : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material.color.Equals(Color.green))
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }

        if (transform.GetChild(0).gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Renderer>().material.color.Equals(Color.green))
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
