


namespace keepr.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    string sql = @"
    INSERT INTO
    keeps(name, creatorId ,description, img)
    VALUES
    (@Name, @CreatorId, @Description, @Img);

    SELECT
    keep.*,
    act.*
    FROM keeps keep
    JOIN accounts act ON act.id = keep.creatorId
    WHERE keep.id = LAST_INSERT_ID();
    ;";
    Keep newKeep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }, keepData).FirstOrDefault();
    return newKeep;
  }

  internal void Edit(Keep keep)
  {
    throw new NotImplementedException();
  }

  internal List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT
    keep.*,
    act.*,
    COUNT(vk.id) AS kept
    FROM keeps keep
    JOIN accounts act ON act.id = keep.creatorId
    LEFT JOIN vaultkeeps vk ON vk.keepId = keep.id
    GROUP BY (keep.id)
    ;";
    List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();
    return keeps;
  }

  internal Keep GetById(int keepId)
  {
    string sql = @"
        SELECT
        keep.*,
        act.*
        FROM keeps keep
        JOIN accounts act ON keep.creatorId = act.id
        WHERE keep.id = @keepId
    ;";
   Keep foundKeep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
   {
        keep.Creator = account;
        return keep;
   }, new { keepId }).FirstOrDefault();
   return foundKeep;
}

 internal void UpdateKeep(Keep keep)
  {
    string sql = @"
    UPDATE keeps SET
    name = @Name,
    description = @Description,
    img = @Img,
    views = @Views
    WHERE id = @Id;
    ";
    _db.Execute(sql, keep);
  }
    internal int DeleteKeep(int keepId)
  {
    string sql = @"
     DELETE FROM keeps
     WHERE id = @keepId 
     LIMIT 1
     ;";
    int rows = _db.Execute(sql, new { keepId });
    return rows;
  }
}
