using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
   public float MoveSpeed;

   public GameObject ProjectilePrefab;
   public float ProjectileLifetime;
   
   class PlayerAuthoringBaker : Baker<PlayerAuthoring>
   {
      public override void Bake(PlayerAuthoring authoring)
      {
         Entity playerEntity = GetEntity(TransformUsageFlags.Dynamic);
         
         AddComponent<PlayerTag>(playerEntity);
         AddComponent<PlayerMoveInput>(playerEntity);
         
         AddComponent(playerEntity, new PlayerMovespeed
         {
            Value = authoring.MoveSpeed
         });
         
         AddComponent<FireProjectileTag>(playerEntity);
         SetComponentEnabled<FireProjectileTag>(playerEntity,false);
         
         AddComponent(playerEntity, new ProjectilePrefab
         {
            Value = GetEntity(authoring.ProjectilePrefab,TransformUsageFlags.Dynamic)
         });
         AddComponent(playerEntity, new EntityLifeTime
         {
            Value = authoring.ProjectileLifetime
         });
         
      }
   }
}

public struct PlayerMoveInput : IComponentData
{
   public float2 Value;
}
public struct PlayerMovespeed : IComponentData
{
   public float Value;
}

public struct PlayerTag : IComponentData{ }

public struct ProjectilePrefab : IComponentData
{
   public Entity Value;
}
public struct projectileMoveSpeed : IComponentData
{
   public float Value;
}

public struct FireProjectileTag : IComponentData, IEnableableComponent { }

public struct EntityLifeTime : IComponentData
{
   public float Value;
}

public struct Lifetime : IComponentData
{
   public float Value;
}

public struct IsDestroying : IComponentData{}