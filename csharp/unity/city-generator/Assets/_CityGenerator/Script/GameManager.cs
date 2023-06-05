using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform tileParent;
    [SerializeField] private Transform tilePrefab;

    [Space]
    [SerializeField] private int xMax;
    [SerializeField] private int zMax;

    public void GenerateCity()
    {
        for (int x = 0; x < xMax; x += 10)
        {
            for (int z = 0; z < zMax; z += 10)
            {
                Instantiate
                (
                    original: tilePrefab,
                    position: new Vector3(x - (xMax/2), 0.00f, z - (zMax/2)),
                    rotation: Quaternion.identity,
                    parent  : tileParent
                );
            }
        }
    }
}
