using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ntech.Modules.React.Controllers
{
    [Route("[Controller]")]
    public class ReactController : ControllerBase
    {
        public ReactController()
        {

        }

        public ActionResult Index()
        {
            return Redirect("~/React/React");
        }

    }
}
