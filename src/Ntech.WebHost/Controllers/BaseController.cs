using Microsoft.AspNetCore.Mvc;
using Ntech.Core.Server;

namespace Ntech.WebHost.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork UnitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

    }
}
