using UnityEngine;
using TMPro;

public class PetersonSync : MonoBehaviour
{
    private bool[] flag = new bool[2]; // Flag to indicate if a process is trying to enter critical section
    private int turn; // Turn for the process selection
    private bool[] inCriticalSection = new bool[2]; // Tracks if the process is in the critical section

    public TMP_Text process1Status, process2Status;
    public TMP_Text alertText; // Alert text component

    // This method is called when the process button is clicked
    public void ToggleCriticalSection(int process)
    {
        if (inCriticalSection[process])
        {
            // If the process is already in the critical section, exit it
            ExitCriticalSection(process);
        }
        else
        {
            // Otherwise, attempt to enter the critical section
            EnterCriticalSection(process);
        }
    }

    // Method to let the process enter the critical section
    public void EnterCriticalSection(int process)
    {
        int other = 1 - process; // Get the other process (0 -> 1, 1 -> 0)

        // Set the flag to indicate the process is trying to enter
        flag[process] = true;
        turn = other;

        // Busy-wait loop (critical section entry condition)
        while (flag[other] && turn == other)
        {
            // If both processes try to enter at the same time, show an alert
            if (flag[0] && flag[1])
            {
                ShowAlert();
                return; // Exit without entering the critical section
            }
        }

        // Successfully entering the critical section
        inCriticalSection[process] = true;
        UpdateStatus(process); // Update status to "In Critical Section"
    }

    // Method to exit the critical section
    public void ExitCriticalSection(int process)
    {
        // Process is exiting the critical section, reset its flag and status
        flag[process] = false;
        inCriticalSection[process] = false;

        // Update status to "Waiting"
        UpdateStatus(process);
    }

    // Helper method to update the process status text
    private void UpdateStatus(int process)
    {
        if (process == 0)
        {
            process1Status.text = inCriticalSection[0] ? "Process 1: In Critical Section" : "Process 1: Waiting";
        }
        else
        {
            process2Status.text = inCriticalSection[1] ? "Process 2: In Critical Section" : "Process 2: Waiting";
        }
    }

    // Method to show the alert when both processes try to enter the critical section
    private void ShowAlert()
    {
        alertText.text = "Both processes are trying to enter the critical section at the same time!";
    }
}
