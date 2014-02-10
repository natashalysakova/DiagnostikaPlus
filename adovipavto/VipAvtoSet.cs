#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Resources;
using adovipavto.Classes;
using adovipavto.Enums;
using adovipavto.Properties;

#endregion

namespace adovipavto
{
    public partial class VipAvtoSet
    {
        private Operator _currentOperator;
        private ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());

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
            var groupId = (int) (
                from DataRow items
                    in Tables[Constants.GroupTableName].Rows
                where Program.VipAvtoDataSet.CreateGroupTitle((int) items["GroupID"]) == groupTitle
                select items["GroupID"]).ToList()[0];

            DataRow[] group = Tables[Constants.GroupTableName].Select(string.Format("GroupID = {0}", groupId));
            DataRow[] norms = group[0].GetChildRows(Relations["FK_CarGroup_Normatives"]);

            return norms;
        }

        public int GetNormativeTag(string title)
        {
            var norm = new Normatives();

            for (int i = 0; i < norm.Count; i++)
            {
                string s = norm[i];
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
                string bef = (bool) row["Before"] ? _rm.GetString("before") : _rm.GetString("after");
                string year = row["Year"].ToString();
                var engine = (int) row["EngineType"];

                if (cat == splitTitle[0] && bef == splitTitle[1] && year == splitTitle[2] &&
                    new Engines()[engine] == splitTitle[3])
                    return Convert.ToInt32(row["GroupID"]);
            }

            return -1;
        }

        public string CreateGroupTitle(int id)
        {
            DataRow groupRow = GetRowById(Constants.GroupTableName, id);

            if (groupRow == null)
                return "";


            string s = (bool) groupRow["Before"] ? _rm.GetString("before") : _rm.GetString("after");
            return groupRow["Category"] + " " + s + " " + groupRow["Year"] + " " +
                   new Engines()[(int) groupRow["EngineType"]];
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
            return
                Program.VipAvtoDataSet.Tables[tableName].Rows.Cast<DataRow>()
                    .FirstOrDefault(item => Convert.ToInt32(item[idheader]) == rowId);
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
                if ((int) Tables[tableName].Rows[i][0] == id)
                {
                    Tables[tableName].Rows.RemoveAt(i);
                    break;
                }
            }

            Tables[tableName].AcceptChanges();
            Tables[tableName].WriteXml(GetFileNameByTableName(tableName));
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
            DataRow[] operatorRow =
                (from DataRow item in Tables[Constants.OperatorsTableName].Rows
                    where item["Login"].ToString() == username
                    select item).ToArray();

            if (operatorRow.Length != 0)
            {
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

        internal void AddOperator(string name, string lastName, string login, string password, string rights)
        {
            DataRow row = Tables[Constants.OperatorsTableName].NewRow();

            row["Name"] = name;
            row["LastName"] = lastName;
            row["Login"] = login;
            row["Password"] = password;

            row["Right"] = GetRightByString(rights);


            Tables[Constants.OperatorsTableName].Rows.Add(row);
            Tables[Constants.OperatorsTableName].AcceptChanges();
            Tables[Constants.OperatorsTableName].WriteXml(Constants.GetFullPath(Settings.Default.Operators));
        }

        private Rights GetRightByString(string rightTitle)
        {
            Rights r;
            switch (rightTitle)
            {
                case "Администратор":
                    r = Rights.Administrator;
                    break;
                case "Оператор":
                    r = Rights.Operator;
                    break;
                case "Уволен":
                    r = Rights.Locked;
                    break;
                default:
                    r = Rights.Operator;
                    break;
            }

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

        internal void AddMechanic(string name, string lastName, string fatherName)
        {
            DataRow r = Tables[Constants.MechanicsTableName].NewRow();

            r["Name"] = name;
            r["LastName"] = lastName;
            r["FatherName"] = fatherName;

            r["State"] = (int) State.Employed;

            Tables[Constants.MechanicsTableName].Rows.Add(r);
            Tables[Constants.MechanicsTableName].AcceptChanges();
            Tables[Constants.MechanicsTableName].WriteXml(Constants.GetFullPath(Settings.Default.Mechanics));
        }


        internal void EditMechanic(int id, string name, string lastName, string fatherName)
        {
            DataRow r = GetRowById(Constants.MechanicsTableName, id);

            r["Name"] = name;
            r["LastName"] = lastName;
            r["FatherName"] = fatherName;

            Tables[Constants.MechanicsTableName].AcceptChanges();
            Tables[Constants.MechanicsTableName].WriteXml(Constants.GetFullPath(Settings.Default.Mechanics));
        }

        internal void LockMechanic(int id)
        {
            DataRow r = GetRowById(Constants.MechanicsTableName, id);
            r["State"] = (int) State.Unemployed;

            Tables[Constants.MechanicsTableName].AcceptChanges();
            Tables[Constants.MechanicsTableName].WriteXml(
                Constants.GetFullPath(Settings.Default.Mechanics));
        }


        internal int AddProtocol(string blankNumber, string mechanicName, DateTime dateTime, string techpass,
            string groupTitle, bool result,
            DateTime nexDateTime, bool visChck, int gbo)
        {
            DataRow r = Tables[Constants.ProtocolsTableName].NewRow();

            r["BlankNumber"] = blankNumber;
            r["IDOperator"] = _currentOperator.Id;
            r["IDMechanic"] = GetMechanicIdByShortName(mechanicName);
            r["Date"] = dateTime;

            if (techpass != "")
                r["TechPhoto"] = techpass;

            r["IDGroup"] = GetGroupId(groupTitle);
            r["Result"] = result;
            r["NextData"] = nexDateTime.Date;
            r["VisualCheck"] = visChck;
            r["GBO"] = gbo;

            Tables[Constants.ProtocolsTableName].Rows.Add(r);
            Tables[Constants.ProtocolsTableName].AcceptChanges();
            Tables[Constants.ProtocolsTableName].WriteXml(Constants.GetFullPath(Settings.Default.Protocols));

            return (int) r["ProtocolID"];
        }

        private int GetMechanicIdByShortName(string mechanicShortName)
        {
            return (from DataRow item in Tables[Constants.MechanicsTableName].Rows
                where
                    GetShortMechanicName((int) item["MechanicID"]) == mechanicShortName
                select (int) item["MechanicID"]).ToArray()[0];
        }

        internal void SetCurrentOperator(string name)
        {
            DataRow r = GetUserByLogin(name);

            _currentOperator = new Operator((Rights) r["Right"], (int) r["OperatorId"], r["Name"].ToString(),
                r["LastName"].ToString());
        }

        private DataRow GetUserByLogin(string name)
        {
            return
                (from DataRow r in Tables[Constants.OperatorsTableName].Rows
                    where r["Login"].ToString() == name
                    select r).ToArray()
                    [0];
        }

        internal void AddMesure(int normativeId, double value, int newProtocolId)
        {
            DataRow item = Tables[Constants.MesuresTableName].NewRow();

            item["NormativeID"] = normativeId;
            item["Value"] = value;
            item["IDProtocol"] = newProtocolId;

            Tables[Constants.MesuresTableName].Rows.Add(item);
            Tables[Constants.MesuresTableName].AcceptChanges();
            Tables[Constants.MesuresTableName].WriteXml(Constants.GetFullPath(Settings.Default.Mesure));
        }

        internal string GetShortMechanicName(int mechanicId)
        {
            return (from DataRow item in Tables[Constants.MechanicsTableName].Rows
                where (int) item["MechanicID"] == mechanicId
                select
                    item["LastName"] + " " + item["Name"].ToString()[0] + ". " + item["FatherName"].ToString()[0] + ".")
                .ToArray()[0];
        }

        internal string GetShortOperatorName(int operatorId)
        {
            return (from DataRow item in Tables[Constants.OperatorsTableName].Rows
                where (int) item["OperatorId"] == operatorId
                select
                    item["LastName"] + " " + item["Name"].ToString()[0] + ".")
                .ToArray()[0];
        }

        internal DataRow[] GetMesuresFromProtocol(DataRow row)
        {
            return row.GetChildRows(relationFK_Protocols_Mesures);
        }


        internal bool GroupContainsNormative(string groupname, string normativename)
        {
            int groupId = GetGroupId(groupname);

            int normtag = GetNormativeTag(normativename);

            List<DataRow> mesures = (from DataRow item in Tables[Constants.NormativesTableName].Rows
                where (int) item["Tag"] == normtag && (int) item["IDGroup"] == groupId
                select item).ToList();

            if (mesures.Count == 0)
                return false;

            return true;
        }

        internal void GetMaxSmokeValue(int groupId, out double minObSmokeVal, out double maxObSmokeVal)
        {
            object[] tmp1 = (
                from DataRow item in Tables[Constants.NormativesTableName].Rows
                where
                    (int) item["IDGroup"] == groupId &&
                    (int) item["Tag"] == new Normatives().IndexOf(_rm.GetString("DVRSUM"))
                select item["MaxValue"]).ToArray();
            object[] tmp2 = (
                from DataRow item in Tables[Constants.NormativesTableName].Rows
                where
                    (int) item["IDGroup"] == groupId &&
                    (int) item["Tag"] == new Normatives().IndexOf(_rm.GetString("DVRSUP"))
                select item["MaxValue"]).ToArray();
            if (tmp1.Length == 1 && tmp2.Length == 1)
            {
                minObSmokeVal = (double) tmp1[0];
                maxObSmokeVal = (double) tmp2[0];
            }
            else
            {
                minObSmokeVal = 0;
                maxObSmokeVal = 0;
            }
        }

        internal bool GroupExist(int year, string category, int engine, bool before)
        {
            List<DataRow> rows = (from DataRow item in Tables[Constants.GroupTableName].Rows
                where
                    (int) item["Year"] == year && item["Category"].ToString() == category &&
                    (int) item["EngineType"] == engine &&
                    (bool) item["Before"] == before
                select item).ToList();

            if (rows.Count == 0)
                return false;

            return true;
        }

        internal Rights GetOperatorRight()
        {
            return _currentOperator.Rights;
        }

        internal int GetOperatorId()
        {
            return _currentOperator.Id;
        }

        internal void RemoveGroup(DataRow selectedRow)
        {
            var groipId = (int) selectedRow["GroupID"];

            List<int> normatives = (from DataRow row in Tables[Constants.NormativesTableName].Rows
                where (int) row["IDGroup"] == groipId
                select (int) row["NormativeID"]).ToList();
            List<int> protocols = (from DataRow row in Tables[Constants.ProtocolsTableName].Rows
                where (int) row["IDGroup"] == groipId
                select (int) row["ProtocolID"]).ToList();

            var mesures = new List<int>();
            foreach (int i in protocols)
            {
                mesures.AddRange((from DataRow row in Tables[Constants.MesuresTableName].Rows
                    where (int) row["IDProtocol"] == i
                    select (int) row["MesureID"]).ToList());
            }

            foreach (int mesure in mesures)
            {
                RemoveRowById(Constants.MesuresTableName, mesure);
            }

            foreach (int protocol in protocols)
            {
                RemoveRowById(Constants.ProtocolsTableName, protocol);
            }
            foreach (int normative in normatives)
            {
                RemoveRowById(Constants.NormativesTableName, normative);
            }

            RemoveRowById(Constants.GroupTableName, groipId);
        }

        internal DataRow GetProtocolByBlankId(string blank)
        {
            DataRow[] rows = (from DataRow item in Tables[Constants.ProtocolsTableName].Rows
                where item["BlankNumber"].ToString() == blank
                select item).ToArray();

            if (rows.Length == 0)
                return null;

            return rows[0];
        }

        internal DataRow[] GetProtocolsBetweenDates(DateTime dateTime1, DateTime dateTime2)
        {
            return (from DataRow item in Protocols.Rows
                where
                    (DateTime) item[Protocols.DateColumn] > dateTime1 &&
                    (DateTime) item[Protocols.DateColumn] <= dateTime2
                select item).ToArray();
        }

        internal bool GroupWithGasEngine(string groupTitle)
        {
            int id = GetGroupId(groupTitle);

            DataRow r = GetRowById(Constants.GroupTableName, id);

            if ((int) r["EngineType"] == 0)
                return true;

            return false;
        }
    }
}