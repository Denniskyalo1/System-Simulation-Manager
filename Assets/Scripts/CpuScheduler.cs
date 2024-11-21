using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CPUScheduler : MonoBehaviour
{
    public TMP_InputField ProcessIDInput;
    public TMP_InputField ArrivalTimeInput;
    public TMP_InputField BurstTimeInput;
    public TMP_Text ResultsText;

    // FCFS Logic
    // FCFS Logic
public void SimulateFCFS()
{
    Debug.Log("FCFS button clicked!");

    // Get the user input from the fields
    string processIDs = ProcessIDInput.text;
    string arrivalTimesInput = ArrivalTimeInput.text;
    string burstTimesInput = BurstTimeInput.text;

    // Parse the user input into arrays
    int[] arrivalTimes = ParseInputToArray(arrivalTimesInput);
    int[] burstTimes = ParseInputToArray(burstTimesInput);

    int n = arrivalTimes.Length;
    int[] waitingTimes = new int[n];
    int[] turnaroundTimes = new int[n];

    int currentTime = 0;

    // FCFS Logic
    for (int i = 0; i < n; i++)
    {
        waitingTimes[i] = Mathf.Max(0, currentTime - arrivalTimes[i]);
        currentTime += burstTimes[i];
        turnaroundTimes[i] = waitingTimes[i] + burstTimes[i];
    }

    // Calculate averages
    float avgWaitingTime = 0f;
    float avgTurnaroundTime = 0f;

    for (int i = 0; i < n; i++)
    {
        avgWaitingTime += waitingTimes[i];
        avgTurnaroundTime += turnaroundTimes[i];
    }

    avgWaitingTime /= n;
    avgTurnaroundTime /= n;

    // Display Results in a cleaner table format
    string results = "<b>Process</b>\t<b>Waiting Time</b>\t<b>Turnaround Time</b>\n";
    for (int i = 0; i < n; i++)
    {
        results += $"P{i + 1}\t\t{waitingTimes[i]}\t\t{turnaroundTimes[i]}\n";
    }
    results += $"\n<b>Average Waiting Time</b>: {avgWaitingTime:F2}\n<b>Average Turnaround Time</b>: {avgTurnaroundTime:F2}";
    ResultsText.text = results;

    Debug.Log("FCFS Simulation Complete");
}


    // SRTF Logic
    public void SimulateSRTF()
    {
        Debug.Log("SRTF button clicked!");

        // Get the user input from the fields
        string arrivalTimesInput = ArrivalTimeInput.text;
        string burstTimesInput = BurstTimeInput.text;

        // Parse the user input into arrays
        int[] arrivalTimes = ParseInputToArray(arrivalTimesInput);
        int[] burstTimes = ParseInputToArray(burstTimesInput);

        int n = arrivalTimes.Length;

        int[] remainingBurstTimes = new int[n];
        System.Array.Copy(burstTimes, remainingBurstTimes, n);

        int[] waitingTimes = new int[n];
        int[] turnaroundTimes = new int[n];
        bool[] isCompleted = new bool[n];

        int currentTime = 0;
        int completedProcesses = 0;

        // SRTF Logic
        while (completedProcesses < n)
        {
            // Find the process with the shortest remaining burst time
            int minTime = int.MaxValue;
            int processIndex = -1;

            for (int i = 0; i < n; i++)
            {
                if (!isCompleted[i] && arrivalTimes[i] <= currentTime && remainingBurstTimes[i] < minTime)
                {
                    minTime = remainingBurstTimes[i];
                    processIndex = i;
                }
            }

            if (processIndex != -1)
            {
                // Execute the selected process (decrease remaining burst time)
                remainingBurstTimes[processIndex]--;

                // Check if the process is completed
                if (remainingBurstTimes[processIndex] == 0)
                {
                    isCompleted[processIndex] = true;
                    completedProcesses++;

                    // Calculate waiting and turnaround times
                    turnaroundTimes[processIndex] = currentTime + 1 - arrivalTimes[processIndex];
                    waitingTimes[processIndex] = turnaroundTimes[processIndex] - burstTimes[processIndex];
                }
            }

            currentTime++;
        }

        // Calculate averages
        float avgWaitingTime = 0f;
        float avgTurnaroundTime = 0f;

        for (int i = 0; i < n; i++)
        {
            avgWaitingTime += waitingTimes[i];
            avgTurnaroundTime += turnaroundTimes[i];
        }

        avgWaitingTime /= n;
        avgTurnaroundTime /= n;

        // Display Results in a cleaner table format
        string results = "<b>Process</b>\t<b>Waiting Time</b>\t<b>Turnaround Time</b>\n";
        for (int i = 0; i < n; i++)
        {
            results += $"P{i + 1}\t\t\t{waitingTimes[i]}\t\t\t{turnaroundTimes[i]}\n";
        }
        results += $"\n<b>Average Waiting Time</b>: {avgWaitingTime:F2}\n<b>Average Turnaround Time</b>: {avgTurnaroundTime:F2}";
        ResultsText.text = results;

        Debug.Log("SRTF Simulation Complete");
    }

    // Parse input from a comma-separated string to an array of integers
    private int[] ParseInputToArray(string input)
    {
        string[] splitInput = input.Split(',');
        int[] result = new int[splitInput.Length];

        for (int i = 0; i < splitInput.Length; i++)
        {
            if (int.TryParse(splitInput[i].Trim(), out int value))
            {
                result[i] = value;
            }
            else
            {
                Debug.LogError($"Invalid input detected: {splitInput[i]}");
            }
        }

        return result;
    }
}
