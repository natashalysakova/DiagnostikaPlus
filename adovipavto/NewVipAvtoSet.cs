using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Enums;
using adovipavto.NewVipAvtoSetTableAdapters;

namespace adovipavto
{
    public partial class NewVipAvtoSet
    {
        private Operator _currentOperator;
        private GroupsTableAdapter _groupsTableAdapter = new GroupsTableAdapter();
        private MechanicsTableAdapter _mechanicsTableAdapter = new MechanicsTableAdapter();
        private MesuresTableAdapter _mesuresTableAdapter = new MesuresTableAdapter();
        private NormativesTableAdapter _normativesTableAdapter = new NormativesTableAdapter();
        private OperatorsTableAdapter _operatorsTableAdapter = new OperatorsTableAdapter();
        private ProtocolsTableAdapter _protocolsTableAdapter = new ProtocolsTableAdapter();
        private ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());


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


        internal DataRow GetRowById(string tableName, int id)
        {
            switch (tableName)
            {
                case Constants.GroupTableName:
                    return Groups.FindByIdGroup(id);
                case Constants.NormativesTableName:
                    return Normatives.FindByIdNormative(id);
                case Constants.MesuresTableName:
                    return Mesures.FindByIdMesure(id);
                case Constants.ProtocolsTableName:
                    return Protocols.FindByIdProtocol(id);
                case Constants.OperatorsTableName:
                    return Operators.FindByIdOperator(id);
                case Constants.MechanicsTableName:
                    return Mechanics.FindByIdMechanic(id);
                default:
                    return null;
            }
        }

        internal void RemoveRow(DataRow selectedRow)
        {
            selectedRow.Delete();

            Update(selectedRow.GetType());

            AcceptChanges();
        }

