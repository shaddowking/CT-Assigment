using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        new PlayerMoveJob
        {
            DeltaTime = deltaTime
        }.Schedule();

    }
}

[BurstCompile]
public partial struct PlayerMoveJob : IJobEntity
{
    public float DeltaTime;
    
    [BurstCompile]
    private void Execute(ref LocalTransform transform, in PlayerMoveInput input, PlayerMovespeed speed)
    {
        transform.Position.xy += input.Value * speed.Value * DeltaTime;
    }
}
