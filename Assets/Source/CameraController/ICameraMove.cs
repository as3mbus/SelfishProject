using UnityEngine;

namespace A3.CameraController
{
    // TODO : Documentation Comment
    // Can be abstracted more but still need more reconsideration in order to do so
    public interface ICameraMove<TInput>
    {
        void Navigation(TInput inputModel);
        void UpdatePos();
        void SetPosition(Vector3 position);
    }
}