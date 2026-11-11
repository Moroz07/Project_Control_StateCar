    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsharingClassLibrary.Models;
using ControlStateCar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TAuthorization
{
    [TestClass]
    public class RepairRequestsFilterTests
    {
        [TestMethod]
        public void ApplyFilter_CarNumberFilter_ReturnsFilteredList()
        {
            var mockRepo = new Mock<IRepairRequestRepository>();
            var adminUser = new User
            {
                Login = "morozov",
                Role = Role.администратор
            };

            var expectedList = new List<RepairRequest>
            {
                new RepairRequest
                {
                    Id = 1,
                    CarNumber = "A123BC77",
                    Status = RepairStatus.New
                }
            };

            mockRepo.Setup(r => r.FilterRepairRequests(
                Role.администратор,
                "morozov",
                It.Is<RepairRequestFilterParameters>(p => p.CarNumber == "123")
            )).Returns(expectedList);

            var model = new RepairRequestsListModel(mockRepo.Object, adminUser);
            var filterParams = new RepairRequestFilterParameters
            {
                CarNumber = "123"
            };

            string result = model.ApplyFilter(filterParams);

            Assert.AreEqual(string.Empty, result);
            Assert.AreEqual(1, model.TotalCount);
            Assert.AreEqual("A123BC77", model.Requests[0].CarNumber);
        }

        [TestMethod]
        public void ApplyFilter_StatusFilter_ReturnsOnlyMatchingStatus()
        {
            var mockRepo = new Mock<IRepairRequestRepository>();
            var adminUser = new User
            {
                Login = "morozov",
                Role = Role.администратор
            };

            var expectedList = new List<RepairRequest>
            {
                new RepairRequest
                {
                    Id = 2,
                    CarNumber = "B456OP77",
                    Status = RepairStatus.InProgress
                },
                new RepairRequest
                {
                    Id = 5,
                    CarNumber = "O654TM77",
                    Status = RepairStatus.InProgress
                }
            };

            mockRepo.Setup(r => r.FilterRepairRequests(
                Role.администратор,
                "morozov",
                It.Is<RepairRequestFilterParameters>(p => p.Status == RepairStatus.InProgress)
            )).Returns(expectedList);

            var model = new RepairRequestsListModel(mockRepo.Object, adminUser);
            var filterParams = new RepairRequestFilterParameters
            {
                Status = RepairStatus.InProgress
            };

            string result = model.ApplyFilter(filterParams);

            Assert.AreEqual(string.Empty, result);
            Assert.AreEqual(2, model.TotalCount);
            for (int i = 0; i < model.Requests.Count; i++)
            {
                Assert.AreEqual(RepairStatus.InProgress, model.Requests[i].Status);
            }
        }

        [TestMethod]
        public void ApplyFilter_TechnicianRole_ForcesOwnLoginInFilter()
        {
            // Arrange (Сценарий 3: Ролевое ограничение при фильтрации для техника)
            var mockRepo = new Mock<IRepairRequestRepository>();
            var techUser = new User
            {
                Login = "ivanov",
                Role = Role.техник
            };

            var techRequests = new List<RepairRequest>
            {
                new RepairRequest
                {
                    Id = 2,
                    CarNumber = "B456OP77",
                    TechnicianLogin = "ivanov"
                }
            };

            mockRepo.Setup(r => r.FilterRepairRequests(
                Role.техник,
                "ivanov",
                It.Is<RepairRequestFilterParameters>(p => p.TechnicianLogin == "ivanov")
            )).Returns(techRequests);

            var model = new RepairRequestsListModel(mockRepo.Object, techUser);
            var filterParams = new RepairRequestFilterParameters
            {
                TechnicianLogin = "sidorov_i" // Попытка указать логин чужого техника
            };

            // Act
            string result = model.ApplyFilter(filterParams);

            // Assert
            Assert.AreEqual(string.Empty, result);
            Assert.AreEqual(1, model.TotalCount);
            Assert.AreEqual("ivanov", model.Requests[0].TechnicianLogin);
            Assert.AreEqual("ivanov", filterParams.TechnicianLogin);
        }

        [TestMethod]
        public void ApplyFilter_NoMatchingRecords_ReturnsErrorMessageAndEmptyList()
        {
            // Arrange (Сценарий 4: Отсутствие записей по заданной комбинации критериев)
            var mockRepo = new Mock<IRepairRequestRepository>();
            var adminUser = new User
            {
                Login = "morozov",
                Role = Role.администратор
            };

            mockRepo.Setup(r => r.FilterRepairRequests(
                Role.администратор,
                "morozov",
                It.IsAny<RepairRequestFilterParameters>()
            )).Returns(new List<RepairRequest>());

            var model = new RepairRequestsListModel(mockRepo.Object, adminUser);
            var filterParams = new RepairRequestFilterParameters
            {
                CarNumber = "X999XX"
            };

            // Act
            string result = model.ApplyFilter(filterParams);

            // Assert
            Assert.AreEqual("Заявки по заданным критериям не найдены", result);
            Assert.AreEqual(0, model.TotalCount);
            Assert.IsNotNull(model.Requests);
            Assert.AreEqual(0, model.Requests.Count);
        }
    }
}
