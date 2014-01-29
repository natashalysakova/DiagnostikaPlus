using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Resources;
using adovipavto.Classes;
using adovipavto.Enums;
using adovipavto.Properties;

namespace adovipavto
{
    public partial class VipAvtoSet
    {
        //partial class MesuresDataTable
        //{
        //}

        ResourceManager rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

        private Operator _currentOperator;

        #region NormativesMetods

        public void AddNormative(string group, string title, double minValue, double maxValue)
        {
            DataRow r = Tables[Constants.NormativesTableName].NewRow();

            r["Tag"] = new Normatives().GetNormativeIndex(title);
            r["MaxValue"] = maxValue;
            r["MinValue"] = minValue;


            r["IDGroup"] = GetGroupId(group);

            Tables[Constants.NormativesTableName].Rows.Add(r);
            Tables[Constants.NormativesTableName].AcceptChanges();
            Tables[Constants.NormativesTableName].WriteXml(Constants.GetFullPath(Settings.Default.Normatives));
        }

        public void EditNormative(int id, string group, string title, double minValue, double maxValue)
        {
            DataRow r = GetRowById(Constants.NormativesTableName, id);

            r["Tag"] = new Normatives().GetNormativeIndex(title);
            r["MaxValue"] = maxValue;
            r["MinValue"] = minValue;


            r["IDGroup"] = GetGroupId(group);

            Tables[Constants.NormativesTableName].AcceptChanges();
            Tables[Constants.NormativesTableName].WriteXml(Constants.GetFullPath(Settings.Default.Normatives));
        }

        public DataRow[] GetNormativesFromGroup(string groupTitle)
        {
            var groupId = (int)(
                from DataRow items
                    in Tables[Constants.GroupTableName].Rows
                where Program.VipAvtoDataSet.CreateGroupTitle((int)items["GroupID"]) == groupTitle
                select items["GroupID"]).ToList()[0];

            DataRow[] group = Tables[Constants.GroupTableName].Select(string.Format("GroupID = {0}", groupId));
            DataRow[] norms = group[0].GetChildRows(Relations["FK_CarGroup_Normatives"]);

            return norms;
        }

        public int GetNormativeTag(string title)
        {
            var norm = new Normatives();

            for (int i = 0; i < norm.NormativesTitle.Length; i++)
            {
                string s = norm.NormativesTitle[i];
                if (s == title)
                {
                    return i;
                }
            }


            return -1;
        }

        #endregion

        #region GroupMetods

        public List<string> GetGroupList()
        {
            return (from DataRow item in Tables[Constants.GroupTableName].Rows select item["Title"].ToString())
                .ToList();
        }

        public void AddGroup(int year, string categoty, int engine, bool before)
        {
            DataRow r = Tables[Constants.GroupTableName].NewRow();
            r["Year"] = year;
            r["Category"] = categoty;
            r["EngineType"] = engine;
            r["Before"] = before;

            Tables[Constants.GroupTableName].Rows.Add(r);
            Tables[Constants.GroupTableName].AcceptChanges();
            Tables[Constants.GroupTableName].WriteXml(Constants.GetFullPath(Settings.Default.Groups));
        }

        public void EditGroup(int id, int year, string categoty, int engine, bool before)
        {
            DataRow r = GetRowById(Constants.GroupTableName, id);
            r["Year"] = year;
            r["Category"] = categoty;
            r["EngineType"] = engine;
            r["Before"] = before;

            Tables[Constants.GroupTableName].AcceptChanges();
            Tables[Constants.GroupTableName].WriteXml(Constants.GetFullPath(Settings.Default.Groups));
        }

        public int GetGroupId(string title)
        {
            string[] splitTitle = title.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach (DataRow row in Tables[Constants.GroupTableName].Rows)
            {
                string cat = row["Category"].ToString();
                string bef = (bool)row["Before"] ? rm.GetString("before") : rm.GetString("after");
                string year = row["Year"].ToString();
                int engine = (int)row["EngineType"];

                if (cat == splitTitle[0] && bef == splitTitle[1] && year == splitTitle[2] && new Engines().EnginesTitle[engine] == splitTitle[3] )
                    return Convert.ToInt32(row["GroupID"]);
            }

            return -1;
        }

