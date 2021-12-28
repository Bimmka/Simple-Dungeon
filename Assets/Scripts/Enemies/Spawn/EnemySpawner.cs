using System.Collections;
using System.Collections.Generic;
using Services.Factories.GameFactories;
using UnityEngine;

namespace Enemies.Spawn
{
  public class EnemySpawner : MonoBehaviour
  {
    private IEnemiesFactory enemiesFactory;
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>(20);

    public void Construct(IEnemiesFactory enemiesFactory)
    {
      this.enemiesFactory = enemiesFactory;
    }

    public void AddPoint(SpawnPoint spawnPoint) => 
      spawnPoints.Add(spawnPoint);

    private void Start()
    {
      StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
      while (true)
      {
        yield return new WaitForSeconds(2f);
        enemiesFactory.SpawnMonster(EnemyTypeId.Guard, spawnPoints[0].transform);
      }
    }
  }
}