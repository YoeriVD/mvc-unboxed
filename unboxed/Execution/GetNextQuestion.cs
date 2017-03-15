using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unboxed.Infrastructure.ServiceLayer;

namespace unboxed.Execution
{
    public class GetNextQuestionRequest : IRequest
    {
        public Guid SurveyInstanceId { get; set; }
    }
    public class GetNextQuestionResponse : IResponse
    {
        public QuestionInstance NextQuestion { get; set; }
        public Guid SurveyId { get; set; }
    }

    public class GetNextQuestionHandler : IRequestHandler<GetNextQuestionRequest, GetNextQuestionResponse>
    {
        private readonly UnboxedDbContext _db;

        public GetNextQuestionHandler()
        {
            _db = new UnboxedDbContext();
        }

        public async Task<GetNextQuestionResponse> Handle(GetNextQuestionRequest request)
        {
            var survey = await _db.SurveyInstances.FirstOrDefaultAsync(s => s.ExternalId == request.SurveyInstanceId);
            var nextQuestion = survey
                .QuestionInstances
                .Where(q => q.State != QuestionState.Answered)
                .OrderBy(q => q.Id).FirstOrDefault();
            
            return new GetNextQuestionResponse()
            {
                SurveyId = survey.ExternalId,
                NextQuestion = nextQuestion
            };
        }
    }
}
