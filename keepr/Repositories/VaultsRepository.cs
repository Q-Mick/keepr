
namespace keepr.Repositories;

public class VaultsRepository
    {
    private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
       INSERT INTO vaults(name, description, img, creatorId, isPrivate)
       VALUES(@Name, @Description, @Img, @CreatorId, @IsPrivate);

       SELECT
       vault.*,
       act.*
       FROM vaults vault
       JOIN accounts act ON act.id = vault.creatorId
       WHERE vault.id = LAST_INSERT_ID()
       ;";
        Vault newVault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, vaultData).FirstOrDefault();
      
        return newVault;
    }
      internal Vault GetById(int vaultId)
  {
    string sql = @"
        SELECT
        vault.*,
        act.*
        FROM vaults vault
        JOIN accounts act ON vault.creatorId = act.id
        WHERE vault.id = @vaultId
    ;";
   Vault foundVault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
   {
        vault.Creator = account;
        return vault;
   }, new { vaultId }).FirstOrDefault();
   return foundVault;
}
 internal void UpdateVault(Vault vault)
  {
    string sql = @"
    UPDATE vaults SET
    name = @Name,
    description = @Description,
    img = @Img,
    isPrivate = @IsPrivate
    WHERE id = @Id;
    ";
    _db.Execute(sql, vault);
  }
      internal int DeleteVault(int vaultId)
  {
    string sql = @"
     DELETE FROM vaults
     WHERE id = @vaultId 
     LIMIT 1
     ;";
    int rows = _db.Execute(sql, new { vaultId });
    return rows;
  }
   internal List<Vault> GetVaultsByProfileId(string profileId)
  {
    string sql = @"
SELECT
    vault.*,
    act.*
FROM
    vaults vault
JOIN
    accounts act ON act.id = vault.CreatorId
WHERE
    vault.CreatorId = @profileId
AND NOT vault.IsPrivate
        ;";
    List<Vault> profileVaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { profileId }).ToList();
    return profileVaults;
  }
  
    }
