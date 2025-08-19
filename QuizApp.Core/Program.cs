
using System;
using System.IO;
using System.Collections.Generic;

namespace QuizApp.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QuizData");
            string questionsPath = Path.Combine(dataDir, "questions.json");
            if (!File.Exists(questionsPath))
            {
                Console.WriteLine($"Questions file not found: {questionsPath}");
                return;
            }

            string json = File.ReadAllText(questionsPath);
            var categories = System.Text.Json.JsonSerializer.Deserialize<List<CategoryQuestions>>(json);
            if (categories == null || categories.Count == 0)
            {
                Console.WriteLine("No categories found.");
                return;
            }

            Console.WriteLine("Select a category:");
            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {categories[i].Category}");
            }

            int catIndex = -1;
            while (true)
            {
                Console.Write($"Enter category number (1-{categories.Count}): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out catIndex) && catIndex >= 1 && catIndex <= categories.Count)
                {
                    catIndex--;
                    break;
                }
                Console.WriteLine("Invalid input. Please enter a valid category number.");
            }

            var selectedCategory = categories[catIndex];
            var questions = selectedCategory.Questions;
            if (questions == null || questions.Count == 0)
            {
                Console.WriteLine("No questions found in this category.");
                return;
            }

            int score = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                var q = questions[i];
                Console.WriteLine($"\nQuestion {i + 1}: {q.Text}");
                for (int j = 0; j < q.Options.Count; j++)
                {
                    Console.WriteLine($"  {j + 1}. {q.Options[j]}");
                }

                int userAnswer = -1;
                while (true)
                {
                    Console.Write($"Your answer (1-{q.Options.Count}): ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out userAnswer) && userAnswer >= 1 && userAnswer <= q.Options.Count)
                    {
                        userAnswer--;
                        break;
                    }
                    Console.WriteLine("Invalid input. Please enter a number corresponding to the options.");
                }

                if (userAnswer == q.CorrectAnswerIndex)
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine($"Incorrect. The correct answer is: {q.Options[q.CorrectAnswerIndex]}");
                }
            }

            Console.WriteLine($"\nQuiz complete! Your score: {score}/{questions.Count}");
        }
    }
}
