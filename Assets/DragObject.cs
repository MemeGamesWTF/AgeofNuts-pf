using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 offset; // To store the offset between mouse position and object position
    private Camera cam;     // To convert screen to world point

    public Sprite[] sprites; // Array of sprites to choose from
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    public SpriteRenderer bgSpriteRenderer; // Reference to the background sprite renderer

    private Bounds bgBounds; // Bounds of the background sprite

    private void Start()
    {
        cam = Camera.main; // Get the main camera
        if (cam == null)
        {
            Debug.LogError("Main camera not found.");
            return;
        }

        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found.");
            return;
        }

        if (bgSpriteRenderer == null)
        {
            Debug.LogError("Background SpriteRenderer not assigned.");
            return;
        }

        if (sprites.Length > 0)
        {
            // Randomly choose a sprite from the array
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
        else
        {
            Debug.LogWarning("No sprites assigned. Please assign sprites in the inspector.");
        }

        // Calculate the bounds of the background sprite
        bgBounds = bgSpriteRenderer.bounds;
    }

    private void OnMouseDown()
    {
        // Calculate offset when the mouse is clicked on the object
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        // Update object position as the mouse moves
        Vector3 newPosition = GetMouseWorldPos() + offset;

        // Clamp the position within the bounds of the background sprite
        newPosition.x = Mathf.Clamp(newPosition.x, bgBounds.min.x, bgBounds.max.x);
        newPosition.y = Mathf.Clamp(newPosition.y, bgBounds.min.y, bgBounds.max.y);

        transform.position = newPosition;
    }

    private Vector3 GetMouseWorldPos()
    {
        // Get mouse position in screen space and convert to world space
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.nearClipPlane; // Use the camera's near clipping plane distance
        return cam.ScreenToWorldPoint(mousePoint);
    }
}
