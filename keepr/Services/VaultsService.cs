namespace keepr.Services;

public class VaultsService
{


  private readonly VaultsRepository _vr;

  public VaultsService(VaultsRepository vr)
  {
    _vr = vr;
  }

  internal Vault CreateVault(Vault VaultData)
  {
    Vault newVault = _vr.CreateVault(VaultData);
    return newVault;
  }
  internal Vault GetById(int vaultId, string userId)
  {
    Vault vault = _vr.GetById(vaultId);
    if (vault.IsPrivate && vault.CreatorId != userId)
    {
      throw new Exception("This is not your vault. Beat it!");
    }
    if (vault.Name == "") throw new Exception("Invalid vault");
     if (vault.Id == 0) throw new Exception("Invalid vault");
    _vr.UpdateVault(vault);
    return vault;
  }



  internal Vault UpdateVault(int vaultId, Vault vaultData)
  {
    Vault originalVault = GetById(vaultId, "");
    if (originalVault.CreatorId != vaultData.CreatorId) throw new Exception("Shoo fly!");
    originalVault.Name = originalVault.Name.Length > 0 ? vaultData.Name : originalVault.Name;
    originalVault.Img = originalVault.Img.Length > 0 ? vaultData.Img : originalVault.Img;
    originalVault.Description = originalVault.Description.Length > 0 ? vaultData.Description : originalVault.Description;
    originalVault.IsPrivate = originalVault.IsPrivate != vaultData.IsPrivate ? vaultData.IsPrivate : originalVault.IsPrivate;
    _vr.UpdateVault(originalVault);

    return originalVault;
  }
  internal void DeleteVault(int vaultId, string userId)
  {

        Vault vault = GetById(vaultId, "");
        if (vault.CreatorId != userId) throw new Exception("Not yours buddy");
        int rows = _vr.DeleteVault(vaultId);
        if (rows > 1) throw new Exception("Something went wrong. deleted more than 1");
    
  }
}
