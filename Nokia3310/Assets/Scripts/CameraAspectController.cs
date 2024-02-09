using UnityEngine;

public class CameraAspectController : MonoBehaviour{
    // Set your desired aspect ratio (e.g., 16:9)
    public float targetAspect = 16f / 9f;

    void Start(){
        // Get the camera component attached to the same GameObject
        Camera camera = GetComponent<Camera>();

        // Calculate the current aspect ratio
        float currentAspect = (float)Screen.width / Screen.height;

        // Calculate the desired aspect ratio
        float desiredAspect = targetAspect / currentAspect;

        // Calculate the new width and height of the viewport rect
        float newWidth = Mathf.Min(1f, desiredAspect);
        float newHeight = Mathf.Min(1f, 1f / desiredAspect);

        // Calculate the new x and y offsets for centering
        float xOffset = (1f - newWidth) / 2f;
        float yOffset = (1f - newHeight) / 2f;

        // Set the new viewport rect
        camera.rect = new Rect(xOffset, yOffset, newWidth, newHeight);
    }
}