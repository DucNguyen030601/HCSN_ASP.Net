using Demo.WebApplication.API.Controllers;
using Demo.WebApplication.BL.FixedAssetBL;
using Demo.WebApplication.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.API.UnitTests
{
    internal class FixedAssetsControllerTests
    {
        [Test]
        public void InsertFixedAsset_ReturnsSucess()
        {
            //Arrange
            var fixedAsset = new FixedAsset()
            {
                fixed_asset_code="TS0001",
                fixed_asset_name="Máy tính Casio",
            };
            var expectedResult = new ObjectResult
            (
              new
              {
                  title = "Thêm tài sản thành công!"
              }
            );
            expectedResult.StatusCode = 201;

            var fakeFixedAssetBL = Substitute.For<IFixedAssetBL>();
            fakeFixedAssetBL.InsertFixedAsset(Arg.Any<FixedAsset>()).Returns(1);
                //Arg.Any<FixedAsset>()).Returns(1);
            var fixedAssetController = new FixedAssetsController(fakeFixedAssetBL);

            //Act
            var actualResult = (ObjectResult)fixedAssetController.InsertFixedAsset(fixedAsset);

            //Assert
            Assert.AreEqual(expectedResult.StatusCode, actualResult.StatusCode);

        }
    }
}
