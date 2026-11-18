using System;
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
                new RepairRequest { Id = 1, CarNumber = "A123BC77", Status = RepairStatus.New, CreatedAt = new DateTime(2026, 11, 10) },
                new RepairRequest { Id = 2, CarNumber = "B456OP77", Status = RepairStatus.InProgress, CreatedAt = new DateTime(2026, 11, 12) }
            };

            mock.Setup(repo => repo.GetAllRepairRequests())
                .Returns(sampleRequests);

            IRepairRequestRepository repository = mock.Object;
            var currentUser = new User { Login = "admin_petr", Role = Role.администратор };
            var model = new RepairRequestsListModel(repository, currentUser);

            model.LoadRecentRequests(15);

            Assert.IsNotNull(model.Requests);
            Assert.AreEqual(2, model.TotalCount);
            // Более новая заявка (Id = 2) должна оказаться первой после сортировки в модели
            Assert.AreEqual("B456OP77", model.Requests[0].CarNumber);
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

            Assert.AreEqual("Не выбран техник для назначения.", errorMessage);
        }
    }
}