﻿using HerzenHelper.RightsService.Mappers.Db;
using HerzenHelper.RightsService.Mappers.Db.Interfaces;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Requests;
using HerzenHelper.UnitTestCore;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HerzenHelper.RightsService.Mappers.RequestsMappers.UnitTests
{
  internal class DbRoleMapperTests
    {
        private IDbRoleMapper _roleRequestMapper;
        private Mock<IHttpContextAccessor> _accessorMock;

        private Guid _userId = Guid.NewGuid();
        private CreateRoleRequest _request;

        //[OneTimeSetUp]
        //public void OneTimeSetUp()
        //{
        //    IDictionary<object, object> _items = new Dictionary<object, object>();
        //    _items.Add("UserId", _userId);

        //    _accessorMock = new();
        //    _accessorMock
        //        .Setup(x => x.HttpContext.Items)
        //        .Returns(_items);

        //    _roleRequestMapper = new DbRoleMapper(_accessorMock.Object);

        //    _request = new CreateRoleRequest
        //    {
        //        Name = "test name",
        //        Description = "test descripton",
        //        Rights = new List<int> { 123 }
        //    };
        //}

        //[Test]
        //public void ShouldThrowArgumentNullExceptionWhenProjectRequestIsNull()
        //{
        //    CreateRoleRequest request = null;

        //    Assert.Throws<ArgumentNullException>(() => _roleRequestMapper.Map(request));
        //}
    }
}
