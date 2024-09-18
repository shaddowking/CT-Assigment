using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(LateSimulationSystemGroup),OrderLast = true)]
public partial struct IsDestroyMagementSystem : ISystem
{
   public void OnCreate(ref SystemState state)
   {
      state.RequireForUpdate<IsDestroying>();
   }

   public void OnUpdate(ref SystemState state)
   {
      var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
      foreach (var (tag, entity) in SystemAPI.Query<IsDestroying>().WithEntityAccess())
      {
         ecb.DestroyEntity(entity);
      }
      state.Dependency.Complete();
      ecb.Playback(state.EntityManager);
      ecb.Dispose();
   }
}
