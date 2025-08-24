using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField]
    public float yAxisDropBuffer = 0.0f;

    Camera mainCamera;

    Vector2 mousePositionOffset;
    Vector3 originalLocation;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Capture mouse position and return the world point
        return mainCamera.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        // Capture mouse offset
        mousePositionOffset = Input.mousePosition - GetMouseWorldPosition();

        // Capture the spot when the object was originally picked up
        originalLocation = gameObject.transform.position;
    }

    private void OnMouseDrag()
    {
        Vector2 newPosition = mainCamera.ScreenToWorldPoint((Vector2)Input.mousePosition - mousePositionOffset);

        // Move the gameobject relative to the mouse
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    //private void OnMouseUp()
    //{
    //    // If the object is dropped and not in trigger space of "package box" then we need to put it back onto the belt

    //    // X will be the same as when it was picked up to put it back onto the belt
    //    // Y will be the same as when it was picked up + a bit of room so it drops onto the belt
    //    // Z will be updated from the point at which it was dropped
    //    transform.position = new Vector3(originalLocation.x, originalLocation.y + 3, gameObject.transform.position.z);
    //}
}
