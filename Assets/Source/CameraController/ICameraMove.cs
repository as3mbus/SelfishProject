using UnityEngine;

namespace A3.CameraController
{
    // TODO : Documentation Comment
    public interface ICameraMove
    {
        void SetZoom(float? value);
        void AddZoom(float? delta);
        void Pan(Vector3? newPos);
        void Navigation(object inputModel);
        void UpdatePos();
        void SetPosition(Vector3 position);
    }
}