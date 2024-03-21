
using Business.Handlers.Employees.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Employees.Queries.GetEmployeeQuery;
using Entities.Concrete;
using static Business.Handlers.Employees.Queries.GetEmployeesQuery;
using static Business.Handlers.Employees.Commands.CreateEmployeeCommand;
using Business.Handlers.Employees.Commands;
using Business.Constants;
using static Business.Handlers.Employees.Commands.UpdateEmployeeCommand;
using static Business.Handlers.Employees.Commands.DeleteEmployeeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using System.Threading;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class EmployeeHandlerTests
    {
        Mock<IEmployeeRepository> _employeeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Employee_GetQuery_Success()
        {
            //Arrange
            var query = new GetEmployeeQuery();

            _employeeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                               .ReturnsAsync(new Employee
                               {
                                   FirstName = "John",
                                   LastName = "Doe",
                                   Email = "john.doe@example.com",
                                   PhoneNumber = "1234567890",
                                   OrganizationId = 1
                               });

            var handler = new GetEmployeeQueryHandler(_employeeRepository.Object, _mediator.Object);

            //Act
            var result = await handler.Handle(query, new CancellationToken());

            //Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.FirstName.Should().Be("John");
        }


        [Test]
        public async Task Employee_GetQueries_Success()
        {
            //Arrange
            var query = new GetEmployeesQuery();

            _employeeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                               .ReturnsAsync(new List<Employee>
                               {
                           new Employee
                           {
                               FirstName = "John",
                               LastName = "Doe",
                               Email = "john.doe@example.com",
                               PhoneNumber = "1234567890",
                               OrganizationId = 1
                           },
                           new Employee
                           {
                               FirstName = "Jane",
                               LastName = "Doe",
                               Email = "jane.doe@example.com",
                               PhoneNumber = "0987654321",
                               OrganizationId = 2
                           }
                               });

            var handler = new GetEmployeesQueryHandler(_employeeRepository.Object, _mediator.Object);

            //Act
            var result = await handler.Handle(query, new CancellationToken());

            //Assert
            result.Success.Should().BeTrue();
            ((List<Employee>)result.Data).Count.Should().Be(2);
        }

        [Test]
        public async Task Employee_CreateCommand_Success()
        {
            //Arrange
            var command = new CreateEmployeeCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                OrganizationId = 1,
                Salary = 50000M
            };

            Employee addedEmployee = null;

            _employeeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                               .ReturnsAsync((Employee)null);

            _employeeRepository.Setup(x => x.Add(It.IsAny<Employee>()))
                               .Callback<Employee>(emp => addedEmployee = emp)
                               .Returns(() => addedEmployee);

            var handler = new CreateEmployeeCommandHandler(_employeeRepository.Object, _mediator.Object);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            _employeeRepository.Verify(x => x.SaveChangesAsync());
            result.Success.Should().BeTrue();
            result.Message.Should().Be(Messages.Added);
        }



        [Test]
        public async Task Employee_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateEmployeeCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                OrganizationId = 1,
                Salary = 50000M
            };

            _employeeRepository.Setup(x => x.Query())
                               .Returns(new List<Employee>
                                {
                            new Employee
                            {
                                FirstName = "John",
                                LastName = "Doe",
                                Email = "john.doe@example.com",
                                PhoneNumber = "1234567890",
                                OrganizationId = 1,
                                Salary = 50000M
                            }
                                }.AsQueryable());

            _employeeRepository.Setup(x => x.Add(It.IsAny<Employee>())).Returns(new Employee());

            var handler = new CreateEmployeeCommandHandler(_employeeRepository.Object, _mediator.Object);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be(Messages.NameAlreadyExist);
        }



        [Test]
        public async Task Employee_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateEmployeeCommand();
            //command.EmployeeName = "test";

            _employeeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                        .ReturnsAsync(new Employee() { /*TODO:propertyler buraya yazılacak EmployeeId = 1, EmployeeName = "deneme"*/ });

            _employeeRepository.Setup(x => x.Update(It.IsAny<Employee>())).Returns(new Employee());

            var handler = new UpdateEmployeeCommandHandler(_employeeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _employeeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Employee_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteEmployeeCommand();

            _employeeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                        .ReturnsAsync(new Employee() { /*TODO:propertyler buraya yazılacak EmployeeId = 1, EmployeeName = "deneme"*/});

            _employeeRepository.Setup(x => x.Delete(It.IsAny<Employee>()));

            var handler = new DeleteEmployeeCommandHandler(_employeeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _employeeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

