namespace AllSpiceSharp.Repositories;

public class FavoritesRepository : BaseRepository
{
  public FavoritesRepository(IDbConnection db) : base(db)
  {
  }

  internal Favorite CreateFavorite(Favorite newFavorite)
  {
    string sql = @"
      INSERT INTO favorites(recipeId, accountId)
      VALUES(@RecipeId, @AccountId);
      SELECT LAST_INSERT_ID()
      ;";
    int id = _db.ExecuteScalar<int>(sql, newFavorite);
    newFavorite.Id = id;

    return newFavorite;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    string sql = @"
    SELECT * FROM favorites
    WHERE id = @favoriteId
    ;";
    Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();
    return favorite;
  }

  internal List<FavRecipe> GetFavoritesByAccount(string accountId)
  {
    string sql = @"
    SELECT
    r.*,
    COUNT(f.id) AS FavoriteCount,
    f.id AS favoriteId,
    r.id AS recipeId,
    a.*
    FROM favorites f
    JOIN recipes r ON r.id = f.recipeId
    JOIN accounts a on a.id = r.creatorId
    WHERE f.accountId = @accountId
    GROUP BY f.id
    ;";
    List<FavRecipe> favs = _db.Query<FavRecipe, Profile, FavRecipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      recipe.AccountId = profile.Id;
      recipe.Favorited = true;
      return recipe;
    }, new { accountId }).ToList();
    return favs;
  }

  internal void RemoveFavorite(Favorite fav)
  {
    string sql = @"
    DELETE FROM favorites
    WHERE id = @id LIMIT 1
    ;";
    _db.Execute(sql, fav);
  }
}