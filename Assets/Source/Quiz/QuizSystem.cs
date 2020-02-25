using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace as3mbus.Selfish.Source
{
    public class QuizSystem
    {
        public event Action<IQuiz> OnActiveQuizChanges;
        public event Action<object, bool?> OnAnswerQuiz;
        private readonly List<IQuiz> _quizzes;
        public ImmutableList<IQuiz> Quizzes => _quizzes.ToImmutableList();
        private IQuiz _activeQuiz;
        public int ActiveQuizIndex => _quizzes.IndexOf(ActiveQuiz);

        public IQuiz ActiveQuiz
        {
            get => _activeQuiz;
            private set
            {
                IQuiz previousValue = _activeQuiz;
                _activeQuiz = value;
                if (previousValue == _activeQuiz) return;
                OnActiveQuizChanges?.Invoke(_activeQuiz);
            }
        }

        public QuizSystem()
        {
            _quizzes = new List<IQuiz>();
            _activeQuiz = null;
        }

        public void AddQuiz(IQuiz quiz)
        {
            if (_quizzes.Contains(quiz)) return;
            _quizzes.Add(quiz);
        }

        public void SelectQuiz(IQuiz quiz)
        {
            if (!_quizzes.Contains(quiz)) throw new Exception("Trying to Select Unregistered Quiz");
            ActiveQuiz = quiz;
        }

        public bool AnswerQuiz(object answer)
        {
            bool? isCorrectAnswer = null;
            try
            {
                isCorrectAnswer = ActiveQuiz.IsCorrect(answer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            OnAnswerQuiz?.Invoke(answer, isCorrectAnswer);
            return isCorrectAnswer ?? false;
        }

        public void NextQuiz()
            => ActiveQuiz = _quizzes[NextActiveQuizIndex()];


        private int NextActiveQuizIndex()
        {
            int nextActiveQuizIndex = ActiveQuizIndex;
            nextActiveQuizIndex++;
            if (nextActiveQuizIndex >= _quizzes.Count) nextActiveQuizIndex = 0;
            return nextActiveQuizIndex;
        }

        public void RemoveQuiz(IQuiz quiz)
        {
            if (ActiveQuiz == quiz) ActiveQuiz = null;
            _quizzes.Remove(quiz);
        }
    }
}