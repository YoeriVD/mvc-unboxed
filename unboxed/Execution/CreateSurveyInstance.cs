using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using unboxed.Infrastructure.ServiceLayer;

namespace unboxed.Execution
{
    public class CreateSurveyInstanceRequest : IRequest
    {
        public Guid SurveyId { get; set; }
    }

    public class CreateSurveyInstanceResponse : IResponse
    {
        public Guid SurveyInstanceId { get; set; }
    }

    public class CreateSurveyInstanceHandler :
        IRequestHandler<CreateSurveyInstanceRequest, CreateSurveyInstanceResponse>
    {
        private readonly UnboxedDbContext _db;

        public CreateSurveyInstanceHandler()
        {
            _db = new UnboxedDbContext();
        }

        public async Task<CreateSurveyInstanceResponse> Handle(CreateSurveyInstanceRequest request)
        {
            var survey = await _db.Surveys.FirstOrDefaultAsync(s => s.ExternalId == request.SurveyId);

            var newInstance = new SurveyInstance
            {
                BasedOn = survey
            };

            survey.Questions
                .OrderBy(q => q.Id)
                .Select(q => new QuestionInstance
                {
                    State = QuestionState.NotAsked,
                    Question = q
                })
                .ToList()
                .ForEach(q => newInstance.QuestionInstances.Add(q));


            _db.SurveyInstances.Add(newInstance);
            await _db.SaveChangesAsync();

            return new CreateSurveyInstanceResponse
            {
                SurveyInstanceId = newInstance.ExternalId
            };
        }
    }
}