
using Business.Handlers.Vehicles.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Vehicles.Queries.GetVehicleQuery;
using Entities.Concrete;
using static Business.Handlers.Vehicles.Queries.GetVehiclesQuery;
using static Business.Handlers.Vehicles.Commands.CreateVehicleCommand;
using Business.Handlers.Vehicles.Commands;
using Business.Constants;
using static Business.Handlers.Vehicles.Commands.UpdateVehicleCommand;
using static Business.Handlers.Vehicles.Commands.DeleteVehicleCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class VehicleHandlerTests
    {
        Mock<IVehicleRepository> _vehicleRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _vehicleRepository = new Mock<IVehicleRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Vehicle_GetQuery_Success()
        {
            //Arrange
            var query = new GetVehicleQuery();

            _vehicleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Vehicle, bool>>>()))
                .ReturnsAsync(new Vehicle()
                {
                    Id = 1, // Assuming Id represents VehicleId
                    LicensePlate = "XYZ 1234",
                    Make = "Test Make",
                    Model = "Test Model",
                    Year = 2024,
                    VIN = "1HGBH41JXMN109186",
                    Mileage = 10000.0,
                    CustomerId = 1, // Sample CustomerId
                    Customer = new Customer() { /* set properties of Customer here */ },
                    ServiceRecords = new List<ServiceRecord>()
                    {
            new ServiceRecord() { /* set properties of ServiceRecord here */ }
                    },
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1, // Sample CreatedBy user Id
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = 1, // Sample UpdatedBy user Id
                    IsDeleted = false
                });


            var handler = new GetVehicleQueryHandler(_vehicleRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.VehicleId.Should().Be(1);

        }

        [Test]
        public async Task Vehicle_GetQueries_Success()
        {
            //Arrange
            var query = new GetVehiclesQuery();

            _vehicleRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Vehicle, bool>>>()))
                                .ReturnsAsync(new List<Vehicle>
                                {
                                    new Vehicle()
                                    {
                                        Id = 1,
                                        LicensePlate = "XYZ 1234",
                                        Make = "Test Make",
                                        Model = "Test Model",
                                        Year = 2024,
                                        VIN = "1HGBH41JXMN109186",
                                        Mileage = 10000.0,
                                        CustomerId = 1,
                                        Customer = new Customer() { /* set properties of Customer here */ },
                                        ServiceRecords = new List<ServiceRecord>()
                                        {
                                            new ServiceRecord() { /* set properties of ServiceRecord here */ }
                                        },
                                        CreatedAt = DateTime.Now,
                                        CreatedBy = 1,
                                        UpdatedAt = DateTime.Now,
                                        UpdatedBy = 1,
                                        IsDeleted = false
                                    },
                                    new Vehicle()
                                    {
                                        Id = 2,
                                        LicensePlate = "ABC 5678",
                                        Make = "Another Make",
                                        Model = "Another Model",
                                        Year = 2023,
                                        VIN = "2HGBH41JXMN109187",
                                        Mileage = 20000.0,
                                        CustomerId = 2,
                                        Customer = new Customer() { /* set properties of Customer here */ },
                                        ServiceRecords = new List<ServiceRecord>()
                                        {
                                            new ServiceRecord() { /* set properties of ServiceRecord here */ }
                                        },
                                        CreatedAt = DateTime.Now,
                                        CreatedBy = 2,
                                        UpdatedAt = DateTime.Now,
                                        UpdatedBy = 2,
                                        IsDeleted = false
                                    }
                                    // Add more Vehicle objects if needed
                                });


            var handler = new GetVehiclesQueryHandler(_vehicleRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Vehicle>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Vehicle_CreateCommand_Success()
        {
            Vehicle rt = null;
            //Arrange
            var command = new CreateVehicleCommand();
            //propertyler buraya yazılacak
            //command.VehicleName = "deneme";

            _vehicleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Vehicle, bool>>>()))
                        .ReturnsAsync(rt);

            _vehicleRepository.Setup(x => x.Add(It.IsAny<Vehicle>())).Returns(new Vehicle());

            var handler = new CreateVehicleCommandHandler(_vehicleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _vehicleRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Vehicle_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateVehicleCommand();
            //propertyler buraya yazılacak 
            //command.VehicleName = "test";

            _vehicleRepository.Setup(x => x.Query())
                                           .Returns(new List<Vehicle> { new Vehicle() { /*TODO:propertyler buraya yazılacak VehicleId = 1, VehicleName = "test"*/ } }.AsQueryable());

            _vehicleRepository.Setup(x => x.Add(It.IsAny<Vehicle>())).Returns(new Vehicle());

            var handler = new CreateVehicleCommandHandler(_vehicleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Vehicle_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateVehicleCommand();
            //command.VehicleName = "test";

            _vehicleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Vehicle, bool>>>()))
                        .ReturnsAsync(new Vehicle() { /*TODO:propertyler buraya yazılacak VehicleId = 1, VehicleName = "deneme"*/ });

            _vehicleRepository.Setup(x => x.Update(It.IsAny<Vehicle>())).Returns(new Vehicle());

            var handler = new UpdateVehicleCommandHandler(_vehicleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _vehicleRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Vehicle_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteVehicleCommand();

            _vehicleRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Vehicle, bool>>>()))
                        .ReturnsAsync(new Vehicle() { /*TODO:propertyler buraya yazılacak VehicleId = 1, VehicleName = "deneme"*/});

            _vehicleRepository.Setup(x => x.Delete(It.IsAny<Vehicle>()));

            var handler = new DeleteVehicleCommandHandler(_vehicleRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _vehicleRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

