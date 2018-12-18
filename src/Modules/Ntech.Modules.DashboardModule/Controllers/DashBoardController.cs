using Microsoft.AspNetCore.Mvc;
using Ntech.Modules.DashboardModule.Services;
using Ntech.Platform.CommonService;

namespace Ntech.Modules.DashboardModule.Controllers
{
    public class DashboardController : Controller
    {
        private ICommonService commonService;

        private IDashboardService dashboardService;

        public DashboardController(ICommonService commonService, IDashboardService dashboardService)
        {
            this.commonService = commonService;
            this.dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            ViewBag.TestCommonService = this.commonService.Logging();
            ViewBag.TestDashboardService = this.dashboardService.TestService();
            return View();
        }
    }
}
