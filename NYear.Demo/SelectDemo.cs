﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NYear.ODA.Cmd;
using NYear.ODA.Model;
using NYear.ODA;
using System.Data;

namespace NYear.Demo
{
    public class SelectDemo
    {
        [Demo(Demo = FuncType.Select, MethodName = "DataTypeTest", MethodDescript = "DataTypeTest")]
        public static object DataTypeTest()
        {
            ODAContext ctx = new ODAContext();
            CmdTestBatchImport t = ctx.GetCmd<CmdTestBatchImport>();
            var rlt = t.SelectFirst<string,decimal,string>(t.ColId, t.ColNum, t.ColTest);
            return rlt;
        }

        [Demo(Demo = FuncType.Select, MethodName = "SelectM", MethodDescript = "SelectM")]
        public static object SelectM()
        {
            ODAContext ctx = new ODAContext();
            CmdPrmRole pr = ctx.GetCmd<CmdPrmRole>();
            List<PRM_ROLE> rlt = pr.Where(pr.ColRoleName == "Administrator").SelectM();
            return rlt;
        }
        [Demo(Demo = FuncType.Select, MethodName = "Join", MethodDescript = "SelectJoin")]
        public static object Join()
        {
            ODAContext ctx = new ODAContext(); 
            CmdPrmRole pr = ctx.GetCmd<CmdPrmRole>();
            CmdPrmRoleAuthorize pra = ctx.GetCmd<CmdPrmRoleAuthorize>();

            List<PRM_ROLE> rlt = pr.InnerJoin(pra, pr.ColRoleName == pra.ColRoleName)
                .Where(pra.ColIsForbidden == "Y", pra.ColResourceName == "resource")
                .Select<PRM_ROLE>(pr.ColRoleName, pr.ColIsSupperAdmin, pr.ColDescript);
            return rlt; 
        }
        [Demo(Demo = FuncType.Select, MethodName = "MultiJoin", MethodDescript = "MultiJoin")]
        public static object MultiJoin()
        {
            ODAContext ctx = new ODAContext();
            
            CmdPrmRole pr = ctx.GetCmd<CmdPrmRole>();
            CmdPrmRoleAuthorize pra = ctx.GetCmd<CmdPrmRoleAuthorize>();

            CmdPrmUserRole ur = ctx.GetCmd<CmdPrmUserRole>();
            CmdPrmPermission p = ctx.GetCmd<CmdPrmPermission>();
            CmdVPrmUserAuthorize ua = ctx.GetCmd<CmdVPrmUserAuthorize>(); 
            List<PRM_ROLE> rlt = pr.InnerJoin(pra, pr.ColRoleName == pra.ColRoleName)
                .InnerJoin(ur, pr.ColRoleName == ur.ColRoleName)
                .InnerJoin(ua, ur.ColUserId == ua.ColUserId)
                .InnerJoin(p, ((p.ColResourceName == pra.ColResourceName).And(p.ColOperateName == pra.ColOperateName)).Or((ua.ColResourceName == p.ColResourceName).And(ua.ColOperateName == p.ColOperateName)))
                .Where(pra.ColIsForbidden == "Y", pra.ColResourceName == "resource")
                .Select<PRM_ROLE>(pr.ColRoleName, pr.ColIsSupperAdmin, pr.ColDescript);
            return rlt;
        }

        [Demo(Demo = FuncType.Select, MethodName = "ToModel", MethodDescript = "SelectToModel")]
        public static object ToModel()
        {
            ODAContext ctx = new ODAContext(); 
            CmdPrmRole pr = ctx.GetCmd<CmdPrmRole>();
            CmdPrmRoleAuthorize pra = ctx.GetCmd<CmdPrmRoleAuthorize>(); 
            List<PRM_ROLE> rlt = pr
                .InnerJoin(pra, pr.ColRoleName == pra.ColRoleName)
                .Where(pra.ColIsForbidden == "Y", pra.ColResourceName == "resource")
                .Select<PRM_ROLE>(pr.ColRoleName, pr.ColIsSupperAdmin, pr.ColDescript);
            return rlt;
        }

