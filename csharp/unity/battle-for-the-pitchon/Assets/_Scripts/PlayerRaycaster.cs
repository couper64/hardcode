using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    // Non-serialisable.
    private RaycastHit2D[] results;

    [Header("Maximum number of raycast hits we can retrieve.")]
    [SerializeField]
    private int collisionsMax;

    [Header("Position from where raycast will occure.")]
    [SerializeField]
    private Transform firePoint;

    [Header("A list of distances retrieved from raycast.")]
    [SerializeField]
    private float[] distances;

    [Header("A list of colliders retrieved from raycast.")]
    [SerializeField]
    private Collider2D[] colliders;

    [Header("A list of points retrieved from raycast.")]
    [SerializeField]
    private Vector2[] points;

    [Header("A list of names retrieved from raycast.")]
    [SerializeField]
    private string[] names;

    [Header("Contact Filter 2D settings used in raycasting.")]
    [SerializeField]
    private ContactFilter2D filter;

    private void Reset()
    {
        // Do re-setting when reset from default Editor options.
        ResetParameters();
    }

    private void Update()
    {
        // Hardcode to behave when LMB is pressed.
        if (Input.GetMouseButton(0))
        {
            // Do raycast and ignore return number of results.
            Physics2D.Raycast(firePoint.position, firePoint.up, filter, results);

            // Fill up debugging info arrays.
            for (int i = 0; i < collisionsMax; i++)
            {
                // Distance.
                distances[i] = results[i].distance;
                // Colliders.
                colliders[i] = results[i].collider;
                // Points.
                points[i] = results[i].point;

                // Because there could be more in items in array then 
                // results from raycast.
                if (results[i].transform)
                {
                    // Names.
                    names[i] = results[i].transform.name;
                }
            }
        }
    }

    public void ResetParameters()
    {
        // Setting new array sizes.
        results = new RaycastHit2D[collisionsMax];
        distances = new float[collisionsMax];
        colliders = new Collider2D[collisionsMax];
        points = new Vector2[collisionsMax];
        names = new string[collisionsMax];
    }
}
