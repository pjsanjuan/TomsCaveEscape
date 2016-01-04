using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 39, 45) * Time.deltaTime);
    }
}
