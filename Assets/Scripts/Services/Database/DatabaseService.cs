using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstantsValue;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

namespace Services.Database
{
  public class DatabaseService : IDatabaseService
  {
    private readonly MongoClient client;
    private readonly IMongoDatabase database;
    private readonly IMongoCollection<LeaderboardPlayer> databaseCollection;
    private float lastUpdateTime;

    public IEnumerable<LeaderboardPlayer> Leaderboard { get; private set; }
    
    public DatabaseService()
    {
      client = new MongoClient(Constants.MongoClientSettings);
      database = client.GetDatabase(Constants.MongoDatabaseName);
      databaseCollection = database.GetCollection<LeaderboardPlayer>(Constants.MongoCollectionName);
      lastUpdateTime = -Constants.LeaderboardUpdateRange;
    }

    public async void AddToLeaderboard(string nickname, int score) => 
      await databaseCollection.InsertOneAsync(DatabaseElement(nickname, score));

    public async Task<IEnumerable<LeaderboardPlayer>> UpdateTopPlayers()
    {
      IAsyncCursor<LeaderboardPlayer> collection = await databaseCollection.FindAsync(FilterDefinition<LeaderboardPlayer>.Empty);
      List<LeaderboardPlayer> listCollection = await collection.ToListAsync();
      IEnumerable<LeaderboardPlayer> leaderboardPlayers = listCollection.OrderByDescending(x => x.Score).Take(Constants.TopLength);
      Leaderboard = leaderboardPlayers;
      UpdateLastUpdateTime();
      return Leaderboard;
    }

    public bool IsNeedToUpdateLeaderboard() => 
      Time.time >= lastUpdateTime + Constants.LeaderboardUpdateRange;

    private LeaderboardPlayer DatabaseElement(string nickname, int score) =>
      new LeaderboardPlayer (nickname, score);

    private void UpdateLastUpdateTime() => 
      lastUpdateTime = Time.time;
  }
}