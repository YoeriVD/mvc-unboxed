using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using unboxed.Execution;
using unboxed.Infrastructure.ServiceLayer;
using unboxed.web.Infrastructure;
using unboxed.web.Models;

namespace unboxed.web.Controllers
{
    public partial class SurveyController : Controller
    {
        private readonly UnboxedDbContext _db;
        private readonly IRequestDispatcher _dispatcher;

        public SurveyController()
        {
            _db = new UnboxedDbContext();
            _dispatcher = new DefaultRequestDispatcher();
        }

        // GET: Survey
        //[TrackTime]
        [SurveyExists]
        public virtual async Task<ActionResult> Index(Guid id)
        {
            var surveyInstances = await _db.SurveyInstances
                .Include(s => s.QuestionInstances)
                .Where(i => i.BasedOn.ExternalId == id)
                .ToListAsync();

            
            var models = surveyInstances.Select(s => new PanelModel
            {
                Title = s.BasedOn.Title,
                Body =
                    $"{s.QuestionInstances.Count(q => q.State == QuestionState.Answered)} vragen van de {s.QuestionInstances.Count} vragen beantwoord.",
                ButtonText = "Hervatten",
                ButtonTarget = MVC.Survey.Resume(s.ExternalId)
            })
            .ToList();

            return View(models);
        }


        public virtual async Task<ActionResult> Start(Guid id)
        {

            var response = await _dispatcher.Execute<CreateSurveyInstanceRequest, CreateSurveyInstanceResponse>(new CreateSurveyInstanceRequest()
            {
                SurveyId = id
            });

            return RedirectToAction(MVC.Survey.Resume(response.SurveyInstanceId));
        }

        public virtual async Task<ActionResult> Resume(Guid id)
        {
            var response = await _dispatcher.Execute<GetNextQuestionRequest, GetNextQuestionResponse>(new GetNextQuestionRequest()
            {
                SurveyInstanceId = id
            });
            if (response.NextQuestion == null) return RedirectToAction(MVC.Survey.Index(response.SurveyId));
            var controllerName = response.NextQuestion.Question.QuestionType;
            return RedirectToAction("Index", controllerName, new
            {
                id = id,
                questionId = response.NextQuestion.ExternalId
            });
        }
    }
}