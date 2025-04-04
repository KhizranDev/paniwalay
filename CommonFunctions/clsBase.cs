using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebTech.Common
{
    public class clsBase
    {
    }

    public enum QueryExecutionState
    {
        First,
        Last,
        AfterMaster,
        BeforeDetail,
        AfterDetail,
        Output
    }

    public enum TrailingUserType
    {
        SelfOnly,
        CrossOnly,
        All,
        None
    }

    public enum RecordUpdation
    {
        Insert,
        Update,
        Delele,
        Cancel
    }

    [Serializable]
    public class TrailHandling
    {
        public bool CreateTrail { get; set; }
        public bool UseServerDate { get; set; }
        public DateTime SystemDate { get; set; }
        public bool DelRecordPhaysically { get; set; }

        public List<string> CancelStatus { get; set; }
        public List<string> EditStatus { get; set; }
        public TrailingUserType RecallMode { get; set; }
        public TrailingUserType AuthorizationMode { get; set; }
        public TrailingUserType CancelAuthMode { get; set; }
        public TrailingUserType RejectAuthMode { get; set; }
        public TrailingUserType RejectCancelAuthMode { get; set; }
        public long UserID { get; set; }
        public bool UseIdKey { get; set; }

        public TrailHandling()
        {
            this.CreateTrail = true;
        }
    }


    public class lParameters
    {
        public string ParameterName { get; set; }
        public string Value { get; set; }
        public bool hasNull { get; set; }
    }

    public class lQueries
    {
        public string Query { get; set; }
        public QueryExecutionState ExecutionState { get; set; }
    }

    public class Queries
    {
        public string Query { get; set; }
        public QueryExecutionState ExecutionState { get; set; }
    }

    public class MasterDetailRelation
    {
        public MasterDetailRelation()
        {

        }

        public MasterDetailRelation(string masterFld, string detailFld)
        {
            this.MasterFld = masterFld;
            this.DetailFld = detailFld;
        }

        public string MasterFld { get; set; }
        public string DetailFld { get; set; }
    }
}