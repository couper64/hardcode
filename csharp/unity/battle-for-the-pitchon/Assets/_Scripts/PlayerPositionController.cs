using UnityEngine;

public class PlayerPositionController : MonoBehaviour
{
    [Header("Player Controller Parameters")]
    [SerializeField]
    private float speed;

    [Header("Debug Information. Do not change it!")]
    [SerializeField]
    private Vector3 mouseScreenPosition;
    [SerializeField]
    private Vector3 mousePosition;
    [SerializeField]
    private bool isOutside;
    [SerializeField]
    public Vector3 mouseStart;
    [SerializeField]
    public Vector3 mouseEnd;
    [SerializeField]
    private Vector3 mouseDeltaPosition;
    [SerializeField]
    public Vector3 mouseDeltaNormal;
    [SerializeField]
    private Rigidbody2D rb;

    private void Update()
    {
        // Start state.
        isOutside = false;
        mouseScreenPosition = Input.mousePosition;
        //mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition, Camera.MonoOrStereoscopicEye.Mono);

        isOutside = mouseScreenPosition.x > Screen.width;
        isOutside = mouseScreenPosition.x < 0.00f ? true : isOutside;
        isOutside = mouseScreenPosition.y > Screen.height ? true : isOutside;
        isOutside = mouseScreenPosition.y < 0.00f ? true : isOutside;

        // Check.
        if (isOutside)
        {
            // Terminate.
            return;
        }

        // This is left for animation state control in 
        // PlayerLifecycle script.
        if (Input.GetMouseButtonDown(0))
        {
            mouseStart = mousePosition;
        }

        // Whenever we touch or right-click on the screen.
        if (Input.GetMouseButton(0))
        {
            // We update the last mouse position with the latest one.
            mousePosition = Camera.main.ScreenToWorldPoint
            (
                mouseScreenPosition, 
                Camera.MonoOrStereoscopicEye.Mono
            );
            mouseEnd = mousePosition;
        }

        // This is left for animation state control in 
        // PlayerLifecycle script.
        if (Input.GetMouseButtonUp(0))
        {
            mouseStart = mouseEnd = Vector3.zero;
        }

        // This is left for animation state control in 
        // PlayerLifecycle script.
        mouseDeltaPosition = mouseEnd - mouseStart;

        // This is left for animation state control in 
        // PlayerLifecycle script.
        mouseDeltaNormal = mouseDeltaPosition.normalized;

        // Apply to rigidbody, because there are boundaries which 
        // bounds player inside the game field.
        rb.MovePosition(mousePosition);
    }
}
