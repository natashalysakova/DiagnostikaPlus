using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using adovipavto.Classes;
using adovipavto.Enums;
using MySql.Data.MySqlClient;

namespace adovipavto
{
    public partial class VipAvtoSet
    {
        private Operator _currentOperator;
        private ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());
        private MySqlConnection _conection;




        public string UpdateProtocolsAndMesures()
        {
            StartConnection(true);

            MySqlDataAdapter adapter;


            var commands = new List<MySqlCommand>
            {
                new MySqlCommand("SELECT * FROM  `mesures` ",
                _conection),
                new MySqlCommand("SELECT * FROM  `protocols` ",
                _conection)
            };

            List<DataTable> tables = new List<DataTable>
            {
               Mesures, Protocols
            };

            for (int i = 0; i < commands.Count; i++)
            {
                adapter = new MySqlDataAdapter(commands[i]);
                var cb = new MySqlCommandBuilder(adapter);

                adapter.UpdateCommand = cb.GetUpdateCommand().Clone();
                adapter.Update(tables[i]);
            }

            foreach (DataTable t in tables)
            {
                t.Clear();
            }

            var sb = new StringBuilder();
            for (int i = 0; i < commands.Count; i++)
            {
                try
                {
                    adapter = new MySqlDataAdapter(commands[i]);
                    adapter.Fill(tables[i]);
                }
                catch (Exception e)
                {
                    sb.AppendLine(e.Message + " " + tables[i].TableName);
                }
            }



            _conection.Close();

            return sb.ToString();
        }

        private void StartConnection(bool mode)
        {



            string connectionString = GetConnectionString(mode);

            _conection = new MySqlConnection(connectionString);
            _conection.Open();

            var t = new MySqlCommand("SHOW DATABASES;", _conection).ExecuteReader();
            bool databaseNotExist = true;
            while (t.Read())
            {
                if (t.GetValue(0).ToString() == Properties.Settings.Default.DataBase)
                {
                    databaseNotExist = false;
                }
            }
            t.Close();

            if (mode == false && databaseNotExist)
            {
                string s0 = "CREATE DATABASE IF NOT EXISTS `" + Properties.Settings.Default.DataBase + "`;";
                var cmd = new MySqlCommand(s0, _conection);
                cmd.ExecuteNonQuery();

                connectionString = GetConnectionString(true);
                _conection = new MySqlConnection(connectionString);
                _conection.Open();


                string s1 = Properties.Resources.defaultDatabase;

                var cmd2 = new MySqlCommand(s1, _conection);
                cmd2.ExecuteNonQuery();
            }
            else if (!databaseNotExist)
            {
                connectionString = GetConnectionString(true);
                _conection = new MySqlConnection(connectionString);
                _conection.Open();
            }
        }

        private static string GetConnectionString(bool withDataBase)
        {
            if (withDataBase)
                return "SERVER=" + Properties.Settings.Default.ServerIp + ";" +
                   "PORT=" + Properties.Settings.Default.Port + ";" +
                   "DATABASE=" + Properties.Settings.Default.DataBase + ";" +
                   "UID=" + Properties.Settings.Default.UserName + ";" +
                   "PASSWORD=" + Properties.Settings.Default.Passwod + ";";


            return "SERVER=" + Properties.Settings.Default.ServerIp + ";" +
                   "PORT=" + Properties.Settings.Default.Port + ";" +
                   "UID=" + Properties.Settings.Default.UserName + ";" +
                   "PASSWORD=" + Properties.Settings.Default.Passwod + ";";
        }

