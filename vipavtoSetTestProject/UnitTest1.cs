using System.Security.Cryptography;
using System.Text;
using adovipavto;
using adovipavto.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace vipavtoSetTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddGroupTest()
        {
            NewVipAvtoSet set = new NewVipAvtoSet();
            set.AddGroup(2004, "M1", 0, true);

            Assert.AreEqual(1, set.Groups.Count);
        }

        [TestMethod]
        public void AddNormativesTest()
        {
            NewVipAvtoSet set = new NewVipAvtoSet();
            set.AddGroup(2004, "M1", 0, true);
            set.AddNormative(set.Groups[0].IdGroup, 0, 30);
            Assert.AreEqual(1, set.Normatives.Count);
        }
        [TestMethod]
        public void AddMechanicTest()
        {
            NewVipAvtoSet set = new NewVipAvtoSet();
            set.AddMechanic("ivan", "ivanov", "ivanovich");
            Assert.AreEqual("ivan", set.Mechanics[0].Name);
            Assert.AreEqual("ivanov", set.Mechanics[0].LastName);
            Assert.AreEqual("ivanovich", set.Mechanics[0].FatherName);

        }
        [TestMethod]
        public void AddOperatorTest()
        {
            NewVipAvtoSet set = new NewVipAvtoSet();
            set.AddOperator("ivan", "ivanov", "vanya", "password", "Администратор");
            Assert.AreEqual("vanya", set.Operators[0].Login);
            Assert.AreEqual("ivan", set.Operators[0].Name);
            Assert.AreEqual("ivanov", set.Operators[0].LastName);
            Assert.AreEqual(NewVipAvtoSet.GetHash("password"), set.Operators[0].Password);
        }

        [TestMethod]
        public void AddMesureTest()
        {
            NewVipAvtoSet set = new NewVipAvtoSet();
            set.AddMesure(0, 1, set.Protocols[0].IdProtocol, set.Groups[0].IdGroup);
            Assert.AreEqual("vanya", set.Operators[0].Login);
            Assert.AreEqual("ivan", set.Operators[0].Name);
            Assert.AreEqual("ivanov", set.Operators[0].LastName);
            Assert.AreEqual(NewVipAvtoSet.GetHash("password"), set.Operators[0].Password);
        }

    }
}
