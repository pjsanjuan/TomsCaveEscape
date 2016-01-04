using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{

    public GameObject menu;

    RectTransform menuBox;


    void Awake()
    {
       
        menuBox = GetComponent<RectTransform>();
        menu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    void OnGUI()
    {
       

            //resume
            if (GUI.Button(new Rect(Screen.width * 0.5f - menuBox.rect.width * 1f, Screen.height / 1.7f - menuBox.rect.height / 5, //position
                                   menuBox.rect.width * 2f, menuBox.rect.height / 3f), "Restart"))
            { //size
                Application.LoadLevel(3);
            }

            //quit
            if (GUI.Button(new Rect(Screen.width * 0.5f - menuBox.rect.width * 1f, Screen.height / 1.7f + menuBox.rect.height / 5, //position
                                   menuBox.rect.width * 2f, menuBox.rect.height / 3f), "Quit"))
            { //size
                QuitGame();
            }

       
    }

   // void PauseGame()
   // {
   //     paused = true;
   //     menu.SetActive(true);
   //     Cursor.visible = true;
   //     Time.timeScale = 0.0f;
   // }
    
    //public void UnpauseGame()
    //{
    //    paused = false;
    //    menu.SetActive(false);
    //    Cursor.visible = false;
    //    Time.timeScale = 1.0f;
    //}

    void QuitGame()
    {
        Application.Quit();
    }
}