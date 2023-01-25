using UnityEngine;

public class CasinoLightRotator : MonoBehaviour
{
    public bool InvertInitialRotation;
    private float speed;

    private void Start()
    {
        if (InvertInitialRotation)
        {
            speed = -1.5f;
        }
        else
        {
            speed = 1.5f;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Rotate(0.0f, 0.0f, speed, Space.Self);
        
        if (transform.eulerAngles.z > 135)
        {
            speed = -1.5f;
        }
        else if(transform.eulerAngles.z < 45)
        {
            speed = 1.5f;
        }     
    }
}
