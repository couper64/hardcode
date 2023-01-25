using UnityEngine;

public class EnemyLifecycle : BossLifecycle
{
    [Header("Cache data. Do not override!")]
    [SerializeField]
    private EnemyPathFollower follower;

    public new void Damage(float damage)
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

            Die();
        }
    }

    private void Reset()
    {
        // Setup references.
        follower = GetComponent<EnemyPathFollower>();
    }

    private void Update()
    {
        // Check against null.
        if (!follower)
        {
            // Prompt with error message.
            Debug.LogError(string.Format("follower is {0}!", follower));

            // Terminate here.
            return;
        }

        // Enemy reached the end of its journey.
        // And, it was not killed by the player.
        if (follower.ended)
        {
            // Game over for a defendee.
            // Deal absolute damage to it.
            enemyManager.GameOver();

            // Game over for that enemy.
            // Don't change life, just die.
            Die();
        }
    }
}
