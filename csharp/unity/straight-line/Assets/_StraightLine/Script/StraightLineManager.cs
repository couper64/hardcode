using System.Collections.Generic;
using UnityEngine;

public class StraightLineManager : MonoBehaviour
{
    [SerializeField] private float b;
    [SerializeField] private float c;
    [SerializeField] private List<float> xList;

    private List<Vector3> xyList;

    [Space]
    [SerializeField] private LineRenderer lineRenderer;

    private void FixedUpdate()
    {
        if (xyList == null)
        {
            xyList = new List<Vector3>();
        }
        else
        {
            xyList.Clear();
        }

        foreach (float x in xList)
        {
            xyList.Add(new Vector2(x, x * b + c));
        }

        lineRenderer.positionCount = xyList.Count;
        lineRenderer.SetPositions(xyList.ToArray());
    }
}
