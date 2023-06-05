using UnityEngine;

public class Controller : MonoBehaviour
{
    const float X_MIN = 0.0f;
    const float X_MAX = 360.0f;
    const float Y_MIN = -90.0f;
    const float Y_MAX = 90.0f;

    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;

    [Space]
    [SerializeField] private float sensitivity;

    void Awake()
    {
        mouseX = transform.rotation.eulerAngles.x;
        mouseY = transform.rotation.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float d = Input.GetAxisRaw("Diagonal");

        transform.Translate(h, d, v, Space.Self);

        mouseX += Input.GetAxis("Mouse X") * (sensitivity * Time.deltaTime);
        if (mouseX < X_MIN) mouseX += X_MAX;
        else if (mouseX > X_MAX) mouseX -= X_MAX;

        mouseY -= Input.GetAxis("Mouse Y") * (sensitivity * Time.deltaTime);
        if (mouseY < Y_MIN) mouseY = Y_MIN;
        else if (mouseY > Y_MAX) mouseY = Y_MAX;

        transform.localEulerAngles = new Vector3(mouseY, mouseX, 0.00f);
    }
}
