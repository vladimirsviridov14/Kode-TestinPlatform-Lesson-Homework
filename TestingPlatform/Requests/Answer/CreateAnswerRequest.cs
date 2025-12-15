namespace TestingPlatform.Requests.Answer
{
    public class CreateAnswerRequest
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
