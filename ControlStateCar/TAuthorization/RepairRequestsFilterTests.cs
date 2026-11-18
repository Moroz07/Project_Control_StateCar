using System;
using System.Collections.Generic;
using CarsharingClassLibrary.Models;
using ControlStateCar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TAuthorization
{
    [TestClass]
    public class RepairRequestsFilterTests
    {
        private List<RepairRequest> CreateSampleRequests()
        {
            return new List<RepairRequest>
            {
                new RepairRequest
                {
                    Id = 1,
                    CarNumber = "A123BC77",
                    Status = RepairStatus.New,
                    TechnicianLogin = "ivanov",
                    CreatedAt = new DateTime(2026, 11, 10)
                },
                new RepairRequest
                {
                    Id = 2,
                    CarNumber = "B456OP77",
                    Status = RepairStatus.InProgress,
                    TechnicianLogin = "sidorov_i",
                    CreatedAt = new DateTime(2026, 11, 11)
                },
                new RepairRequest
                {
                    Id = 3,
                    CarNumber = "C789MK77",
                    Status = RepairStatus.Completed,
                    TechnicianLogin = "ivanov",
                    CreatedAt = new DateTime(2026, 11, 12)
                }
            };
        }

        [TestMethod]
        public void ApplyFilter_IvanovSearchesByIvanov_ReturnsIvanovRequests()
        {
            // Иванов ищет заявки по Иванову -> выдаются только заявки Иванова
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(repo => repo.GetAllRepairRequests()).Returns(CreateSampleRequests());

            var userIvanov = new User { Login = "ivanov", Role = Role.техник };
            var model = new RepairRequestsListModel(mockRepo.Object, userIvanov);

            var filterParams = new RepairRequestFilterParameters
            {
                TechnicianLogin = "ivanov"
            };

            string result = model.ApplyFilter(filterParams);

            Assert.AreEqual(string.Empty, result);
            Assert.AreEqual(2, model.TotalCount);
            Assert.AreEqual("ivanov", model.Requests[0].TechnicianLogin);
            Assert.AreEqual("ivanov", model.Requests[1].TechnicianLogin);
            // Проверка сортировки в модели: более новая заявка (Id = 3) идёт первой
            Assert.AreEqual(3, model.Requests[0].Id);
        }

        [TestMethod]
        public void ApplyFilter_ByCarNumber_ReturnsMatchingRequest()
        {
            // Поиск по фрагменту госномера "456" -> выдаётся заявка "B456OP77"
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(repo => repo.GetAllRepairRequests()).Returns(CreateSampleRequests());

            var model = new RepairRequestsListModel(mockRepo.Object);
            var filterParams = new RepairRequestFilterParameters
            {
                CarNumber = "456"
            };

            string result = model.ApplyFilter(filterParams);

            Assert.AreEqual(string.Empty, result);
            Assert.AreEqual(1, model.TotalCount);
            Assert.AreEqual("B456OP77", model.Requests[0].CarNumber);
        }

        [TestMethod]
        public void ApplyFilter_ByStatus_ReturnsMatchingRequest()
        {
            // Фильтрация по статусу "новый" -> выдаётся заявка со статусом New
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(repo => repo.GetAllRepairRequests()).Returns(CreateSampleRequests());

            var model = new RepairRequestsListModel(mockRepo.Object);
            var filterParams = new RepairRequestFilterParameters
            {
                Status = RepairStatus.New
            };

            string result = model.ApplyFilter(filterParams);

            Assert.AreEqual(string.Empty, result);
            Assert.AreEqual(1, model.TotalCount);
            Assert.AreEqual(RepairStatus.New, model.Requests[0].Status);
        }

        [TestMethod]
        public void ApplyFilter_NoMatches_ReturnsMessageAndEmptyList()
        {
            // Поиск несуществующего госномера -> возвращается сообщение и пустой список
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(repo => repo.GetAllRepairRequests()).Returns(CreateSampleRequests());

            var model = new RepairRequestsListModel(mockRepo.Object);
            var filterParams = new RepairRequestFilterParameters
            {
                CarNumber = "X999XX"
            };

            string result = model.ApplyFilter(filterParams);

            Assert.AreEqual("Заявки по заданным критериям не найдены", result);
            Assert.AreEqual(0, model.TotalCount);
        }
    }
}