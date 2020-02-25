using System;

namespace as3mbus.Selfish.Source
{
    public abstract class Quiz<TQuestion, TAnswer>: IQuiz<TQuestion, TAnswer>
    {
        object IQuiz.Question => Question;
        public bool IsCorrect(object answer)
        {
            if (!(answer is TAnswer)) throw new ArgumentException();
            return IsCorrect((TAnswer) answer);
        }
        
        public abstract TQuestion Question { get; }
        public abstract bool IsCorrect(TAnswer answer);

    }
}