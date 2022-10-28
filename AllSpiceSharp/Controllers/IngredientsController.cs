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

}
