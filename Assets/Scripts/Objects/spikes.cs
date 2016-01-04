using UnityEngine;
using System.Collections;

public class spikes : MonoBehaviour {

    public Vector3 pointB;

    IEnumerator Start()
    {
       
        var pointA = transform.position;
        pointB = transform.parent.transform.position - new Vector3(0,-0.5f,0);
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 0.4f));
            yield return new WaitForSeconds(1.5f);
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 0.4f));
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}
