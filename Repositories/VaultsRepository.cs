using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr2.Models;

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

    public int Create(Vault newVault)
    {
      string sql = @"
            INSERT INTO vaults 
            (name, description, isPrivate, creatorId)
            VALUES
            (@Name, @Description, @IsPrivate, @CreatorId);
            SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVault);
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

    public IEnumerable<Vault> Get()
    {
      string sql = populateCreator;
      // + "WHERE isPrivate = 1"
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, splitOn: "id");
    }
    // public IEnumerable<Vault> Get()
    // {
    //   string sql = "SELECT * from vaults";
    //   return _db.Query<Vault>(sql);
    // }
  }
}