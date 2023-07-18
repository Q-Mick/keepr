namespace keepr.Services;

public class VaultsService
{


  private readonly VaultsRepository _vr;

  public VaultsService(VaultsRepository vr)
  {
    _vr = vr;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault newVault = _vr.CreateVault(vaultData);
    return newVault;
  }
  internal Vault GetById(int vaultId, string userId)
  {
    Vault vault = _vr.GetById(vaultId);
    if (vault == null) throw new Exception("Cant find that vault");
    if (vault.IsPrivate == true && userId != vault.CreatorId)throw new Exception("Vault is private.");
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
  internal string DeleteVault(int vaultId, string userId)
  {

    Vault vault = GetById(vaultId, userId);
    if (vault.CreatorId != userId) throw new Exception("Not yours buddy");
    _vr.DeleteVault(vaultId);
    return "Vault has been deleted";

  }

  internal List<Vault> GetMyVaults(string profileId)
  {
    throw new NotImplementedException();
  }
}
