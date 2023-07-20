namespace keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
 

  public AccountController(AccountService accountService, Auth0Provider auth0Provider)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
 
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("vaults")]
  [Authorize]
  public async Task<ActionResult<List<Vault>>> GetMyVaults(string profileId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _accountService.GetMyVaults(userInfo.Id);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
[HttpPut]
[Authorize]
public async Task<ActionResult<Account>> Edit(Account account)
{
    try
    {
        Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        if (userInfo.Id != account.Id)
        {
            return Unauthorized();
        }

        Account updatedAccount = _accountService.Edit(account, account.Email);
        return Ok(updatedAccount);
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}
}
