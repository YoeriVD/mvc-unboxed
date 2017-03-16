using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace unboxed.web.Infrastructure
{
    public class SurveyExists : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var db = new UnboxedDbContext())
            {
                object id;
                if (filterContext.ActionParameters.TryGetValue("id", out id))
                {
                    Guid idGuid;
                    if (Guid.TryParse(id?.ToString(), out idGuid))
                        if (db.Surveys.Any(s => s.ExternalId == idGuid))
                        {
                            base.OnActionExecuting(filterContext);
                            return;
                        }
                }
                // BAD PRACTICE ... altijd redirecten in een web omgeving, anders klopt de url niet meer!
                //filterContext.ActionParameters["id"] = db.Surveys.FirstOrDefault()?.ExternalId; 
                //beter:
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName },
                    { "action", filterContext.ActionDescriptor.ActionName },
                    { "id",  db.Surveys.FirstOrDefault()?.ExternalId }
                });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}