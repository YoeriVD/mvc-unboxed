using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using unboxed.Definition.Questions;
using unboxed.Execution;
using unboxed.web.Models;
using unboxed.web.Models.Forms;

namespace unboxed.web.Controllers
{
    public partial class MultipleChoiceController : Controller
    {
        private readonly UnboxedDbContext _db;

        public MultipleChoiceController()
        {
            _db = new UnboxedDbContext();
        }


        // GET: YesNo
        public virtual ActionResult Index(Guid id, Guid questionId)
        {
            var survey = _db.SurveyInstances.First(s => s.ExternalId == id);
            var question = survey.QuestionInstances.First(q => q.ExternalId == questionId);
            var questionDefinition = (MultipleChoice)question.Question;

            var model = new SimpleQuestionFormViewModel()
            {
                Title = question.Question.QuestionText,
                Input = new LookupViewModel()
                {
                    Value = question.Answer,
                    PossibleValues = questionDefinition.PossibleAnswers.Select(a => new LookupItem() { Key = a.Category, Description = a.Category })
                }
            };

            return View(model);
        }
        [HttpPost]
        public virtual async Task<ActionResult> Index(Guid id, Guid questionId, SimpleQuestionFormViewModel form)
        {
            var survey = _db.SurveyInstances.First(s => s.ExternalId == id);
            var question = survey
                .QuestionInstances
                .First(q => q.ExternalId == questionId);

            //in real life ga je de resultaten iets slimmer stockeren, maar dat mogen jullie zelf uitmodeleren :-)
            question.Answer = form.Input.Value;
            question.State = QuestionState.Answered;

            await _db.SaveChangesAsync();

            return RedirectToAction(MVC.Survey.Resume(id));
        }
    }
}