        public string CreateGroupTitle(int id)
        {
            DataRow groupRow = GetRowById(Constants.GroupTableName, id);

            string s = (bool) groupRow["Before"] ? rm.GetString("before") : rm.GetString("after");


            return groupRow["Category"] + " " + s + " " + groupRow["Year"] + " " + new Engines().EnginesTitle[(int)groupRow["EngineType"]];
        }

        #endregion

        #region UniversalMethods

        internal DataRow GetRowByIndex(string tableName, int rowIndex)
        {
            return Tables[tableName].Rows[rowIndex];
        }

        internal DataRow GetRowById(string tableName, int rowId)
        {
            string idheader = Program.VipAvtoDataSet.Tables[tableName].Columns[0].ColumnName;
            return Program.VipAvtoDataSet.Tables[tableName].Rows.Cast<DataRow>().FirstOrDefault(item => Convert.ToInt32(item[idheader]) == rowId);
        }

        internal void RemoveRow(string tableName, DataRow selectedRow)
        {
            Tables[tableName].Rows.Remove(selectedRow);
            Tables[tableName].AcceptChanges();
            Tables[tableName].WriteXml(GetFileNameByTableName(tableName));
        }

        internal void RemoveRowById(string tableName, int id)
        {
            for (int i = 0; i < Tables[tableName].Rows.Count; i++)
            {
                if ((int)Tables[tableName].Rows[i][0] == id)
                {
                    Tables[tableName].Rows.RemoveAt(i);
                    break;
                }
            }

            Tables[tableName].AcceptChanges();
            Tables[tableName].WriteXml(GetFileNameByTableName(tableName));

        }

        #endregion

        #region LogMethods

        internal int AddEnterLog(int id)
        {
            DataRow r = Program.VipAvtoDataSet.Tables[Constants.LogsTableName].NewRow();
            r["Type"] = "Enter";
            r["DateTime"] = DateTime.Now;
            r["IDOperator"] = id;
            Tables[Constants.LogsTableName].Rows.Add(r);
            AcceptChanges();
            Tables[Constants.LogsTableName].WriteXml(Constants.GetFullPath(Settings.Default.Logs));

            return (int) r["LogItemID"];
        }

        #endregion

        #region OperatorMethods

        internal void CreateAdministratorUser()
        {
            DataRow admin = Tables[Constants.OperatorsTableName].NewRow();
            admin["Name"] = "admin";
            admin["LastName"] = "admin";
            admin["Login"] = "admin";
            admin["Password"] = "admin";
            admin["Right"] = Rights.Administrator;
            Tables[Constants.OperatorsTableName].Rows.Add(admin);
            AcceptChanges();
            WriteXml(Constants.GetFullPath(Settings.Default.Operators));
        }

        internal string GetUserPasswors(string username)
        {
            string filter = "Login = '" + username + "'";

            DataRow[] operatorRow = Tables[Constants.OperatorsTableName].Select(filter);
            if (operatorRow.Length != 0)
            {
                string name = operatorRow[0]["Name"].ToString();
                string lastname = operatorRow[0]["LastName"].ToString();
                int id = Convert.ToInt32(operatorRow[0]["OperatorId"]);
                var rights = (Rights)operatorRow[0]["Right"];
                return operatorRow[0]["Password"].ToString();
            }

            return "";
        }

        #endregion



