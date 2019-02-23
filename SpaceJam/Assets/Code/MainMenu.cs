using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	//Who are you why are you looking at these notes
    
    public void PlayGame ()
    {
        SceneManager.LoadScene("JethroScene");
    }
    public void QuitGame ()
    {
        Application.Quit();
    }
}
