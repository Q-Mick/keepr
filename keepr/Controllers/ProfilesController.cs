namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly ProfileService _profileService;
  private readonly Auth0Provider _auth0Provider;
  private readonly AccountService _accountService;

  public ProfilesController(ProfileService profileService, Auth0Provider auth0Provider, AccountService accountService)
  {
    _profileService = profileService;
    _auth0Provider = auth0Provider;
    _accountService = accountService;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfile(string profileId)
  {
    try
    {
      Profile profile = _profileService.GetProfile(profileId);
      return profile;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
  {
    try
    {
      List<Keep> keeps = _profileService.GetKeepsByProfileId(profileId);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
    [HttpGet("{profileId}/vaults")]
  public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string profileId)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Profile>(HttpContext);
      List<Vault> vaults = _profileService.GetVaultsByProfileId(profileId, userInfo?.Id);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
