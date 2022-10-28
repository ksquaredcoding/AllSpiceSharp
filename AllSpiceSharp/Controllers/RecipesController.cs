namespace AllSpiceSharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  private readonly RecipesService _rs;
  private readonly IngredientsService _is;

  public RecipesController(Auth0Provider auth0provider, RecipesService rs, IngredientsService @is)
  {
    _auth0provider = auth0provider;
    _rs = rs;
    _is = @is;
  }


  [HttpGet]
  public ActionResult<List<Recipe>> GetAllRecipes()
  {
    try
    {
      List<Recipe> recipes = _rs.GetAllRecipes();
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsByRecipe(int recipeId)
  {
    try
    {
      List<Ingredient> ingredients = _is.GetIngredientsByRecipe(recipeId);
      return Ok(ingredients);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}")]
  [Authorize]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try
    {
      Recipe recipe = _rs.GetRecipeById(recipeId);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Recipe>> Create([FromBody] Recipe newRecipe)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      newRecipe.CreatorId = userInfo.Id;
      Recipe createdRecipe = _rs.CreateRecipe(newRecipe);
      createdRecipe.Creator = userInfo;
      return Ok(createdRecipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{recipeId}")]
  [Authorize]
  public ActionResult<Recipe> EditRecipe(int recipeId, [FromBody] Recipe recData)
  {
    try
    {
      recData.Id = recipeId;
      Recipe recipe = _rs.EditRecipe(recData);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{recipeId}")]
  [Authorize]
  public async Task<ActionResult<Recipe>> RemoveRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _rs.RemoveRecipe(recipeId, userInfo.Id);
      return Ok("Recipe Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
