using System.Collections.Generic;
using System.Linq;
using HerzenHelper.Models.Broker.Models;
using HerzenHelper.RightsService.Mappers.Models.Interfaces;
using HerzenHelper.RightsService.Mappers.Responses.Interfaces;
using HerzenHelper.RightsService.Models.Db;
using HerzenHelper.RightsService.Models.Dto.Responses;

namespace HerzenHelper.RightsService.Mappers.Responses
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
