using UnityEngine;
using System.Collections;

public class DestroyRemains : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        StartCoroutine(x());
    }

    IEnumerator x()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }
}
