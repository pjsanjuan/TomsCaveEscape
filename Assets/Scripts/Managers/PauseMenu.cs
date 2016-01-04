using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public GameObject menu;
	bool paused;
	RectTransform menuBox;


	void Awake () {
		paused = false;
		menuBox = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Cancel") && !paused) {
			PauseGame ();
		}
	}

	void OnGUI () {
		if(paused) {
			//resume
			if(GUI.Button(new Rect(Screen.width*0.5f - menuBox.rect.width*1f, Screen.height/2f - menuBox.rect.height/5, //position
			                       menuBox.rect.width*2f, menuBox.rect.height/3f), "Resume")) { //size
				UnpauseGame ();
			}

			//quit
			if(GUI.Button(new Rect(Screen.width*0.5f - menuBox.rect.width*1f, Screen.height/2f + menuBox.rect.height/5, //position
			                       menuBox.rect.width*2f, menuBox.rect.height/3f), "Quit")) { //size
				QuitGame ();
			}
		}

	}

	void PauseGame () {
		paused = true;
		menu.SetActive(true);
		Cursor.visible = true;
		Time.timeScale = 0.0f;
	}
	
	public void UnpauseGame () {
		paused = false;
		menu.SetActive(false);
		Cursor.visible = false;
		Time.timeScale = 1.0f;
	}

	void QuitGame () {
		Application.Quit();
	}
}
