namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase
{
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth0;
  private readonly VaultKeepsService _vaultKeepsService;

  public VaultsController(VaultsService vaultsService, Auth0Provider auth0, VaultKeepsService vaultKeepsService)
  {
    _vaultsService = vaultsService;
    _auth0 = auth0;
    _vaultKeepsService = vaultKeepsService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      Vault newVault = _vaultsService.CreateVault(vaultData);
      return Ok(newVault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
    [HttpGet("{vaultId}")]
  public async Task<ActionResult<Vault>> GetById(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.GetById(vaultId, userInfo.Id);
      return vault;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
      [HttpGet("{vaultId}/keeps")]
    async public Task<ActionResult<List<VaultKeepItem>>> GetKeepsInVault(int vaultId)
    {
        try
        {
          Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            List<VaultKeepItem> vaultKeeps = _vaultKeepsService.GetKeepsInVault(vaultId, userInfo.Id);
            return vaultKeeps;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPut("{vaultId}")]
  [Authorize]
  public async Task<ActionResult<Vault>> UpdateVault(int vaultId, [FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      Vault vault = _vaultsService.UpdateVault(vaultId, vaultData);
      return new ActionResult<Vault>(Ok(vault));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
    [HttpDelete("{vaultId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteVault(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      
      _vaultsService.DeleteVault(vaultId, userInfo.Id);
      return Ok("Vault was successfully deleted!");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}