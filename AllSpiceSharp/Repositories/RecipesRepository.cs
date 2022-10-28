namespace AllSpiceSharp.Repositories;

public class RecipesRepository : BaseRepository
{
  public RecipesRepository(IDbConnection db) : base(db)
  {
  }

  internal List<Recipe> GetAllRecipes()
  {
    string sql = @"
    SELECT
    r.*,
    COUNT(f.id) AS FavoriteCount,
    a.*
    FROM recipes r
    JOIN accounts a ON a.id = r.creatorId
    LEFT JOIN favorites f ON f.recipeId = r.id
    GROUP BY r.id
    ;";
    return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    }).ToList();
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
    SELECT
    r.*,
    a.*
    FROM recipes r
    JOIN accounts a ON a.id = r.creatorId
    WHERE r.id = @recipeId
    ;";
    return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    }, new { recipeId }).FirstOrDefault();
  }

  internal Recipe EditRecipe(Recipe updatedRecipe)
  {
    string sql = @"
    UPDATE recipes SET
      title = @title,
      instructions = @instructions,
      img = @img,
      category = @category
    WHERE id = @id
    ;";
    int affectedRows = _db.Execute(sql, updatedRecipe);
    if (affectedRows == 0)
    {
      throw new Exception("No changes made to Recipe.");
    }
    return updatedRecipe;
  }

  internal void RemoveRecipe(Recipe recipe)
  {
    string sql = @"
    DELETE FROM recipes WHERE id = @Id
    ;";
    _db.Execute(sql, recipe);
  }

  internal Recipe CreateRecipe(Recipe newRecipe)
  {
    string sql = @"
    INSERT INTO recipes(title, img, category, creatorId, instructions)
    VALUES(@Title, @Img, @Category, @CreatorId, @Instructions);
    SELECT LAST_INSERT_ID()
    ;";
    int recipeId = _db.ExecuteScalar<int>(sql, newRecipe);
    newRecipe.Id = recipeId;
    return newRecipe;
  }
}