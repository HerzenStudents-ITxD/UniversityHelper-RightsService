using UniversityHelper.RightsService.Business.Role.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Models;
using Moq.AutoMock;
using NUnit.Framework;
using System.Collections.Generic;

namespace UniversityHelper.RightsService.Business.Commands.Role;

class FindRolesCommandTests
{
  private IEnumerable<DbRoleLocalization> _dbRoles;
  private IEnumerable<UserInfo> _rolesInfo;

  private AutoMocker _mocker;
  private IFindRolesCommand _command;

  [OneTimeSetUp]
  public void OneTimeSetUp()
  {
  }

  [SetUp]
  public void SetUp()
  {
    _mocker = new AutoMocker();
    _command = _mocker.CreateInstance<IFindRolesCommand>();
  }

  // TODO add tests
}
