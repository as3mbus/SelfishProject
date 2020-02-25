namespace as3mbus.Selfish.Source
{
    public interface IQuiz
    {
        object Question { get; }
        bool IsCorrect(object answer);
    }
}