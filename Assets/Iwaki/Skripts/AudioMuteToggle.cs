using UnityEngine;
using UnityEngine.UI;

public class AudioMuteToggle : MonoBehaviour
{
    public AudioSource audioSource;
    public Button muteButton;
    void Start()
    {
        muteButton.onClick.AddListener(ToggleMute);
    }

    void ToggleMute()
    {
        audioSource.mute = !audioSource.mute;
    }
}
