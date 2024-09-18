using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Spawner : IComponentData
{
   public Entity Prefab;
   public float NextSpawnTime;
   public float SpawnRate;
   public bool CanSpawnEnemy;
   public int EnemysSpawnd;
   public int MaxEnemySpawnd;
   public float WavePauseTime;
}
