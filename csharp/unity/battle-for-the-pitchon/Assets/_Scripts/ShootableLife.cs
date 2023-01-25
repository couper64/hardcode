using UnityEngine;

public interface IDamagable
{
    void Damage(float damage);
}

public class ShootableLife : MonoBehaviour
{
    [Header("Explosion Effect on Death.")]
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private float explosionTime;

    [Header("Bullet's Damage.")]
    [SerializeField]
    private float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Play explosion animation.
        Destroy(Instantiate(explosionPrefab, transform.position, transform.rotation), Time.deltaTime * explosionTime);

        // Attempt on dealing damage.
        GameObject other = collision.gameObject;
        IDamagable damagable = null;

        // Check if holder exists.
        if (other)
        {
            // Try to find the interface.
            damagable = other.GetComponent<IDamagable>();
        }

        // Check if holder contains IDamagable.
        if (damagable != null)
        {
            // And, then apply it.
            damagable.Damage(damage);
        }

        // End of life cycle.
        Destroy(gameObject);
    }
}
