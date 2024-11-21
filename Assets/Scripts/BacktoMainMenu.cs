using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene loading

public class MainMenuHandler : MonoBehaviour
{
    // This method will be called when the "Back to Main Menu" button is clicked
    public void GoToMainMenu()
    {
        // Load the Main Menu scene by name (replace "MainMenu" with your actual scene name)
        SceneManager.LoadScene("MainMenu");
    }
}
