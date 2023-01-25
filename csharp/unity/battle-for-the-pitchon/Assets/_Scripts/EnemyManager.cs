using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public struct Path
    {
        [Header("Parent of all locations in path.")]
        [SerializeField]
        public Transform pathRoot;

        [Header("Enemy start location to spawn.")]
        [SerializeField]
        public Transform pathStart;

        [Header("Enemy intermediate locations to move to, if any.")]
        [SerializeField]
        public Transform[] pathIntermediates;

        [Header("Enemy end location to die.")]
        [SerializeField]
        public Transform pathEnd;

        // Called from editor script to automate 
        // drag'n'drop of path points in hierarchy tree.
        public void ScanChildren()
        {
            // Null-reference checking for pathRoot.
            if (pathRoot == null)
            {
                // Throw an error message.
                Debug.LogError(string.Format("Variable pathRoot is {0}", pathRoot));

                // Terminate here. Don't proceed.
                return;
            }

            // Allocate counter for pathIntermediates. Used in 
            // for-loop later to add children to array.
            int intermdiateCount = 0;

            // Null-reference and out-of-boundaries checking.
            // 2 - because single Start and single End.
            if (pathRoot.childCount > 2)
            {
                // And, all other children must be intermediate points.
                pathIntermediates = new Transform[pathRoot.childCount - 2];
            }

            // Iterate over.
            for (int i = 0; i < pathRoot.childCount; i++)
            {
                // Because child - is shorter.
                Transform child = pathRoot.GetChild(i);

                // Simple string tag comparison.
                if (child.CompareTag("PathStartLocation"))
                {
                    pathStart = child;
                }
                else if (child.CompareTag("PathIntermediateLocation"))
                {
                    pathIntermediates[intermdiateCount++] = child;
                }
                else if (child.CompareTag("PathEndLocation"))
                {
                    pathEnd = child;
                }
            }
        }
    }

    [System.Serializable]
    public struct PathGroup
    {
        [Header("Top parent of all paths")]
        [SerializeField]
        public Transform groupRoot;

        [Header("Paths")]
        [SerializeField]
        public Path[] paths;

        // To be called from editor.
        public void ScanChildren()
        {
            // Null-reference checking for pathRoot.
            if (groupRoot == null)
            {
                // Throw an error message.
                Debug.LogError(string.Format("Variable groupRoot is {0}", groupRoot));

                // Terminate here. Don't proceed.
                return;
            }

            // Group must have at least single path.
            if (groupRoot.childCount == 0)
            {
                // Throw an error message.
                Debug.LogError(string.Format("Variable groupRoot has {0} children", groupRoot.childCount));

                // Terminate here. Don't proceed.
                return;
            }

            // Allocate new array.
            paths = new Path[groupRoot.childCount];

            // Iterate over group's path roots.
            for (int i = 0; i < paths.Length; i++)
            {
                // Shortcut.
                Transform child = groupRoot.GetChild(i);

                // Assign as the root of the path.
                paths[i].pathRoot = child;

                // Scan for start, intermediate and end locations.
                paths[i].ScanChildren();
            }
        }
    }

    [Header("Paths")]
    [SerializeField]
    public PathGroup[] pathGroups;
    [SerializeField]
    public PathGroup[] bossPathGroups;

    [Header("Enemy Template Prefabs")]
    [SerializeField]
    // Accessed from outside.
    public GameObject[] enemyPrefabs;

    [Header("Enemy Template Prefabs")]
    [SerializeField]
    public GameObject[] bossPrefabs;

    #region Enemy Spawn Parameters

    [Header("Spawn Parameters. Path Step must not be zero!")]
    [SerializeField]
    public int pathStep;

    [Header("Valid values are in range of enemy prefabs array length above")]
    [SerializeField]
    public int enemyPrefabIndex;

    [Header("Valid values are in range of path groups array length above")]
    [SerializeField]
    public int pathGroupIndex;

    [Header("Applied to the whole row")]
    [SerializeField]
    public float spawneeSpeed;

    [Header("Applied to the whole row")]
    [SerializeField]
    public float spawneeReachThreshold;

    #endregion

    #region Boss Spawn Parameters

    [Header("Valid values are in range of boss prefabs array length above")]
    [SerializeField]
    public int bossPrefabIndex;

    [Header("Valid values are in range of boss path groups array length above")]
    [SerializeField]
    public int bossPathGroupIndex;

    [Header("Applied to a boss only")]
    [SerializeField]
    public float bossSpeed;

    [Header("Applied to a boss only")]
    [SerializeField]
    public float bossReachThreshold;

    #endregion

    [Header("Level Progressor on the scene.")]
    [SerializeField]
    private LevelProgressor levelProgressor;

    public void Spawn(int enemyPrefabIndex, int pathGroupIndex, int pathStep, float speed, float threshold)
    {
        // Prevent division by zero error.
        if (enemyPrefabs.Length == 0)
        {
            // Error.
            Debug.LogError(string.Format("Length of enemyPrefabs is {0}", enemyPrefabs.Length));

            // Terminate.
            return;
        }

        // Prevent division by zero error.
        if (pathGroups.Length == 0)
        {
            // Error.
            Debug.LogError(string.Format("Length of pathGroups is {0}", pathGroups.Length));

            // Terminate.
            return;
        }

        // Trim inputs for out-of-bounds errors.
        enemyPrefabIndex %= enemyPrefabs.Length;
        pathGroupIndex %= pathGroups.Length;

        // 0 - zero leads to infinite loop.
        if (pathStep <= 0)
        {
            // Terminate here.
            return;
        }

        // Iterate over.
        for (int i = 0; i < pathGroups[pathGroupIndex].paths.Length; i += pathStep)
        {
            // Prepare containers.
            GameObject clone = null;
            EnemyPathFollower follower = null;
            EnemyLifecycle lifecycle = null;

            // Clone.
            clone = Instantiate(enemyPrefabs[enemyPrefabIndex], pathGroups[pathGroupIndex].paths[i].pathStart.position, Quaternion.identity);

            // Retrieve required components.
            follower = clone.GetComponent<EnemyPathFollower>();
            lifecycle = clone.GetComponent<EnemyLifecycle>();

            // Assign its path.
            follower.currentPath = pathGroups[pathGroupIndex].paths[i];

            // Assign its manager. This - the one spawned it.
            lifecycle.enemyManager = this;

            // Setup parameters.
            follower.followSpeed = speed;
            follower.reachThreshold = threshold;
        }
    }

    public void Spawn(int bossPrefabIndex, int pathGroupIndex, float speed, float threshold)
    {
        // Prevent division by zero error.
        if (bossPrefabs.Length == 0)
        {
            // Error.
            Debug.LogError(string.Format("Length of enemyPrefabs is {0}", bossPrefabs.Length));

            // Terminate.
            return;
        }

        // Prevent division by zero error.
        if (bossPathGroups.Length == 0)
        {
            // Error.
            Debug.LogError(string.Format("Length of pathGroups is {0}", bossPathGroups.Length));

            // Terminate.
            return;
        }

        // Trim inputs for out-of-bounds errors.
        bossPrefabIndex %= bossPrefabs.Length;
        pathGroupIndex %= bossPathGroups.Length;

        // Prepare containers.
        GameObject clone = null;
        EnemyPathFollower follower = null;
        BossLifecycle lifecycle = null;

        // Clone. [0] - because there is only 1 boss => only 1 path.
        clone = Instantiate(bossPrefabs[bossPrefabIndex], bossPathGroups[pathGroupIndex].paths[0].pathStart.position, Quaternion.identity);

        // Retrieve required components.
        follower = clone.GetComponent<EnemyPathFollower>();
        lifecycle = clone.GetComponent<BossLifecycle>();

        // Assign its path. [0] - because there is only 1 boss => only 1 path.
        follower.currentPath = bossPathGroups[pathGroupIndex].paths[0];

        // Assign its manager. This - the one spawned it.
        lifecycle.enemyManager = this;

        // Setup parameters.
        follower.followSpeed = speed;
        follower.reachThreshold = threshold;
    }

    public void BalanceUp(uint profit)
    {
        levelProgressor.playerData.balance += profit;
    }

    public void GameOver()
    {
        EnemyLifecycle[] enemies = FindObjectsOfType<EnemyLifecycle>();

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].Die();
        }

        levelProgressor.LoseLevel();
    }
}
