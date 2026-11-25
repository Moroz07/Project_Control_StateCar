using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ControlStateCar;

namespace TAuthorization
{
    [TestClass]
    public class RepairRequestDetailsTests
    {
        private List<RepairRequestDetails> CreateSampleDetailsList()
        {
            return new List<RepairRequestDetails>
            {
                new RepairRequestDetails
                {
                    Id = 1,
                    Status = RepairStatus.InProgress,
                    CreatedAt = new DateTime(2026, 11, 12, 10, 30, 0),
                    ClosedAt = null,
                    CarBrand = "Ford",
                    CarModel = "Focus",
                    CarNumber = "A123BC77",
                    Vin = "XTA12345678901231",
                    Year = 2018,
                    CustomerName = "ООО «Каршеринг Плюс»",
                    CustomerPhone = "+7 (495) 000-11-22",
                    TechnicianLogin = "ivanov",
                    TechnicianFullName = "Иванов Иван Иванович",
                    TechnicianPhone = "+7 (999) 111-22-33",
                    Description = "Замена передних тормозных колодок и дисков",
                    WorkDone = ""
                },
                new RepairRequestDetails
                {
                    Id = 3,
                    Status = RepairStatus.New,
                    CreatedAt = new DateTime(2026, 11, 14, 9, 0, 0),
                    ClosedAt = null,
                    CarBrand = "Kia",
                    CarModel = "Rio",
                    CarNumber = "C789MK77",
                    Vin = "Z94CB41BAGR123456",
                    Year = 2020,
                    CustomerName = "Петров Пётр Петрович",
                    CustomerPhone = "+7 (916) 555-44-33",
                    TechnicianLogin = null,
                    TechnicianFullName = "Не назначен",
                    TechnicianPhone = "Не назначен",
                    Description = "Диагностика ходовой части",
                    WorkDone = ""
                }
            };
        }

        [TestMethod]
        public void GetRequestDetails_ExistingRequest_ReturnsFullDetails()
        {
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(repo => repo.GetAllRepairRequestDetails()).Returns(CreateSampleDetailsList());

            var model = new RepairRequestsListModel(mockRepo.Object);
            var actual = model.GetRequestDetails(1);

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual("XTA12345678901231", actual.Vin);
            Assert.AreEqual("ООО «Каршеринг Плюс»", actual.CustomerName);
            Assert.AreEqual("Иванов Иван Иванович", actual.TechnicianFullName);
        }

        [TestMethod]
        public void GetRequestDetails_NonExistentRequest_ReturnsNull()
        {
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(repo => repo.GetAllRepairRequestDetails()).Returns(CreateSampleDetailsList());

            var model = new RepairRequestsListModel(mockRepo.Object);
            var actual = model.GetRequestDetails(9999);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void CloseRequest_ValidWorkDone_ReturnsEmptyErrorString()
        {
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(repo => repo.GetAllRepairRequestDetails()).Returns(CreateSampleDetailsList());
            mockRepo.Setup(repo => repo.CloseRequest(1, "Заменены колодки", It.IsAny<DateTime>())).Returns(true);

            var model = new RepairRequestsListModel(mockRepo.Object);
            string error = model.CloseRequest(1, "Заменены колодки", new DateTime(2026, 11, 13, 15, 0, 0));

            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void CloseRequest_EmptyWorkDone_ReturnsValidationError()
        {
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(repo => repo.GetAllRepairRequestDetails()).Returns(CreateSampleDetailsList());

            var model = new RepairRequestsListModel(mockRepo.Object);
            string error = model.CloseRequest(1, "   ", new DateTime(2026, 11, 13, 15, 0, 0));

            Assert.AreNotEqual(string.Empty, error);
        }

        [TestMethod]
        public void UpdateRequestClosedDate_DateBeforeCreation_ReturnsValidationError()
        {
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(repo => repo.GetAllRepairRequestDetails()).Returns(CreateSampleDetailsList());

            var model = new RepairRequestsListModel(mockRepo.Object);
            string error = model.UpdateRequestClosedDate(1, new DateTime(2026, 11, 10, 9, 0, 0), "Тест");

            Assert.AreNotEqual(string.Empty, error);
        }
    }
}