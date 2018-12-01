using Clinic.Api.Controllers;
using Clinic.Api.Core;
using Clinic.Api.Core.Domain;
using Clinic.Api.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Clinic.Test
{
    public class PatientControllerTest
    {
        [Fact]
        public async void Get_Returns_Patient()
        {
            //ARRANGE
            var pankajPatient = new Patient { Name = "Pankaj", MobileNumber = "01717372345" };
            var patientList = new List<Patient>
                {
                    pankajPatient
                };
            var moqPatientRepositoey = new Mock<IPatientRepository>();
            moqPatientRepositoey.Setup(repo => repo.Get()).ReturnsAsync(patientList);
            var moqUnitOfWork = new Mock<IUnitOfWork>();
            moqUnitOfWork.Setup(unitOfWork => unitOfWork.PatientRepository)
                .Returns(moqPatientRepositoey.Object);
            var controller = new PatientController(moqUnitOfWork.Object);
            //ACT
            var value = ((await controller.Get()) as OkObjectResult).Value;
            var result = value as List<Patient>;
            //ASSERT
            Assert.NotNull(value);
            Assert.Equal(result[0].Name, patientList[0].Name);
        }
        [Fact]
        public async void Get_Returns_IEnumerable_of_Patient()
        {
            //Arrange
            var pankajPatient = new Patient { Name = "Pankaj", MobileNumber = "01717372345" };
            IEnumerable<Patient> patientList = new List<Patient>
                {
                    pankajPatient
                };
            var moqPatientRepositoey = new Mock<IPatientRepository>();
            moqPatientRepositoey.Setup(repo => repo.Get()).ReturnsAsync(patientList);
            var moqUnitOfWork = new Mock<IUnitOfWork>();
            moqUnitOfWork.Setup(unitOfWork => unitOfWork.PatientRepository)
                .Returns(moqPatientRepositoey.Object);
            var controller = new PatientController(moqUnitOfWork.Object);
            //Act
            var value = ((await controller.Get()) as OkObjectResult).Value;
            //Assert
            Assert.IsAssignableFrom<IEnumerable<Patient>>(value);
        }
    }
}
