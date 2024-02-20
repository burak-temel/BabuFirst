
using Business.Handlers.People.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.People.Queries.GetPersonQuery;
using Entities.Concrete;
using static Business.Handlers.People.Queries.GetPeopleQuery;
using static Business.Handlers.People.Commands.CreatePersonCommand;
using Business.Handlers.People.Commands;
using Business.Constants;
using static Business.Handlers.People.Commands.UpdatePersonCommand;
using static Business.Handlers.People.Commands.DeletePersonCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class PersonHandlerTests
    {
        Mock<IPersonRepository> _personRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _personRepository = new Mock<IPersonRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Person_GetQuery_Success()
        {
            //Arrange
            var query = new GetPersonQuery();

            _personRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Person, bool>>>())).ReturnsAsync(new Person()
//propertyler buraya yazılacak
//{																		
//PersonId = 1,
//PersonName = "Test"
//}
);

            var handler = new GetPersonQueryHandler(_personRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PersonId.Should().Be(1);

        }

        [Test]
        public async Task Person_GetQueries_Success()
        {
            //Arrange
            var query = new GetPeopleQuery();

            _personRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                        .ReturnsAsync(new List<Person> { new Person() { /*TODO:propertyler buraya yazılacak PersonId = 1, PersonName = "test"*/ } });

            var handler = new GetPeopleQueryHandler(_personRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Person>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Person_CreateCommand_Success()
        {
            Person rt = null;
            //Arrange
            var command = new CreatePersonCommand();
            //propertyler buraya yazılacak
            //command.PersonName = "deneme";

            _personRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                        .ReturnsAsync(rt);

            _personRepository.Setup(x => x.Add(It.IsAny<Person>())).Returns(new Person());

            var handler = new CreatePersonCommandHandler(_personRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _personRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Person_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePersonCommand();
            //propertyler buraya yazılacak 
            //command.PersonName = "test";

            _personRepository.Setup(x => x.Query())
                                           .Returns(new List<Person> { new Person() { /*TODO:propertyler buraya yazılacak PersonId = 1, PersonName = "test"*/ } }.AsQueryable());

            _personRepository.Setup(x => x.Add(It.IsAny<Person>())).Returns(new Person());

            var handler = new CreatePersonCommandHandler(_personRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Person_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePersonCommand();
            //command.PersonName = "test";

            _personRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                        .ReturnsAsync(new Person() { /*TODO:propertyler buraya yazılacak PersonId = 1, PersonName = "deneme"*/ });

            _personRepository.Setup(x => x.Update(It.IsAny<Person>())).Returns(new Person());

            var handler = new UpdatePersonCommandHandler(_personRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _personRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Person_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePersonCommand();

            _personRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                        .ReturnsAsync(new Person() { /*TODO:propertyler buraya yazılacak PersonId = 1, PersonName = "deneme"*/});

            _personRepository.Setup(x => x.Delete(It.IsAny<Person>()));

            var handler = new DeletePersonCommandHandler(_personRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _personRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

