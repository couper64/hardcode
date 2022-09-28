using UnityEngine;

namespace couper64.Utility
{
    public class LookAt : MonoBehaviour
    {
        [SerializeField, Header("Set in Editor")]
        private Transform _from;

        [SerializeField]
        private Transform _to;

        private void Update()
        {
            _from.LookAt(_to, Vector3.up);
        }
    } // LookAt.
} // couper64.Utility.
