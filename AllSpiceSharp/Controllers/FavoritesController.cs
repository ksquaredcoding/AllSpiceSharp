namespace AllSpiceSharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  private readonly FavoritesService _fs;

  public FavoritesController(Auth0Provider auth0provider, FavoritesService fs)
  {
    _auth0provider = auth0provider;
    _fs = fs;
  }

  [HttpGet("{favoriteId}")]
  [Authorize]
  public ActionResult<Favorite> GetFavoriteById(int favoriteId)
  {
    try
    {
      Favorite favorite = _fs.GetFavoriteById(favoriteId);
      return Ok(favorite);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Favorite>> CreateFavorite([FromBody] Favorite newFavorite)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      newFavorite.AccountId = userInfo.Id;
      Favorite createdFavorite = _fs.CreateFavorite(newFavorite);
      return Ok(createdFavorite);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{favoriteId}")]
  [Authorize]
  public async Task<ActionResult<Ingredient>> RemoveIngredient(int favoriteId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _fs.RemoveFavorite(favoriteId, userInfo.Id);
      return Ok("Ingredient Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
