using Unity.VisualScripting;
using UnityEngine;

internal class CameraZoom : MonoBehaviour
{
    public float zoom;
    public float zoomMultiplier = 4f;
    public float minZoon = 2f;
    public float MaxZoom = 8;
    public float velocity = 0f;
    public float smoothTime = 0.25f;

    [SerializeField] public Camera cam;

    void Start()
    {
        zoom = cam.orthographicSize;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoon,MaxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom,ref velocity, smoothTime );
    }
}