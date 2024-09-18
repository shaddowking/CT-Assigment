using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public partial struct SpawnerSystems : ISystem
{
  public void OnUpdate(ref SystemState state)
  {

    foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
    {
      
      Spawn(ref state,spawner);
      
      WavePause(ref state,spawner);
      
    }
  }

  public void Spawn(ref SystemState state,RefRW<Spawner> spawner)
  {
    if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime && spawner.ValueRO.CanSpawnEnemy)
    {
      spawner.ValueRW.EnemysSpawnd++;
      float X = Random.Range(-8,8);
      float Y = Random.Range(4.5f,4.2f);
      Entity newentity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
      float3 pos = new float3(X, Y, 0);
      state.EntityManager.SetComponentData(newentity,LocalTransform.FromPosition(pos));
      spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;

      if (spawner.ValueRO.EnemysSpawnd >= spawner.ValueRO.MaxEnemySpawnd)
      {
        
        spawner.ValueRW.CanSpawnEnemy = false;
      }
    }
  }

  public void WavePause(ref SystemState state,RefRW<Spawner> spawner)
  {
    if (spawner.ValueRO.CanSpawnEnemy == false && spawner.ValueRO.WavePauseTime < SystemAPI.Time.ElapsedTime)
    {
      spawner.ValueRW.EnemysSpawnd = 0;
      spawner.ValueRW.CanSpawnEnemy = true;
      spawner.ValueRW.WavePauseTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.WavePauseTime;

    }
  }
  
}