        [Demo(Demo = FuncType.Select, MethodName = "Function", MethodDescript = " Function")]
        public static object Function()
        {
            ODAContext ctx = new ODAContext(); 
            CmdPrmRole pr = ctx.GetCmd<CmdPrmRole>();
            var rlt = pr.Where(pr.ColRoleName == "Administrator")
                .Select(pr.ColDescript.Upper, pr.ColIsSupperAdmin.Ascii, pr.Function.CreateFunc("myFunction", ODAdbType.OVarchar, pr.ColRoleName, pr.ColDescript, "param0", 1));

            return rlt;
        }

        [Demo(Demo = FuncType.Select, MethodName = "Where Exists", MethodDescript = "Where Exists")]
        public static object WhereExists()
        {
            ODAContext ctx = new ODAContext(); 
            CmdPrmRole pr = ctx.GetCmd<CmdPrmRole>();
            CmdPrmRoleAuthorize ra = ctx.GetCmd<CmdPrmRoleAuthorize>();
            ra.Where(ra.ColIsForbidden == "Y", pr.ColRoleName == ra.ColRoleName);

            var rlt = pr.Where(
                  pr.ColRoleName == "Administrator",
                  pr.Function.Exists(ra, ra.Function.VisualColumn("1", ODAdbType.OInt))
                  )
                  .Select(pr.ColDescript, pr.ColIsSupperAdmin.Ascii); 
            return rlt;
        }
        [Demo(Demo = FuncType.Select, MethodName = "Where IN", MethodDescript = "Where IN")]
        public static object WhereIN( )
        {
            ODAContext ctx = new ODAContext(); 
            CmdPrmRole pr = ctx.GetCmd<CmdPrmRole>();
            CmdPrmRoleAuthorize ra = ctx.GetCmd<CmdPrmRoleAuthorize>();
            ra.Where(ra.ColIsForbidden == "Y", pr.ColRoleName == ra.ColRoleName);

            var rlt = pr.Where(
                   pr.ColRoleName == "Administrator",
                   pr.ColRoleName.In(ra, ra.Function.VisualColumn("1", ODAdbType.OInt))
                   )
                   .Select(pr.ColDescript, pr.ColIsSupperAdmin.Ascii);
            return rlt;
        }
        [Demo(Demo = FuncType.Select, MethodName = "Distinct", MethodDescript = "Distinct")]
        public static object Distinct()
        {
            ODAContext ctx = new ODAContext(); 
            CmdPrmRole pr = ctx.GetCmd<CmdPrmRole>();
            CmdPrmRoleAuthorize pra = ctx.GetCmd<CmdPrmRoleAuthorize>(); 
            int TotalRows = 0;
            List<PRM_ROLE> rlt = pr
                .InnerJoin(pra, pr.ColRoleName == pra.ColRoleName)
                .Where(pra.ColIsForbidden == "Y", pra.ColResourceName == "resource")
                .Distinct.Select<PRM_ROLE>(0, int.MaxValue, out TotalRows, pr.ColRoleName, pr.ColIsSupperAdmin, pr.ColDescript); 
            return rlt;
        }

        [Demo(Demo = FuncType.Select, MethodName = "SelectFirst", MethodDescript = "Distinct")]
        public static object SelectFirst()
        {
            ODAContext ctx = new ODAContext();
            CmdPrmRole pr = ctx.GetCmd<CmdPrmRole>();
            CmdPrmRoleAuthorize pra = ctx.GetCmd<CmdPrmRoleAuthorize>();
            var rlt = pr
                .InnerJoin(pra, pr.ColRoleName == pra.ColRoleName)
                .Where(pra.ColIsForbidden == "Y", pra.ColResourceName == "resource")
                .Distinct.SelectFirst<string,string,string>( pr.ColRoleName, pr.ColIsSupperAdmin, pr.ColDescript); 
            return rlt; 
        }

        [Demo(Demo = FuncType.Select, MethodName = "whereOr", MethodDescript = "whereOr")]
        public static object whereOr()
        {
            return null;
        }
  

    }

    
}
