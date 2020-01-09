namespace A3.CameraController
{
    public interface IInput<T>
    {
        T InputValue { get; }
        void ValidateInput();
    }
}