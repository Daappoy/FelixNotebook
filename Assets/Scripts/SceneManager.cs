using UnityEngine;
using UnityEngine.SceneManagement;


public class Scene : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("Gameplay");
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Gameover()
    {
        SceneManager.LoadScene("Gameover");
    }
}
