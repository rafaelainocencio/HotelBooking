using Application;
using Application.Guest.DTOs;
using Application.Guest.Requests;
using Domain.Entities;
using Domain.Ports;
using Moq;

namespace ApplicationTests
{



    public class Tests
    {
        GuestManager guestManager;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task HappyPath()
        {
            var guestDto = new GuestDTO
            {
                Name = "Fulana",
                Surname = "da Silva",
                Email = "abc@gmail.com",
                IdNumber = "abca",
                IdTypeCode = 1
            };

            var expectedId = 222;

            var request = new CreateGuestRequest
            {
                Data = guestDto,
            };


            // mock repo
            var fakeRepo = new Mock<IGuestRepository>();

            // methods
            fakeRepo.Setup(x => x.Create(
                It.IsAny<Guest>())).Returns(Task.FromResult(222));
            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.That(expectedId, Is.EqualTo(res.Data.Id));
            Assert.That(guestDto.Name, Is.EqualTo(res.Data.Name));
        }

        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        [TestCase("")]
        [TestCase(null)]
        public async Task Should_Return_InvalidPersonDocumentIdException_WhenDocsAreInvalid(string docNumber)
        {
            var guestDto = new GuestDTO
            {
                Name = "Fulana",
                Surname = "da Silva",
                Email = "abc@gmail.com",
                IdNumber = docNumber,
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest
            {
                Data = guestDto,
            };


            // mock repo
            var fakeRepo = new Mock<IGuestRepository>();

            // methods
            fakeRepo.Setup(x => x.Create(
                It.IsAny<Guest>())).Returns(Task.FromResult(222));
            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodes.INVALID_PERSON_ID));
            Assert.That(res.Message, Is.EqualTo("The id passed is not valid."));
        }

        [TestCase("", "da Silva", "abc@gmail.com")]
        [TestCase(" ", "da Silva", "abc@gmail.com")]
        [TestCase(null, "da Silva", "abc@gmail.com")]

        [TestCase("Fulana", "", "abc@gmail.com")]
        [TestCase("Fulana", " ", "abc@gmail.com")]
        [TestCase("Fulana", null, "abc@gmail.com")]

        [TestCase("Fulana", "da Silva", "")]
        [TestCase("Fulana", "da Silva", " ")]
        [TestCase("Fulana", "da Silva", null)]
        public async Task Should_Return_MissingRequiredInformationException_WhenDocsAreInvalid(string name, string surname, string email)
        {
            var guestDto = new GuestDTO
            {
                Name = name,
                Surname = surname,
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest
            {
                Data = guestDto,
            };


            // mock repo
            var fakeRepo = new Mock<IGuestRepository>();

            // methods
            fakeRepo.Setup(x => x.Create(
                It.IsAny<Guest>())).Returns(Task.FromResult(222));
            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodes.MISSING_REQUIRED_INFORMATION));
            Assert.That(res.Message, Is.EqualTo("Missing required information."));
        }

        [TestCase("Fulana")]
        [TestCase("Fulana.com.br")]
        [TestCase("Fulana @gmail.com")]
        [TestCase("Fulana@gmail")]
        public async Task Should_Return_InvalidEmailException_WhenDocsAreInvalid(string email)
        {
            var guestDto = new GuestDTO
            {
                Name = "Fulana",
                Surname = "da Silva",
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest
            {
                Data = guestDto,
            };


            // mock repo
            var fakeRepo = new Mock<IGuestRepository>();

            // methods
            fakeRepo.Setup(x => x.Create(
                It.IsAny<Guest>())).Returns(Task.FromResult(222));
            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodes.INVALID_EMAIL));
            Assert.That(res.Message, Is.EqualTo("The giving email is not valid."));
        }
    }
}