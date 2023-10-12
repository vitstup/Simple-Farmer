using Leopotam.Ecs;
using UnityEngine;

public abstract class MonoTriggerStack : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;

        if (other.TryGetComponent<EntityReference>(out var entityComponent))
        {
            ref var entity = ref entityComponent.Entity;

            Trigerred(ref entity);
        }
    }

    protected abstract void Trigerred(ref EcsEntity entity);
}