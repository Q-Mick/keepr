namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vkr;
  private readonly VaultsService _vaultsService;
  private readonly KeepsService _keepsService;

  public VaultKeepsService(VaultKeepsRepository vkr, VaultsService vaultsService, KeepsService keepsService)
  {
    _vkr = vkr;
    _vaultsService = vaultsService;
    _keepsService = keepsService;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
  {
    Vault vault = _vaultsService.GetById(vaultKeepData.VaultId, userId);
    if (vault.CreatorId != userId) throw new Exception("Not yours");
    Keep keep = _keepsService.GetById(vaultKeepData.KeepId);
    keep.Kept++;
    _keepsService.UpdateKeptCount(keep.Id, keep);
    VaultKeep newVaultKeep = _vkr.CreateVaultKeep(vaultKeepData);

    return newVaultKeep;
  }
  
      internal VaultKeep GetById(int VaultKeepId)
    {
        VaultKeep VaultKeep = _vkr.GetById(VaultKeepId);
        return VaultKeep;
    }
  internal void DeleteVaultKeep(int vaultKeepId, string userId)
  {
        VaultKeep vaultKeep = GetById(vaultKeepId);
        if (vaultKeep.CreatorId != userId) throw new Exception("Unauthorized to remove this vaultKeep!");
        int rows = _vkr.DeleteVaultKeep(vaultKeepId);
        Keep keep = _keepsService.GetById(vaultKeep.KeepId);
        keep.Kept--;
        _keepsService.UpdateKeptCount(keep.Id, keep);
        if (rows > 1) throw new Exception("Something went wrong");
    
  }

  internal List<VaultKeepItem> GetKeepsInVault(int vaultId)
  {

          List<VaultKeepItem> vaultKeeps = _vkr.GetKeepsInVault(vaultId);
         if (vaultKeeps == null) throw new Exception("No vaultKeep found");
        return vaultKeeps;
  }
}
