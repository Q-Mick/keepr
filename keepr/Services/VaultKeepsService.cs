namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vkr;


  public VaultKeepsService(VaultKeepsRepository vkr)
  {
    _vkr = vkr;

  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    VaultKeep newVaultKeep = _vkr.CreateVaultKeep(vaultKeepData);
    return newVaultKeep;
  }
  
      internal VaultKeep GetById(int VaultKeepId)
    {
        VaultKeep VaultKeep = _vkr.GetById(VaultKeepId);
        if (VaultKeep == null) new Exception("Invalid id");
        return VaultKeep;
    }
  internal void DeleteVaultKeep(int vaultKeepId, string userId)
  {
        VaultKeep vaultKeep = GetById(vaultKeepId);
        if (vaultKeep.CreatorId != userId) throw new Exception("Unauthorized to remove this collaborator!");
        int rows = _vkr.DeleteVaultKeep(vaultKeepId);
        if (rows > 1) throw new Exception("Something went wrong");
    
  }

  internal List<VaultKeepItem> GetKeepsInVault(int vaultId)
  {
          List<VaultKeepItem> vaultKeeps = _vkr.GetKeepsInVault(vaultId);
        return vaultKeeps;
  }
}
