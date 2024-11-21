using UnityEngine;
using UnityEngine.UI; // For UI elements like Button
using TMPro; // For TextMeshPro support
using System.Collections.Generic; // For using Queue<T>

public class FIFOPageReplacement : MonoBehaviour
{
    // TextMeshPro elements for displaying the page frame status
    public TextMeshProUGUI pageFrame1Text, pageFrame2Text, pageFrame3Text;
    
    // Button to simulate the process request
    public Button nextPageButton;
    
    // Queue for managing pages
    private Queue<int> pageQueue;
    
    // Number of frames (now 3)
    private int maxFrames = 3; 

    void Start()
    {
        // Initialize the Queue
        pageQueue = new Queue<int>(); 

        // Setup button listener to handle page requests
        nextPageButton.onClick.AddListener(RequestPage);
    }

    // Function to handle page request
    void RequestPage()
    {
        int page = Random.Range(0, 10); // Simulating page request with a random page number
        
        // Debug: Print page number
        Debug.Log("Page " + page + " requested");

        // If the queue is full, replace the oldest page (FIFO)
        if (pageQueue.Count >= maxFrames)
        {
            int replacedPage = pageQueue.Dequeue(); // Remove the oldest page
            Debug.Log("Page " + replacedPage + " replaced");
        }

        // Add the new page to the queue
        pageQueue.Enqueue(page);

        // Update the display of page frames
        UpdatePageFrames();
    }

    // Function to update the page frames UI
    void UpdatePageFrames()
    {
        // Clear current page frame text and update based on queue contents
        // Check if the queue contains enough pages to fill the frames
        pageFrame1Text.text = pageQueue.Count > 0 ? "Page " + pageQueue.ToArray()[0] : "Empty";
        pageFrame2Text.text = pageQueue.Count > 1 ? "Page " + pageQueue.ToArray()[1] : "Empty";
        pageFrame3Text.text = pageQueue.Count > 2 ? "Page " + pageQueue.ToArray()[2] : "Empty";
    }
}
