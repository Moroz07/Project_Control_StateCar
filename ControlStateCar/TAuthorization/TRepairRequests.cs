using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ControlStateCar;

namespace TAuthorization
{
    [TestClass]
    public class TRepairRequests
    {
        [TestMethod]
        public void TestMethod_GetRecentRequests_Success()
        {
            var mock = new Mock<IRepairRequestRepository>();
            var sampleRequests = new List<RepairRequest>
            {
                new RepairRequest { Id = 1, CarNumber = "A123BC77", Status = RepairStatus.New },
                new RepairRequest { Id = 2, CarNumber = "B456OP77", Status = RepairStatus.InProgress }
            };

            mock.Setup(repo => repo.GetRecentRepairRequests(Role.администратор, "admin_petr", 15))
                .Returns(sampleRequests);

            IRepairRequestRepository repository = mock.Object;
            var currentUser = new User { Login = "admin_petr", Role = Role.администратор };
            var model = new RepairRequestsListModel(repository, currentUser);

            model.LoadRecentRequests(15);

            Assert.IsNotNull(model.Requests);
            Assert.AreEqual(2, model.TotalCount);
            Assert.AreEqual("A123BC77", model.Requests[0].CarNumber);
        }

        [TestMethod]
        public void TestMethod_GetRecentRequests_ForTechnician()
        {
            var mock = new Mock<IRepairRequestRepository>();
            var techRequests = new List<RepairRequest>
            {
                new RepairRequest { Id = 2, CarNumber = "B456OP77", Status = RepairStatus.InProgress, TechnicianLogin = "ivanov" }
            };

            mock.Setup(repo => repo.GetRecentRepairRequests(Role.техник, "ivanov", 15))
                .Returns(techRequests);

            IRepairRequestRepository repository = mock.Object;
            var currentUser = new User { Login = "ivanov", Role = Role.техник };
            var model = new RepairRequestsListModel(repository, currentUser);

            model.LoadRecentRequests(15);

            Assert.AreEqual(1, model.TotalCount);
            Assert.AreEqual("ivanov", model.Requests[0].TechnicianLogin);
        }

        [TestMethod]
        public void TestMethod_AssignTechnician_Success()
        {
            var mock = new Mock<IRepairRequestRepository>();
            mock.Setup(repo => repo.AssignTechnician(1, "ivanov"))
                .Returns(true);

            IRepairRequestRepository repository = mock.Object;
            var model = new RepairRequestsListModel(repository);

            string errorMessage = model.AssignTechnicianToRequest(1, "ivanov");

            Assert.AreEqual(string.Empty, errorMessage);
        }

        [TestMethod]
        public void TestMethod_AssignTechnician_EmptyLogin()
        {
            var mock = new Mock<IRepairRequestRepository>();
            IRepairRequestRepository repository = mock.Object;
            var model = new RepairRequestsListModel(repository);

            string errorMessage = model.AssignTechnicianToRequest(1, "");

            Assert.AreEqual("Выберите техника из списка.", errorMessage);
        }
    }
}