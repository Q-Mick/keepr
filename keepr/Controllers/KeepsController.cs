namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class KeepsController : ControllerBase
{
  private readonly KeepsService _keepsService;
  private readonly Auth0Provider _auth0;

  public KeepsController(KeepsService keepsService, Auth0Provider auth0)
  {
    _keepsService = keepsService;
    _auth0 = auth0;
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      Keep newKeep = _keepsService.CreateKeep(keepData);
      return Ok(newKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Keep>> GetAllKeeps()
  {
    try
    {
      List<Keep> keeps = _keepsService.GetAllKeeps();
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetById(int keepId)
  {
    try
    {
      Keep keep = _keepsService.GetById(keepId);
      return keep;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpPut("{keepId}")]
  [Authorize]
  public async Task<ActionResult<Keep>> UpdateKeep(int keepId, [FromBody] Keep keepData)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      Keep keep = _keepsService.UpdateKeep(keepId, keepData);
      return new ActionResult<Keep>(Ok(keep));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{keepId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteKeep(int keepId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      
      _keepsService.DeleteKeep(keepId, userInfo.Id);
      return Ok("Keep was successfully deleted!");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}