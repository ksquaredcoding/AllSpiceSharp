namespace AllSpiceSharp.Repositories;

public class IngredientsRepository : BaseRepository
{
  public IngredientsRepository(IDbConnection db) : base(db)
  {
  }

  internal Ingredient CreateIngredient(Ingredient newIngredient)
  {
    string sql = @"
    INSERT INTO ingredients(name, quantity, recipeId)
    VALUES(@Name, @Quantity, @RecipeId);
    SELECT LAST_INSERT_ID()
    ;";
    int id = _db.ExecuteScalar<int>(sql, newIngredient);
    newIngredient.Id = id;
    return newIngredient;
  }

  internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
  {
    string sql = @"
    SELECT
    *
    FROM ingredients
    WHERE recipeId = @recipeId
    ;";
    List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    return ingredients;
  }
}