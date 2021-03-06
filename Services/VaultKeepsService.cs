using System;
using System.Collections.Generic;
using keepr2.Models;
using keepr2.Repositories;
using System.Threading.Tasks;

namespace keepr2.Services
{
  public class VaultKeepsService
  {

    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    public async Task<VaultKeep> Create(VaultKeep newVk)
    {
      var result = await _repo.Create(newVk);
      Console.WriteLine(result);
      return result;
    }

    public async Task<IEnumerable<Keep>> GetKeepsByVaultId(int vaultId)
    {
      var result = await _repo.GetKeepsByVaultId(vaultId);
      return result;

    }

    internal string Delete(int id, string userId)
    {
      VaultKeep original = _repo.Get(id);
      if (original == null) { throw new Exception("Bad Id"); }
      if (original.CreatorId != userId)
      {
        throw new Exception("Not the User : Access Denied");
      }
      if (_repo.Remove(id))
      {
        return "deleted succesfully";
      }
      return "did not remove succesfully";
    }
  }
}
