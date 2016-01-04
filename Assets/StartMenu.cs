using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

    public GameObject menu;
    bool paused;
    RectTransform menuBox;


    void Awake()
    {
        paused = false;
        menuBox = GetComponent<RectTransform>();
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width * 0.5f - menuBox.rect.width * 1f, Screen.height / 2f - menuBox.rect.height / 5, //position
                              menuBox.rect.width * 2f, menuBox.rect.height / 3f), "Start"))
        { //size
            Application.LoadLevel(1);
        }

        //quit
        if (GUI.Button(new Rect(Screen.width * 0.5f - menuBox.rect.width * 1f, Screen.height / 2f + menuBox.rect.height / 5, //position
                               menuBox.rect.width * 2f, menuBox.rect.height / 3f), "Quit"))
        { //size
            QuitGame();
        }

    }


    void QuitGame()
    {
        Application.Quit();
    }
}
