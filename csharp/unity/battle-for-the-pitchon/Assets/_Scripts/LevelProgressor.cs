using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///  LevelProgressor is reponsible for 
///  smartly scrolling down background 
///  images.
/// </summary>
public class LevelProgressor : MonoBehaviour
{
    // State of the level.
    [System.Serializable]
    public enum State
    {
        Pause = 0x0001,
        Enemies = 0x0002,
        JumpNoRewards = 0x0003,
        Boss = 0x0004,
        BossFight = 0x0005,
        JumpRewards = 0x0006
    }

    [System.Serializable]
    public struct Wave
    {
        // Launch waves if started.
        public static bool enabled = false;

        // Used to decided whether to 
        // compare against start delay or end.
        public bool started;

        // Before wave delay in miliseconds.
        public float startDelay;

        // After wave delay in miliseconds.
        public float endDelay;

        // Speed of the wave of the enemies.
        public float speed;

        // For-loop iterator step-value used 
        // during spawning of the enemies.
        public int enemyStep;

        // Index of enemy in Enemy Manager array 
        // of enemies.
        public int enemyType;

        // Index of path group in Enemy Manager array 
        // of path groups.
        public int pathType;
    }
    
    // In seconds.
    private const float MAX_ENEMIES_PERIOD = 30.00f;
    // In seconds.
    private const float MAX_JUMP_REWARDS_PERIOD = 5.00f;

    [Header("Delay before loading new scene.")]
    [SerializeField]
    private float openDelay;

    [Header("Waves settings")]
    [SerializeField]
    private float waveStartDelay;

    [Header("Player's Data")]
    [SerializeField]
    // Its public because accessed from outside.
    public PlayerData playerData;

    [Header("Level Over UI.")]
    [SerializeField]
    private GameObject levelOverUI;
    [SerializeField]
    private LevelOver levelOver;

    [Header("Cache data. Do not change it!")]
    [SerializeField]
    private int waveCount;
    [SerializeField]
    private float waveTimer;
    [SerializeField]
    private float waveDelay;
    [SerializeField]
    private EnemyManager enemyManager;
    [SerializeField]
    private State state = State.Enemies;
    [SerializeField]
    private float enemiesTimer;
    [SerializeField]
    private float jumpRewardTimer;
    [SerializeField]
    private BossLifecycle bossLifecycle;

    private void Reset()
    {
        // Get references.
        enemyManager = FindObjectOfType<EnemyManager>();

        // Default values.
        openDelay = 5.00f;
    }

    private void Awake()
    {
        // Initialise.
        waveCount = 0;
        waveDelay = waveStartDelay;
        playerData = PlayerSaver.Load();
    }

    private void Update()
    {
        switch (state)
        {
            case State.Pause:
                // Do nothing.
                break;
            case State.Enemies:
                // Spawn waves of enemies.

                // Tick local in-between wave timer.
                waveTimer += Time.deltaTime;
                // Global enemies period timer.
                enemiesTimer += Time.deltaTime;

                // After MAX_ENEMIES_PERIOD.
                if (enemiesTimer > MAX_ENEMIES_PERIOD)
                {
                    // Reset timer.
                    waveTimer = 0.00f;
                    enemiesTimer = 0.00f;

                    // Set new delay.
                    waveDelay -= 0.25f * waveStartDelay;

                    // After enemies there is always a boss.
                    state = State.JumpNoRewards;
                }

                if (waveTimer > waveDelay)
                {
                    // Reset timer.
                    waveTimer = 0.00f;

                    int enemyPrefabIndex = Random.Range(0, enemyManager.enemyPrefabs.Length);
                    int pathGroupIndex = Random.Range(0 , enemyManager.pathGroups.Length);
                    int pathStep = Random.Range(1, 5);
                    float speed = Random.Range(0.50f, 5.00f);

                    enemyManager.Spawn
                    (
                        enemyPrefabIndex, 
                        pathGroupIndex, 
                        pathStep, 
                        speed, 
                        1.00f
                    );
                }

                break;
            case State.Boss:

                Debug.Log("You reached Boss.");

                if (!bossLifecycle)
                {
                    int bossPrefabIndex = Random.Range(0, enemyManager.bossPrefabs.Length);
                    int pathGroupIndex = Random.Range(0, enemyManager.bossPathGroups.Length);
                    float speed = Random.Range(2.50f, 5.00f);

                    enemyManager.Spawn
                    (
                        bossPrefabIndex,
                        pathGroupIndex,
                        speed,
                        1.00f
                    );

                    bossLifecycle = FindObjectOfType<BossLifecycle>();

                    state = State.BossFight;
                }

                break;
            case State.BossFight:

                if (!bossLifecycle)
                {
                    state = State.JumpRewards;
                    Debug.Log("You killed Boss. Here's your reward.");
                }

                break;
            case State.JumpRewards:
                // Tick timer.
                jumpRewardTimer += Time.deltaTime;

                if (jumpRewardTimer > MAX_JUMP_REWARDS_PERIOD)
                {
                    // Reset.
                    jumpRewardTimer = 0.00f;

                    // Start enemies again.
                    state = State.Enemies;
                }

                break;
            case State.JumpNoRewards:

                Debug.Log("You reached JumpNoRewards.");

                EnemyLifecycle[] lifecycles = FindObjectsOfType<EnemyLifecycle>();

                if (lifecycles.Length.Equals(0))
                {
                    state = State.Boss;
                    Debug.Log("You left JumpNoRewards.");
                }

                break;
        }
    }

    private System.Collections.IEnumerator Open(string scene)
    {
        // Custom delay.
        yield return new WaitForSeconds(openDelay);

        // Then, load new scene.
        SceneManager.LoadScene(scene);
    }

    public void LoseLevel()
    {
        // Disable waves.
        Wave.enabled = false;

        // Reset.
        waveCount = 0;
        waveTimer = 0.00f;

        // Level is over.
        Debug.Log("Not so happy days. You lost you first LEVEL!");

        // To simulate delay.
        //StartCoroutine(Open("MainMenu_Test"));

        // Show Level Over UI.
        levelOverUI.SetActive(true);

        // And, make it win.
        levelOver.state = LevelOver.State.Lose;

        // Stop enemies.
        state = State.Pause;
    }

    public void UnPauseProgression() 
    {
        // Set to start.
        state = State.Enemies;
    }
}
