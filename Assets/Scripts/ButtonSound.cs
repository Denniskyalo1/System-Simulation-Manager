using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundManager : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound; // Drag your hover sound here in Unity
    public AudioClip clickSound; // Drag your click sound here in Unity
    private AudioSource audioSource;

    void Start()
    {
        // Get or create an AudioSource component on this button
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play the hover sound
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Play the click sound
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
