using GSTSMSHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTSMSLibrary.Secretary
{
    public class BALSecretary
    {
        MSSQL cls = new MSSQL();

          // its used to fetch survey from survey table
        public async Task<DataSet> GetUserList()
        {
            Dictionary<string, String> Getdata = new Dictionary<string, String>();
            Getdata.Add("@Flag", "ShowSuveryList");
            DataSet ds = await cls.ExecuteStoreProcedureReturnDS("Secretary", Getdata);
            return ds;
        }


        // its used to fetch poll  from survey table

        public async Task<DataSet> GetpollList()
        {
            Dictionary<string, String> Getdata = new Dictionary<string, String>();
            Getdata.Add("@Flag", "ShowPollList");
            DataSet ds = await cls.ExecuteStoreProcedureReturnDS("Secretary", Getdata);
            return ds;
        }




        // its fetch subtype from GSTtblSubType
        public async Task<DataTable> GetSubTypes()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("@Flag", "ShowType");

            DataSet ds = await cls.ExecuteStoreProcedureReturnDS("Secretary", data);
            return ds.Tables[0];
        }


        // here we fecth last code by query  for survey 
        public async Task<DataSet> GetQuestionCode()
        {
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("@Flag", "GenerateQuestionCode");
            MSSQL db = new MSSQL();
            DataSet ds = await db.ExecuteStoreProcedureReturnDS("Secretary", para);
            return ds;
        }
        // here we fecth last code by query  for Poll 
        public async Task<DataSet> GetQuestionCodepoll()
        {
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("@Flag", "GenerateQuestionCodepoll");
            MSSQL db = new MSSQL();
            DataSet ds = await db.ExecuteStoreProcedureReturnDS("Secretary", para);
            return ds;
        }







        // here we insert the the survey question and option to database
        public async Task<bool> InsertSurveyQuestion(Secretary obj, string questionCode)
        {
            foreach (var question in obj.Questions)
            {
                
                Dictionary<string, string> Data = new Dictionary<string, string>();
                Data.Add("@Flag", "SurveyQuestion");
                Data.Add("@QuestionCode", questionCode);
                Data.Add("@SubTypeId", obj.SubTypeId.ToString());
                Data.Add("@question_Text", question.Text);
                Data.Add("@Start_Date", obj.StartDate);
                Data.Add("@Dedline_Date", obj.DeadlineDate);
                Data.Add("@CreatedBy", "STF001");// hardcore 

               
                object result = await cls.ExecuteStoreProcedureReturnObj("Secretary", Data);
                int questionId = Convert.ToInt32(result);  

         
                if (question.Options != null)
                {
                    foreach (var optionText in question.Options)
                    {
                        Dictionary<string, string> optionData = new Dictionary<string, string>();
                        optionData.Add("@Flag", "SurveyOption");
                        optionData.Add("@Question_Id", questionId.ToString());
                        optionData.Add("@Option_Text", optionText);

                        await cls.ExecuteStoreProcedure("Secretary", optionData);
                    }
                }
            }

            return true;
        }




        // here we insert the the poll question and option to database
        public async Task<bool> InsertpollQuestion(Secretary obj, string questionCode)
        {
            foreach (var question in obj.Questions)
            {
   
                Dictionary<string, string> Data = new Dictionary<string, string>();
                Data.Add("@Flag", "SurveyQuestion");
                Data.Add("@QuestionCode", questionCode);
                Data.Add("@SubTypeId", obj.SubTypeId.ToString());
                Data.Add("@question_Text", question.Text);
                Data.Add("@Start_Date", obj.StartDate);
                Data.Add("@Dedline_Date", obj.DeadlineDate);
                Data.Add("@CreatedBy", "STF001");


                object result = await cls.ExecuteStoreProcedureReturnObj("Secretary", Data);
                int questionId = Convert.ToInt32(result);

     
                if (question.Options != null)
                {
                    foreach (var optionText in question.Options)
                    {
                        Dictionary<string, string> optionData = new Dictionary<string, string>();
                        optionData.Add("@Flag", "pollOption");
                        optionData.Add("@Question_Id", questionId.ToString());
                        optionData.Add("@Option_Text", optionText);

                        await cls.ExecuteStoreProcedure("Secretary", optionData);
                    }
                }
            }

            return true;
        }



    }
}
