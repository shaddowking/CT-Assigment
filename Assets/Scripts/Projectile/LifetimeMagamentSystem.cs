using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial struct LifetimeMagamentSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Lifetime>();
    }
    
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (lifetime, entity) in SystemAPI.Query<RefRW <Lifetime>>().WithEntityAccess())
        {
            lifetime.ValueRW.Value -= deltaTime;
            if (lifetime.ValueRO.Value <= 0)
            {
                ecb.AddComponent<IsDestroying>(entity);
            }
        }
        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

