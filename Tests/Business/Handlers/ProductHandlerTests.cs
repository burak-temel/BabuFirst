
using Business.Handlers.Products.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Products.Queries.GetProductQuery;
using Entities.Concrete;
using static Business.Handlers.Products.Queries.GetProductsQuery;
using static Business.Handlers.Products.Commands.CreateProductCommand;
using Business.Handlers.Products.Commands;
using Business.Constants;
using static Business.Handlers.Products.Commands.UpdateProductCommand;
using static Business.Handlers.Products.Commands.DeleteProductCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using Core.CrossCuttingConcerns.Context;
using Core.Extensions;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ProductHandlerTests
    {
        Mock<IProductRepository> _productRepository;
        Mock<IMediator> _mediator;
        Mock<IAppContextService> _appContextService;

        [SetUp]
        public void Setup()
        {
            _productRepository = new Mock<IProductRepository>();
            _mediator = new Mock<IMediator>();
            _appContextService = new Mock<IAppContextService>();

            var appContextInstance = new BabuAppContext
            {
                UserId = 1,
                OrganizationId = 1
            };

            _appContextService.Setup(a => a.GetAppContext()).Returns(appContextInstance);
        }

        [Test]
        public async Task Product_GetQuery_Success()
        {
            //Arrange
            var query = new GetProductQuery();

            _productRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                                .ReturnsAsync(new Product()
                                {
                                    Id = 1, // Assuming Id represents ProductId
                                    Name = "Test Product",
                                    Price = 99.99m,
                                    TaxRateId = 1,
                                    TaxRate = new TaxRate() { /* set properties of TaxRate here */ },
                                    ServiceItems = new List<ServiceItem>()
                                    {
                                        new ServiceItem() { /* set properties of ServiceItem here */ }
                                    },
                                    OrganizationId = 1,
                                    CreatedAt = DateTime.Now,
                                    CreatedBy = 1,
                                    UpdatedAt = DateTime.Now,
                                    UpdatedBy = 1,
                                    IsDeleted = false
                                });


            var handler = new GetProductQueryHandler(_productRepository.Object, _mediator.Object, _appContextService.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ProductId.Should().Be(1);

        }

        [Test]
        public async Task Product_GetQueries_Success()
        {
            //Arrange
            var query = new GetProductsQuery();

            _productRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                                .ReturnsAsync(new List<Product>
                                {
                                    new Product()
                                    {
                                        Id = 1,
                                        Name = "Test Product 1",
                                        Price = 99.99m,
                                        TaxRateId = 1,
                                        TaxRate = new TaxRate() { /* set properties of TaxRate here */ },
                                        ServiceItems = new List<ServiceItem>()
                                        {
                                            new ServiceItem() { /* set properties of ServiceItem here */ }
                                        },
                                        OrganizationId = 1
                                        ,
                                        CreatedAt = DateTime.Now,
                                        CreatedBy = 1,
                                        UpdatedAt = DateTime.Now,
                                        UpdatedBy = 1,
                                        IsDeleted = false
                                    },
                                    new Product()
                                    {
                                        Id = 2,
                                        Name = "Test Product 2",
                                        Price = 199.99m,
                                        TaxRateId = 2,
                                        TaxRate = new TaxRate() { /* set properties of TaxRate here */ },
                                        ServiceItems = new List<ServiceItem>()
                                        {
                                            new ServiceItem() { /* set properties of ServiceItem here */ }
                                        },
                                        OrganizationId = 1,
                                        CreatedAt = DateTime.Now,
                                        CreatedBy = 2,
                                        UpdatedAt = DateTime.Now,
                                        UpdatedBy = 2,
                                        IsDeleted = false
                                    }
                                    // Add more Product objects if needed
                                });

            var handler = new GetProductsQueryHandler(_productRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Product>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Product_CreateCommand_Success()
        {
            Product rt = null;
            //Arrange
            var command = new CreateProductCommand();
            //propertyler buraya yazılacak
            //command.ProductName = "deneme";

            _productRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                        .ReturnsAsync(rt);

            _productRepository.Setup(x => x.Add(It.IsAny<Product>())).Returns(new Product());

            var handler = new CreateProductCommandHandler(_productRepository.Object, _mediator.Object, _appContextService.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _productRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Product_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateProductCommand();
            //propertyler buraya yazılacak 
            //command.ProductName = "test";

            _productRepository.Setup(x => x.Query())
                                           .Returns(new List<Product> { new Product() { /*TODO:propertyler buraya yazılacak ProductId = 1, ProductName = "test"*/ } }.AsQueryable());

            _productRepository.Setup(x => x.Add(It.IsAny<Product>())).Returns(new Product());

            var handler = new CreateProductCommandHandler(_productRepository.Object, _mediator.Object, _appContextService.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Product_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateProductCommand();
            //command.ProductName = "test";

            _productRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                        .ReturnsAsync(new Product() { /*TODO:propertyler buraya yazılacak ProductId = 1, ProductName = "deneme"*/ });

            _productRepository.Setup(x => x.Update(It.IsAny<Product>())).Returns(new Product());

            var handler = new UpdateProductCommandHandler(_productRepository.Object, _mediator.Object, _appContextService.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _productRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Product_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteProductCommand() { Id = 1};

            _productRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                        .ReturnsAsync(new Product()
                        {
                            Id = 1,
                            Name = "Test Product 1",
                            Price = 99.99m,
                            TaxRateId = 1,
                            TaxRate = new TaxRate() { /* set properties of TaxRate here */ },
                            ServiceItems = new List<ServiceItem>()
                                        {
                                            new ServiceItem() { /* set properties of ServiceItem here */ }
                                        },
                            OrganizationId = 1
                                        ,
                            CreatedAt = DateTime.Now,
                            CreatedBy = 1,
                            UpdatedAt = DateTime.Now,
                            UpdatedBy = 1,
                            IsDeleted = false
                        });

            _productRepository.Setup(x => x.Delete(It.IsAny<Product>()));

            var handler = new DeleteProductCommandHandler(_productRepository.Object, _mediator.Object, _appContextService.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _productRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

