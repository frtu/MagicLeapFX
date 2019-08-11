using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

namespace MagicLeap
{
    /// <summary>
    /// Class outputs to input UI.Text the most up to date gestures
    /// and confidence values for each of the hands.
    /// </summary>
    [RequireComponent(requiredComponent: typeof(HandTracking))]
    public class MLHandVisibilityToggle : MonoBehaviour
    {
        #region Private Variables
        private const float CONFIDENCE_THRESHOLD = 0.95f;

        [SerializeField, Tooltip("Game object to activate/deactivate.")]
        private GameObject _targetObject = null;

        [SerializeField, Tooltip("KeyPose to Activate game object.")]
        private MLHandKeyPose _keyPoseToToggleON = MLHandKeyPose.NoPose;

        [SerializeField, Tooltip("KeyPose to Desactivate game object.")]
        private MLHandKeyPose _keyPoseToToggleOFF = MLHandKeyPose.NoPose;

        [Space, SerializeField, Tooltip("Flag to specify if left hand should be tracked.")]
        private bool _trackLeftHand = true;

        [SerializeField, Tooltip("Flag to specify id right hand should be tracked.")]
        private bool _trackRightHand = true;

        [SerializeField, Tooltip("Text to display gesture status to.")]
        private Text _statusText = null;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Check editor set variables against null or for NoPose references.
        /// </summary>
        void Awake()
        {
            if (_targetObject == null) {
                Debug.LogError("Error: _targetObject is not set, disabling script.");
                enabled = false;
                return;
            }

            if (_keyPoseToToggleON == MLHandKeyPose.NoPose 
            || _keyPoseToToggleOFF == MLHandKeyPose.NoPose)
            {
                Debug.LogError("Error: Either _keyPoseToToggleON/OFF are not set, disabling script.");
                enabled = false;
                return;
            }
        }

        /// <summary>
        ///  Polls the Gestures API for up to date confidence values.
        /// </summary>
        void Update()
        {
            if (!MLHands.IsStarted) {
                return;
            }

            if (_statusText != null) {
                _statusText.text = string.Format(
                    "Current Hand Gestures\nLeft: {0}, {2}% confidence\nRight: {1}, {3}% confidence",
                    MLHands.Left.KeyPose.ToString(),
                    MLHands.Right.KeyPose.ToString(),
                    (MLHands.Left.KeyPoseConfidence * 100.0f).ToString("n0"),
                    (MLHands.Right.KeyPoseConfidence * 100.0f).ToString("n0"));
            }

            // If inactive, listen to Activate signal
            if (!_targetObject.activeSelf 
                && GetKeyPoseConfidence(MLHands.Left, MLHands.Right, _keyPoseToToggleON)) 
            {
                _statusText.text = string.Format("Hand Gestures {0} Detected !!",
                    _keyPoseToToggleON.ToString());
                _targetObject.SetActive(true);
            }
            
            // If active, listen to Deactivate signal
            if (_targetObject.activeSelf
                && GetKeyPoseConfidence(MLHands.Left, MLHands.Right, _keyPoseToToggleOFF)) 
            {
                _statusText.text = string.Format("Hand Gestures {0} Detected !!",
                    _keyPoseToToggleOFF.ToString());
                _targetObject.SetActive(false);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get the confidence value for the hand being tracked.
        /// </summary>
        /// <param name="hand">Hand to check the confidence value on. </param>
        /// <param name="handKeyPose">Which hand pose to check against. </param>
        /// <returns></returns>
        private bool GetKeyPoseConfidence(MLHand leftHand, MLHand rightHand, MLHandKeyPose handKeyPose) {
            float confidenceLeft = _trackLeftHand ? GetKeyPoseConfidence(leftHand, handKeyPose) : 0.0f;
            float confidenceRight = _trackRightHand ? GetKeyPoseConfidence(rightHand, handKeyPose) : 0.0f;
            float confidenceValue = Mathf.Max(confidenceLeft, confidenceRight);

            return confidenceValue > 0.0f;
        }

        /// <summary>
        /// Get the confidence value for the hand being tracked.
        /// </summary>
        /// <param name="hand">Hand to check the confidence value on. </param>
        /// <param name="handKeyPose">Which hand pose to check against. </param>
        /// <returns></returns>
        private float GetKeyPoseConfidence(MLHand hand, MLHandKeyPose handKeyPose) {
            if (hand != null) {
                if (hand.KeyPose == handKeyPose) {
                    return hand.KeyPoseConfidence;
                }
            }
            return 0.0f;
        }
        #endregion
    }
}
