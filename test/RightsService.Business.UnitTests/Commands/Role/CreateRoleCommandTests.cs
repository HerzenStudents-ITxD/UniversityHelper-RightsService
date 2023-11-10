//using UniversityHelper.Core.AccessValidatorEngine.Interfaces;
//using UniversityHelper.Core.Enums;
//using UniversityHelper.Core.Responses;
//using UniversityHelper.RightsService.Business.Role.Interfaces;
//using UniversityHelper.RightsService.Business.Role;
//using UniversityHelper.RightsService.Data.Interfaces;
//using UniversityHelper.RightsService.Data.Provider;
//using UniversityHelper.RightsService.Mappers.Interfaces;
//using UniversityHelper.RightsService.Models.Db;
//using UniversityHelper.RightsService.Models.Dto;
//using UniversityHelper.RightsService.Validation.Interfaces;
//using Microsoft.AspNetCore.Http;
//using Moq;
//using Moq.AutoMock;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;

//namespace UniversityHelper.RightsService.Business.UnitTests.Commands.Role
//{
//    internal class CreateRoleCommandTests
//    {
//        private AutoMocker _mocker;
//        private DbRole _dbRole;
//        private CreateRoleRequest _newRequest;
//        private ICreateRoleCommand _command;
//        private OperationResultResponse<Guid> _response;

//        private readonly Guid _authorId = Guid.NewGuid();
//        private readonly Guid _roleId = Guid.NewGuid();
//        private readonly int _rightId = 123;
//        private readonly Guid _userId = Guid.NewGuid();

//        [OneTimeSetUp]
//        public void OneTimeSetUp()
//        {
//            _mocker = new AutoMocker();
//            _command = _mocker.CreateInstance<CreateRoleCommand>();

//            _newRequest = new CreateRoleRequest
//            {
//                Name = "Create Smth",
//                Description = "Create smth in somewhere",
//                Rights = new List<int> { _rightId }
//            };

//            var dbUser = new DbUser {
//                UserId = _userId,
//                Role = new DbRole { Id = _roleId }
//            };
//            var dbRoleRight = new DbRoleRight {
//                Id = Guid.NewGuid(),
//                Role = new DbRole { Id = _roleId },
//                Rights = new DbRight { Id = _rightId }
//            };

//            _dbRole = new DbRole
//            {
//                Id = Guid.NewGuid(),
//                Name = "Create Smth",
//                Description = "Create smth in somewhere",
//                CreatedAt = DateTime.UtcNow,
//                CreatedBy = _authorId,
//                Rights = new List<DbRoleRight> { dbRoleRight },
//                Users = new List<DbUser> { dbUser }
//            };

//            _response = new OperationResultResponse<Guid>
//            {
//                Body = _dbRole.Id,
//                Status = OperationResultStatusType.FullSuccess,
//                Errors = new List<string>()
//            };
//        }

//        [SetUp]
//        public void SetUp()
//        {
//            _mocker.GetMock<ICreateRoleRequestValidator>().Reset();
//            _mocker.GetMock<IAccessValidator>().Reset();
//            _mocker.GetMock<IDbRoleMapper>().Reset();
//            _mocker.GetMock<IRoleRepository>().Reset();
//            _mocker.GetMock<IHttpContextAccessor>().Reset();
//            _mocker.GetMock<IDataProvider>().Reset();
//        }

//        // TODO
//    }
//}
