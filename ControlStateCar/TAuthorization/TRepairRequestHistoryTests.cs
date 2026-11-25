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
    public class RepairRequestHistoryTests
    {
        private List<RepairRequestHistoryItem> CreateSampleHistory()
        {
            return new List<RepairRequestHistoryItem>
            {
                new RepairRequestHistoryItem
                {
                    Id = 1,
                    RequestId = 1,
                    ChangedAt = new DateTime(2026, 11, 12, 10, 0, 0),
                    StatusName = "новый",
                    EmployeeDisplay = "Не указан",
                    Comment = "Заявка зарегистрирована"
                },
                new RepairRequestHistoryItem
                {
                    Id = 2,
                    RequestId = 2,
                    ChangedAt = new DateTime(2026, 11, 12, 11, 0, 0),
                    StatusName = "новый",
                    EmployeeDisplay = "Не указан",
                    Comment = "Другая заявка"
                },
                new RepairRequestHistoryItem
                {
                    Id = 3,
                    RequestId = 1,
                    ChangedAt = new DateTime(2026, 11, 12, 14, 30, 0),
                    StatusName = "в работе",
                    EmployeeDisplay = "Иванов И.И. (ivanov)",
                    Comment = "Принято в работу"
                }
            };
        }

        [TestMethod]
        public void GetRequestHistory_ExistingRequest_FiltersAndSortsNewestFirst()
        {
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(r => r.GetAllRequestHistory()).Returns(CreateSampleHistory());

            var model = new RepairRequestsListModel(mockRepo.Object);
            var history = model.GetRequestHistory(1);

            Assert.AreEqual(2, history.Count);
            Assert.AreEqual(3, history[0].Id);
            Assert.AreEqual(1, history[1].Id);
        }

        [TestMethod]
        public void GetRequestHistory_NoRecordsForRequest_ReturnsEmptyList()
        {
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(r => r.GetAllRequestHistory()).Returns(CreateSampleHistory());

            var model = new RepairRequestsListModel(mockRepo.Object);
            var history = model.GetRequestHistory(999);

            Assert.IsNotNull(history);
            Assert.AreEqual(0, history.Count);
        }

        [TestMethod]
        public void CloseRequest_ValidData_RecordsHistoryEntry()
        {
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(r => r.GetAllRepairRequestDetails()).Returns(new List<RepairRequestDetails>
            {
                new RepairRequestDetails
                {
                    Id = 1,
                    Status = RepairStatus.InProgress,
                    CreatedAt = new DateTime(2026, 11, 12, 10, 0, 0)
                }
            });
            mockRepo.Setup(r => r.CloseRequest(1, "Заменены колодки", It.IsAny<DateTime>())).Returns(true);
            mockRepo.Setup(r => r.AddRequestHistory(1, 5, "завершён", It.IsAny<string>(), It.IsAny<DateTime>())).Returns(true);

            var model = new RepairRequestsListModel(mockRepo.Object);
            string error = model.CloseRequest(1, "Заменены колодки", new DateTime(2026, 11, 12, 16, 0, 0), 5);

            Assert.AreEqual(string.Empty, error);
            mockRepo.Verify(r => r.AddRequestHistory(1, 5, "завершён", It.Is<string>(c => c.Contains("Заменены колодки")), It.IsAny<DateTime>()), Times.Once);
        }

        [TestMethod]
        public void UpdateRequestClosedDate_ValidData_RecordsHistoryEntry()
        {
            var mockRepo = new Mock<IRepairRequestRepository>();
            mockRepo.Setup(r => r.GetAllRepairRequestDetails()).Returns(new List<RepairRequestDetails>
            {
                new RepairRequestDetails
                {
                    Id = 1,
                    Status = RepairStatus.Completed,
                    CreatedAt = new DateTime(2026, 11, 12, 10, 0, 0)
                }
            });
            mockRepo.Setup(r => r.UpdateClosedDate(1, It.IsAny<DateTime>(), "Уточнена дата")).Returns(true);
            mockRepo.Setup(r => r.AddRequestHistory(1, 5, "изменена дата закрытия", It.IsAny<string>(), It.IsAny<DateTime>())).Returns(true);

            var model = new RepairRequestsListModel(mockRepo.Object);
            string error = model.UpdateRequestClosedDate(1, new DateTime(2026, 11, 12, 18, 0, 0), "Уточнена дата", 5);

            Assert.AreEqual(string.Empty, error);
            mockRepo.Verify(r => r.AddRequestHistory(1, 5, "изменена дата закрытия", It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
        }
    }
}
