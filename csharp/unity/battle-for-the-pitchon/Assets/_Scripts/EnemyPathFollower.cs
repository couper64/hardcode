using UnityEngine;

public class EnemyPathFollower : MonoBehaviour
{
    [SerializeField]
    public EnemyManager.Path currentPath;

    [Header(
        "Speed at which this enemy is " +
        "moving towards the next waypoint."
    )]
    [SerializeField]
    public float followSpeed;

    [Header(
        "Minimum distance which must be between enemy " +
        "and waypoint to switch to the next waypoint."
    )]
    [SerializeField]
    public float reachThreshold;

    [Header("Cached data. Do not override!")]
    [SerializeField]
    private bool started;
    [SerializeField]
    private int intermediateCount;
    // It is public because accessed by EnemyLifecycle.
    [SerializeField]
    public bool ended;

    private void Awake()
    {
        // Clear currentPath.
        currentPath.pathRoot = null;
        currentPath.pathStart = null;
        currentPath.pathIntermediates = null;
        currentPath.pathEnd = null;

        // Clear parameters.
        //followSpeed = reachThreshold = 0;

        // Clear path state counters.
        started = false;
        intermediateCount = 0;
        ended = false;
    }

    private void Update()
    {
        // Enemy will not move without a path.
        if (!currentPath.pathRoot)
        {
            // Little error report.
            Debug.LogError(string.Format("currentPath's pathRoot is {0}!", currentPath.pathRoot));

            // Terminate here.
            return;
        }

        if (!started)
        {
            // if false, repeat. Else, continue to the next stage.
            started = Follow(currentPath.pathStart, Time.deltaTime * followSpeed, reachThreshold);
        }
        else if (intermediateCount < currentPath.pathIntermediates.Length)
        {
            // if false, repeat. Else, continue to the next stage.
            if (Follow(currentPath.pathIntermediates[intermediateCount], Time.deltaTime * followSpeed, reachThreshold))
            {
                // Increment.
                intermediateCount++;
            }
        }
        else if (!ended)
        {
            // if false, repeat. Else, continue to the next stage.
            ended = Follow(currentPath.pathEnd, Time.deltaTime * followSpeed, reachThreshold);
        }
        else
        {
            // Reset.
            started = false;
            intermediateCount = 0;
            ended = false;
        }
    }

    private bool Follow(Transform followee, float speed, float threshold)
    {
        // Move game object.
        transform.position = Vector3.MoveTowards(transform.position, followee.position, speed);

        // Check if close enough.
        if (Vector3.Distance(transform.position, followee.position) < threshold)
        {
            // On success, we reached the point. Terminate here.
            return (true);
        }

        // Continue moving towards target.
        return (false);
    }
}
