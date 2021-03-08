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
      Console.WriteLine(newVk);
      string query = @"SELECT * from keeps WHERE id = @keepId";
      int keepId = newVk.KeepId;
      Console.WriteLine(newVk);
      var keepToUpdate = await _db.QueryFirstOrDefaultAsync<Keep>(query, new { keepId });
      int updatedCount = keepToUpdate.Keeps + 1;

      string incrementKeeps = @"
        UPDATE keeps
        SET keeps = @UpdatedCount
        WHERE id = @KeepId;";
      var dictionary = new Dictionary<string, object>
        {
          { "@UpdatedCount", updatedCount },
          { "@KeepId", keepId }
        };
      var parameters = new DynamicParameters(dictionary);
      var result = await _db.ExecuteScalarAsync<Keep>(incrementKeeps, parameters);


      var sql = @"INSERT INTO vaultkeeps(vaultId, keepId, creatorId) VALUES (@VaultId, @KeepId, @CreatorId); SELECT LAST_INSERT_ID()";
      var lastInsertedId = await _db.ExecuteScalarAsync<int>(sql, new { CreatorId = newVk.CreatorId, VaultId = newVk.VaultId, KeepId = newVk.KeepId });

      string getNewlyCreated = @"SELECT * from vaultkeeps WHERE id = @lastInsertedId";
      return await _db.QueryFirstOrDefaultAsync<VaultKeep>(getNewlyCreated, new { lastInsertedId });

      // var secondSql = @"INSERT INTO NewsDtl_Tbl(NewsId, DetailName, Details) VALUES (@Id, @DetailName, @Details)";
      // var secondQuery = _db.ExecuteAsync(secondSql, new { Id = lastInsertedId, DetailName = n.DetailName, Details = n.Details });


    }

    public async Task<IEnumerable<Keep>> GetKeepsByVaultId(int vaultId)
    {
      // string sql = @"
      //   SELECT k.*,
      //   vk.id as VaultKeepId,
      //   p.*
      //   FROM vaultkeeps vk
      //   JOIN keeps k ON k.id = vk.keepId
      //   JOIN profiles p ON p.id = k.creatorId
      //   WHERE vaultId = @cvaultId;";
      // var result = await _db.QueryAsync<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { vaultId }, splitOn: "id");

      // string sql = @"SELECT * from keeps WHERE @vaultId IN (SELECT ID FROM vaults) AND ID IN (SELECT keepId FROM vaultkeeps)";
      // var result = await _db.QueryAsync<Keep>(sql, new { vaultId });

      string sql = @"
                select vk.id as VaultKeepId, k.*, p.*
                from vaultkeeps vk
                join keeps k on k.id = vk.keepId
                join profiles p on p.id = k.creatorId
                where vk.vaultId = @vaultId";
      var result = await _db.QueryAsync<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { vaultId }, splitOn: "id");
      return result;
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