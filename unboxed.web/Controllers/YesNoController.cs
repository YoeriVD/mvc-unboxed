﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace unboxed.web.Controllers
{
    public partial class YesNoController : Controller
    {
        // GET: YesNo
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}