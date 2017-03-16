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
    public partial class YesNoController : Controller
    {
        private readonly UnboxedDbContext _db;

        public YesNoController()
        {
            _db = new UnboxedDbContext();
        }

        // GET: YesNo
        public virtual ActionResult Index(Guid id, Guid questionId)
        {
            var survey = _db.SurveyInstances.First(s => s.ExternalId == id);
            var question = survey.QuestionInstances.First(q => q.ExternalId == questionId);


            var model = new SimpleQuestionFormViewModel()
            {
                Title = question.Question.QuestionText,
                Input = new LookupViewModel()
                {
                    Value = question.Answer,
                    PossibleValues = new List<LookupItem>()
                    {
                        new LookupItem() {Key = true.ToString(), Description = "Ja"},
                        new LookupItem() {Key = false.ToString(), Description = "Nee"},
                    }
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

            question.Answer = form.Input.Value;
            question.State = QuestionState.Answered;

            await _db.SaveChangesAsync();

            return RedirectToAction(MVC.Survey.Resume(id));
        }
    }
}