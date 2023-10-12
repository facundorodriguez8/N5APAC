namespace PAC.Tests.WebApi;
using System.Collections.ObjectModel;

using System.Data;
using Moq;
using PAC.IBusinessLogic;
using PAC.Domain;
using PAC.WebAPI;
using Microsoft.AspNetCore.Mvc;

[TestClass]
public class StudentControllerTest
{
        private Student _request;

        [TestInitialize]
        public void InitTest()
        {
                _request = new Student();
                _request.Id = 3;
                _request.Name = "Alberto";
        }

        [TestCleanup]
        public void Cleanup()
        {
            _request = null;
        }

        [TestMethod]
        public void CreateStudentOk()
        {
            // Arrange
            Mock<IStudentdLogic> logic = new Mock<IStudentdLogic>(MockBehavior.Strict);
            logic.Setup(l => l.InsertStudents(It.IsAny<Student>())).Returns(_request);
            StudentController controller = new StudentController(logic.Object);
            CreatedAtActionResult expectedObjectResult = new CreatedAtActionResult("CreateStudent", "CreateStudent",
                new { id = 3 }, _request);

            // Act
            IActionResult result = controller.CreateStudent(_request);

            // Assert
            logic.VerifyAll();
            CreatedAtActionResult resultObject = result as CreatedAtActionResult;
            Student resultValue = resultObject.Value as Student;
            Assert.AreEqual(expectedObjectResult.StatusCode, resultObject.StatusCode);
            Assert.AreEqual(_request.Name, resultValue.Name);
        }

        [TestMethod]
        public void CreateStudentFail()
        {
            // Arrange
            Mock<IStudentdLogic> logic = new Mock<IStudentdLogic>(MockBehavior.Strict);
            logic.Setup(l => l.InsertStudents(It.IsAny<Student>())).Returns(_request);
            StudentController controller = new StudentController(logic.Object);
            CreatedAtActionResult expectedObjectResult = new CreatedAtActionResult("CreateStudent", "CreateStudent",
                new { id = 5 }, _request);

            Assert.ThrowsException<BadRequest>(() =>
            {
                // Act
                IActionResult result = controller.CreateStudent(null);
            });
        }

}