        public void LoadData()
        {
            StartConnection(false);

            MySqlCommand operatorsCommand = new MySqlCommand("SELECT * FROM  `operators` ", _conection);
            MySqlCommand mechanicsCommand = new MySqlCommand("SELECT * FROM  `mechanics` ", _conection);
            MySqlCommand protocolsCommand = new MySqlCommand("SELECT * FROM  `protocols` ", _conection);
            MySqlCommand mesuresCommand = new MySqlCommand("SELECT * FROM  `mesures` ", _conection);
            MySqlCommand normativesCommand = new MySqlCommand("SELECT * FROM  `normatives` ", _conection);
            MySqlCommand groupsCommand = new MySqlCommand("SELECT * FROM  `groups` ", _conection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(operatorsCommand);
            adapter.Fill(Operators);

            adapter = new MySqlDataAdapter(mechanicsCommand);
            adapter.Fill(Mechanics);



            adapter = new MySqlDataAdapter(groupsCommand);
            adapter.Fill(Group);

            adapter = new MySqlDataAdapter(normativesCommand);
            adapter.Fill(Normatives);


            adapter = new MySqlDataAdapter(protocolsCommand);
            adapter.Fill(Protocols);

            adapter = new MySqlDataAdapter(mesuresCommand);
            adapter.Fill(Mesures);

            _conection.Close();
        }



        internal void LockOperator(int id)
        {
            var r = GetRowById(Constants.OperatorsTableName, id) as OperatorsRow;
            if (r != null) 
                r.Right = (int)Rights.Locked;

            UpdateOperatorsAndMechanics();
        }

        internal bool AddOperator(string name, string lastName, string login, string password, string rights)
        {
            OperatorsRow[] operators =
                (from OperatorsRow item in Operators.Rows where item.Login == login select item).ToArray();

            if (operators.Length != 0)
                return false;

            OperatorsRow row = Operators.NewOperatorsRow();

            row.Name = name;
            row.LastName = lastName;
            row.Login = login;
            row.Password = GetHash(password);

            row.Right = (int)GetRightByString(rights);


            Operators.AddOperatorsRow(row);
            UpdateOperatorsAndMechanics();

            return true;
        }

        private string UpdateOperatorsAndMechanics()
        {

            StartConnection(true);

            MySqlDataAdapter adapter;


            var commands = new List<MySqlCommand>
            {
                new MySqlCommand("SELECT * FROM  `operators` ",
                _conection),
                new MySqlCommand("SELECT * FROM  `mechanics` ",
                _conection)
            };

            List<DataTable> tables = new List<DataTable>
            {
               Operators, Mechanics
            };

            for (int i = 0; i < commands.Count; i++)
            {
                adapter = new MySqlDataAdapter(commands[i]);
                var cb = new MySqlCommandBuilder(adapter);

                adapter.UpdateCommand = cb.GetUpdateCommand().Clone();
                adapter.Update(tables[i]);
            }

            for (int i = 0; i < tables.Count; i++)
            {
                tables[i].Clear();
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < commands.Count; i++)
            {
                try
                {
                    adapter = new MySqlDataAdapter(commands[i]);
                    adapter.Fill(tables[i]);
                }
                catch (Exception e)
                {
                    sb.AppendLine(e.Message + " " + tables[i].TableName);
                }
            }



            _conection.Close();

            return sb.ToString();
        }

        public static string GetHash(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] byteHash = md5.ComputeHash(bytes);

            string hash = string.Empty;
            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);
            return hash;
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
            var r = GetRowById(Constants.OperatorsTableName, id) as OperatorsRow;
            if (r != null)
            {
                r.Name = name;
                r.LastName = lastName;
                r.Login = login;
                r.Password = GetHash(pass);
            }

