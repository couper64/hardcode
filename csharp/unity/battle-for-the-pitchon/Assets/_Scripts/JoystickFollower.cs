using UnityEngine;
using UnityEngine.UI;

public class JoystickFollower : MonoBehaviour
{
    [Header("Player Position Controller.")]
    [SerializeField]
    private PlayerPositionController positioner;

    [Header("Back joystick image.")]
    [SerializeField]
    private RectTransform joystickBack;
    [SerializeField]
    private Image joystickBackImage;

    [Header("Front joystick image.")]
    [SerializeField]
    private RectTransform joystickFront;
    [SerializeField]
    private Image joystickFrontImage;

    [Header("Maximum distance from back to front.")]
    [SerializeField]
    private float radius;

    private void Reset()
    {
        // Defaults;
        positioner = null;
        radius = 0.00f;
    }

    private void Update()
    {
        // Allocate temporary containers.
        Vector3 start = Vector3.zero;
        Vector3 end = Vector3.zero;

        start = Camera.main.WorldToScreenPoint(positioner.mouseStart);
        end = Camera.main.WorldToScreenPoint(positioner.mouseEnd);

        // Direction towards end.
        end = end - start;

        if (end.magnitude > radius)
        {
            // To control ourselves by radius.
            end.Normalize();

            // Limit by radius.
            end *= radius;
        }

        if (Input.GetMouseButtonDown(0))
        {

            joystickBackImage.color = new Color(1.00f, 1.00f, 1.00f, 32.00f / 255.00f);
            joystickFrontImage.color = new Color(1.00f, 1.00f, 1.00f, 64.00f / 255.00f);
        }

        if (Input.GetMouseButton(0))
        {
            joystickBack.transform.position = start;
            joystickFront.transform.position = start + end;
        }

        if (Input.GetMouseButtonUp(0))
        {
            joystickBackImage.color = new Color(1.00f, 1.00f, 1.00f, 0.00f);
            joystickFrontImage.color = joystickBackImage.color;
        }
    }
}
