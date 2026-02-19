using GSTSMSLibrary.Secretary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GSTSMS.Controllers
{
    public class SecretaryController : Controller
    {
        BALSecretary obj = new BALSecretary();

    
        public ActionResult Index()
        {
            return View();
        }



        ///Its show list of survey question

        public async Task<ActionResult> ShowList()
        {
            DataSet ds = await obj.GetUserList();
            List<Secretary> lstUserDtl1 = new List<Secretary>();


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                lstUserDtl1.Add(new Secretary
                {
                    SrNo = Convert.ToInt32(row["SrNo"]),
                    Title = row["Title"].ToString(),
                    EndDate = row["EndDate"].ToString(),
                    Status = row["Status"].ToString(),
                    TotalResponses = row["TotalResponse"].ToString()


                });
            }
            /// here we want to need to display result also 

            Secretary obj2 = new Secretary { lstUserDtl = lstUserDtl1 };
            return View(obj2);
        }
















        // Its show list of poll  question 
        [HttpGet]
        public async Task<ActionResult> ShowListpoll()
        {
            DataSet ds = await obj.GetpollList(); 
            List<Secretary> poll = new List<Secretary>();

          
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                poll.Add(new Secretary
                {
                    SrNo = Convert.ToInt32(row["SrNo"]),
                    Title = row["Title"].ToString(),
                    EndDate = row["EndDate"].ToString(),
                    Status = row["Status"].ToString(),
                    TotalResponses = row["TotalResponse"].ToString()
                });
            }
            /// here we want to need to display result also 

            Secretary model = new Secretary { poll = poll };
            return PartialView("ShowListpoll", model);
        }







        // its used to open LoadSurveyform partial view 


        [HttpGet]
        public async Task<ActionResult> LoadSurveyform()
        {
            var model = new Secretary(); 
            DataTable dt = await obj.GetSubTypes();

            //  SubTypeList from the DataTable
            model.SubTypeList = dt.AsEnumerable()
                .Select(row => new Secretary
                {
                    SubTypeId = Convert.ToInt32(row["SubTypeId"]), // Replace column name with actual ID column
                    SubType = row["SubType"].ToString()    // Replace column name with actual Name column
                })
                .ToList();

            return PartialView("LoadSurveyform", model); // Pass the model to the view
        }





        





        // here we insert the conduct survey question and option 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> InsertSurveyQuestion(Secretary model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // here we take pervioues last code of survey and incress by 1 
                    BALSecretary obj = new BALSecretary();
                    DataSet ds = await obj.GetQuestionCode();
                    string MeetingCode = ds.Tables[0].Rows[0]["QuestionCode"].ToString();
                    int next = int.Parse(MeetingCode.Substring(1)) + 1;
                    string questionCode = "S" + next.ToString("D3");

                    await obj.InsertSurveyQuestion(model, questionCode); // go to method 

                    return Json(new { success = true, message = "Survey created successfully!" });
                }

                return Json(new { success = false, message = "Validation failed. Please check the input fields." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        // we load poll from conduct poll from here 
        [HttpGet]
        public async Task<ActionResult> LoadpollForm()
        {
            var model = new Secretary(); 


            
            DataTable dt = await obj.GetSubTypes(); 

            
            model.SubTypeList = dt.AsEnumerable()
                .Select(row => new Secretary
                {
                    SubTypeId = Convert.ToInt32(row["SubTypeId"]), 
                    SubType = row["SubType"].ToString()    
                })
                .ToList();// its pass to view 

            return PartialView("LoadpollForm", model); 
        }





        // here we insert the conduct poll question and option 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> InsertPollQuestion(Secretary model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    BALSecretary obj = new BALSecretary();
                    DataSet ds = await obj.GetQuestionCodepoll();
                    string MeetingCode = ds.Tables[0].Rows[0]["QuestionCode"].ToString();
                    int next = int.Parse(MeetingCode.Substring(1)) + 1;
                    string questionCode = "P" + next.ToString("D3");

                    await obj.InsertpollQuestion(model, questionCode);

                    return Json(new { success = true, message = "Poll created successfully!" });
                }

                return Json(new { success = false, message = "Validation failed. Please check the input fields." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

























    }
}
