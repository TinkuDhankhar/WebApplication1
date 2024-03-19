using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using WebApplication1.Models;
using static Dapper.SqlMapper;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            string con = _configuration.GetConnectionString("app");
            PendingCase pendingCases = new PendingCase();
            using (var conection = new SqlConnection(con))
            {
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                var pendingCasesList = await conection.QueryMultipleAsync("USP_IOPending", commandType: CommandType.StoredProcedure);
                pendingCases.PendingCaseList = await pendingCasesList.ReadAsync<PendingCase>(true);
                pendingCases.DistrictList = await pendingCasesList.ReadAsync<StateDistrict>(true);
            }
            return View(pendingCases);
        }
        public async Task<IActionResult> GetPoliceStationByName(string Distict)
        {
            string con = _configuration.GetConnectionString("app");
            IEnumerable<PendingCase> pendingCases = new List<PendingCase>();
            using (var conection = new SqlConnection(con))
            {
                var param = new DynamicParameters();
                param.Add("@District", Distict);
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                pendingCases = await conection.QueryAsync<PendingCase>("USP_GetPoliceStationByName", param: param, commandType: CommandType.StoredProcedure);
            }
            return Json(pendingCases);
        }
        public async Task<IActionResult> GetIOPendinByName(string Distict, string PS)
        {
            string con = _configuration.GetConnectionString("app");
            IEnumerable<PendingCase> pendingCases = new List<PendingCase>();
            using (var conection = new SqlConnection(con))
            {
                var param = new DynamicParameters();
                param.Add("@Dist", Distict);
                param.Add("@PS", PS);
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                pendingCases = await conection.QueryAsync<PendingCase>("USP_GetIOPendingByDistAndPS", param: param, commandType: CommandType.StoredProcedure);
            }
            return Json(pendingCases);
        }
        public async Task<IActionResult> GetIOCaseDetails(string IOName, string dist, string ps)
        {
            string con = _configuration.GetConnectionString("app");
            IEnumerable<PendingCase> pendingCases = new List<PendingCase>();
            using (var conection = new SqlConnection(con))
            {
                var param = new DynamicParameters();
                param.Add("IOName", IOName);
                param.Add("Dist", dist);
                param.Add("PS", ps);
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                pendingCases = await conection.QueryAsync<PendingCase>("USP_IOCaseDetails", param: param, commandType: CommandType.StoredProcedure);
            }
            return Json(pendingCases);
        }
        public async Task<IActionResult> GetIOAllCaseDetails(string IOName)
        {
            string con = _configuration.GetConnectionString("app");
            IEnumerable<PendingCase> pendingCases = new List<PendingCase>();
            using (var conection = new SqlConnection(con))
            {
                var param = new DynamicParameters();
                param.Add("IOName", IOName);
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                pendingCases = await conection.QueryAsync<PendingCase>("USP_GetIOCaseDetails", param: param, commandType: CommandType.StoredProcedure);
            }
            return Json(pendingCases);
        }
        public IActionResult MailTemplete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MailTemplete(ComplaintModel complaint)
        {
            string con = _configuration.GetConnectionString("app");
            int status = 0;
            using (var conection = new SqlConnection(con))
            {
                var param = new DynamicParameters();
                param.Add("Type", 1);
                param.Add("GroupName", complaint.MailFrom);
                param.Add("MailSubj", complaint.MailSubject);
                param.Add("MailTemp", complaint.MailBody);
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                status = await conection.ExecuteAsync("USP_MailTemplate", param: param, commandType: CommandType.StoredProcedure);
            }
            if (status > 0)
            {
                return RedirectToAction(nameof(Templetes));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "there are some technical error");
                return View();
            }
        }
        public async Task<IActionResult> Templetes()
        {
            string con = _configuration.GetConnectionString("app");
            IEnumerable<ComplaintModel> pendingCases = new List<ComplaintModel>();
            using (var conection = new SqlConnection(con))
            {
                var param = new DynamicParameters();
                param.Add("Type", 0);
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                pendingCases = await conection.QueryAsync<ComplaintModel>("USP_MailTemplate", param: param, commandType: CommandType.StoredProcedure);
            }
            return View(pendingCases);
        }
        public async Task<IActionResult> DeadBody()
        {
            string con = _configuration.GetConnectionString("app");
            DeadBody deadBody = new DeadBody();
            using (var conection = new SqlConnection(con))
            {
                var param = new DynamicParameters();
                param.Add("Type", 0);
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                var deadbody = await conection.QueryMultipleAsync("USP_DeadBodys", param: param, commandType: CommandType.StoredProcedure);
                deadBody.DeadBodyList = await deadbody.ReadAsync<DeadBody>(true);
                deadBody.PoliceStationList = await deadbody.ReadAsync<PoliceStation>(true);
            }
            return View(deadBody);
        }
        [HttpPost]
        public async Task<IActionResult> DeadBodySearch(string ps, DateTime from, DateTime to)
        {
            string con = _configuration.GetConnectionString("app");
            IEnumerable<DeadBody> deadBody = new List<DeadBody>();
            using (var conection = new SqlConnection(con))
            {
                var param1 = new DynamicParameters();
                param1.Add("@Type", 3);
                param1.Add("PoliceStation", ps.Trim());
                param1.Add("SearchFrom", from, DbType.DateTime);
                param1.Add("SearchTo", to, DbType.DateTime);
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                try
                {
                    deadBody = await conection.QueryAsync<DeadBody>("USP_DeadBodys", param: param1, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return Json(deadBody);
        }
        public async Task<IActionResult> ArrestedAccused()
        {
            string con = _configuration.GetConnectionString("app");
            IEnumerable<ArrestedAccused> arrestedAccused = new List<ArrestedAccused>();
            using (var conection = new SqlConnection(con))
            {
                var param = new DynamicParameters();
                param.Add("Type", 0);
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                arrestedAccused = await conection.QueryAsync<ArrestedAccused>("USP_FIRArrestedAccused", param: param, commandType: CommandType.StoredProcedure);
            }
            return View(arrestedAccused);
        }
        public async Task<IActionResult> ArrestedAccusedDetailsById(Int64 id)
        {
            string con = _configuration.GetConnectionString("app");
            FIRArrestedAccusedDetails arrestedAccusedDetails = new FIRArrestedAccusedDetails();
            using (var conection = new SqlConnection(con))
            {
                var param = new DynamicParameters();
                param.Add("Type", 2);
                param.Add("FIRAId", id);
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                var accuseDetails = await conection.QueryMultipleAsync("USP_FIRArrestedAccused", param: param, commandType: CommandType.StoredProcedure);
                try
                {
                    arrestedAccusedDetails.ArrestAddressList = await accuseDetails.ReadAsync<ArrestAddress>(true);
                    arrestedAccusedDetails.FIRACTSectionList = await accuseDetails.ReadAsync<FIRACTSection>(true);
                    arrestedAccusedDetails.ArrestPersonDetailList = await accuseDetails.ReadAsync<ArrestPersonDetails>(true);
                    arrestedAccusedDetails.AccusedPhyDescList = await accuseDetails.ReadAsync<AccusedPhyDesc>(true);
                    arrestedAccusedDetails.AccusedPhotoList = await accuseDetails.ReadAsync<AccusedPhoto>(true);
                    arrestedAccusedDetails.IODetailList = await accuseDetails.ReadAsync<IODetails>(true);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return View("_FIRArrestedAccusedDetails", arrestedAccusedDetails);
        }
        public async Task<IActionResult> MissingPerson()
        {
            string con = _configuration.GetConnectionString("app");
            IEnumerable<MissingPerson> arrestedAccused = new List<MissingPerson>();
            using (var conection = new SqlConnection(con))
            {
                var param = new DynamicParameters();
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                arrestedAccused = await conection.QueryAsync<MissingPerson>("USP_GetMissingPerson", param: param, commandType: CommandType.StoredProcedure);
            }
            return View(arrestedAccused);
        }
        public IActionResult Privacy()
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