namespace TestingPlatform.Mappings
{
    public class QuestionProfile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionDTO, QuestionResponse>();
            CreateMap<CreateQuestionRequest, QuestionDTO>();
            CreateMap<UpdateQuestionRequest, QuestionDTO>();
        }
    }
}
