using System.Collections.Generic;
using System.Linq;
using UniversityHelper.Models.Broker.Models;
using UniversityHelper.Models.Broker.Models.User;
using UniversityHelper.RightsService.Mappers.Models.Interfaces;
using UniversityHelper.RightsService.Mappers.Responses.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using UniversityHelper.RightsService.Models.Dto.Responses;

namespace UniversityHelper.RightsService.Mappers.Responses
{
  public class RoleResponseMapper : IRoleResponseMapper
  {
    private readonly IRoleInfoMapper _roleInfoMapper;
    private readonly IUserInfoMapper _userInfoMapper;
    private readonly IRightInfoMapper _rightMapper;

    public RoleResponseMapper(
      IRoleInfoMapper roleInfoMapper,
      IUserInfoMapper userInfoMapper,
      IRightInfoMapper rightMapper)
    {
      _roleInfoMapper = roleInfoMapper;
      _userInfoMapper = userInfoMapper;
      _rightMapper = rightMapper;
    }

    public RoleResponse Map(DbRole role, List<DbRightLocalization> rights, List<UserData> users)
    {
      if (role == null)
      {
        return null;
      }

      var userInfos = users?.Select(_userInfoMapper.Map).ToList();

      return new RoleResponse
      {
        Role = _roleInfoMapper.Map(role, rights.Select(_rightMapper.Map).ToList(), userInfos),
        Users = userInfos?.Where(ui => role.Users.Any(ud => ud.UserId == ui.Id)).ToList()
      };
    }
  }
}
