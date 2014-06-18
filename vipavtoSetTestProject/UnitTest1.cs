using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using adovipavto;
using adovipavto.Classes;
using adovipavto.Enums;
using adovipavto.Properties;
using DRandomLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace vipavtoSetTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GroupTest()
        {
            var set = new NewVipAvtoSet();

            if (!set.GroupExist(2006, "M1", 1, true))
                set.AddGroup(2006, "M1", 1, true);

            if (set.GroupExist(2006, "M1", 1, true))
                set.AddGroup(2024, "M1", 1, true);

            Assert.AreEqual(2, set.Groups.Count);


            string title = set.GetGroupTitle(set.Groups[0].IdGroup);

            Assert.AreEqual("M1 До 2006 Дизельный", title);

            int id = set.Groups[0].IdGroup;
            Assert.AreEqual(id, set.GetGroupId(title));
        }

        [TestMethod]
        public void NormativesTest()
        {
            var set = new NewVipAvtoSet();

            set.AddGroup(2004, "M1", 0, true);
            set.AddNormative(set.Groups[0].IdGroup, 0, 30);
            Assert.AreEqual(1, set.Normatives.Count);
        }

        [TestMethod]
        public void MechanicTest()
        {
            var set = new NewVipAvtoSet();

            set.AddMechanic("ivan", "ivanov", "ivanovich");
            Assert.AreEqual("ivan", set.Mechanics[0].Name);
            Assert.AreEqual("ivanov", set.Mechanics[0].LastName);
            Assert.AreEqual("ivanovich", set.Mechanics[0].FatherName);


            int s = set.GetMechanicIdByShortName("lous");

            Assert.AreEqual(-1, s);
        }

        [TestMethod]
        public void OperatorTest()
        {
            var set = new NewVipAvtoSet();

            set.AddOperator("ivan", "ivanov", "vanya", "password", "Администратор");
            set.SetCurrentOperator("vanya");

            set.AddOperator("ivan", "ivanov", "vann", "password", "Оператор");
            set.AddOperator("ivan", "ivanov", "vffdanya", "password", "Уволен");
            set.AddOperator("ivan", "ivanov", "fdssdf", "password", "Дятел");


            Assert.AreEqual("vanya", set.Operators[0].Login);
            Assert.AreEqual("ivan", set.Operators[0].Name);
            Assert.AreEqual("ivanov", set.Operators[0].LastName);
            Assert.AreEqual(NewVipAvtoSet.GetHash("password"), set.Operators[0].Password);


            Assert.AreEqual(Rights.Administrator, set.GetOperatorRight());
            Assert.AreEqual(set.Operators[0].IdOperator, set.GetOperatorId());

            set.SetCurrentOperator(null);
        }

        [TestMethod]
        public void ProtocolTest()
        {
            var set = new NewVipAvtoSet();
            set.AddMechanic("ivan", "ivanov", "ivanovich");
            set.AddOperator("ivan", "ivanov", "valn", "dfdss", "Оператор");
            set.SetCurrentOperator("valn");
            set.AddGroup(2007, "M1", 0, true);
            set.AddNormative(set.Groups[0].IdGroup, 0, 30);
            set.AddProtocol("4324342", set.GetShortMechanicName(set.Mechanics[0].IdMechanic), DateTime.Now,
                set.Groups[0].IdGroup, true, DateTime.Now.AddDays(365), true, 0);

            set.AddPhoto(new Bitmap(128, 128), set.Protocols[0].IdProtocol);
            Assert.AreEqual(1, set.Protocols.Count);


            NewVipAvtoSet.ProtocolsRow[] prot = set.GetProtocolsBetweenDates(DateTime.Now.AddDays(-2), DateTime.Now);
            Assert.AreEqual(1, set.Protocols.Count);

            Assert.AreEqual(set.Protocols[0].IdProtocol, set.GetProtocolByBlankId("4324342").IdProtocol);
            Assert.IsNull(set.GetProtocolByBlankId("dsfsddsf"));
            Assert.IsTrue(set.UniqProtocolNumber("432435353"));
            Assert.IsFalse(set.UniqProtocolNumber("4324342"));
        }


        [TestMethod]
        public void MesureTest()
        {
            var set = new NewVipAvtoSet();
            set.AddMechanic("ivan", "ivanov", "ivanovich");
            set.AddOperator("ivan", "ivanov", "valn", "dfdss", "Оператор");
            set.SetCurrentOperator("valn");
            set.AddGroup(2007, "M1", 0, true);
            set.AddNormative(set.Groups[0].IdGroup, 0, 30);
            set.AddProtocol("4324342", set.GetShortMechanicName(set.Mechanics[0].IdMechanic), DateTime.Now,
                set.Groups[0].IdGroup, true, DateTime.Now.AddDays(365), true, 0);

            set.AddMesure(1, 1, set.Protocols[0].IdProtocol, set.Groups[0].IdGroup);
            set.Update(typeof (NewVipAvtoSet.MesuresRow));
            Assert.AreEqual(1, set.Mesures.Count);


            var document = new PrintProtocolDocument(set.Protocols[0],  set);
            document.Print();

            var document2 = new PrintProtocolDocument(set.Protocols.ToArray(), DateTime.Now.AddDays(-25), DateTime.Now,
                set);
            document2.Print();

            set.AddProtocol("43243432", set.GetShortMechanicName(set.Mechanics[0].IdMechanic), DateTime.Now,
                set.Groups[0].IdGroup, false, DateTime.Now.AddDays(365), false, 1);
            set.AddPhoto(new Bitmap(400, 878), set.Protocols[1].IdProtocol);

            document = new PrintProtocolDocument(set.Protocols[1], set);
            document.Print();


            set.AddGroup(2007, "M1", 1, false);
            set.AddNormative(set.Groups[1].IdGroup, 0, 30);

            set.AddProtocol("43243432", set.GetShortMechanicName(set.Mechanics[0].IdMechanic), DateTime.Now,
                set.Groups[1].IdGroup, false, DateTime.Now.AddDays(365), false, 2);
            set.AddPhoto(new Bitmap(652, 400), set.Protocols[2].IdProtocol);
            document = new PrintProtocolDocument(set.Protocols[2], set);
            document.Print();

            Assert.AreEqual("4324342",
                set.GetRowById(Constants.ProtocolsTableName, set.Protocols[0].IdProtocol)[1].ToString());
            set.RemoveRowById(Constants.ProtocolsTableName, set.Protocols[0].IdProtocol);

            set.LockMechanic(set.Mechanics[0].IdMechanic);
        }


        [TestMethod]
        public void VisualRowTest()
        {
            var set = new NewVipAvtoSet();
            set.AddGroup(2007, "M1", 0, true);
            set.AddNormative(set.Groups[0].IdGroup, 0, 30);

            var row = new VisualRow(set.Normatives[0], new DRandom());
            row.TextBox = new TextBox();
            Settings.Default.AutoGeneratedData = true;
            row.TextBox = new TextBox();
            row.MinLabel = new Label();
            row.MaxLabel = new Label();
            row.SetMinMax();

            row.TextBox.Text = "";
            row.Requred();

            row.TextBox.Text = "43";
            row.IsValid();
            row.TextBox.Text = "fedsa";
            row.IsValid();
            row.TextBox.Text = "25";
            row.IsValid();

            row.Requred();

            row.PressKey(Keys.D);
            row.PressKey(Keys.OemBackslash);

            Assert.AreEqual(typeof (Label), row.MinLabel.GetType());
            Assert.AreEqual(typeof (Label), row.MaxLabel.GetType());
            Assert.AreEqual(typeof (TextBox), row.TextBox.GetType());
        }


        [TestMethod]
        public void EnginesTest()
        {
            var t = new Engines();

            Assert.AreEqual(0, t.GetEngineIndex("Бензиновый"));
            Assert.AreEqual(1, t.GetEngineIndex("Дизельный"));
            Assert.AreEqual(-1, t.GetEngineIndex("Гибридный"));
            Console.WriteLine(t[0]);

            string[] c = t.GetAllEngines();
            Assert.AreEqual(3, c.Length);

            Assert.AreEqual("Администратор", Constants.GetEnumDescription(Rights.Administrator));
            Assert.AreEqual("Germetical", Constants.GetEnumDescription(Gbo.Germetical));
        }

        [TestMethod]
        public void NormativesClassTest()
        {
            var t = new Normatives();


            Assert.AreEqual(24, t.Count);
            Assert.AreEqual(24, t.GetAllNormatives().Count);


            var set = new NewVipAvtoSet();
            set.LoadData();
            set.AddGroup(2007, "M1", 0, true);
            set.AddNormative(set.Groups[0].IdGroup, 0, 30);


            Assert.AreEqual(0, set.GetNormativeTag(t[0]));
            if (set.GetNormativesFromGroup(set.Groups[0].Title).Length == 0)
                Assert.Fail();

            Assert.AreEqual(NewVipAvtoSet.GetHash("admin"), set.GetUserPasswors("admin"));
            Assert.AreEqual("", set.GetUserPasswors("lolosha"));


            set.LockOperator(set.Operators[0].IdOperator);
            Assert.IsFalse(set.GroupWithGasEngine(set.Groups[1].Title));
            Assert.IsTrue(set.GroupWithGasEngine(set.Groups[2].Title));



        }

        [TestMethod]
        public void SomeQueries()
        {
            NewVipAvtoSet set = new NewVipAvtoSet();
            set.LoadData();


            var t1 = set.GroupByTermin();

            foreach (string s in t1)
            {
                Console.WriteLine(s);
            }
        }
    }
}