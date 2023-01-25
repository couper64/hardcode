using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;

    void Update()
    {
        transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
    }
}

