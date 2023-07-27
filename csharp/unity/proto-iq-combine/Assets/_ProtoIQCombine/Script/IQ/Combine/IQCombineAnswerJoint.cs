using Oculus.Interaction;
using UnityEngine;

namespace InteractionVR.IQ.Combine
{
    /// <summary>
    /// Put this script on the object with a collider to 
    /// trigger when snapped to and check whether the 
    /// object is the one it expects otherwise consider
    /// the joint unsolved.
    /// </summary>
    public class IQCombineAnswerJoint : MonoBehaviour
    {
        public SnapInteractable snapInteractable;

        /// <summary>
        /// Sought name of GameObject.
        /// </summary>
        public string targetName;

        /// <summary>
        /// Set this true if we want to use it in the solution.
        /// </summary>
        public bool isSolvable;

        /// <summary>
        /// If the targetName and the SnapInteractor are the same, then we 
        /// return true.
        /// </summary>
        public bool isSolved;

        private Collider _collider;

        private void Start()
        {
            _collider = GetComponent<Collider>();
            IInteractable interactable = snapInteractable;
            interactable.WhenInteractorViewAdded += (IInteractorView view) =>
            {
                SnapInteractor interactor = view.Data as SnapInteractor;

                _collider.enabled = false;
                if (isSolvable)
                {
                    isSolved = interactor.transform.parent.name == targetName;
                }
            };
            interactable.WhenInteractorViewRemoved += (IInteractorView view) =>
            {
                SnapInteractor interactor = view.Data as SnapInteractor;

                _collider.enabled = true;
                isSolved = false;
            };
        }
    } // IQCombineAnswerJoint
} // InteractionVR.IQ.Combine
