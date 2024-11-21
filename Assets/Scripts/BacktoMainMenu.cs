using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuHandler : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
