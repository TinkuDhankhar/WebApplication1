using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CaseController : Controller
    {
        private readonly IConfiguration _configuration;
        public CaseController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DownloadFile(string html, string name)
        {
            var htmlToPdf = new HtmlToPDFCore.HtmlToPDF();
            var pdf = htmlToPdf.ReturnPDF(html);
            //pdf.Margins = new PageMargins(0,0,0,0);
            //FileStream fs = new FileStream("teste.pdf", FileMode.OpenOrCreate);
            //fs.Write(pdf, 0, pdf.Length);
            //var pathToImage = Path.Combine(Directory.GetCurrentDirectory(), "text.pdf");
            //System.IO.File.Delete(pathToImage);
            //System.IO.File.WriteAllBytesAsync(pathToImage, pdf);
            //System.IO.File.ReadAllBytes(pdf);

            //fs.Dispose();
            //return File(fs, "application/pdf");
            return File(pdf.ToArray(), "application/pdf", $"{name}.pdf");
        }
        public IActionResult Complaint()
        {
            var model = new ComplaintModel();
            model.complaintTypesList.Add(new ComplaintTypeSubject()
            {
                id = 1,
                ComplainTypeSubjectName = "CCTNS NOT WORKING",
                GroupName = "CCTNS PORTAL"
            });
            model.complaintTypesList.Add(new ComplaintTypeSubject()
            {
                id = 2,
                ComplainTypeSubjectName = "CCTNS NOT LOGIN",
                GroupName = "CCTNS PORTAL"
            });
            model.complaintTypesList.Add(new ComplaintTypeSubject()
            {
                id = 3,
                ComplainTypeSubjectName = "CCTNS PAGE NOT OPEN",
                GroupName = "CCTNS PORTAL"
            });
            model.complaintTypesList.Add(new ComplaintTypeSubject()
            {
                id = 4,
                ComplainTypeSubjectName = "CCTNS PAGE NOT WORKING",
                GroupName = "CCTNS PORTAL"
            });
            model.complaintTypesList.Add(new ComplaintTypeSubject()
            {
                id = 5,
                ComplainTypeSubjectName = "CCTNS PAGE OPEN BUT DATA NOT ADDED",
                GroupName = "CCTNS PORTAL"
            });
            model.complaintTypesList.Add(new ComplaintTypeSubject()
            {
                id = 6,
                ComplainTypeSubjectName = "INTERNET NOT WORKING",
                GroupName = "INTERNET"
            });
            model.complaintTypesList.Add(new ComplaintTypeSubject()
            {
                id = 7,
                ComplainTypeSubjectName = "INTERNET WORKING BUT LATE WORKING",
                GroupName = "INTERNET"
            });
            model.complaintTypesList.Add(new ComplaintTypeSubject()
            {
                id = 8,
                ComplainTypeSubjectName = "ASSAM COPS PAGE NOT WORKING",
                GroupName = "ASSAM COPS"
            });
            model.complaintTypesList.Add(new ComplaintTypeSubject()
            {
                id = 9,
                ComplainTypeSubjectName = "ASSAM COPS NOT WORKING",
                GroupName = "ASSAM COPS"
            });
            model.complaintTypesList.Add(new ComplaintTypeSubject()
            {
                id = 10,
                ComplainTypeSubjectName = "ASSAM COPS WORKING BUT PAGE NOT WORKING",
                GroupName = "ASSAM COPS"
            });
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Complaint(ComplaintModel complaint)
        {
            string con = _configuration.GetConnectionString("app");
            string GroupName = string.Empty;
            if (complaint.subject.ToLower().Contains("cctns"))
            {
                GroupName = "CCTNS";
            }
            else if (complaint.subject.ToLower().Contains("internet"))
            {
                GroupName = "INTERNET";
            }
            else if (complaint.subject.ToLower().Contains("assam"))
            {
                GroupName = "ASSAM";
            }
            var model = new ComplaintModel();
            using (var conection = new SqlConnection(con))
            {
                var param = new DynamicParameters();
                param.Add("Type", 2);
                param.Add("GroupName", GroupName);
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                model = await conection.QueryFirstOrDefaultAsync<ComplaintModel>("USP_MailTemplate", param: param, commandType: CommandType.StoredProcedure);
            }
            if (model is not null)
            {
                string[] strings = complaint.subject.Split(',');
                model.MailSubject = model.MailSubject.Replace("{date}", DateTime.Now.ToString("dd/MM/yyyy"));
                string li = string.Empty;
                for (int i = 0; i < strings.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strings[i]) && !string.IsNullOrWhiteSpace(strings[i]))
                    {
                        li += $"<li>{strings[i]}</li>";
                    }
                }
                string list = $"<ol>{li}</ol>";
                model.MailBody = model.MailBody.Replace("{list}", list);
                model.MailBody = model.MailBody.Replace("{fromtime}", complaint.IssueFrom.ToString("HH:mm:ss tt"));
                model.MailBody = model.MailBody.Replace("{fromdate}", complaint.IssueFrom.ToString("dd/MM/yyyy"));
                model.MailBody = model.MailBody.Replace("{nowtime}", complaint.IssueTo.ToString("HH:mm:ss tt"));
                model.MailBody = model.MailBody.Replace("{nowdate}", complaint.IssueTo.ToString("dd/MM/yyyy"));
                model.MailTo = complaint.MailTo;
                model.MailFrom = complaint.MailFrom;
                model.subject = "showpopup";
                using (var conection = new SqlConnection(con))
                {
                    var param = new DynamicParameters();
                    param.Add("to", complaint.MailTo);
                    param.Add("from", complaint.MailFrom);
                    param.Add("Subject", complaint.subject);
                    param.Add("IssueTo", complaint.IssueTo);
                    param.Add("IssueFrom", complaint.IssueFrom);
                    param.Add("body", model.MailBody);
                    if (conection.State == ConnectionState.Closed)
                    {
                        await conection.OpenAsync();
                    }
                    await conection.ExecuteAsync("USP_MailLog", param: param, commandType: CommandType.StoredProcedure);
                }
                return View(model);
            }
            else
            {
                return RedirectToAction(nameof(MailLogs));
            }
        }
        public async Task<IActionResult> MailLogs()
        {
            IEnumerable<ComplaintModel> complaintModels = new List<ComplaintModel>();
            string con = _configuration.GetConnectionString("app");
            using (var conection = new SqlConnection(con))
            {
                if (conection.State == ConnectionState.Closed)
                {
                    await conection.OpenAsync();
                }
                complaintModels = await conection.QueryAsync<ComplaintModel>("USP_GetMailLogs", commandType: CommandType.StoredProcedure);
            }
            return View(complaintModels);
        }
    }
}
