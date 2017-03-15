using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using unboxed.Execution;
using unboxed.web.Models;

namespace unboxed.web.Controllers
{
    public partial class SurveyController : Controller
    {
        private readonly UnboxedDbContext _db;

        public SurveyController()
        {
            _db = new UnboxedDbContext();
        }

        // GET: Survey
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
                    $"{s.QuestionInstances.Count(q => q.State == QuestionState.NotAsked)} vragen van de {s.QuestionInstances.Count} vragen beantwoord.",
                ButtonText = "Hervatten",
                ButtonTarget = MVC.Survey.Resume(s.ExternalId)
            })
            .ToList();

            return View(models);
        }


        public virtual async Task<ActionResult> Start(Guid id)
        {
            var survey = await _db.Surveys.FirstOrDefaultAsync(s => s.ExternalId == id);

            var newInstance = new SurveyInstance()
            {
                BasedOn = survey
            };

            survey.Questions.OrderBy(q => q.Id).Select(q => new QuestionInstance
                {
                    State = QuestionState.NotAsked,
                    Question = q
                })
                .ToList()
                .ForEach(q => newInstance.QuestionInstances.Add(q));


            _db.SurveyInstances.Add(newInstance);
            await _db.SaveChangesAsync();
            return RedirectToAction(MVC.Survey.Resume(newInstance.ExternalId));
        }

        public virtual async Task<ActionResult> Resume(Guid id)
        {
            var survey = await _db.SurveyInstances.FirstOrDefaultAsync(s => s.ExternalId == id);
            var nextQuestion = survey
                .QuestionInstances
                .Where(q => q.State != QuestionState.Answered)
                .OrderBy(q => q.Id).FirstOrDefault();
            if (nextQuestion == null) return RedirectToAction(MVC.Survey.Index(survey.BasedOn.ExternalId));
            var controllerName = nextQuestion.Question.QuestionType;
            return RedirectToAction("Index", controllerName, new {id = nextQuestion.ExternalId});
        }
    }
}