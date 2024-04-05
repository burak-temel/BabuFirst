
using Business.Handlers.Organizations.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Organizations.Queries.GetOrganizationQuery;
using Entities.Concrete;
using static Business.Handlers.Organizations.Queries.GetOrganizationsQuery;
using static Business.Handlers.Organizations.Commands.CreateOrganizationCommand;
using Business.Handlers.Organizations.Commands;
using Business.Constants;
using static Business.Handlers.Organizations.Commands.UpdateOrganizationCommand;
using static Business.Handlers.Organizations.Commands.DeleteOrganizationCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using Core.Entities.Concrete;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OrganizationHandlerTests
    {
        Mock<IOrganizationRepository> _organizationRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _organizationRepository = new Mock<IOrganizationRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Organization_GetQuery_Success()
        {
            //Arrange
            var query = new GetOrganizationQuery();
            _organizationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Organization, bool>>>())).ReturnsAsync(new Organization()
            {
                Id = 1, // Assuming Id represents OrganizationId
                Name = "Test Organization",
                Address = "123 Test Street",

                CreatedAt = DateTime.Now,
                CreatedBy = 1, // Sample CreatedBy user Id
                UpdatedAt = DateTime.Now,
                UpdatedBy = 1, // Sample UpdatedBy user Id
                IsDeleted = false
            });

            var handler = new GetOrganizationQueryHandler(_organizationRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OrganizationId.Should().Be(1);

        }

        [Test]
        public async Task Organization_GetQueries_Success()
        {
            //Arrange
            var query = new GetOrganizationsQuery();

            _organizationRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Organization, bool>>>()))
                .ReturnsAsync(new List<Organization>
                {
                new Organization()
                {
                    Id = 1, // Assuming Id represents OrganizationId
                    Name = "Test Organization 1",
                    Address = "123 Test Street",

                    CreatedAt = DateTime.Now,
                    CreatedBy = 1, // Sample CreatedBy user Id
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = 1, // Sample UpdatedBy user Id
                    IsDeleted = false
                },
                new Organization()
                {
                    Id = 2, // Different OrganizationId for the second organization
                    Name = "Test Organization 2",
                    Address = "456 Another Street",

                    CreatedAt = DateTime.Now,
                    CreatedBy = 2, // Different CreatedBy user Id
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = 2, // Different UpdatedBy user Id
                    IsDeleted = false
                }
                });


            var handler = new GetOrganizationsQueryHandler(_organizationRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Organization>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Organization_CreateCommand_Success()
        {
            Organization rt = null;
            //Arrange
            var command = new CreateOrganizationCommand();
            //propertyler buraya yazılacak
            //command.OrganizationName = "deneme";

            _organizationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Organization, bool>>>()))
                        .ReturnsAsync(rt);

            _organizationRepository.Setup(x => x.Add(It.IsAny<Organization>())).Returns(new Organization());

            var handler = new CreateOrganizationCommandHandler(_organizationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _organizationRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Organization_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOrganizationCommand();
            //propertyler buraya yazılacak 
            //command.OrganizationName = "test";

            _organizationRepository.Setup(x => x.Query())
                                           .Returns(new List<Organization> { new Organization() { /*TODO:propertyler buraya yazılacak OrganizationId = 1, OrganizationName = "test"*/ } }.AsQueryable());

            _organizationRepository.Setup(x => x.Add(It.IsAny<Organization>())).Returns(new Organization());

            var handler = new CreateOrganizationCommandHandler(_organizationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Organization_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOrganizationCommand();
            //command.OrganizationName = "test";

            _organizationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Organization, bool>>>()))
                        .ReturnsAsync(new Organization() { /*TODO:propertyler buraya yazılacak OrganizationId = 1, OrganizationName = "deneme"*/ });

            _organizationRepository.Setup(x => x.Update(It.IsAny<Organization>())).Returns(new Organization());

            var handler = new UpdateOrganizationCommandHandler(_organizationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _organizationRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Organization_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOrganizationCommand();

            _organizationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Organization, bool>>>()))
                        .ReturnsAsync(new Organization() { /*TODO:propertyler buraya yazılacak OrganizationId = 1, OrganizationName = "deneme"*/});

            _organizationRepository.Setup(x => x.Delete(It.IsAny<Organization>()));

            var handler = new DeleteOrganizationCommandHandler(_organizationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _organizationRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

