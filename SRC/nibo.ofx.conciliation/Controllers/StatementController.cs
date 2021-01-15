using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nibo.ofx.conciliation.Extensions;
using nibo.ofx.conciliation.Models;
using nibo.ofx.conciliation.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace nibo.ofx.conciliation.Controllers
{
    public class StatementController : Controller
    {
        private readonly ILogger<StatementController> _logger;

        public StatementController(ILogger<StatementController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {            
            var statement = ConciliationService.ConciliateFiles();

            return View(statement);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
