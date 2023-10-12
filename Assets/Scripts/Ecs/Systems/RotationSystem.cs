using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSystem : IEcsRunSystem
{
    private readonly EcsFilter<RotationComponent, DirectionComponent> rotationFilter = null;

    public void Run()
    {
        foreach (var i in rotationFilter)
        {
            ref var rotationComponent = ref rotationFilter.Get1(i);
            ref var transform = ref rotationComponent.transform;
            ref var speed = ref rotationComponent.rotationSpeed;

            ref var directionComponent = ref rotationFilter.Get2(i);
            ref var direction = ref directionComponent.direction;

            float angleRad = Mathf.Atan2(direction.x, direction.z);

            float angleDeg = angleRad * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, angleDeg, 0);

            float step = speed * Time.deltaTime;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
        }
    }
}