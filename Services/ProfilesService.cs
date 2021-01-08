using System;
using System.Collections.Generic;
using keepr2.Models;
using System.Threading.Tasks;
using keepr2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr2.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _repo;

    public ProfilesService(ProfilesRepository repo)
    {
      _repo = repo;
    }

    public Profile GetOrCreateProfile(Profile userInfo)
    {
      Profile foundProfile = _repo.GetByEmail(userInfo.Email);
      if (foundProfile == null)
      {
        return _repo.Create(userInfo);
      }
      return foundProfile;
    }

    public async Task<ActionResult<Profile>> GetProfileById(string id)
    {
      var result = await _repo.GetProfileById(id);

      if (result == null) return null;

      return result;
    }
  }
}