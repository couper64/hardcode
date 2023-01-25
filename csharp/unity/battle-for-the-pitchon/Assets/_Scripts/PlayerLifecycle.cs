using UnityEngine;

public class PlayerLifecycle : MonoBehaviour
{
    // Player's Animation State.
    // Values tighten to animator parameters.
    public enum State
    {
        Forward = 0,
        Right = 1,
        Left = 2,
        Backwards = 3
    }

    [Header("Player's state.")]
    [SerializeField]
    private State state;

    [Header("Cache data. Do not override!")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private int stateId;
    [SerializeField]
    private PlayerPositionController controller;

    private void Reset()
    {
        // Make an attempt to reset pointer to 
        // an animator in child components.
        animator = GetComponentInChildren<Animator>();

        // Generate hash id for animator parameter.
        stateId = Animator.StringToHash("State");

        // Start state.
        state = State.Forward;

        // Get a reference.
        controller = GetComponent<PlayerPositionController>();
    }

    private void Update()
    {
        // If PlayerPositionController is null, then 
        // there is no point of playing animations.
        if (!controller)
        {
            // Terminate here.
            return;
        }

        // Pass state to animator.
        animator.SetInteger(stateId, (int)state);

        // Get direction over axis.
        float forwardRate = Vector3.Dot(Vector3.up, controller.mouseDeltaNormal);
        float rightRate = Vector3.Dot(Vector3.right, controller.mouseDeltaNormal);

        if (Mathf.Abs(rightRate) > Mathf.Abs(forwardRate))
        {
            // State either right or left.
            if (rightRate > 0)
            {
                state = State.Right;
            }
            else
            {
                state = State.Left;
            }
        }
        else
        {
            // State either forward or backwards.
            // Order was inversed because in case of 
            // no input it would fall back to 
            // forward state, which in turn would 
            // fall back to forward animation.
            if (forwardRate < 0)
            {
                state = State.Backwards;
            }
            else
            {
                state = State.Forward;
            }
        }
    }
}