        private string GetFileNameByTableName(string tableName)
        {
            switch (tableName)
            {
                case Constants.GroupTableName:
                    return Constants.GetFullPath(Settings.Default.Groups);
                case Constants.LogsTableName:
                    return Constants.GetFullPath(Settings.Default.Logs);
                case Constants.MechanicsTableName:
                    return Constants.GetFullPath(Settings.Default.Mechanics);
                case Constants.NormativesTableName:
                    return Constants.GetFullPath(Settings.Default.Normatives);
                case Constants.OperatorsTableName:
                    return Constants.GetFullPath(Settings.Default.Operators);
                case Constants.ProtocolsTableName:
                    return Constants.GetFullPath(Settings.Default.Protocols);
                case Constants.MesuresTableName:
                    return Constants.GetFullPath(Settings.Default.Mesure);
            }
            return "Error.txt";
        }

        internal void LockOperator(int id)
        {
            DataRow r = GetRowById(Constants.OperatorsTableName, id);
            r["Right"] = Rights.Locked;

            Tables[Constants.OperatorsTableName].AcceptChanges();
            Tables[Constants.OperatorsTableName].WriteXml(
                Constants.GetFullPath(Settings.Default.Operators));

        }

        internal void AddOperator(string p1, string p2, string p3, string p4, string p5)
        {
            DataRow row = Tables[Constants.OperatorsTableName].NewRow();

            row["Name"] = p1;
            row["LastName"] = p2;
            row["Login"] = p3;
            row["Password"] = p4;

            row["Right"] = GetRightByString(p5);



            Tables[Constants.OperatorsTableName].Rows.Add(row);
            Tables[Constants.OperatorsTableName].AcceptChanges();
            Tables[Constants.OperatorsTableName].WriteXml(Constants.GetFullPath(Settings.Default.Operators));

        }

        private Rights GetRightByString(string p5)
        {
            Rights r;
            Enum.TryParse(p5, false, out r);
            return r;
        }

        internal void EditOperator(int id, string name, string lastName, string login, string pass)
        {
            DataRow r = GetRowById(Constants.OperatorsTableName, id);
            r["Name"] = name;
            r["LastName"] = lastName;
            r["Login"] = login;
            r["Password"] = pass;

            Tables[Constants.OperatorsTableName].AcceptChanges();
            Tables[Constants.OperatorsTableName].WriteXml(Constants.GetFullPath(Settings.Default.Operators));

        }

        internal void AddMechanic(string p1, string p2, string p3)
        {
            DataRow r = Tables[Constants.MechanicsTableName].NewRow();

            r["Name"] = p1;
            r["LastName"] = p2;
            r["FatherName"] = p3;

            r["State"] = (int)State.Employed;

            Tables[Constants.MechanicsTableName].Rows.Add(r);
            Tables[Constants.MechanicsTableName].AcceptChanges();
            Tables[Constants.MechanicsTableName].WriteXml(Constants.GetFullPath(Settings.Default.Mechanics));

        }


        internal void EditMechanic(int id, string p1, string p2, string p3)
        {
            DataRow r = GetRowById(Constants.MechanicsTableName, id);

            r["Name"] = p1;
            r["LastName"] = p2;
            r["FatherName"] = p3;

            Tables[Constants.MechanicsTableName].AcceptChanges();
            Tables[Constants.MechanicsTableName].WriteXml(Constants.GetFullPath(Settings.Default.Mechanics));

        }

        internal void LockMechanic(int id)
        {
            DataRow r = GetRowById(Constants.MechanicsTableName, id);
            r["State"] = (int)State.Unemployed;

            Tables[Constants.MechanicsTableName].AcceptChanges();
            Tables[Constants.MechanicsTableName].WriteXml(
                Constants.GetFullPath(Settings.Default.Mechanics));

        }


