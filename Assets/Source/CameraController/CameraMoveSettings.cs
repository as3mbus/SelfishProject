using System.Collections.Generic;
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

    [CreateAssetMenu(menuName = "A3/Camera/MoveSettings", order = 0, fileName = "CameraMoveSetting.asset")]
    public class CameraMoveSettings : ScriptableObject
    {
        [Header("Direction / Dimension")]
        [SerializeField]
        private Axis _xAxis = Axis.X;

        [SerializeField]
        private Axis _yAxis = Axis.Y;

        [Header("Speed")]
        [SerializeField]
        private float _panSpeed = 1;

        [SerializeField]
        private float _zoomSpeed = 1;

        [SerializeField]
        private float _cameraSpeed = 1;

        [Header("Zoom Limit")]
        [SerializeField]
        private bool _isZoomLimited = false;

        [SerializeField]
        private float _minZoom = 1;

        [SerializeField]
        private float _maxZoom = 179;

        [Header("Movement Limit")]
        [SerializeField]
        private bool _isMovementLimited = false;

        [SerializeField]
        private Vector2 _lowerLimit = Vector2.zero;

        [SerializeField]
        private Vector2 _upperLimit = Vector2.one;


        public float PanSpeed => _panSpeed;
        public float ZoomSpeed => _zoomSpeed;
        public float MinZoom => _minZoom;
        public float MaxZoom => _maxZoom;
        public float CameraSpeed => _cameraSpeed;

        public float PanSpeedZoomModifier(float zoomValue) =>
            Mathf.Clamp(zoomValue, _minZoom, (_maxZoom / 2f))
            / (_maxZoom + _minZoom / 2f);

        public float LimitZoom(float zoomValue) =>
            (_isZoomLimited) ? Mathf.Clamp(zoomValue, _minZoom, _maxZoom) : zoomValue;

        public Vector3 LimitPosition(Vector3 vector3)
        {
            if (!_isMovementLimited) return vector3;
            Vector3 translatedLowerLimit = TranslateVector(_lowerLimit);
            Vector3 translatedUpperLimit = TranslateVector(_upperLimit);
            Vector3 clampedVector = ClampVector(vector3, translatedLowerLimit, translatedUpperLimit);
            if (_xAxis != Axis.X && _yAxis != Axis.X) clampedVector.x = vector3.x;
            if (_xAxis != Axis.Y && _yAxis != Axis.Y) clampedVector.y = vector3.y;
            if (_xAxis != Axis.Z && _yAxis != Axis.Z) clampedVector.z = vector3.z;
            return clampedVector;
        }

        public Vector3 TranslateVector(Vector3 vector3)
        {
            Vector3 result = Vector3.zero;
            result[(int) _xAxis] += vector3.x;
            result[(int) _yAxis] += vector3.y;
            return result;
        }

        public List<Axis> LockedAxis()
        {
            List<Axis> result = new List<Axis>();
            if (_xAxis != Axis.X && _yAxis != Axis.X) result.Add(Axis.X);
            if (_xAxis != Axis.Y && _yAxis != Axis.Y) result.Add(Axis.Y);
            if (_xAxis != Axis.Z && _yAxis != Axis.Z) result.Add(Axis.Z);
            return result;
        }

        public static Vector3 ClampVector(Vector3 vector3, Vector3 lowerVector, Vector3 upperVector)
        {
            Vector3 v = new Vector3();
            for (int i = 0; i < 3; i++)
                v[i] = Mathf.Clamp(vector3[i], lowerVector[i], upperVector[i]);
            return v;
        }
    }
}