using System;
using System.Collections.Generic;
using keepr2.Models;
using keepr2.Repositories;

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

        public Profile GetProfileById(string Id)
        {
          return _repo.GetProfileById(Id);
        }
    }
}