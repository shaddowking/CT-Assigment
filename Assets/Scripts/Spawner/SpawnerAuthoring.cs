using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerAuthoring : MonoBehaviour
{
   public GameObject Prefab;
   public float SpawnRate;
   public float WavePauseTime;
   public int MaxEnemySpawnd;
  


   class SpawnerBaker : Baker<SpawnerAuthoring>
   {
      public override void Bake(SpawnerAuthoring authoring)
      {
         
            
         Entity entity = GetEntity(TransformUsageFlags.Dynamic);
         
         AddComponent(entity, new Spawner
         {
            Prefab = GetEntity(authoring.Prefab,TransformUsageFlags.Dynamic),
            NextSpawnTime = 0,
            SpawnRate = authoring.SpawnRate,
            CanSpawnEnemy = true,
            EnemysSpawnd = 0,
            WavePauseTime = authoring.WavePauseTime,
            MaxEnemySpawnd = authoring.MaxEnemySpawnd
         });
      }
   }
}

public struct EnemyMoveSpeed : IComponentData
{
   public float Value;
}


