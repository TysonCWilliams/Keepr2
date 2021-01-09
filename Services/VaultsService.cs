using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using keepr2.Models;
using keepr2.Repositories;
using System.Threading.Tasks;

namespace keepr2.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    public Vault Create(Vault newVault)
    {
      newVault.Id = _repo.Create(newVault);
      return newVault;
    }

    public IEnumerable<Vault> Get()
    {
      return _repo.Get();
    }

    public async Task<int> DeleteVaultById(string Id)
    {
      var result = await _repo.DeleteVaultById(Id);
      return result;
    }

    public async Task<Vault> GetVaultById(string Id)
    {
      return await _repo.GetVaultById(Id);
    }

    internal IEnumerable<Vault> GetVaultsByProfile(string profId, string userId)
    {
      return _repo.getVaultsByProfile(profId).ToList().FindAll(p => p.CreatorId == userId);
    }
  }
}