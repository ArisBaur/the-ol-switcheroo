using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenuButtonHandler : MonoBehaviour
{

    [SerializeField] private string level;


    public void onPlayClicked()
    {
        SceneManager.LoadScene(level);
        tries_state_saver.tries_counter = 0;
    }

    public void onExitClicked()
    {
        Application.Quit();
    }


}
