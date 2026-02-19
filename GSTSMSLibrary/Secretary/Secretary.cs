using System;
using System.Collections.Generic;

namespace GSTSMSLibrary.Secretary
{
    public class Secretary
    {

        public int SrNo { get; set; }
        public string  EndDate { get; set; }
        public int SubTypeId { get; set; }
        public string SubType { get; set; }
        public string DeadlineDate { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        
        public string CreatedBy { get; set; }

        public List<OptionResult> Options { get; set; } // Add this

        public string Status { get; set; }
        public string TotalResponses { get; set; }

        public List<SurveyQuestion> Questions { get; set; } = new List<SurveyQuestion>();

        // Lists used for UI dropdowns or data displays
        public List<Secretary> lstUserDtl { get; set; } = new List<Secretary>();
        public List<Secretary> poll { get; set; } = new List<Secretary>();
        public List<Secretary> SubTypeList { get; set; } = new List<Secretary>();
    }

    public class SurveyQuestion
    {
        public int SrNo { get; set; }
        public string Text { get; set; }
        public string question_Text { get; set; }

    
        public List<string> Options { get; set; } = new List<string>();
    }





    public class OptionResult
    {
        public string OptionName { get; set; }
        public int Votes { get; set; }
    }
}
