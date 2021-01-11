using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr2.Models;
using System.Threading.Tasks;

namespace keepr2.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    private readonly string populateCreator = "SELECT keep.*, profile.* FROM keeps keep INNER JOIN profiles profile ON keep.creatorId = profile.id ";

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public int Create(Keep newKeep)
    {
      string sql = @"
            INSERT INTO keeps 
            (name, description, img, views, shares, keeps, creatorId)
            VALUES
            (@Name, @Description, @Img, @Views, @Shares, @Keeps, @CreatorId);
            SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newKeep);
    }

    public async Task<Keep> IncrementViewCount(int keepId)
    {

      string query = @"SELECT * from keeps WHERE id = @keepId";
      var keepToUpdate = await _db.QueryFirstOrDefaultAsync<Keep>(query, new { keepId });
      int updatedCount = keepToUpdate.Views + 1;

      string sql = @"
        UPDATE keeps
        SET views = @UpdatedCount
        WHERE id = @KeepId;";
      var dictionary = new Dictionary<string, object>
        {
          { "@UpdatedCount", updatedCount },
          { "@KeepId", keepId }
        };
      var parameters = new DynamicParameters(dictionary);
      var result = await _db.ExecuteScalarAsync<Keep>(sql, parameters);
      return result;
    }

    internal IEnumerable<Keep> getKeepsByProfile(string profId)
    {
      string sql = @"
        SELECT
        keep.*,
        k.*
        FROM keeps keep
        JOIN profiles k ON keep.creatorId = k.id
        WHERE keep.creatorId = @profId; ";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { profId }, splitOn: "id");
    }

    public async Task<Keep> GetKeepById(string id)
    {
      string sql = @"SELECT * from keeps WHERE id = @id";

      var results = await _db.QueryFirstOrDefaultAsync<Keep>(sql, new { id });

      return results;
    }

    public async Task<int> DeleteKeepById(string id)
    {
      string sql = "DELETE from keeps WHERE id = @id";
      return await _db.ExecuteAsync(sql, new { id });
    }

    internal void Edit(Keep editData)
    {
      string sql = @"
        UPDATE keeps
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        views = @Views,
        shares = @Shares,
        keeps = @Keeps,
        WHERE id = @Id;";
      _db.Execute(sql, editData);
    }

    public IEnumerable<Keep> Get()
    {
      string sql = populateCreator;
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, splitOn: "id");
    }

    internal bool Remove(int id)
    {
      string sql = "DELETE from keeps WHERE id = @id";
      int valid = _db.Execute(sql, new { id });
      return valid > 0;
    }
  }
}