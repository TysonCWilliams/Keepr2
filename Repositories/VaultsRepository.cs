using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr2.Models;
using System.Threading.Tasks;

namespace keepr2.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    private readonly string populateCreator = "SELECT vault.*, profile.* FROM vaults vault INNER JOIN profiles profile ON vault.creatorId = profile.id ";

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    public async Task<int> Create(Vault newVault)
    {
      string sql = @"
            INSERT INTO vaults 
            (name, description, isPrivate, creatorId)
            VALUES
            (@Name, @Description, @IsPrivate, @CreatorId);
            SELECT LAST_INSERT_ID();";
      return await _db.ExecuteScalarAsync<int>(sql, newVault);
    }

    public async Task<Vault> GetVaultById(string id)
    {
      string sql = @"SELECT * from vaults WHERE id = @id";

      var results = await _db.QueryFirstOrDefaultAsync<Vault>(sql, new { id });

      return results;
    }

    internal IEnumerable<Vault> getVaultsByProfile(string profId)
    {
      string sql = @"
        SELECT
        vault.*,
        v.*
        FROM vaults vault
        JOIN profiles v ON vault.creatorId = v.id
        WHERE vault.creatorId = @profId; ";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { profId }, splitOn: "id");
    }

    internal Vault GetOne(int id)
    {
      string sql = @"SELECT * from vaults WHERE id = @id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    }

    internal void Edit(Vault editData)
    {
      string sql = @"
        UPDATE vaults
        SET
        name = @Name,
        description = @Description,
        isPrivate = @IsPrivate,
        WHERE id = @Id;";
      _db.Execute(sql, editData);
    }

    public async Task<int> DeleteVaultById(string id)
    {
      string sql = "DELETE from vaults WHERE id = @id";
      return await _db.ExecuteAsync(sql, new { id });
    }

    public IEnumerable<Vault> Get()
    {
      string sql = populateCreator;
      // + "WHERE isPrivate = 1"
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; vault.IsPrivate = false; return vault; }, splitOn: "id");
    }
    public async Task<IEnumerable<Vault>> GetAllVaultsForUser(string id)
    {
      string sql = @"SELECT * from vaults WHERE creatorId = @id";
      var result = await _db.QueryAsync<Vault>(sql, new { id });
      return result;
    }

    public async Task<IEnumerable<Vault>> GetAllPublicVaultsForUser(string id)
    {
      string sql = @"SELECT * from vaults WHERE creatorId = @id AND isPrivate = 0";
      var result = await _db.QueryAsync<Vault>(sql, new { id });
      return result;
    }
  }
}