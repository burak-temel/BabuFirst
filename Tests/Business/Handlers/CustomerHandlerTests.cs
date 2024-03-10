
using Business.Handlers.Customers.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Customers.Queries.GetCustomerQuery;
using Entities.Concrete;
using static Business.Handlers.Customers.Queries.GetCustomersQuery;
using static Business.Handlers.Customers.Commands.CreateCustomerCommand;
using Business.Handlers.Customers.Commands;
using Business.Constants;
using static Business.Handlers.Customers.Commands.UpdateCustomerCommand;
using static Business.Handlers.Customers.Commands.DeleteCustomerCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using System.Threading;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class CustomerHandlerTests
    {
        Mock<ICustomerRepository> _customerRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Customer_GetQuery_Success()
        {
            //Arrange
            int testOrganizationId = 1;
            var query = new GetCustomerQuery { OrganizationId = testOrganizationId };

            _customerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                               .ReturnsAsync(new Customer
                               {
                                   FirstName = "John",
                                   LastName = "Doe",
                                   Email = "john.doe@example.com",
                                   PhoneNumber = "1234567890",
                                   OrganizationId = testOrganizationId
                               });

            var handler = new GetCustomerQueryHandler(_customerRepository.Object, _mediator.Object);

            //Act
            var result = await handler.Handle(query, new CancellationToken());

            //Assert
            result.Success.Should().BeTrue();
            result.Data.OrganizationId.Should().Be(testOrganizationId);
        }


        [Test]
        public async Task Customer_GetQueries_Success()
        {
            //Arrange
            var query = new GetCustomersQuery();

            _customerRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                               .ReturnsAsync(new List<Customer>
                               {
                           new Customer
                           {
                               FirstName = "John",
                               LastName = "Doe",
                               Email = "john.doe@example.com",
                               PhoneNumber = "1234567890",
                               OrganizationId = 1
                           },
                           new Customer
                           {
                               FirstName = "Jane",
                               LastName = "Doe",
                               Email = "jane.doe@example.com",
                               PhoneNumber = "0987654321",
                               OrganizationId = 2
                           }
                               });

            var handler = new GetCustomersQueryHandler(_customerRepository.Object, _mediator.Object);

            //Act
            var result = await handler.Handle(query, new CancellationToken());

            //Assert
            result.Success.Should().BeTrue();
            ((List<Customer>)result.Data).Count.Should().Be(2);
        }


        [Test]
        public async Task Customer_CreateCommand_Success()
        {
            Customer rt = null;
            //Arrange
            var command = new CreateCustomerCommand();
            //propertyler buraya yazılacak
            //command.CustomerName = "deneme";

            _customerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                        .ReturnsAsync(rt);

            _customerRepository.Setup(x => x.Add(It.IsAny<Customer>())).Returns(new Customer());

            var handler = new CreateCustomerCommandHandler(_customerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Customer_CreateCommand_CustomerAlreadyExist()
        {
            //Arrange
            var command = new CreateCustomerCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                OrganizationId = 1
            };

            _customerRepository.Setup(x => x.Query())
                               .Returns(new List<Customer> {
                           new Customer {
                               FirstName = "John",
                               LastName = "Doe",
                               Email = "john.doe@example.com",
                               PhoneNumber = "1234567890",
                               OrganizationId = 1,
                               CreatedAt = DateTime.UtcNow
                           }
                               }.AsQueryable());

            _customerRepository.Setup(x => x.Add(It.IsAny<Customer>())).Returns(new Customer());

            var handler = new CreateCustomerCommandHandler(_customerRepository.Object, _mediator.Object);
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Customer_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateCustomerCommand();
            //command.CustomerName = "test";

            _customerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                        .ReturnsAsync(new Customer() { /*TODO:propertyler buraya yazılacak CustomerId = 1, CustomerName = "deneme"*/ });

            _customerRepository.Setup(x => x.Update(It.IsAny<Customer>())).Returns(new Customer());

            var handler = new UpdateCustomerCommandHandler(_customerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Customer_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteCustomerCommand();

            _customerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                        .ReturnsAsync(new Customer() { /*TODO:propertyler buraya yazılacak CustomerId = 1, CustomerName = "deneme"*/});

            _customerRepository.Setup(x => x.Delete(It.IsAny<Customer>()));

            var handler = new DeleteCustomerCommandHandler(_customerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

