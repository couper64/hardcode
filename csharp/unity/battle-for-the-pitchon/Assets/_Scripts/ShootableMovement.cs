using UnityEngine;

public class ShootableMovement : MonoBehaviour
{
    [Header("Up movement impulse factor.")]
    [SerializeField]
    private float impulse;

    [Header("References. Read-only!")]
    [SerializeField]
    private Rigidbody2D rb;

    private void Reset()
    {
        // When reset from Editor default options.
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Send bullet upwards.
        rb.AddForce(Vector2.up * impulse * Time.deltaTime, ForceMode2D.Impulse);
    }
}
