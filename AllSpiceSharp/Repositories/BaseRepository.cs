namespace AllSpiceSharp.Repositories;

public class BaseRepository
{
  private readonly IDbConnection _db;

  public BaseRepository(IDbConnection db)
  {
    _db = db;
  }
}