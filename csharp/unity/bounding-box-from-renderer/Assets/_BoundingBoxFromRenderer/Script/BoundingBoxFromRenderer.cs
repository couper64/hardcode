using System.Collections.Generic;
using UnityEngine;

public class BoundingBoxFromRenderer : MonoBehaviour
{
    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private List<Vector3> _points;

    private void OnGUI()
    {
        _collider = GetComponent<Collider>();
        Bounds b = _collider.bounds;

        _points = new List<Vector3>();

        _points.Add(b.min);
        _points.Add(b.max);
        _points.Add(new Vector3(b.min.x, b.min.y, b.max.z));
        _points.Add(new Vector3(b.min.x, b.max.y, b.min.z));
        _points.Add(new Vector3(b.max.x, b.min.y, b.min.z));
        _points.Add(new Vector3(b.min.x, b.max.y, b.max.z));
        _points.Add(new Vector3(b.max.x, b.min.y, b.max.z));
        _points.Add(new Vector3(b.max.x, b.max.y, b.min.z));

        float distanceMin = float.MaxValue;
        float distanceMax = float.MinValue;
        Vector3 xMin = Vector3.zero; // In screen coordinates.
        Vector3 xMax = Vector3.zero; // In screen coordinates.
        Vector3 center = _camera.WorldToScreenPoint(b.center);

        foreach (Vector3 point in _points)
        {
            Vector3 x = _camera.WorldToScreenPoint(point);
            float distance = Vector3.Distance(_camera.WorldToScreenPoint(center), x);

            if (distance < distanceMin)
            {
                distanceMin = distance;
                xMin = x;
            }

            if (distance > distanceMax)
            {
                distanceMax = distance;
                xMax = x;
            }
        }

        Vector3 point1 = new Vector3(xMin.x, xMax.y); // 1 ---- 2
        Vector3 point2 = new Vector3(xMax.x, xMax.y); // |      |
        Vector3 point3 = new Vector3(xMax.x, xMin.y); // |      |
        Vector3 point4 = new Vector3(xMin.x, xMin.y); // 4 ---- 3

        // Top Left corner to Top Right corner.
        //Gizmos.DrawLine(cam.ScreenToWorldPoint(point1), cam.ScreenToWorldPoint(point2));
        //Gizmos.DrawLine(cam.ScreenToWorldPoint(point2), cam.ScreenToWorldPoint(point3));
        //Gizmos.DrawLine(cam.ScreenToWorldPoint(point3), cam.ScreenToWorldPoint(point4));
        //Gizmos.DrawLine(cam.ScreenToWorldPoint(point4), cam.ScreenToWorldPoint(point1));

        Debug.Log(point1);

        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();

        GUI.DrawTexture(new Rect(point1.x, point1.y, 1f, 1f), texture);
        GUI.DrawTexture(new Rect(point2.x, point2.y, 1f, 1f), texture);
        GUI.DrawTexture(new Rect(point3.x, point3.y, 1f, 1f), texture);
        GUI.DrawTexture(new Rect(point4.x, point4.y, 1f, 1f), texture);
    }
}
