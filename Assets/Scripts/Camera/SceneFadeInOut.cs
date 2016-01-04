using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour {

    public float fadeSpeed = 1.5f;

    private bool sceneStarting = true;

    RectTransform rect;

    // Use this for initialization
    void Awake () {
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
