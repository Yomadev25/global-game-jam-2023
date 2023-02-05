using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatManager : MonoBehaviour
{
    public static CheatManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene("Stage1");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene("Stage2");
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SceneManager.LoadScene("Stage4");
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            SceneManager.LoadScene("Stage3");
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            SceneManager.LoadScene("Stage5");
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
