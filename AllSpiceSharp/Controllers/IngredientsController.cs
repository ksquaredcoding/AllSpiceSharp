namespace AllSpiceSharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  private readonly IngredientsService _is;

  public IngredientsController(Auth0Provider auth0provider, IngredientsService @is)
  {
    _auth0provider = auth0provider;
    _is = @is;
  }

  [HttpGet("{ingredientId}")]
  [Authorize]
  public ActionResult<Ingredient> GetIngredientById(int ingredientId)
  {
    try
    {
      Ingredient ingredient = _is.GetIngredientById(ingredientId);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpPost]
  [Authorize]
  public ActionResult<Ingredient> CreateIngredient([FromBody] Ingredient newIngredient)
  {
    try
    {
      Ingredient createdIngredient = _is.CreateIngredient(newIngredient);
      return Ok(createdIngredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{ingredientId}")]
  [Authorize]
  public async Task<ActionResult<Ingredient>> RemoveIngredient(int ingredientId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _is.RemoveIngredient(ingredientId, userInfo.Id);
      return Ok("Ingredient Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
