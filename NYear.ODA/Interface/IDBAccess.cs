﻿using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;

namespace NYear.ODA
{
    [ServiceKnownType(typeof(DBNull))]
    [ServiceContract]
    public interface IDBAccess
    {
        char ParamsMark { get; }
        DbAType DBAType { get; }
        string ConnString { get; }
        IDbTransaction Transaction { get; set; }
        [OperationContract]
        void BeginTransaction();
        [OperationContract]
        void Commit();
        [OperationContract]
        void RollBack();
        [OperationContract]
        DataSet ExecuteProcedure(string SQL, ODAParameter[] ParamList);
        [OperationContract]
        int ExecuteSQL(string SQL, ODAParameter[] ParamList);
        [OperationContract]
        DateTime GetDBDateTime();
        [OperationContract]
        object GetExpressResult(string ExpressionString);
        [OperationContract]
        long GetSequenceNextVal(string SequenceName);
        [OperationContract]
        DataTable GetTableColumns();
        [OperationContract]
        string[] GetUserTables();
        [OperationContract]
        string[] GetUserViews();
        [OperationContract]
        string[] GetUserProcedure();
        [OperationContract]
        string[] GetPrimarykey(string TableName);
        [OperationContract]
        DataTable GetUniqueIndex(string TableName);
        [OperationContract]
        DatabaseColumnInfo ODAColumnToOrigin(string Name, string ColumnType,int Length);
        [OperationContract]
        DataTable GetViewColumns(); 
        [OperationContract]
        List<T> Select<T>(string SQL, ODAParameter[] ParamList) where T : class;
        [OperationContract]
         List<T> Select<T>(string SQL, ODAParameter[] ParamList, int StartIndex, int MaxRecord,string Orderby) where T : class;
        [OperationContract]
        List<T> Select<T>(string SQL, ODAParameter[] ParamList, string StartWithExpress, string ConnectBy, string Prior, string ConnectColumn, string ConnectChar, int MaxLevel) where T : class;
        [OperationContract(Name = "Select")]
        DataTable Select(string SQL, ODAParameter[] ParamList);
        [OperationContract(Name = "SelectBlock")]
        DataTable Select(string SQL, ODAParameter[] ParamList, int StartIndex, int MaxRecord,string Orderby);
        [OperationContract(Name = "SelectFirst")]
        object[] SelectFirst(string SQL, ODAParameter[] ParamList); 
        [OperationContract(Name = "SelectRecursion")]
        DataTable Select(string SQL, ODAParameter[] ParamList, string StartWithExpress, string ConnectBy, string Prior, string ConnectColumn, string ConnectChar, int MaxLevel);
        [OperationContract]
        DataTable GetUserProcedureArguments(string ProcedureName);
        [OperationContract]
        bool Import(DataTable Data, ODAParameter[] Prms);

      
    }
}
