using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class FrustumMeshComponent : MonoBehaviour
{
    [Range(0.03f, 1.00f)]
    [SerializeField] private float coverage;

    private void Start()
    {
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        meshCollider.convex = true;
        meshCollider.isTrigger = true;

        List<Vector3> vertices = new List<Vector3>();

        Vector3[] frustumFarCorners = new Vector3[4];
        Camera.main.CalculateFrustumCorners
        (
            new Rect(0, 0, 1, 1)               ,
            Camera.main.farClipPlane * coverage,
            Camera.MonoOrStereoscopicEye.Mono  ,
            frustumFarCorners
        );
        vertices.AddRange(frustumFarCorners);
 
        Vector3[] frustumNearCorners = new Vector3[4];
        Camera.main.CalculateFrustumCorners
        (
            new Rect(0, 0, 1, 1)             ,
            Camera.main.nearClipPlane        ,
            Camera.MonoOrStereoscopicEye.Mono,
            frustumNearCorners
        );
        vertices.AddRange(frustumNearCorners);

        List<int> triangles = new List<int>()
        {
            0, 1, 2,
            2, 3, 0,
            0, 4, 5,
            0, 5, 1,
            1, 5, 6,
            1, 6, 2,
            2, 6, 7,
            2, 7, 3,
            3, 7, 0,
            0, 7, 4
        };

        Mesh mesh = new Mesh();
        mesh.Clear();
        mesh.name = "Frustum";
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles.ToArray(), 0);

        meshCollider.sharedMesh = mesh;
    }
}
