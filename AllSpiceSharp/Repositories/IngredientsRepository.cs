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

  internal Ingredient GetIngredientById(int ingredientId)
  {
    string sql = @"
    SELECT
    i.*,
    r.*
    FROM ingredients i
    JOIN recipes r ON r.id = i.recipeId
    WHERE i.id = @ingredientId
    ;";
    return _db.Query<Ingredient, Recipe, Ingredient>(sql, (ingredient, recipe) =>
    {
      ingredient.Recipe = recipe;
      return ingredient;
    }, new { ingredientId }).FirstOrDefault();
  }

  internal void RemoveIngredient(Ingredient ingredient)
  {
    string sql = @"
    DELETE FROM ingredients WHERE id = @Id
    ;";
    _db.Execute(sql, ingredient);
  }
}