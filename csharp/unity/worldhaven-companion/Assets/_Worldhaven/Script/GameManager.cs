using System.Collections.Generic;
using UnityEngine;

namespace PeasantsLogic
{
    public class GameManager : MonoBehaviour
    {
        public Transform contentTransform;
        public float zoomSpeed = 0.01f;
        public float zoomMin = 0.50f;
        public float zoomMax = 2.50f;

        [Space]
        public List<GameObject> characterPerks;

        public void OnDropdownChange(int index)
        {
            characterPerks.ForEach((go) => go.SetActive(false));
            characterPerks[index].SetActive(true);
        }

        private void Update()
        {
            if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                float distance = Vector3.Distance
                (
                    touch0.position,
                    touch1.position
                );

                Vector3 touch0Prev = touch0.position - touch0.deltaPosition;
                Vector3 touch1Prev = touch1.position - touch1.deltaPosition;

                float distancePrev = Vector3.Distance
                (
                    touch0Prev,
                    touch1Prev
                );

                // Describes the direction and how strong the direction is.
                float force = distance - distancePrev;

                Vector3 scale = contentTransform.localScale;
                scale += Vector3.one * force * zoomSpeed * Time.deltaTime;

                scale.x = Mathf.Clamp(scale.x, zoomMin, zoomMax);
                scale.y = Mathf.Clamp(scale.y, zoomMin, zoomMax);
                scale.z = 1.00f;

                contentTransform.localScale = scale;
            }
        }
    } // GameManager
} // PeasantsLogic
