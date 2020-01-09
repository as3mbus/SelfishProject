using A3.Utilities.EventSystems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace A3.CameraController
{
    public class CameraInput : IInput<CameraInputModel>
    {
        public CameraInputModel InputValue { get; private set; }
        private Vector3 _lastTouchVal;
        private Vector3? _lastDeltaTouch;
        private float _flickEffectValue = 0;

        // TODO : can be separated into another instance

        #region Flick Configuration

        private const float FLICK_SPEED_MODIFIER = 0.1f;
        private const float FLICK_EFFECT_DURATION = 1;

        #endregion

        public void ValidateInput()
        {
            InputValue = new CameraInputModel()
            {
                Direction = PcPanInput(),
                Zoom = AndroidPinchZoomInput() ?? PcMouseScrollZoomInput(),
            };
        }

        private Vector3? FlickInput()
        {
            if (_lastDeltaTouch == null) return null;
            if (_flickEffectValue <= 0) return null;
            _flickEffectValue -= (Time.deltaTime * FLICK_SPEED_MODIFIER);
            _flickEffectValue = Mathf.Max(0, _flickEffectValue);
            _lastDeltaTouch *= _flickEffectValue;
            return _lastDeltaTouch;
        }

        #region Pan Input

        private Vector3? PcPanInput()
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverUiObject()) return null;
            if (Input.touchCount > 1) return null;

            if (Input.GetMouseButtonDown(0))
            {
                _lastTouchVal = Input.mousePosition;
                _flickEffectValue = FLICK_EFFECT_DURATION;
            }

            if (Input.GetMouseButtonUp(0))
                _lastTouchVal = Input.mousePosition;

            if (!Input.GetMouseButton(0)) return FlickInput();

            Vector3 touchPos = Input.mousePosition;

            if (Vector3.Distance(_lastTouchVal, touchPos) <= 0) _lastDeltaTouch = null;
            else _lastDeltaTouch = (_lastTouchVal - touchPos) / Mathf.Min(Screen.width, Screen.height);

            _lastTouchVal = touchPos;

            return _lastDeltaTouch;
        }

        #endregion

        #region Zoom Input

        private static float? PcMouseScrollZoomInput()
        {
            float? deltaScroll = Input.GetAxis("Mouse ScrollWheel");
            return (Mathf.Abs(deltaScroll.Value) > 0) ? -deltaScroll : null;
        }


        private static float? AndroidPinchZoomInput()
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return null;
            if (Input.touchCount < 2) return null;
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector3 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector3 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            return deltaMagnitudeDiff / Mathf.Min(Screen.width, Screen.height);
        }

        // Previous project notes
        // FOV max zoom out = `(MaxZoomOut / 180f) * 179.9f)`
        // min zoom = `10`

        #endregion
    }
}