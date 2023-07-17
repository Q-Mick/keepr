
namespace keepr.Repositories;

public class VaultRepository
    {
    private readonly IDbConnection _db;

  public VaultRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
       INSERT INTO vaults (name, description, img, creatorId, isPrivate)
       VALUES(@name, @description, @img, @creatorId @isPrivate);

       SELECT
       vault.*,
       act.*
       FROM vaults
       JOIN accounts act ON act.id = vault.creatorId
       WHERE vault.id = LAST_INSERT_ID()
       ;";
        Vault newVault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, vaultData).FirstOrDefault();
        // let newCult = await dbContext.Cults.find({cultId})
        return newVault;
    }
    }
