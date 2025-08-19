using System.Collections.Generic;

namespace QuizApp.Core
{
    public class CategoryQuestions
    {
        public string Category { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
