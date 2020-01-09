using UnityEngine;

namespace A3.CameraController
{
    // TODO : Ease Of Life
    // - Custom Editor with modified label -> Pan Speed to `Speed` (header already provide zoom Info
    // TODO : Bool Toggle Limit
    // - Zoom Limit toggle 
    // - Position Limit Toggle
    // - might also make custom editor to show and hide field if limit is True
    // TODO : specify Orthographic / perspective zoom limit one for each
    // - which also mean to specify orthographic / perspective conditions

    [CreateAssetMenu(menuName = "GL-Sim/Camera/MoveSettings", order = 0, fileName = "CameraMoveSetting.asset")]
    public class CameraMoveSettings : ScriptableObject
    {
        [Header("Pan")]
        [SerializeField]
        private float _panSpeed = 1;

        [Header("Zoom")]
        [SerializeField]
        private float _zoomSpeed = 1;

        [SerializeField]
        private float _minZoom = 1;

        [SerializeField]
        private float _maxZoom = 179;

        [Header("Camera")]
        [SerializeField]
        private float _cameraSpeed = 1;

        [SerializeField]
        private Vector2 _lowerLimit = Vector2.zero;

        [SerializeField]
        private Vector2 _upperLimit = Vector2.one;

        [SerializeField]
        private Axis _xAxis;

        [SerializeField]
        private Axis _yAxis;

        public float PanSpeed => _panSpeed;
        public float ZoomSpeed => _zoomSpeed;
        public float MinZoom => _minZoom;
        public float MaxZoom => _maxZoom;
        public float CameraSpeed => _cameraSpeed;

        public Vector3 Clamp(Vector3 vector3)
        {
            return new Vector3()
            {
                x = (_xAxis != Axis.X && _yAxis != Axis.X) ? vector3.x : Mathf.Clamp(vector3.x, TranslateVector(_lowerLimit).x, TranslateVector(_upperLimit).x),
                y = (_xAxis != Axis.Y && _yAxis != Axis.Y) ? vector3.y : Mathf.Clamp(vector3.y, TranslateVector(_lowerLimit).y, TranslateVector(_upperLimit).y),
                z = (_xAxis != Axis.Z && _yAxis != Axis.Z) ? vector3.z : Mathf.Clamp(vector3.z, TranslateVector(_lowerLimit).z, TranslateVector(_upperLimit).z)
            };
        }

        public Vector3 TranslateVector(Vector3 vector3)
            => new Vector3(
                0 + ((_xAxis == Axis.X) ? vector3.x : 0) + ((_yAxis == Axis.X) ? vector3.y : 0),
                0 + ((_xAxis == Axis.Y) ? vector3.x : 0) + ((_yAxis == Axis.Y) ? vector3.y : 0),
                0 + ((_xAxis == Axis.Z) ? vector3.x : 0) + ((_yAxis == Axis.Z) ? vector3.y : 0)
            );
    }
}