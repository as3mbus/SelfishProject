namespace as3mbus.Selfish.Source
{
    public interface IQuiz<TQuestion,TAnswer>: IQuiz
    {
        TQuestion Question { get; }
        bool IsCorrect(TAnswer answer);
    }
}