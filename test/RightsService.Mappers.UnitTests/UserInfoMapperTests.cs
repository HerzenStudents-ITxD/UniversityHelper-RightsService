using HerzenHelper.Models.Broker.Models;
using HerzenHelper.RightsService.Mappers.Models;
using HerzenHelper.RightsService.Mappers.Models.Interfaces;
using HerzenHelper.RightsService.Models.Dto.Models;
using HerzenHelper.UnitTestCore;
using NUnit.Framework;
using System;

namespace HerzenHelper.RightsService.Mappers.UnitTests.Models
{
  class UserInfoMapperTests
    {
        public UserData _userData;
        public UserInfo _expectedUserInfo;

        public IUserInfoMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mapper = new UserInfoMapper();

            _userData = new UserData(
                id: Guid.NewGuid(),
                firstName: "test name",
                lastName: "test lastname",
                middleName: "test middlename",
                isActive: true,
                imageId: null);

            _expectedUserInfo = new UserInfo
            {
                Id = _userData.Id,
                FirstName = _userData.FirstName,
                LastName = _userData.LastName,
                MiddleName = _userData.MiddleName
            };
        }

        [Test]
        public void ShouldReturnNullWhenUserDataIsNull()
        {
            Assert.IsNull(_mapper.Map(null));
        }

        [Test]
        public void ShouldReturnUserInfoSuccessful()
        {
            SerializerAssert.AreEqual(_expectedUserInfo, _mapper.Map(_userData));
        }
    }
}
