using UnityEngine;

namespace A3.CameraController
{
    public class CameraController : MonoBehaviour
    {
        #region Fields / Attributes

        private ICameraMove<CameraInputModel> _cameraMove;
        private IInput<CameraInputModel> m_Input;
        private CameraInputModel _inputValue => m_Input.InputValue;

        [SerializeField]
        private CameraMoveSettings _moveSettings = null;


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
            _cameraMove.Navigation(_inputValue);
            _cameraMove.UpdatePos();
        }

        #endregion

        // set camera pos to position
        public void SetCameraPos(Vector3 pos)
            => _cameraMove.SetPosition(pos);
    }
}