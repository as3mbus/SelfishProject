using UnityEngine;

namespace A3.CameraController
{
    // TODO : different axis handling
    public class CameraController : MonoBehaviour
    {
        #region Fields / Attributes

        private ICameraMove _cameraMove;
        private IInput<CameraInputModel> m_Input;

        [SerializeField]
        private CameraMoveSettings _moveSettings = null;

        private CameraInputModel _inputValue => m_Input.InputValue;

        private bool _isReady = false;

        #endregion


        #region Init Methods

        /// <summary>
        /// Init Camera Controller need to be called beforehand either from awake or start inherit if needed
        /// </summary>
        public void Init()
        {
            if (_isReady) return;
            _cameraMove = new CameraMove2D(GetComponentInChildren<Camera>(), transform, _moveSettings);
            m_Input = new CameraInput();
            _isReady = true;
        }

        private void Awake()
        {
            Init();
        }

        #endregion

        #region Unity Events

        private void Update()
        {
            if (!_isReady) return;
            m_Input.ValidateInput();
            _cameraMove.AddZoom(_inputValue.Zoom);
            _cameraMove.Pan(_inputValue.Direction);
            _cameraMove.UpdatePos();
        }

        #endregion
        
        // set camera pos to position
        public void SetCameraPos(Vector3 pos)
            => _cameraMove.SetPosition(pos);
    }
}