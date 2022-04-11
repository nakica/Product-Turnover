using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductTurnover.Infra;
using ProductTurnover.Infra.Record;
using ProductTurnover.Rest;
using ProductTurnover.Rest.Controllers;
using System;
using Xunit;
using static Moq.It;

namespace ProductTurnover.Xunit
{
    public class ProductControllerUnitTests
    {
        private readonly Mock<ILoggingFacility<ProductController>> _logMoq = new Mock<ILoggingFacility<ProductController>>();
        private readonly Mock<IProductRepository> _productRepoMoq = new Mock<IProductRepository>();
        private readonly Mock<IProductTurnoverCache> _cacheMoq = new Mock<IProductTurnoverCache>();
        private readonly Mock<ITaxation> _taxationMoq = new Mock<ITaxation>();

        /// <summary>
        /// Test if <see cref="OkResult"/> with <see cref="ProductTurnoverBreakdown"/> is returned.
        /// </summary>
        [Fact]
        public void CalculateNetTurnover()
        {
            var vat = 0.1M;
            var grossTurnover = 1000;
            var netTurnover = 900;

            var turnover = new Rest.ProductTurnover
            {
                DateCreated = DateTime.Now,
                EAN = "112233445566",
                GrossTurnover = grossTurnover,
                ProductName = "test product"
            };

            var product = new Product
            {
                Category = new Category { Name = "test category", VAT = vat },
                EAN = "112233445566",
                Name = "test product",
                Price = 10
            };

            _productRepoMoq.Setup(prm => prm.Read(IsAny<string>())).Returns(product).Verifiable();
            _taxationMoq.Setup(tm => tm.CalculateNetTurnover(IsAny<decimal>(), IsAny<decimal>())).Returns(netTurnover).Verifiable();

            var sut = new ProductController(_logMoq.Object, _cacheMoq.Object, _productRepoMoq.Object, _taxationMoq.Object);

            var test = sut.NetTurnover(turnover);

            _productRepoMoq.Verify();
            _taxationMoq.Verify();
            var result = Assert.IsAssignableFrom<OkObjectResult>(test);
            var turnoverBreakdown = (ProductTurnoverBreakdown)result.Value;
            Assert.Equal(grossTurnover, turnoverBreakdown.GrossTurnover);
            Assert.Equal(netTurnover, turnoverBreakdown.NetTurnover);
            Assert.Equal(vat, turnoverBreakdown.VAT);
        }

        /// <summary>
        /// Tests if <see cref="BadRequestResult"/> is returned if <see cref="Rest.ProductTurnover"/> is invalid.
        /// </summary>
        [Fact]
        public void NetTurnoverFailsOnInvalidProductTurnover()
        {
            var sut = new ProductController(_logMoq.Object, _cacheMoq.Object, _productRepoMoq.Object, _taxationMoq.Object);

            var nullProductTurnoverTest = sut.NetTurnover(null);
            var missingProductNameTest = sut.NetTurnover(new Rest.ProductTurnover());
            var missingProductEanTest = sut.NetTurnover(new Rest.ProductTurnover { ProductName = "name" });
            var invalidProductEanTest = sut.NetTurnover(new Rest.ProductTurnover { ProductName = "name", EAN = "0" });
            var invalidProductEanTest1 = sut.NetTurnover(new Rest.ProductTurnover { ProductName = "name", EAN = "11223344556677" });

            Assert.IsAssignableFrom<BadRequestResult>(nullProductTurnoverTest);
            Assert.IsAssignableFrom<BadRequestResult>(missingProductNameTest);
            Assert.IsAssignableFrom<BadRequestResult>(missingProductEanTest);
            Assert.IsAssignableFrom<BadRequestResult>(invalidProductEanTest);
            Assert.IsAssignableFrom<BadRequestResult>(invalidProductEanTest1);
        }

        /// <summary>
        /// Test if <see cref="NotFoundResult"/> is returned if <see cref="Rest.ProductTurnover.EAN"/> is valid but non existent.
        /// </summary>
        [Fact]
        public void NetTurnoverFailsOnNonExistentProductEan()
        {
            var turnover = new Rest.ProductTurnover
            {
                DateCreated = DateTime.Now,
                EAN = "665544332211",
                GrossTurnover = 5000,
                ProductName = "missing product"
            };

            Product prod = null;

            _productRepoMoq.Setup(prm => prm.Read(IsAny<string>())).Returns(prod).Verifiable();

            var sut = new ProductController(_logMoq.Object, _cacheMoq.Object, _productRepoMoq.Object, _taxationMoq.Object);

            var test = sut.NetTurnover(turnover);

            _productRepoMoq.Verify();
            Assert.IsAssignableFrom<NotFoundResult>(test);
        }
    }
}
