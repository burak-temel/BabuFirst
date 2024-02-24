
using Business.Handlers.ServiceItems.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ServiceItems.Queries.GetServiceItemQuery;
using Entities.Concrete;
using static Business.Handlers.ServiceItems.Queries.GetServiceItemsQuery;
using static Business.Handlers.ServiceItems.Commands.CreateServiceItemCommand;
using Business.Handlers.ServiceItems.Commands;
using Business.Constants;
using static Business.Handlers.ServiceItems.Commands.UpdateServiceItemCommand;
using static Business.Handlers.ServiceItems.Commands.DeleteServiceItemCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ServiceItemHandlerTests
    {
        Mock<IServiceItemRepository> _serviceItemRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _serviceItemRepository = new Mock<IServiceItemRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ServiceItem_GetQuery_Success()
        {
            //Arrange
            var query = new GetServiceItemQuery();

            _serviceItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ServiceItem, bool>>>()))
                    .ReturnsAsync(new ServiceItem()
                    {
                        ServiceRecordId = 1, // Sample ServiceRecordId
                        ServiceRecord = new ServiceRecord() { /* set properties of ServiceRecord here */ },
                        ProductId = 1, // Sample ProductId
                        Product = new Product() { /* set properties of Product here */ },
                        Quantity = 5,
                        UnitPrice = 50.00m
                    });


            var handler = new GetServiceItemQueryHandler(_serviceItemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ServiceItemId.Should().Be(1);

        }

        [Test]
        public async Task ServiceItem_GetQueries_Success()
        {
            //Arrange
            var query = new GetServiceItemsQuery();

            _serviceItemRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ServiceItem, bool>>>()))
                            .ReturnsAsync(new List<ServiceItem>
                            {
                                new ServiceItem()
                                {
                                    ServiceRecordId = 1,
                                    ServiceRecord = new ServiceRecord() { /* set properties of ServiceRecord here */ },
                                    ProductId = 1,
                                    Product = new Product() { /* set properties of Product here */ },
                                    Quantity = 5,
                                    UnitPrice = 50.00m
                                },
                                new ServiceItem()
                                {
                                    ServiceRecordId = 2,
                                    ServiceRecord = new ServiceRecord() { /* set properties of ServiceRecord here */ },
                                    ProductId = 2,
                                    Product = new Product() { /* set properties of Product here */ },
                                    Quantity = 10,
                                    UnitPrice = 100.00m
                                }
                                // Add more ServiceItem objects if needed
                            });

            var handler = new GetServiceItemsQueryHandler(_serviceItemRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ServiceItem>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ServiceItem_CreateCommand_Success()
        {
            ServiceItem rt = null;
            //Arrange
            var command = new CreateServiceItemCommand();
            //propertyler buraya yazılacak
            //command.ServiceItemName = "deneme";

            _serviceItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ServiceItem, bool>>>()))
                        .ReturnsAsync(rt);

            _serviceItemRepository.Setup(x => x.Add(It.IsAny<ServiceItem>())).Returns(new ServiceItem());

            var handler = new CreateServiceItemCommandHandler(_serviceItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _serviceItemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ServiceItem_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateServiceItemCommand();
            //propertyler buraya yazılacak 
            //command.ServiceItemName = "test";

            _serviceItemRepository.Setup(x => x.Query())
                                           .Returns(new List<ServiceItem> { new ServiceItem() { /*TODO:propertyler buraya yazılacak ServiceItemId = 1, ServiceItemName = "test"*/ } }.AsQueryable());

            _serviceItemRepository.Setup(x => x.Add(It.IsAny<ServiceItem>())).Returns(new ServiceItem());

            var handler = new CreateServiceItemCommandHandler(_serviceItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ServiceItem_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateServiceItemCommand();
            //command.ServiceItemName = "test";

            _serviceItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ServiceItem, bool>>>()))
                        .ReturnsAsync(new ServiceItem() { /*TODO:propertyler buraya yazılacak ServiceItemId = 1, ServiceItemName = "deneme"*/ });

            _serviceItemRepository.Setup(x => x.Update(It.IsAny<ServiceItem>())).Returns(new ServiceItem());

            var handler = new UpdateServiceItemCommandHandler(_serviceItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _serviceItemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ServiceItem_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteServiceItemCommand();

            _serviceItemRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ServiceItem, bool>>>()))
                        .ReturnsAsync(new ServiceItem() { /*TODO:propertyler buraya yazılacak ServiceItemId = 1, ServiceItemName = "deneme"*/});

            _serviceItemRepository.Setup(x => x.Delete(It.IsAny<ServiceItem>()));

            var handler = new DeleteServiceItemCommandHandler(_serviceItemRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _serviceItemRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

