using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowAudio : MonoBehaviour
{
    Camera toFollow;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // Load into the main menu.
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (toFollow == null)
        {
            if (FindObjectOfType<Camera>())
            {
                toFollow = FindObjectOfType<Camera>();
            }
        }
        else
        {
            gameObject.transform.position = toFollow.transform.position;
        }
    }
}