        public void Update(Type type)
        {
            if (typeof (GroupsRow) == type)
                _groupsTableAdapter.Update(Groups);
            else if (typeof (NormativesRow) == type)
            {
                try
                {
                    _normativesTableAdapter.Update(Normatives);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message + ex.ParamName);
                }
            }
            else if (typeof (MesuresRow) == type)
            {
                _mesuresTableAdapter.Update(Mesures);
            }
            else if (typeof (ProtocolsRow) == type)
            {
                _protocolsTableAdapter.Update(Protocols);
            }
            else if (typeof (OperatorsRow) == type)
            {
                _operatorsTableAdapter.Update(Operators);
            }
            else if (typeof (MechanicsRow) == type)
            {
                _mechanicsTableAdapter.Update(Mechanics);
            }
        }

        public ProtocolsRow GetProtocolByBlankId(string blank)
        {
            ProtocolsRow[] rows = (from ProtocolsRow item in Protocols.Rows
                where item.BlankNumber == blank
                select item).ToArray();

            if (rows.Length == 0)
                return null;

            return rows[0];
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


        internal void SetCurrentOperator(string name)
        {
            OperatorsRow r = GetUserByLogin(name);

            _currentOperator = new Operator((Rights) r.Right, r.IdOperator, r.Name,
                r.LastName);
        }

        private OperatorsRow GetUserByLogin(string name)
        {
            OperatorsRow[] rows = (from OperatorsRow row in Operators.Rows
                where row.Login == name
                select row).ToArray();

            if (rows.Length != 0)
                return rows[0];

            return null;
        }

        //internal string GroupTitle(int id)
        //{
        //    var groupRow = GetRowById(Constants.GroupTableName, id) as GroupsRow;

        //    if (groupRow == null)
        //        return "";


        //    string s = groupRow.Before ? _rm.GetString("before") : _rm.GetString("after");
        //    return groupRow.Category + " " + s + " " + groupRow.Year + " " +
        //           new Engines()[groupRow.EngineType];
        //}

        internal void AddMechanic(string name, string lastName, string fatherName)
        {
            MechanicsRow r = Mechanics.NewMechanicsRow();

            r.Name = name;
            r.LastName = lastName;
            r.FatherName = fatherName;

            r.State = (int) State.Employed;

            Mechanics.AddMechanicsRow(r);
            Update(r.GetType());
            AcceptChanges();
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

            Update(r.GetType());
            AcceptChanges();
        }


        internal void LockMechanic(int id)
        {
            var r = GetRowById(Constants.MechanicsTableName, id) as MechanicsRow;
            if (r != null)
            {
                r.State = (int) State.Unemployed;
                Update(r.GetType());
                AcceptChanges();
            }
        }


        internal int AddProtocol(string blankNumber, string mechanicName, DateTime dateTime, string techpass,
            string groupTitle, bool result,
            DateTime nexDateTime, bool visChck, int gbo)
        {
            ProtocolsRow r = Protocols.NewProtocolsRow();

            r.BlankNumber = blankNumber;
            r.OperatorId = _currentOperator.Id;
            r.MechanicId = GetMechanicIdByShortName(mechanicName);
            r.Date = dateTime;

            if (techpass != "")
            {
                var converter = new ImageConverter();
                var b = new Bitmap(techpass);
                r.TechPhoto = (byte[]) converter.ConvertTo(b, typeof (byte[]));
            }


            r.GroupId = GetGroupId(groupTitle);
            r.Result = result;
            r.NextData = nexDateTime.Date;
            r.VisualCheck = visChck;
            r.GBO = gbo;

            Protocols.AddProtocolsRow(r);
            Update(r.GetType());
            AcceptChanges();

            int id = r.IdProtocol;


            return id;
        }

        private int GetMechanicIdByShortName(string mechanicShortName)
        {
            int[] rows = (from MechanicsRow item in Mechanics.Rows
                where
                    GetShortMechanicName(item.IdMechanic) == mechanicShortName
                select item.IdMechanic).ToArray();
            if (rows.Length != 0)
                return rows[0];

            return -1;
        }

        internal string GetShortMechanicName(int mechanicId)
        {
            return (from MechanicsRow item in Mechanics.Rows
                where item.IdMechanic == mechanicId
                select
                    item.LastName + " " + item.Name[0] + ". " + item.FatherName[0] + ".")
                .ToArray()[0];
        }

        internal string GetShortOperatorName(int operatorId)
        {
            return (from OperatorsRow item in Operators.Rows
                where item.IdOperator == operatorId
                select
                    item.LastName + " " + item.Name[0] + ".")
                .ToArray()[0];
        }

        public int GetGroupId(string title)
        {
            string[] splitTitle = title.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach (GroupsRow row in Groups.Rows)
            {
                string cat = row.Category;
                string bef = row.Before ? _rm.GetString("before") : _rm.GetString("after");
                string year = row.Year.ToString();
                int engine = row.EngineType;
                if (splitTitle.Length == 4)
                {
                    if (cat == splitTitle[0] && bef == splitTitle[1] && year == splitTitle[2] &&
                        new Engines()[engine] == splitTitle[3])
                        return Convert.ToInt32(row.IdGroup);
                }
                else
                {
                    if (cat == splitTitle[0] && bef == splitTitle[1] && year == splitTitle[2])
                        return Convert.ToInt32(row.IdGroup);
                }
            }

            return -1;
        }


        public void EditNormative(int id, string group, string title, double minValue, double maxValue)
        {
            NormativesRow[] tmp =
                (from NormativesRow row in Normatives.Rows where row.IdNormative == id select row).ToArray();
            if (tmp.Length != 0)
            {
                NormativesRow r = tmp.First();
                r.Tag = new Normatives().GetNormativeIndex(title);
                r.MaxValue = maxValue;
                r.MinValue = minValue;
                r.GroupId = GetGroupId(@group);

                Update(r.GetType());
                AcceptChanges();
            }
        }

        internal void RemoveRowById(string tableName, int id)
        {
            DataRow row = GetRowById(tableName, id);
            if (row != null)
            {
                row.Delete();

                Update(row.GetType());
                AcceptChanges();
            }
        }

        internal void EditOperator(int id, string name, string lastName, string login, string pass)
        {
            var r = GetRowById(Constants.OperatorsTableName, id) as OperatorsRow;
            if (r != null)
            {
                r.Name = name;
                r.LastName = lastName;
                r.Login = login;
                if (pass != @"********")
                    r.Password = GetHash(pass);

                Update(r.GetType());
                AcceptChanges();
            }
        }


        internal bool AddOperator(string name, string lastName, string login, string password, string rights)
        {
            OperatorsRow[] operators =
                (from OperatorsRow item in Operators.Rows where item.Login == login select item).ToArray();

            if (operators.Length != 0)
                return false;

            OperatorsRow r = Operators.NewOperatorsRow();

            r.Name = name;
            r.LastName = lastName;
            r.Login = login;
            r.Password = GetHash(password);

            r.Right = GetRightByString(rights);


            Operators.AddOperatorsRow(r);

            Update(r.GetType());
            AcceptChanges();

            return true;
        }


        private int GetRightByString(string rightTitle)
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

            return (int) r;
        }

        internal bool GroupContainsNormative(string groupname, string normativename)
        {
            int groupId = GetGroupId(groupname);

            int normtag = GetNormativeTag(normativename);

            List<NormativesRow> mesures = (from NormativesRow item in Normatives.Rows
                where item.Tag == normtag && item.GroupId == groupId
                select item).ToList();

            if (mesures.Count == 0)
                return false;

            return true;
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


        public void AddGroup(int year, string categoty, int engine, bool before)
        {
            GroupsRow r = Groups.NewGroupsRow();
            r.Year = year;
            r.Category = categoty;
            r.EngineType = engine;
            r.Before = before;

            String s = r.Before ? _rm.GetString("before") : _rm.GetString("after");
            r.Title = r.Category + " " + s + " " + r.Year + " " + new Engines()[r.EngineType];

            Groups.AddGroupsRow(r);

            Update(r.GetType());
            AcceptChanges();
        }

        public void EditGroup(int id, int year, string categoty, int engine, bool before)
        {
            var r = GetRowById(Constants.GroupTableName, id) as GroupsRow;
            if (r != null)
            {
                r.Year = year;
                r.Category = categoty;
                r.EngineType = engine;
                r.Before = before;
                String s = r.Before ? _rm.GetString("before") : _rm.GetString("after");
                r.Title = r.Category + " " + s + " " + r.Year + " " + new Engines()[r.EngineType];
                Update(r.GetType());
                AcceptChanges();
            }
        }

        internal bool GroupExist(int year, string category, int engine, bool before)
        {
            List<GroupsRow> rows = (from GroupsRow item in Groups.Rows
                where
                    item.Year == year && item.Category == category &&
                    item.EngineType == engine &&
                    item.Before == before
                select item).ToList();

            if (rows.Count == 0)
                return false;

            return true;
        }


        public void AddNormative(string group, string title, double minValue, double maxValue)
        {
            NormativesRow r = Normatives.NewNormativesRow();
            r.Tag = new Normatives().GetNormativeIndex(title);
            r.MaxValue = maxValue;
            r.MinValue = minValue;
            r.GroupId = GetGroupId(group);

            Normatives.AddNormativesRow(r);
            Update(r.GetType());
            AcceptChanges();
        }

        internal Rights GetOperatorRight()
        {
            return _currentOperator.Rights;
        }

        internal int GetOperatorId()
        {
            return _currentOperator.Id;
        }


        internal void LockOperator(int id)
        {
            var r = GetRowById(Constants.OperatorsTableName, id) as OperatorsRow;
            if (r != null)
                r.Right = (int) Rights.Locked;

            Update(r.GetType());
            AcceptChanges();
        }

        internal ProtocolsRow[] GetProtocolsBetweenDates(DateTime dateTime1, DateTime dateTime2)
        {
            return (from ProtocolsRow item in Protocols.Rows
                where
                    item.Date >= dateTime1 &&
                    item.Date <= dateTime2
                select item).ToArray();
        }

        internal bool GroupWithGasEngine(string groupTitle)
        {
            int id = GetGroupId(groupTitle);

            var r = GetRowById(Constants.GroupTableName, id) as GroupsRow;

            if (r != null && r.EngineType == 0)
                return true;

            return false;
        }


        public NormativesRow[] GetNormativesFromGroup(string groupTitle)
        {
            List<GroupsRow> group =
                (from GroupsRow item in Groups.Rows where item.Title == groupTitle select item).ToList();

            if (group.Count != 0)
            {
                NormativesRow[] norms = group[0].GetNormativesRows();
                return norms;
            }

            return null;
        }

        internal void AddMesure(int normativeTag, double value, int newProtocolId, int groupId)
        {
            MesuresRow r = Mesures.NewMesuresRow();

            r.NormativeTag = normativeTag;
            r.Value = value;
            r.ProtocolId = newProtocolId;
            r.MaxValue =
                (from NormativesRow row in Normatives.Rows
                    where row.Tag == normativeTag && row.GroupId == groupId
                    select row.MaxValue)
                    .ToArray()[0];
            r.MinValue =
                (from NormativesRow row in Normatives.Rows
                    where row.Tag == normativeTag && row.GroupId == groupId
                    select row.MinValue)
                    .ToArray()[0];


            Mesures.AddMesuresRow(r);
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


        internal void LoadData()
        {
            _operatorsTableAdapter.Fill(Operators);
            _mechanicsTableAdapter.Fill(Mechanics);
            _groupsTableAdapter.Fill(Groups);
            _normativesTableAdapter.Fill(Normatives);
            _protocolsTableAdapter.Fill(Protocols);
            _mesuresTableAdapter.Fill(Mesures);
        }


        internal string GetGroupTitle(int id)
        {
            GroupsRow r = Groups.FindByIdGroup(id);
            if (r != null)
                return r.Title;

            return null;
        }
    }
}
