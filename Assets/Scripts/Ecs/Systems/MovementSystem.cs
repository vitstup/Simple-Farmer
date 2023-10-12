using Leopotam.Ecs;
using UnityEngine;

public sealed class MovementSystem : IEcsRunSystem
{
    private readonly EcsFilter<ModelComponent, MovableComponent> movableFilter = null;

    public void Run()
    {
        foreach (var i in movableFilter)
        {
            ref var modelComponent = ref movableFilter.Get1(i);
            ref var movableComponent = ref movableFilter.Get2(i);

            ref var isMoving = ref movableComponent.isMoving;
            if (!isMoving) continue;

            ref var transform = ref modelComponent.modelTransfrom;

            ref var characterController = ref movableComponent.controller;
            ref var speed = ref movableComponent.speed;

            characterController.Move(transform.forward * speed * Time.deltaTime);
        }
    }
}