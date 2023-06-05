using UnityEngine;

public class RandomScaleComponent : MonoBehaviour
{
    [SerializeField] private Transform target;

    [Space]
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;

    [Space]
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    [Space]
    [SerializeField] private float zMin;
    [SerializeField] private float zMax;

    private void Start()
    {
        target.localScale = new Vector3
        (
            x: Random.Range(xMin, xMax),
            y: Random.Range(yMin, yMax),
            z: Random.Range(zMin, zMax)
        );
    }
}
