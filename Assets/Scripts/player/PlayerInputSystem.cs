using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[UpdateInGroup(typeof(InitializationSystemGroup),OrderLast = true)]
public partial class PlayerInputSystem : SystemBase
{
    private GameInput InputActions;
    private Entity player;

    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();
        InputActions = new GameInput();
    }

    protected override void OnStartRunning()
    {
        InputActions.Enable();
        InputActions.GamePlay.Shoot.performed += OnShoot;

        player = SystemAPI.GetSingletonEntity<PlayerTag>();
    }

    protected void OnShoot(InputAction.CallbackContext context)
    {
        if(!SystemAPI.Exists(player)) return;
        
        SystemAPI.SetComponentEnabled<FireProjectileTag>(player, true);
    }
    
    protected override void OnUpdate()
    {
        Vector2 moveInput = InputActions.GamePlay.Move.ReadValue<Vector2>();
        
        SystemAPI.SetSingleton(new PlayerMoveInput{Value = moveInput});

    }

    protected override void OnStopRunning()
    {
        InputActions.Disable();
        player = Entity.Null;
    }
}
