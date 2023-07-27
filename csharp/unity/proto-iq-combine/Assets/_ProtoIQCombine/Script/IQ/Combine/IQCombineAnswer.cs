using System.Collections.Generic;
using UnityEngine;

namespace InteractionVR.IQ.Combine
{
    /// <summary>
    /// To attach to the answer item.
    /// </summary>
    public class IQCombineAnswer : MonoBehaviour
    {
        public bool isSolved;
        public int solvedCount;
        public int solvedTotal;
        public List<IQCombineAnswerJoint> joints;

        private void Update()
        {
            // reset every frame.
            solvedCount = 0;

            foreach (var item in joints)
            {
                if (item.isSolvable && item.isSolved)
                {
                    solvedCount += 1;
                }
            }
        }
    } // IQCombineAnswer
} // InteractionVR.IQ.Combine