        internal int  AddProtocol(string p1, string p2, DateTime dateTime, string photo, string techpass, string p3, bool result, DateTime nexDateTime, bool visChck)
        {
            DataRow r = Tables[Constants.ProtocolsTableName].NewRow();

            r["BlankNumber"] = p1;
            r["IDOperator"] = _currentOperator.Id;
            r["IDMechanic"] = GetMechanicIdByShortName(p2);
            r["Date"] = dateTime;

            if (photo != "")
                r["CarPhoto"] = photo;
            if (techpass != "")
                r["TechPhoto"] = techpass;

            r["IDGroup"] = GetGroupId(p3);
            r["Result"] = result;
            r["NextData"] = nexDateTime.Date;
            r["VisualCheck"] = visChck;

            Tables[Constants.ProtocolsTableName].Rows.Add(r);
            Tables[Constants.ProtocolsTableName].AcceptChanges();
            Tables[Constants.ProtocolsTableName].WriteXml(Constants.GetFullPath(Settings.Default.Protocols));

            //return (from DataRow item in Tables[Constants.ProtocolsTableName].Rows
            //        where item["BlankNumber"].ToString() == p1 && (DateTime)item["Date"] == dateTime
            //        select (int)item["ProtocolID"]).ToArray()[0];
            return (int)r["ProtocolID"];
        }

        private int GetMechanicIdByShortName(string p2)
        {
            return (from DataRow item in Tables[Constants.MechanicsTableName].Rows
                where
                    GetShortMechanicName((int)item["MechanicID"]) == p2
                select (int)item["MechanicID"]).ToArray()[0];
        }

        internal void SetCurrentOperator(string name)
        {
            DataRow r = GetUserByLogin(name);

            _currentOperator = new Operator((Rights)r["Right"], (int)r["OperatorId"], r["Name"].ToString(), r["LastName"].ToString(), AddEnterLog((int)r["OperatorId"]));


        }

        private DataRow GetUserByLogin(string name)
        {
            return
                (from DataRow r in Tables[Constants.OperatorsTableName].Rows where r["Login"].ToString() == name select r).ToArray()
                    [0];
        }

        internal void AddMesure(int p1, double p2, int newProtocolId)
        {
            DataRow item = Tables[Constants.MesuresTableName].NewRow();

            item["NormativeID"] = p1;
            item["Value"] = p2;
            item["IDProtocol"] = newProtocolId;

            Tables[Constants.MesuresTableName].Rows.Add(item);
            Tables[Constants.MesuresTableName].AcceptChanges();
            Tables[Constants.MesuresTableName].WriteXml(Constants.GetFullPath(Settings.Default.Mesure));

        }

        internal string GetShortMechanicName(int p)
        {
            return (from DataRow item in Tables[Constants.MechanicsTableName].Rows
                where (int) item["MechanicID"] == p
                select
                    item["LastName"] + " " + item["Name"].ToString()[0] + ". " + item["FatherName"].ToString()[0] + ".")
                .ToArray()[0];
        }

        internal string GetShortOperatorName(int p)
        {
            return (from DataRow item in Tables[Constants.OperatorsTableName].Rows
                    where (int)item["OperatorId"] == p
                    select
                        item["LastName"] + " " + item["Name"].ToString()[0] + ".")
                .ToArray()[0];
        }

        internal DataRow[] GetMesuresFromProtocol(int newProtocolId)
        {
            return (from DataRow item in Tables[Constants.MesuresTableName].Rows
                where (int) item["IDProtocol"] == newProtocolId
                select item).ToArray();

        }

        internal void OperatorExit()
        {
            DataRow r = GetRowById(Constants.LogsTableName, _currentOperator.SessionId);

            r["ExitDateTime"] = DateTime.Now;
            
            Tables[Constants.LogsTableName].AcceptChanges();
            Tables[Constants.LogsTableName].WriteXml(Settings.Default.Logs);
        }

        internal bool GroupContainsNormative(string groupname, string normativename)
        {
                        int groupId = GetGroupId(groupname);

            int normtag = GetNormativeTag(normativename);

            var mesures = (from DataRow item in Tables[Constants.NormativesTableName].Rows
                           where (int)item["Tag"] == normtag && (int)item["IDGroup"] == groupId
                           select item).ToList();

            if (mesures.Count == 0)
                return false;

            return true;
        }
    }
}