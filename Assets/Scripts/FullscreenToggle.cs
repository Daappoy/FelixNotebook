using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour
{
    // Reference to the toggle UI element
    public Toggle fullscreenToggle;

    void Start()
    {
        // Set the toggle state based on the current fullscreen mode
        fullscreenToggle.isOn = Screen.fullScreen;

        // Add listener for when the toggle state changes
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    // This method is called when the toggle changes value
    public void SetFullscreen(bool isFullscreen)
    {
        // Set the fullscreen mode based on the toggle state
        Screen.fullScreen = isFullscreen;
    }
}
