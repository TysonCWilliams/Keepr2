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
      var lastInsertedId = await _db.ExecuteScalarAsync<int>(sql, new { CreatorId = newVk.CreatorId, VaultId = newVk.VaultId, KeepId = newVk.VaultId });

      string getNewlyCreated = @"SELECT * from vaultkeeps WHERE id = @lastInsertedId";
      return await _db.QueryFirstOrDefaultAsync<VaultKeep>(getNewlyCreated, new { lastInsertedId });

      // var secondSql = @"INSERT INTO NewsDtl_Tbl(NewsId, DetailName, Details) VALUES (@Id, @DetailName, @Details)";
      // var secondQuery = _db.ExecuteAsync(secondSql, new { Id = lastInsertedId, DetailName = n.DetailName, Details = n.Details });


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