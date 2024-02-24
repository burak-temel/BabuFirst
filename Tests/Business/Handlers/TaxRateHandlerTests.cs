
using Business.Handlers.TaxRates.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.TaxRates.Queries.GetTaxRateQuery;
using Entities.Concrete;
using static Business.Handlers.TaxRates.Queries.GetTaxRatesQuery;
using static Business.Handlers.TaxRates.Commands.CreateTaxRateCommand;
using Business.Handlers.TaxRates.Commands;
using Business.Constants;
using static Business.Handlers.TaxRates.Commands.UpdateTaxRateCommand;
using static Business.Handlers.TaxRates.Commands.DeleteTaxRateCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class TaxRateHandlerTests
    {
        Mock<ITaxRateRepository> _taxRateRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _taxRateRepository = new Mock<ITaxRateRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task TaxRate_GetQuery_Success()
        {
            //Arrange
            var query = new GetTaxRateQuery();

            _taxRateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<TaxRate, bool>>>()))
                            .ReturnsAsync(new TaxRate()
                            {
                                Id = 1, // TaxRate Id
                                Description = "Standard Rate",
                                Rate = 15.00m, // Sample tax rate
                                Products = new List<Product>()
                                {
                                    new Product() { /* set properties of Product here */ }
                                },
                                CreatedAt = DateTime.Now,
                                CreatedBy = 1, // Sample CreatedBy user Id
                                UpdatedAt = DateTime.Now,
                                UpdatedBy = 1, // Sample UpdatedBy user Id
                                IsDeleted = false
                            });


            var handler = new GetTaxRateQueryHandler(_taxRateRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.TaxRateId.Should().Be(1);

        }

        [Test]
        public async Task TaxRate_GetQueries_Success()
        {
            //Arrange
            var query = new GetTaxRatesQuery();

            _taxRateRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<TaxRate, bool>>>()))
                                .ReturnsAsync(new List<TaxRate>
                                {
                                    new TaxRate()
                                    {
                                        Id = 1,
                                        Description = "Standard Rate",
                                        Rate = 15.00m,
                                        Products = new List<Product>()
                                        {
                                            new Product() { /* set properties of Product here */ }
                                        },
                                        CreatedAt = DateTime.Now,
                                        CreatedBy = 1,
                                        UpdatedAt = DateTime.Now,
                                        UpdatedBy = 1,
                                        IsDeleted = false
                                    },
                                    new TaxRate()
                                    {
                                        Id = 2,
                                        Description = "Reduced Rate",
                                        Rate = 10.00m,
                                        Products = new List<Product>()
                                        {
                                            new Product() { /* set properties of Product here */ }
                                        },
                                        CreatedAt = DateTime.Now,
                                        CreatedBy = 2,
                                        UpdatedAt = DateTime.Now,
                                        UpdatedBy = 2,
                                        IsDeleted = false
                                    }
                                    // Add more TaxRate objects if needed
                                });

            var handler = new GetTaxRatesQueryHandler(_taxRateRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<TaxRate>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task TaxRate_CreateCommand_Success()
        {
            TaxRate rt = null;
            //Arrange
            var command = new CreateTaxRateCommand();
            //propertyler buraya yazılacak
            //command.TaxRateName = "deneme";

            _taxRateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<TaxRate, bool>>>()))
                        .ReturnsAsync(rt);

            _taxRateRepository.Setup(x => x.Add(It.IsAny<TaxRate>())).Returns(new TaxRate());

            var handler = new CreateTaxRateCommandHandler(_taxRateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _taxRateRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task TaxRate_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateTaxRateCommand();
            //propertyler buraya yazılacak 
            //command.TaxRateName = "test";

            _taxRateRepository.Setup(x => x.Query())
                                           .Returns(new List<TaxRate> { new TaxRate() { /*TODO:propertyler buraya yazılacak TaxRateId = 1, TaxRateName = "test"*/ } }.AsQueryable());

            _taxRateRepository.Setup(x => x.Add(It.IsAny<TaxRate>())).Returns(new TaxRate());

            var handler = new CreateTaxRateCommandHandler(_taxRateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task TaxRate_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateTaxRateCommand();
            //command.TaxRateName = "test";

            _taxRateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<TaxRate, bool>>>()))
                        .ReturnsAsync(new TaxRate() { /*TODO:propertyler buraya yazılacak TaxRateId = 1, TaxRateName = "deneme"*/ });

            _taxRateRepository.Setup(x => x.Update(It.IsAny<TaxRate>())).Returns(new TaxRate());

            var handler = new UpdateTaxRateCommandHandler(_taxRateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _taxRateRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task TaxRate_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteTaxRateCommand();

            _taxRateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<TaxRate, bool>>>()))
                        .ReturnsAsync(new TaxRate() { /*TODO:propertyler buraya yazılacak TaxRateId = 1, TaxRateName = "deneme"*/});

            _taxRateRepository.Setup(x => x.Delete(It.IsAny<TaxRate>()));

            var handler = new DeleteTaxRateCommandHandler(_taxRateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _taxRateRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

