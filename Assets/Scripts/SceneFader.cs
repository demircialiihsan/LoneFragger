using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    private Animator anim;
    private string targetScene;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeScene(string _targetScene)
    {
        targetScene = _targetScene;
        anim.SetTrigger("Fade_In");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(targetScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
