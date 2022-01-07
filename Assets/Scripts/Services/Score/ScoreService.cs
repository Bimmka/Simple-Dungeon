using Enemies.Entity;
using Enemies.Spawn;
using Services.Database;
using Services.PlayerData;
using StaticData.Score;
using UnityEngine;

namespace Services.Score
{
  public class ScoreService : IScoreService
  {
    private readonly IEnemySpawner enemySpawner;
    private readonly ScoreStaticData scoreStaticData;
    private readonly PlayerScore playerScore;
    private readonly IDatabaseService databaseService;

    public ScoreService(IEnemySpawner enemySpawner, ScoreStaticData scoreStaticData, PlayerScore playerScore, IDatabaseService databaseService)
    {
      this.enemySpawner = enemySpawner;
      this.scoreStaticData = scoreStaticData;
      this.playerScore = playerScore;
      this.databaseService = databaseService;
      this.enemySpawner.Spawned += OnEnemySpawned;
    }

    public void Cleanup()
    {
      enemySpawner.Spawned -= OnEnemySpawned;
    }

    public bool IsPLayerInTop() => 
      playerScore.Score > 5;

    public void SavePlayerInLeaderboard(string nickname) => 
      databaseService.AddToLeaderboard(nickname, playerScore.Score);

    private void OnEnemySpawned(GameObject enemy)
    {
      enemy.GetComponent<EnemyDeath>().Happened += OnEnemyDeath;
    }

    private void OnEnemyDeath(EnemyTypeId typeId, GameObject enemy)
    {
      enemy.GetComponent<EnemyDeath>().Happened -= OnEnemyDeath;
      
      playerScore.IncScore(EnemyCost(typeId));
    }

    private int EnemyCost(EnemyTypeId typeId)
    {
      for (int i = 0; i < scoreStaticData.Scores.Length; i++)
      {
        if (scoreStaticData.Scores[i].Type == typeId)
          return scoreStaticData.Scores[i].Cost;
      }
      return 0;
    }
  }
}