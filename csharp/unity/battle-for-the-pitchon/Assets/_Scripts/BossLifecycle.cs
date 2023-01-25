using UnityEngine;

public class BossLifecycle : MonoBehaviour, IDamagable
{
    [Header("Enemy life parameters.")]
    [SerializeField]
    private float maxHP;
    [SerializeField]
    public float currentHP;

    [Header("Amount of money it returns to the player.")]
    [SerializeField]
    protected uint deathValue;

    [Header("Explosion Effect on Death.")]
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private float explosionTime;

    [Header("Cache data. Do not override!")]
    [SerializeField]
    public EnemyManager enemyManager;

    public void Damage(float damage)
    {
        // Prevent from healing enemy.
        if (damage < 0)
        {
            // Terminate.
            return;
        }

        // Deal it.
        currentHP -= damage;

        if (currentHP <= 0)
        {
            // Return some reward to the player.
            enemyManager.BalanceUp(deathValue);

            // And, die!
            Die();
        }
    }

    public void Die()
    {
        // Play explosion animation.
        Destroy(Instantiate(explosionPrefab, transform.position, transform.rotation), Time.deltaTime * explosionTime);

        // Destroy not this class, but object 
        // which holds this class.
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void Awake()
    {
        // Begin play setup.
        currentHP = maxHP;
    }
}

