using System.Collections.Generic;

namespace QuizApp.Core
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectAnswerIndex { get; set; }
    }
}
