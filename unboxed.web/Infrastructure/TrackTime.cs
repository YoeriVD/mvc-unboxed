using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace unboxed.web.Infrastructure
{
    public class TrackTime : ActionFilterAttribute
    {
        private readonly Stopwatch _watch = new Stopwatch();
        private long _action = 0;
        private long _total = 0;


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _watch.Start();
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _action = _watch.ElapsedMilliseconds;
            base.OnActionExecuted(filterContext);
        }


        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _watch.Stop();
            _total = _watch.ElapsedMilliseconds;
            
            Debug.WriteLine($"-- TrackTime -- Action rendered in {_action}");
            Debug.WriteLine($"-- TrackTime -- total rendered in {_total}");

            base.OnResultExecuted(filterContext);
        }
    }
}