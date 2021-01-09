using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr2.Models;
using System.Threading.Tasks;

namespace keepr2.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public async Task<VaultKeep> Create(VaultKeep newVk)
    {
      string sql = @"
        INSERT INTO vaultkeeps
        (vaultId, keepId, creatorId)
        VALUES
        (@VaultId, @KeepId, @CreatorId);
        SELECT LAST_INSERT_ID();";
      var newId = await _db.ExecuteScalarAsync<int>(sql, newVk);
      Console.WriteLine(newId);
      string getNew = @"SELECT * from vaultkeeps WHERE id = @newId";
      return await _db.QueryFirstOrDefaultAsync<VaultKeep>(getNew, new { newId });
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
        SELECT k.*,
        vk.id as VaultKeepId,
        p.*
        FROM vaultkeeps vk
        JOIN keeps k ON k.id = vk.keepId
        JOIN profiles p ON p.id = k.creatorId
        WHERE vaultId = @cvaultId;";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { vaultId }, splitOn: "id");
    }

    internal bool Remove(int id)
    {
      string sql = "DELETE from vaultkeeps WHERE id = @id";
      int valid = _db.Execute(sql, new { id });
      return valid > 0;
    }

    internal VaultKeep Get(int id)
    {
      string sql = @"SELECT * from vaultkeeps WHERE id = @id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
  }
}