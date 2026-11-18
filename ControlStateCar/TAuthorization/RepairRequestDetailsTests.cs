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
                    TechnicianLogin = "ivanov",
                    TechnicianFullName = "Иванов Иван Иванович",
                    TechnicianPhone = "+7 (999) 111-22-33",
                    Description = "Замена передних тормозных колодок и дисков"
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
                    TechnicianLogin = null,
                    TechnicianFullName = "Не назначен",
                    TechnicianPhone = "Не назначен",
                    Description = "Диагностика ходовой части"
                }
            };
        }

        [TestMethod]
        public void GetRequestDetails_ExistingRequest_ReturnsFullDetails()
        {
            // Поиск существующей заявки по Id в модели возвращает полные данные
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(repo => repo.GetAllRepairRequestDetails()).Returns(CreateSampleDetailsList());

            var model = new RepairRequestsListModel(mockRepo.Object);
            var actual = model.GetRequestDetails(1);

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual("Ford", actual.CarBrand);
            Assert.AreEqual("Focus", actual.CarModel);
            Assert.AreEqual("A123BC77", actual.CarNumber);
            Assert.AreEqual("XTA12345678901231", actual.Vin);
            Assert.AreEqual(2018, actual.Year);
            Assert.AreEqual("ivanov", actual.TechnicianLogin);
            Assert.AreEqual("Иванов Иван Иванович", actual.TechnicianFullName);
        }

        [TestMethod]
        public void GetRequestDetails_NonExistentRequest_ReturnsNull()
        {
            // Поиск несуществующего Id возвращает null
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(repo => repo.GetAllRepairRequestDetails()).Returns(CreateSampleDetailsList());

            var model = new RepairRequestsListModel(mockRepo.Object);
            var actual = model.GetRequestDetails(9999);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetRequestDetails_UnassignedTechnician_ReturnsDefaultUnassignedValues()
        {
            // Заявка без назначенного техника и без даты закрытия
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(repo => repo.GetAllRepairRequestDetails()).Returns(CreateSampleDetailsList());

            var model = new RepairRequestsListModel(mockRepo.Object);
            var actual = model.GetRequestDetails(3);

            Assert.IsNotNull(actual);
            Assert.IsNull(actual.ClosedAt);
            Assert.IsNull(actual.TechnicianLogin);
            Assert.AreEqual("Не назначен", actual.TechnicianFullName);
            Assert.AreEqual("Не назначен", actual.TechnicianPhone);
        }
    }
}