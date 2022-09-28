using UnityEngine;

namespace Utility
{
    public class ScreenLogger : MonoBehaviour
    {
        public static ScreenLogger Instance;

        #region Properties

        public string ScreenMessage
        {
            get
            {
                return _screenMessage;
            }
            set
            {
                _timer = 0;
                _screenMessage = value;
            }
        }
        [SerializeField]
        private string _screenMessage = "Screen Logger is ON.";

        public float Delay
        {
            get
            {
                return _delay;
            }
        }
        [SerializeField]
        private float _delay = 1.00f;

        public float Timer
        {
            get
            {
                return _timer;
            }
        }
        [SerializeField, ReadOnly]
        private float _timer;

        #endregion // Properties.

        #region Unity Messages

        private void Awake()
        {
            // Mimicking Singleton pattern using Unity mechanisms.
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > Delay)
            {
                _timer = 0;

                ScreenMessage = string.Empty;
            }
        }

        private void OnGUI()
        {
            GUI.Label
            (
                new Rect
                (
                      x: 0
                    , y: 0
                    , width: Screen.width
                    , height: Screen.height
                )
                , ScreenMessage
            );
        }

        #endregion // Unity Messages.
    } // ScreenLogger.
} // Utility.
