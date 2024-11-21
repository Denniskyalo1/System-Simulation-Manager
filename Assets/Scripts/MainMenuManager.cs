using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LoadCPUSchedulingScene()
    { 
        Debug.Log("Button clicked. Attempting to load Cpu Scheduling scene...");
        SceneManager.LoadScene("CpuScheduling");
    }

    public void LoadProcessSyncScene()
    { 
         Debug.Log("Button clicked. Attempting to load Process Sync scene...");
        SceneManager.LoadScene("ProcessSynchronization");
    }

    public void LoadMemoryManagementScene()
    { 
        Debug.Log("Button clicked. Attempting to load Memory Management scene...");
        SceneManager.LoadScene("MemoryManagement");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
