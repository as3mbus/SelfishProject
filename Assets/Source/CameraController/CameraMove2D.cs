using UnityEngine;

namespace A3.CameraController
{
    public enum Axis
    {
        X = 0,
        Y = 1,
        Z = 2
    }

    public class CameraMove2D : ICameraMove<CameraInputModel>
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
        private Vector3 _calculatedNewPos;
        private readonly CameraMoveSettings _settings;

        #endregion

        #region Properties

        private bool IsOrthographic => _camera.orthographic;

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

        #region Zoom

        private void SetZoom(float value)
            => ZoomValue = _settings.LimitZoom(value);

        private void AddZoom(float delta)
            => SetZoom(ZoomValue + delta * _settings.ZoomSpeed);

        #endregion

        #region Positions

        private void Pan(Vector3 movementDirection)
        {
            Vector3 movPos = _settings.TranslateVector(movementDirection);
            movPos = _settings.PanSpeed * _settings.PanSpeedZoomModifier(ZoomValue) * movPos;
            SetPosition(_calculatedNewPos + (_transform.rotation * movPos));
        }

        public void SetPosition(Vector3 position)
        {
            foreach (Axis axis in _settings.LockedAxis())
                position[(int) axis] = _transform.position[(int) axis];
//            position.y = _transform.position.y;
            _calculatedNewPos = position;
            _calculatedNewPos = _settings.LimitPosition(_calculatedNewPos);
        }

        #endregion

        public void UpdatePos()
            => _transform.position = Vector3.Lerp(_transform.position, _calculatedNewPos,
                Time.deltaTime * _settings.CameraSpeed);


        #region Navigation

        /// <summary>
        /// Combination of Zoom and Pan
        /// </summary>
        /// <param name="inputModel">Input model that represent movement,  and zoom</param>
        public void Navigation(CameraInputModel inputModel)
        {
            Pan(inputModel.MovementDirection);
            AddZoom(inputModel.ZoomScroll);
        }

        #endregion
    }
}