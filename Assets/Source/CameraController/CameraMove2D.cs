using UnityEngine;

namespace A3.CameraController
{
    public enum Axis
    {
        X,
        Y,
        Z
    }

    public class CameraMove2D : ICameraMove
    {

        public CameraMove2D(Camera cam, Transform transform, CameraMoveSettings settings)
        {
            _camera = cam;
            _transform = transform;
            _calculatedNewPos = _transform.position;
            _settings = settings;
        }

        

        #region Fields

        private readonly Camera _camera;
        private readonly Transform _transform;
        private Vector3? _calculatedNewPos;
        private readonly CameraMoveSettings _settings;

        #endregion

        #region Properties

        private bool IsOrthographic => _camera.orthographic;
        private float _minZoom => _settings.MinZoom;
        private float _maxZoom => _settings.MaxZoom;

        private float PanSpeedZoomModifier =>
            Mathf.Clamp
            (
                ZoomValue,
                _minZoom,
                _maxZoom / 2f
            ) / (_maxZoom + _minZoom / 2f);

        private float ZoomValue
        {
            get =>
                IsOrthographic ? _camera.orthographicSize : _camera.fieldOfView;
            set
            {
                if (IsOrthographic) _camera.orthographicSize = value;
                else _camera.fieldOfView = value;
            }
        }

        #endregion

        public void SetZoom(float? value)
        {
            if (value == null) return;
            float zoom = Mathf.Clamp(value.Value, _minZoom, _maxZoom);
            ZoomValue = zoom;
        }

        public void AddZoom(float? delta)
        {
            SetZoom(ZoomValue + delta * _settings.ZoomSpeed);
        }

        public void Pan(Vector3? newPos)
        {
            if (newPos == null) return;
            Vector3 movPos = _settings.TranslateVector(newPos.Value);
            movPos = _settings.PanSpeed * PanSpeedZoomModifier * movPos;
            _calculatedNewPos += _transform.rotation * movPos;
            LimitMovement();
        }

        public void SetPosition(Vector3 position)
        {
            position.y = _transform.position.y;
            _calculatedNewPos = position;
            LimitMovement();
        }

        private void LimitMovement()
        {
            if (_calculatedNewPos == null) return;
            _calculatedNewPos = _settings.Clamp(_calculatedNewPos.Value);
        }

        public void UpdatePos()
        {
            if (_calculatedNewPos == null) return;
            _transform.position = Vector3.Lerp(_transform.position, _calculatedNewPos.Value,
                Time.deltaTime * _settings.CameraSpeed);
        }

        /// <summary>
        /// Combination of Zoom and Pan
        /// </summary>
        /// <param name="inputModel">Input model that represent movement,  and zoom</param>

        #region Navigation

        public void Navigation(object inputModel)
        {
            if (inputModel == null) return;
            if (!(inputModel is CameraInputModel)) return;
            Navigation((CameraInputModel) inputModel);
        }

        private void Navigation(CameraInputModel input)
        {
            Pan(input.Direction);
            AddZoom(input.Zoom);
        }

        #endregion
    }
}