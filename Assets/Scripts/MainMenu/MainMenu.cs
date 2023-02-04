using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string scene;

    private void Start()
    {
        Transition.Instance.FadeOut();
    }

    public void Play()
    {
        Transition.Instance.FadeIn(() =>
        {
            SceneManager.LoadScene(scene);
        });      
    }

    public void Exit()
    {
        Transition.Instance.FadeIn(() =>
        {
            Application.Quit();
        });
    }
}
