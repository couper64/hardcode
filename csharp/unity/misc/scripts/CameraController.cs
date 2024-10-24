using UnityEngine;

public class CameraController : MonoBehaviour
{
    // WASD : basic movement
    // Shift : Makes camera accelerate

    [Space]
    public Transform controllee;

    [Space]
    public bool showCursor;
    public bool invertMouseY;
    public float speed;
    public float speedShift;
    public float sensitivity;

    [Space]
    public Vector3 offset;

    private void Start()
    {
        Cursor.visible = showCursor;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        if (invertMouseY)
        {
            mouseY = -mouseY;
        }

        offset.x += mouseY * sensitivity * Time.deltaTime;
        offset.y += mouseX * sensitivity * Time.deltaTime;

        offset.x = Mathf.Clamp(offset.x, -90, 90);

        controllee.localEulerAngles = offset;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float d = Input.GetAxisRaw("Diagonal");

        h *= Time.deltaTime * speed;
        v *= Time.deltaTime * speed;
        d *= Time.deltaTime * speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            h *= speedShift;
            v *= speedShift;
            d *= speedShift;
        }

        controllee.Translate(h, d, v);
    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
