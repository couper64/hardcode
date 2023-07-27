using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionVR.IQ.Combine
{
    public class IQCombineManager : MonoBehaviour
    {
        public List<IQCombineAnswer> iqCombineAnswers;

        [Space]
        public Transform checkBox;
        public GameObject nextButton;

        [Space]
        public List<SnapInteractable> questionAnchors;

        public void OnSelect()
        {
            int totalSolvable = 0;
            int totalSolved = 0;
            bool allSolvableInside = true; // Start from true.

            // Total number of solvables is the number to pass a test.
            foreach (var iqCombineAnswer in iqCombineAnswers)
            {
                foreach (var joint in iqCombineAnswer.joints)
                {
                    if (joint.isSolvable)
                    {
                        totalSolvable++;

                        // Is inside the answer box and we care only about solvable items.
                        allSolvableInside = allSolvableInside && Vector3.Distance(joint.transform.position, checkBox.position) < 0.50f;
                    }

                    if (joint.isSolved)
                    {
                        totalSolved++;
                    }
                }
            }

            Debug.Log($"{totalSolvable == totalSolved}: Total Solvable = {totalSolvable} and Total Solved = {totalSolved}; Inside? {allSolvableInside}");

            if (totalSolvable == totalSolved && allSolvableInside)
            {
                checkBox.GetComponent<MeshRenderer>().material.color = new Color(0, 1.00f, 0, 0.50f);
            }
            else
            {
                checkBox.GetComponent<MeshRenderer>().material.color = new Color(1.00f, 0.00f, 0, 0.50f);
            }

            nextButton.SetActive(true);
        }

        public void NextLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    } // IQCombineManager
} // InteractionVR.IQ.Combine
