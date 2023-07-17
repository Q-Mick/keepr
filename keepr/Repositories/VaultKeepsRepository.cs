
namespace keepr.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    string sql = @"
      INSERT INTO vaultkeeps(creatorId, vaultId, keepId)
      VALUES(@CreatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID()
      ;";
      int lastInsertId = _db.ExecuteScalar<int>(sql, vaultKeepData);
      vaultKeepData.Id = lastInsertId;
      return vaultKeepData;
  }
    internal VaultKeep GetById(int vaultKeepId)
    {
        string sql = @"
        SELECT
        vk.*
        FROM vaultkeeps vk
        WHERE vk.id = @vaultKeepId
        ;";
        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
        return vaultKeep;
    }
  internal int DeleteVaultKeep(int vaultKeepId)
  {
    string sql = @"
    DELETE FROM vaultkeeps
    WHERE id = @vaultKeepId 
    LIMIT 1
    ;";
    int rows = _db.Execute(sql, new { vaultKeepId });
    return rows;
  }

  internal List<VaultKeepItem>GetKeepsInVault(int vaultId)
  {
          string sql = @"
      SELECT
      vks.*,
      keep.*
      FROM vaultkeeps vks
      JOIN keeps keep ON keep.id = vks.KeepId
      WHERE vks.VaultId = @VaultId
      ;";
        List<VaultKeepItem> keeps = _db.Query<VaultKeepItem, VaultKeepItem, VaultKeepItem>(sql, (vaultKeep, keep) =>
        {
            keep.VaultKeepId = vaultKeep.Id;
            return keep;
        }, new { vaultId }).ToList();
        return keeps;
  }
}
