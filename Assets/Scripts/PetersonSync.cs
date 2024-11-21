using UnityEngine;
using TMPro;

public class PetersonSync : MonoBehaviour
{
    private bool[] flag = new bool[2];  
    private int turn;  
    private bool[] inCriticalSection = new bool[2];  

    public TMP_Text process1Status, process2Status;  
    public TMP_Text alertText;  

    public void ToggleCriticalSection(int process)
    {
        if (inCriticalSection[process])
        {
            ExitCriticalSection(process);  // Exit if already in critical section
        }
        else
        {
            EnterCriticalSection(process);  // Enter if not in critical section
        }
    }

    // Method for process to enter the critical section
    public void EnterCriticalSection(int process)
    {
         // Get the other process (0 -> 1, 1 -> 0)
        int other = 1 - process; 
        flag[process] = true; 
        turn = other;  

        // Busy-wait loop (critical section entry condition)
        while (flag[other] && turn == other)
        {
            // If both processes are trying to enter at the same time, show an alert
            if (flag[0] && flag[1])
            {
                ShowAlert();
                return;  // Exit if there is a conflict (both are trying to enter)
            }
        }

        // If no conflict, the process can safely enter the critical section
        inCriticalSection[process] = true;
        UpdateStatus(process);  // Update the process status in the UI
    }

    public void ExitCriticalSection(int process)
    {
        flag[process] = false;
        inCriticalSection[process] = false;

        UpdateStatus(process);
    }

    private void UpdateStatus(int process)
    {
        if (process == 0)
        {
            // Update the status for Process 1
            process1Status.text = inCriticalSection[0] ? "Process 1: In Critical Section" : "Process 1: Waiting";
        }
        else
        {
            // Update the status for Process 2
            process2Status.text = inCriticalSection[1] ? "Process 2: In Critical Section" : "Process 2: Waiting";
        }
    }

    private void ShowAlert()
    {
        alertText.text = "Both processes are trying to enter the critical section at the same time!";
    }
}
