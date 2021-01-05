using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr2.Models;

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
            (title, body, img, views, shares, keeps, creatorId)
            VALUES
            (@Title, @Body, @Img, @Views, @Shares, @Keeps, @CreatorId);
            SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newKeep);
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

    internal Keep GetOne(int id)
    {
      string sql = @"SELECT * from keeps WHERE id = @id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }

    internal void Edit(Keep editData)
    {
      string sql = @"
        UPDATE keeps
        SET
        title = @Title,
        body = @Body,
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
  }
}