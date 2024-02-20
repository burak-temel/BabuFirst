
using Business.Handlers.ServiceRecords.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ServiceRecords.Queries.GetServiceRecordQuery;
using Entities.Concrete;
using static Business.Handlers.ServiceRecords.Queries.GetServiceRecordsQuery;
using static Business.Handlers.ServiceRecords.Commands.CreateServiceRecordCommand;
using Business.Handlers.ServiceRecords.Commands;
using Business.Constants;
using static Business.Handlers.ServiceRecords.Commands.UpdateServiceRecordCommand;
using static Business.Handlers.ServiceRecords.Commands.DeleteServiceRecordCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ServiceRecordHandlerTests
    {
        Mock<IServiceRecordRepository> _serviceRecordRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _serviceRecordRepository = new Mock<IServiceRecordRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ServiceRecord_GetQuery_Success()
        {
            //Arrange
            var query = new GetServiceRecordQuery();

            _serviceRecordRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ServiceRecord, bool>>>())).ReturnsAsync(new ServiceRecord()
//propertyler buraya yazılacak
//{																		
//ServiceRecordId = 1,
//ServiceRecordName = "Test"
//}
);

            var handler = new GetServiceRecordQueryHandler(_serviceRecordRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ServiceRecordId.Should().Be(1);

        }

        [Test]
        public async Task ServiceRecord_GetQueries_Success()
        {
            //Arrange
            var query = new GetServiceRecordsQuery();

            _serviceRecordRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ServiceRecord, bool>>>()))
                        .ReturnsAsync(new List<ServiceRecord> { new ServiceRecord() { /*TODO:propertyler buraya yazılacak ServiceRecordId = 1, ServiceRecordName = "test"*/ } });

            var handler = new GetServiceRecordsQueryHandler(_serviceRecordRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ServiceRecord>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ServiceRecord_CreateCommand_Success()
        {
            ServiceRecord rt = null;
            //Arrange
            var command = new CreateServiceRecordCommand();
            //propertyler buraya yazılacak
            //command.ServiceRecordName = "deneme";

            _serviceRecordRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ServiceRecord, bool>>>()))
                        .ReturnsAsync(rt);

            _serviceRecordRepository.Setup(x => x.Add(It.IsAny<ServiceRecord>())).Returns(new ServiceRecord());

            var handler = new CreateServiceRecordCommandHandler(_serviceRecordRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _serviceRecordRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ServiceRecord_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateServiceRecordCommand();
            //propertyler buraya yazılacak 
            //command.ServiceRecordName = "test";

            _serviceRecordRepository.Setup(x => x.Query())
                                           .Returns(new List<ServiceRecord> { new ServiceRecord() { /*TODO:propertyler buraya yazılacak ServiceRecordId = 1, ServiceRecordName = "test"*/ } }.AsQueryable());

            _serviceRecordRepository.Setup(x => x.Add(It.IsAny<ServiceRecord>())).Returns(new ServiceRecord());

            var handler = new CreateServiceRecordCommandHandler(_serviceRecordRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ServiceRecord_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateServiceRecordCommand();
            //command.ServiceRecordName = "test";

            _serviceRecordRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ServiceRecord, bool>>>()))
                        .ReturnsAsync(new ServiceRecord() { /*TODO:propertyler buraya yazılacak ServiceRecordId = 1, ServiceRecordName = "deneme"*/ });

            _serviceRecordRepository.Setup(x => x.Update(It.IsAny<ServiceRecord>())).Returns(new ServiceRecord());

            var handler = new UpdateServiceRecordCommandHandler(_serviceRecordRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _serviceRecordRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ServiceRecord_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteServiceRecordCommand();

            _serviceRecordRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ServiceRecord, bool>>>()))
                        .ReturnsAsync(new ServiceRecord() { /*TODO:propertyler buraya yazılacak ServiceRecordId = 1, ServiceRecordName = "deneme"*/});

            _serviceRecordRepository.Setup(x => x.Delete(It.IsAny<ServiceRecord>()));

            var handler = new DeleteServiceRecordCommandHandler(_serviceRecordRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _serviceRecordRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

