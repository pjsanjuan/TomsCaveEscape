using UnityEngine;
using System.Collections;

public class PickRandomPlatform : MonoBehaviour
{

    int maxsize = 24;
    ArrayList randomList;
    ArrayList originalTransforms;
    Vector3 originalTransform;


    // Use this for initialization

    void Start()
    {

        originalTransform = transform.GetChild(1).position;

        originalTransforms = new ArrayList();

        OriginalTransforms();

        

    }

    void OnEnable()
    {
        randomList = new ArrayList();
        GenerateRandomList();
        StartCoroutine(cutsceneStart(randomList));
        StopCoroutine(cutsceneStart(randomList));
    }


    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
         

            transform.GetChild(1).transform.position = originalTransform;
            transform.GetChild(1).GetComponent<MoveXFinalObstacle>().enabled = false;
            transform.GetChild(1).GetComponent<TimedPlatformFinalObstacle>().enabled = false;
            transform.GetChild(1).gameObject.SetActive(true);

            AssignOriginalTransforms();

            transform.GetChild(0).gameObject.SetActive(false);

            StopCoroutine(cutsceneStart(randomList));
            GetComponent<PickRandomPlatform>().enabled = false;
        }
    }

    IEnumerator cutsceneStart(ArrayList arr)
    {
        for (int i = 0; i <= 21; i+=3)
        {
            yield return new WaitForSeconds(7f);
            transform.GetChild(0).GetChild((int)arr[i]).GetComponent<TimedPlatformFinalObstacle>().enabled = true;
            transform.GetChild(0).GetChild((int)arr[i+1]).GetComponent<TimedPlatformFinalObstacle>().enabled = true;
            transform.GetChild(0).GetChild((int)arr[i+2]).GetComponent<TimedPlatformFinalObstacle>().enabled = true;
        }
        
    }

    void OriginalTransforms()
    {
        for (int i = 0; i < maxsize; ++i)
        {
            originalTransforms.Add(transform.GetChild(0).GetChild(i).position);
        }
    }

    void AssignOriginalTransforms()
    {
        for (int i = 0; i < maxsize; ++i)
        {
            transform.GetChild(0).GetChild(i).transform.position = ((Vector3)originalTransforms[i]);
            transform.GetChild(0).GetChild(i).GetComponent<TimedPlatformFinalObstacle>().enabled = false;
            transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
        }
    }


    void GenerateRandomList()
    {
        for (int i = 0; i < maxsize; ++i)
        {
            int numToAdd = Random.Range(0, maxsize);
            while (randomList.Contains(numToAdd))
            {
                numToAdd = Random.Range(0, maxsize);
            }
            randomList.Add(numToAdd);
        }
    }
}
