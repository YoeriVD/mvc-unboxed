using unboxed.Definition;
using unboxed.Definition.Questions;

namespace unboxed.TestData
{
    public static class TestDataFactory
    {
        public static Survey GetSurveyWithPredefinedQuestions()
        {
            var survey = new Survey {Title = "De ultieme java vs c# quiz"};
            survey.Questions.Add(new YesNoQuestion
            {
                QuestionText = "Hebben we er zin in?",
                PositiveAnswer = new Answer {Category = "C#"},
                NegativeAnswer = new Answer {Category = "Java"}
            });

            survey.Questions.Add(new MultipleChoice
            {
                QuestionText = "Wat taal zou je spontaan kiezen?",
                PossibleAnswers = new[]
                {
                    new Answer {Category = "C#", Score = 2},
                    new Answer {Category = "Java", Score = 2}
                }
            });

            survey.Questions.Add(new MultipleChoice
            {
                QuestionText = "Visual Studio of IntelliJ?",
                PossibleAnswers = new[]
                {
                    new Answer {Category = "C#"},
                    new Answer {Category = "Java"}
                }
            });

            return survey;
        }
    }
}