            UpdateOperatorsAndMechanics();
        }

        internal void AddMechanic(string name, string lastName, string fatherName)
        {
            MechanicsRow r = Mechanics.NewMechanicsRow();

            r.Name = name;
            r.LastName = lastName;
            r.FatherName = fatherName;

            r.State = (int)State.Employed;

            Mechanics.AddMechanicsRow(r);
            UpdateOperatorsAndMechanics();
        }


        internal void EditMechanic(int id, string name, string lastName, string fatherName)
        {
            var r = GetRowById(Constants.MechanicsTableName, id) as MechanicsRow;

            if (r != null)
            {
                r.Name = name;
                r.LastName = lastName;
                r.FatherName = fatherName;
            }

            UpdateOperatorsAndMechanics();
        }

        internal void LockMechanic(int id)
        {
            var r = GetRowById(Constants.MechanicsTableName, id) as MechanicsRow;
            if (r != null) r.State = (int)State.Unemployed;

            UpdateOperatorsAndMechanics();
        }


        internal int AddProtocol(string blankNumber, string mechanicName, DateTime dateTime, string techpass,
            string groupTitle, bool result,
            DateTime nexDateTime, bool visChck, int gbo)
        {
            ProtocolsRow r = Protocols.NewProtocolsRow();

            r.ProtocolID = new Random().Next(int.MaxValue);
            r.BlankNumber = blankNumber;
            r.IDOperator = _currentOperator.Id;
            r.IDMechanic = GetMechanicIdByShortName(mechanicName);
            r.Date = dateTime;

            if (techpass != "")
                r.TechPhoto = techpass;

            r.IDGroup = GetGroupId(groupTitle);
            r.Result = result;
            r.NextData = nexDateTime.Date;
            r.VisualCheck = visChck;
            r.GBO = gbo;

            Protocols.AddProtocolsRow(r);
            int id = r.ProtocolID;
            UpdateProtocolsAndMesures();
            Protocols.AcceptChanges();
            //Protocols.WriteXml(Constants.GetFullPath(Settings.Instance.Protocols));

            return id;
        }

        private int GetMechanicIdByShortName(string mechanicShortName)
        {
            int[] rows = (from MechanicsRow item in Mechanics.Rows
                          where
                              GetShortMechanicName(item.MechanicID) == mechanicShortName
                          select item.MechanicID).ToArray();
            if (rows.Length != 0)
                return rows[0];

            return -1;
        }

        internal void SetCurrentOperator(string name)
        {
            OperatorsRow r = GetUserByLogin(name);

            _currentOperator = new Operator((Rights)r.Right, r.OperatorId, r.Name,
                r.LastName);
        }

        private OperatorsRow GetUserByLogin(string name)
        {
            OperatorsRow[] rows = (from OperatorsRow r in Operators.Rows
                                   where r.Login == name
                                   select r).ToArray();

            if (rows.Length != 0)
                return rows[0];

            return null;
        }

        internal void AddMesure(int normativeId, double value, int newProtocolId)
        {
            MesuresRow item = Mesures.NewMesuresRow();

            item.NormativeID = normativeId;
            item.Value = value;
            item.IDProtocol = newProtocolId;

            Mesures.AddMesuresRow(item);
            UpdateProtocolsAndMesures();
        }

        internal string GetShortMechanicName(int mechanicId)
        {
            return (from MechanicsRow item in Mechanics.Rows
                    where item.MechanicID == mechanicId
                    select
                        item.LastName + " " + item.Name[0] + ". " + item.FatherName[0] + ".")
                .ToArray()[0];
        }

        internal string GetShortOperatorName(int operatorId)
        {
            return (from OperatorsRow item in Operators.Rows
                    where item.OperatorId == operatorId
                    select
                        item.LastName + " " + item.Name[0] + ".")
                .ToArray()[0];
        }



        internal bool GroupContainsNormative(string groupname, string normativename)
        {
            int groupId = GetGroupId(groupname);

            int normtag = GetNormativeTag(normativename);

            List<NormativesRow> mesures = (from NormativesRow item in Normatives.Rows
                                           where item.Tag == normtag && item.IDGroup == groupId
                                           select item).ToList();

            if (mesures.Count == 0)
                return false;

            return true;
        }

        internal void GetMaxSmokeValue(int groupId, out double minObSmokeVal, out double maxObSmokeVal)
        {
            double[] tmp1 = (
                from NormativesRow item in Normatives.Rows
                where
                    item.IDGroup == groupId &&
                    item.Tag == new Normatives().IndexOf(_rm.GetString("DVRSUM"))
                select item.MaxValue).ToArray();

            double[] tmp2 = (
                from NormativesRow item in Normatives.Rows
                where
                    item.IDGroup == groupId &&
                    item.Tag == new Normatives().IndexOf(_rm.GetString("DVRSUP"))
                select item.MaxValue).ToArray();

            if (tmp1.Length == 1 && tmp2.Length == 1)
            {
                minObSmokeVal = tmp1[0];
                maxObSmokeVal = tmp2[0];
            }
            else
            {
                minObSmokeVal = 0;
                maxObSmokeVal = 0;
            }
        }

        internal bool GroupExist(int year, string category, int engine, bool before)
        {
            List<GroupRow> rows = (from GroupRow item in Group.Rows
                                   where
                                       item.Year == year && item.Category == category &&
                                       item.EngineType == engine &&
                                       item.Before == before
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
            if (selectedRow != null)
            {
                selectedRow.Delete();

                UpdateGroupAndNormatives();
            }
        }

        internal ProtocolsRow GetProtocolByBlankId(string blank)
        {
            ProtocolsRow[] rows = (from ProtocolsRow item in Protocols.Rows
                                   where item.BlankNumber == blank
                                   select item).ToArray();

            if (rows.Length == 0)
                return null;

            return rows[0];
        }

        internal ProtocolsRow[] GetProtocolsBetweenDates(DateTime dateTime1, DateTime dateTime2)
        {
            return (from ProtocolsRow item in Protocols.Rows
                    where
                        item.Date > dateTime1 &&
                        item.Date <= dateTime2
                    select item).ToArray();
        }

        internal bool GroupWithGasEngine(string groupTitle)
        {
            int id = GetGroupId(groupTitle);

            var r = GetRowById(Constants.GroupTableName, id) as GroupRow;

            if (r != null && r.EngineType == 0)
                return true;

            return false;
        }

        internal void RemoveAllNormatives(int id)
        {
            NormativesRow[] rows =
                (from NormativesRow row in Normatives.Rows where row.IDGroup == id select row).ToArray();

            foreach (NormativesRow dataRow in rows)
            {
                RemoveRowById(Constants.NormativesTableName, dataRow.IDGroup);
            }
        }

        internal void RemoveAllGroup()
        {
            for (int i = 0; i < Group.Rows.Count; i++)
            {
                RemoveGroup(Group.Rows[i]);
            }
        }

        internal bool UniqProtocolNumber(string number)
        {
            ProtocolsRow[] prot =
                (from ProtocolsRow rows in Protocols.Rows where rows.BlankNumber == number select rows)
                    .ToArray();

            if (prot.Length == 0)
                return true;

            return false;
        }

        #region NormativesMetods

        public void AddNormative(string group, string title, double minValue, double maxValue)
        {
            NormativesRow r = Normatives.NewNormativesRow();
            r.Tag = new Normatives().GetNormativeIndex(title);
            r.MaxValue = maxValue;
            r.MinValue = minValue;
            r.IDGroup = GetGroupId(group);

            Normatives.AddNormativesRow(r);
            UpdateGroupAndNormatives();
        }

        public void EditNormative(int id, string group, string title, double minValue, double maxValue)
        {
            var r = Normatives.Rows.Find(id) as NormativesRow;
            if (r != null)
            {
                r.Tag = new Normatives().GetNormativeIndex(title);
                r.MaxValue = maxValue;
                r.MinValue = minValue;
                r.IDGroup = GetGroupId(@group);
            }

            UpdateGroupAndNormatives();
        }

        public NormativesRow[] GetNormativesFromGroup(string groupTitle)
        {
            int groupId = (
                from NormativesRow items
                    in Normatives.Rows
                where GroupTitle(items.IDGroup) == groupTitle
                select items.IDGroup).ToList()[0];

            List<GroupRow> group = (from GroupRow item in Group.Rows where item.GroupID == groupId select item).ToList();

            if (group.Count != 0)
            {
                NormativesRow[] norms = group[0].GetNormativesRows();
                return norms;
            }

            return null;
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

        public void AddGroup(int year, string categoty, int engine, bool before)
        {
            GroupRow r = Group.NewGroupRow();
            r.Year = year;
            r.Category = categoty;
            r.EngineType = engine;
            r.Before = before;

            Group.AddGroupRow(r);
            UpdateGroupAndNormatives();
        }

        public void EditGroup(int id, int year, string categoty, int engine, bool before)
        {
            var r = GetRowById(Constants.GroupTableName, id) as GroupRow;
            if (r != null)
            {
                r.Year = year;
                r.Category = categoty;
                r.EngineType = engine;
                r.Before = before;
            }

            UpdateGroupAndNormatives();
        }

        public int GetGroupId(string title)
        {
            string[] splitTitle = title.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (GroupRow row in Group.Rows)
            {
                string cat = row.Category;
                string bef = row.Before ? _rm.GetString("before") : _rm.GetString("after");
                string year = row.Year.ToString();
                int engine = row.EngineType;
                if (splitTitle.Length == 4)
                {
                    if (cat == splitTitle[0] && bef == splitTitle[1] && year == splitTitle[2] &&
                        new Engines()[engine] == splitTitle[3])
                        return Convert.ToInt32(row.GroupID);
                }
                else
                {
                    if (cat == splitTitle[0] && bef == splitTitle[1] && year == splitTitle[2])
                        return Convert.ToInt32(row.GroupID);
                }
            }

            return -1;
        }

        public string GroupTitle(int id)
        {
            var groupRow = GetRowById(Constants.GroupTableName, id) as GroupRow;

            if (groupRow == null)
                return "";


            string s = groupRow.Before ? _rm.GetString("before") : _rm.GetString("after");
            return groupRow.Category + " " + s + " " + groupRow.Year + " " +
                   new Engines()[groupRow.EngineType];
        }

        #endregion

        #region UniversalMethods

        internal DataRow GetRowByIndex(string tableName, int rowIndex)
        {
            return Tables[tableName].Rows[rowIndex];
        }

        internal DataRow GetRowById(string tableName, int rowId)
        {
            string idheader = Tables[tableName].Columns[0].ColumnName;
            return
                Tables[tableName].Rows.Cast<DataRow>()
                    .FirstOrDefault(item => Convert.ToInt32(item[idheader]) == rowId);
        }

        internal void RemoveRow(string tableName, DataRow selectedRow)
        {
            selectedRow.Delete();
            Update(tableName);
        }

        private string UpdateGroupAndNormatives()
        {
            StartConnection(true);

            MySqlDataAdapter adapter;


            List<MySqlCommand> commands = new List<MySqlCommand>
            {
                new MySqlCommand("SELECT * FROM  `normatives` ",
                _conection),
                new MySqlCommand("SELECT * FROM  `groups` ",
                _conection)
            };

            List<DataTable> tables = new List<DataTable>
            {
               Normatives, Group
            };

            for (int i = 0; i < commands.Count; i++)
            {
                adapter = new MySqlDataAdapter(commands[i]);
                var cb = new MySqlCommandBuilder(adapter);

                adapter.UpdateCommand = cb.GetUpdateCommand().Clone();
                adapter.Update(tables[i]);
            }

            for (int i = 0; i < tables.Count; i++)
            {
                tables[i].Clear();
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < commands.Count; i++)
            {
                try
                {
                    adapter = new MySqlDataAdapter(commands[i]);
                    adapter.Fill(tables[i]);
                }
                catch (Exception e)
                {
                    sb.AppendLine(e.Message + " " + tables[i].TableName);
                }
            }



            _conection.Close();

            return sb.ToString();

        }

        internal void RemoveRowById(string tableName, int id)
        {
            for (int i = 0; i < Tables[tableName].Rows.Count; i++)
            {
                if ((int)Tables[tableName].Rows[i][0] == id)
                {
                    Tables[tableName].Rows[i].Delete();
                    break;
                }
            }

            Update(tableName);
        }

        private void Update(string tableName)
        {
            switch (tableName)
            {
                case Constants.GroupTableName:
                    UpdateGroupAndNormatives();
                    break;
                case Constants.NormativesTableName:
                    UpdateGroupAndNormatives();
                    break;
                case Constants.MechanicsTableName:
                    UpdateOperatorsAndMechanics();
                    break;
                case Constants.OperatorsTableName:
                    UpdateOperatorsAndMechanics();
                    break;
                case Constants.ProtocolsTableName:
                    UpdateProtocolsAndMesures();
                    break;
                case Constants.MesuresTableName:
                    UpdateProtocolsAndMesures();
                    break;
            }
        }

        #endregion

        #region OperatorMethods

        

        internal string GetUserPasswors(string username)
        {
            OperatorsRow[] operatorRow =
                (from OperatorsRow item in Operators.Rows
                 where item.Login == username
                 select item).ToArray();

            if (operatorRow.Length != 0)
            {
                return operatorRow[0].Password;
            }

            return "";
        }

        #endregion

    }
}