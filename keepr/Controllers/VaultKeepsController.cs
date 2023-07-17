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
      // NOTE: HttpContext is the C# equivalent of 'req' in Node.JS
      // NOTE we are essentially passing in the entire req or context, so then we can pass the bearer token from the request headers to Auth0
      vaultKeepData.CreatorId = userInfo.Id;
      VaultKeep newVaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData);
      return Ok(newVaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
