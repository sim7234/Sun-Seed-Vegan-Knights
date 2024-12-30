using UnityEngine;

public class RestrictMovementToCamera : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 minBounds; 
    private Vector2 maxBounds; 
    private float objectWidth; 
    private float objectHeight; 

    void Start()
    {
        mainCamera = Camera.main;

        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
        {
            objectWidth = spriteRenderer.bounds.extents.x; 
            objectHeight = spriteRenderer.bounds.extents.y; 
        }
    }

    void Update()
    {
        UpdateCameraBounds();

        Vector3 position = transform.position;

        position.x = Mathf.Clamp(position.x, minBounds.x + objectWidth, maxBounds.x - objectWidth);
        position.y = Mathf.Clamp(position.y, minBounds.y + objectHeight, maxBounds.y - objectHeight);

        transform.position = position;
    }

    void UpdateCameraBounds()
    {
        minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.transform.position.z));
    }
}