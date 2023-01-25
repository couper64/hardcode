using UnityEngine;

public class PlayerPrefabShooter : MonoBehaviour
{
    public enum ShootType
    {
        Hold = 0x0001,
        Invoke = 0x0002
    }

    [Header("Fire Points")]
    [SerializeField]
    private Transform[] firePoints;

    [Header("Shootables")]
    [SerializeField]
    private GameObject[] shootables;

    [Header("Restrictors")]
    [SerializeField]
    private float delay;

    [Header("Way of shooting.")]
    [SerializeField]
    private ShootType shootType;

    [Header("Cache. Don't change!")]
    [SerializeField]
    private float timer;

    private void Update()
    {
        switch (shootType)
        {
            case ShootType.Hold:
                ShootOnHold();
                break;
            case ShootType.Invoke:
                ShootOnInvoke();
                break;
        }
    }

    public void Shoot()
    {
        // Clone bullet.
        Instantiate(shootables[0], firePoints[0].position, firePoints[0].rotation);
    }

    private void ShootOnHold()
    {
        // Hardcoded to LMB to shoot. + Extra check to limit the rate 
        // at which shootables are instatiated.
        if (Input.GetMouseButton(0) && (timer > delay))
        {
            // Reset.
            timer = 0.00f;

            // And, instantiate.
            Shoot();
        }

        // Tick timer.
        timer += Time.deltaTime;
    }

    private void ShootOnInvoke()
    {
        // Extra check to limit the rate 
        // at which shootables are instatiated.
        if (timer > delay)
        {
            // Reset.
            timer = 0.00f;

            // And, instantiate.
            Shoot();
        }

        // Tick timer.
        timer += Time.deltaTime;
    }
}
