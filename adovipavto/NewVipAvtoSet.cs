using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using adovipavto.Classes;
using adovipavto.Enums;
using adovipavto.NewVipAvtoSetTableAdapters;
using adovipavto.Properties;

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
        PhotosTableAdapter _photosTableAdapter = new PhotosTableAdapter();
        private ResourceManager _rm = new ResourceManager("adovipavto.StringResource", Assembly.GetExecutingAssembly());


        public string GetUserPasswors(string username)
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


        public DataRow GetRowById(string tableName, int id)
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

        public void RemoveRow(DataRow selectedRow)
        {
            selectedRow.Delete();

            Update(selectedRow.GetType());

            AcceptChanges();
        }

        public void Update(Type type)
        {
            if (typeof(GroupsRow) == type)
                _groupsTableAdapter.Update(Groups);
            else if (typeof(NormativesRow) == type)
            {
                    _normativesTableAdapter.Update(Normatives);
            }
            else if (typeof(MesuresRow) == type)
            {
                _mesuresTableAdapter.Update(Mesures);
            }
            else if (typeof(ProtocolsRow) == type)
            {
                _protocolsTableAdapter.Update(Protocols);
            }
            else if (typeof(OperatorsRow) == type)
            {
                _operatorsTableAdapter.Update(Operators);
            }
            else if (typeof(MechanicsRow) == type)
            {
                _mechanicsTableAdapter.Update(Mechanics);
            }
            else if (typeof(PhotosRow) == type)
            {
                _photosTableAdapter.Update(Photos);
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


        public void SetCurrentOperator(string name)
        {
            OperatorsRow r = GetUserByLogin(name);

            if (r != null)
                _currentOperator = new Operator((Rights)r.Right, r.IdOperator, r.Name,
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

        //public string GroupTitle(int id)
        //{
        //    var groupRow = GetRowById(Constants.GroupTableName, id) as GroupsRow;

        //    if (groupRow == null)
        //        return "";


        //    string s = groupRow.Before ? _rm.GetString("before") : _rm.GetString("after");
        //    return groupRow.Category + " " + s + " " + groupRow.Year + " " +
        //           new Engines()[groupRow.EngineType];
        //}

        public void AddMechanic(string name, string lastName, string fatherName)
        {
            MechanicsRow r = Mechanics.NewMechanicsRow();

            r.Name = name;
            r.LastName = lastName;
            r.FatherName = fatherName;

            r.State = (int)State.Employed;

            Mechanics.AddMechanicsRow(r);
            Update(r.GetType());
            AcceptChanges();
        }

        public void EditMechanic(int id, string name, string lastName, string fatherName)
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


        public void LockMechanic(int id)
        {
            var r = GetRowById(Constants.MechanicsTableName, id) as MechanicsRow;
            if (r != null)
            {
                r.State = (int)State.Unemployed;
                Update(r.GetType());
                AcceptChanges();
            }
        }


        public int AddProtocol(string blankNumber, string mechanicName, DateTime dateTime,
            int groupid, bool result,
            DateTime nexDateTime, bool visChck, int gbo)
        {
            ProtocolsRow r = Protocols.NewProtocolsRow();

            r.BlankNumber = blankNumber;
            r.OperatorId = _currentOperator.Id;
            r.MechanicId = GetMechanicIdByShortName(mechanicName);
            r.Date = dateTime;
            r.GroupId = groupid;
            r.Result = result;
            r.NextData = nexDateTime.Date;
            r.VisualCheck = visChck;
            r.GBO = gbo;

            Protocols.AddProtocolsRow(r);
            Update(r.GetType());
            AcceptChanges();

            return r.IdProtocol;
        }

        public void AddPhoto(Image image, int protocolId)
        {


            PhotosRow r = Photos.NewPhotosRow();
            r.ProtocolId = protocolId;

            var converter = new ImageConverter();

            try
            {
                Int32 width, height;

                double ratio;
                if (image.Width > image.Height)
                {

                    ratio = image.Height / (double)image.Width;
                    width = 640;

                    height = Convert.ToInt32(width * ratio);
                }
                else
                {
                    ratio = image.Width / (double)image.Height;
                    height = 640;
                    width = Convert.ToInt32(height * ratio);
                }

                Bitmap bmp = new System.Drawing.Bitmap(width, height);
                bmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    Rectangle src = new Rectangle(0, 0, image.Width, image.Height);
                    Rectangle dst = new Rectangle(0, 0, bmp.Width, bmp.Height);
                    g.DrawImage(image, dst, src, GraphicsUnit.Pixel);
                    r.Photo = (byte[])converter.ConvertTo(bmp, typeof(byte[]));

                }

                Photos.AddPhotosRow(r);
                Update(r.GetType());
                AcceptChanges();


            }
            catch
            { }
        }

        public int GetMechanicIdByShortName(string mechanicShortName)
        {
            int[] rows = (from MechanicsRow item in Mechanics.Rows
                          where
                              GetShortMechanicName(item.IdMechanic) == mechanicShortName
                          select item.IdMechanic).ToArray();
            if (rows.Length != 0)
                return rows[0];

            return -1;
        }

        public string GetShortMechanicName(int mechanicId)
        {
            return (from MechanicsRow item in Mechanics.Rows
                    where item.IdMechanic == mechanicId
                    select
                        item.LastName + " " + item.Name[0] + ". " + item.FatherName[0] + ".")
                .ToArray()[0];
        }

        public string GetShortOperatorName(int operatorId)
        {
            return (from OperatorsRow item in Operators.Rows
                    where item.IdOperator == operatorId
                    select
                        item.LastName + " " + item.Name[0] + ".")
                .ToArray()[0];
        }

        public int GetGroupId(string title)
        {
            var t = (from GroupsRow row in Groups.Rows where row.Title == title select row.IdGroup);
            return t.First();
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

        public void RemoveRowById(string tableName, int id)
        {
            DataRow row = GetRowById(tableName, id);
            if (row != null)
            {
                row.Delete();

                Update(row.GetType());
                AcceptChanges();
            }
        }

        public void EditOperator(int id, string name, string lastName, string login, string pass)
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


        public bool AddOperator(string name, string lastName, string login, string password, string rights)
        {
            OperatorsRow[] operators =
                (from OperatorsRow item in Operators.Rows where item.Login == login select item).ToArray();

            if (operators.Length != 0)
                return false;

            OperatorsRow r = Operators.NewOperatorsRow();

            if (name == null || lastName == null || login == null || password == null)
            {
                return false;
            }
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

            return (int)r;
        }

        public bool GroupContainsNormative(string groupname, string normativename)
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

        public bool GroupExist(int year, string category, int engine, bool before)
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

        public void AddNormative(int group, double minValue, double maxValue)
        {
            NormativesRow r = Normatives.NewNormativesRow();
            r.Tag = 1;
            r.MaxValue = maxValue;
            r.MinValue = minValue;
            r.GroupId = group;

            Normatives.AddNormativesRow(r);
            Update(r.GetType());
            AcceptChanges();
        }

        public Rights GetOperatorRight()
        {
            return _currentOperator.Rights;
        }

        public int GetOperatorId()
        {
            return _currentOperator.Id;
        }


        public void LockOperator(int id)
        {
            var r = GetRowById(Constants.OperatorsTableName, id) as OperatorsRow;
            if (r != null)
                r.Right = (int)Rights.Locked;

            Update(r.GetType());
            AcceptChanges();
        }

        public ProtocolsRow[] GetProtocolsBetweenDates(DateTime dateTime1, DateTime dateTime2)
        {
            return (from ProtocolsRow item in Protocols.Rows
                    where
                        item.Date >= dateTime1 &&
                        item.Date <= dateTime2
                    select item).ToArray();
        }

        public bool GroupWithGasEngine(string groupTitle)
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

        public void AddMesure(int normativeTag, double value, int newProtocolId, int groupId)
        {
            MesuresRow r = Mesures.NewMesuresRow();

            r.NormativeTag = normativeTag;
            r.Value = value;
            r.ProtocolId = newProtocolId;
            var t = (from NormativesRow row in Normatives.Rows
                     where row.Tag == normativeTag && row.GroupId == groupId
                     select row)
                .ToArray();
            if (t.Length != 0)
            {
                r.MaxValue = t[0].MaxValue;
                r.MinValue = t[0].MinValue;
                Mesures.AddMesuresRow(r);
            }


        }


        public bool UniqProtocolNumber(string number)
        {
            ProtocolsRow[] prot =
                (from ProtocolsRow rows in Protocols.Rows where rows.BlankNumber == number select rows)
                    .ToArray();

            if (prot.Length == 0)
                return true;

            return false;
        }


        public void LoadData()
        {
            string connectionString = ReadConnectionStringFromFile();

            SqlConnection conn = new SqlConnection(connectionString);
            _operatorsTableAdapter.Connection = conn;
            _mechanicsTableAdapter.Connection = conn;
            _groupsTableAdapter.Connection = conn;
            _normativesTableAdapter.Connection = conn;
            _protocolsTableAdapter.Connection = conn;
            _mesuresTableAdapter.Connection = conn;
            _photosTableAdapter.Connection = conn;

            _operatorsTableAdapter.Fill(Operators);
            _mechanicsTableAdapter.Fill(Mechanics);
            _groupsTableAdapter.Fill(Groups);
            _normativesTableAdapter.Fill(Normatives);
            _protocolsTableAdapter.Fill(Protocols);
            _mesuresTableAdapter.Fill(Mesures);
            _photosTableAdapter.Fill(Photos);
        }

        private string ReadConnectionStringFromFile()
        {
            string userDocFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = userDocFolder + "\\" + "DiagnostikaPlus";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string connectionString;
            string filePath = folderPath + "\\" + "settings.ini";
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write)))
                {
                   sw.WriteLine(Settings.Default.VipAvtoDBConnectionString);
                    connectionString = Settings.Default.VipAvtoDBConnectionString;
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
                {
                    connectionString = sr.ReadLine();
                }
            }

            return connectionString;
        }


        public string GetGroupTitle(int id)
        {
            GroupsRow r = Groups.FindByIdGroup(id);
            if (r != null)
                return r.Title;

            return null;
        }
    }
}