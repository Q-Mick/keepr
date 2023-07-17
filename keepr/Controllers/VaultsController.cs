namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase
{
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth0;

  public VaultsController(VaultsService vaultsService, Auth0Provider auth0)
  {
    _vaultsService = vaultsService;
    _auth0 = auth0;
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