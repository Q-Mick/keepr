namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase

{
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly Auth0Provider _auth0;

  public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0)
  {
    _vaultKeepsService = vaultKeepsService;
    _auth0 = auth0;
  }
  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);

      vaultKeepData.CreatorId = userInfo.Id;
      VaultKeep newVaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData, userInfo?.Id);
      return Ok(newVaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{vaultKeepId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);

      _vaultKeepsService.DeleteVaultKeep(vaultKeepId, userInfo?.Id);
      return Ok("VaultKeep was successfully deleted!");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
