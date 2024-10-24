using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    public GameObject playerObject;
    public InputSystem_Actions playerAction;
    public float playerSpeed;

    private InputAction playerMove;
    private InputAction playerFire;

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }

    private void Move()
    {
        playerObject
            .transform
            .Translate(
                playerMove
                .ReadValue<Vector2>()
                * Time.deltaTime
                * playerSpeed);
    }

    private void Awake()
    {
        // Goot tutorial explaining how to do New Input System:
        // https://www.youtube.com/watch?v=HmXU4dZbaMw
        playerAction = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        playerMove = playerAction.Player.Move;
        playerMove.Enable();

        playerFire = playerAction.Player.Attack;
        playerFire.Enable();

        playerFire.performed += Fire;
    }

    private void OnDisable()
    {
        playerMove.Disable();
        playerFire.Disable();
    }

    private void Update()
    {
        Move();
    }
}
