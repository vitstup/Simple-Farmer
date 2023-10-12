using Leopotam.Ecs;
using UI_InputSystem.Base;
using UnityEngine;

public sealed class PlayerInputSystem : IEcsRunSystem
{
    private readonly EcsFilter<PlayerComponent, DirectionComponent> directionFilter = null;
    private readonly EcsFilter<PlayerComponent, MovableComponent> movableFilter = null;

    private float axisX;
    private float axisZ;

    private bool thresholdPassed;

    private const float threshold = 0.01f;

    public void Run()
    {
        SetDirection();

        foreach (var i in directionFilter)
        {
            ref var directionComponent = ref directionFilter.Get2(i);
            ref var direction = ref directionComponent.direction;

            direction.x = axisX;
            direction.z = axisZ;
        }

        foreach (var i in movableFilter)
        {
            ref var movableComponent = ref movableFilter.Get2(i);
            movableComponent.isMoving = thresholdPassed;
        }
    }

    private void SetDirection()
    {
        var horizontal = UIInputSystem.ME.GetAxisHorizontal(JoyStickAction.Movement);
        var vertical = UIInputSystem.ME.GetAxisVertical(JoyStickAction.Movement);

        thresholdPassed = false;

        if (Mathf.Abs(horizontal) > threshold) { axisX = horizontal; thresholdPassed = true; }
        if (Mathf.Abs(vertical) > threshold) { axisZ = vertical; thresholdPassed = true; }
    }
}