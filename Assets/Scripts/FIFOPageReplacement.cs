using UnityEngine;
using UnityEngine.UI; 
using TMPro; 
using System.Collections.Generic; 

public class FIFOPageReplacement : MonoBehaviour
{
    public TextMeshProUGUI pageFrame1Text, pageFrame2Text, pageFrame3Text;
    
    public Button nextPageButton;
    
    // Queue for managing pages
    private Queue<int> pageQueue;
    
    // Number of frames
    private int maxFrames = 3; 

    void Start()
    {
        // Initialize the Queue
        pageQueue = new Queue<int>(); 

        // Setup button listener to handle page requests
        nextPageButton.onClick.AddListener(RequestPage);
    }

    void RequestPage()
    {
        int page = Random.Range(0, 10); 
        
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

    // Update the page frames UI
    void UpdatePageFrames()
    {
        
        pageFrame1Text.text = pageQueue.Count > 0 ? "Page " + pageQueue.ToArray()[0] : "Empty";
        pageFrame2Text.text = pageQueue.Count > 1 ? "Page " + pageQueue.ToArray()[1] : "Empty";
        pageFrame3Text.text = pageQueue.Count > 2 ? "Page " + pageQueue.ToArray()[2] : "Empty";
    }
}
