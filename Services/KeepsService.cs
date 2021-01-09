using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using keepr2.Models;
using keepr2.Repositories;
using System.Threading.Tasks;

namespace keepr2.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public Keep Create(Keep newKeep)
    {
      newKeep.Id = _repo.Create(newKeep);
      return newKeep;
    }

    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }

    internal IEnumerable<Keep> GetKeepsByProfile(string profId, string userId)
    {
      return _repo.getKeepsByProfile(profId).ToList().FindAll(p => p.CreatorId == userId);
    }

    // internal Keep Edit(Keep editData, string userId)
    // {
    //   Keep original = _repo.GetKeepById(editData.Id);
    //   if (original == null) { throw new Exception("Bad Id"); }
    //   if (original.CreatorId != userId)
    //   {
    //     throw new Exception("Not the User : Access Denied");
    //   }
    //   _repo.Edit(editData);

    //   return _repo.GetKeepById(editData.Id);

    // }

    public async Task<Keep> GetKeepById(string Id)
    {
      return await _repo.GetKeepById(Id);
    }

    public async Task<int> DeleteKeepById(string Id)
    {
      var result = await _repo.DeleteKeepById(Id);
      return result;
    }

    // internal string Delete(int id, string userId)
    // {
    //   Keep original = _repo.Get(id);
    //   if (original == null) { throw new Exception("Bad Id"); }
    //   if (original.CreatorId != userId)
    //   {
    //     throw new Exception("Not the User : Access Denied");
    //   }
    //   if (_repo.Remove(id))
    //   {
    //     return "deleted succesfully";
    //   }
    //   return "did not remove succesfully";
    // }
  }
}