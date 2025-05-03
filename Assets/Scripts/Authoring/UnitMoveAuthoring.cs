using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UnitMoveAuthoring : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public float3 targetPosition;
    public float stopDistance;

    public class Baker : Baker<UnitMoveAuthoring>
    {
        public override void Bake(UnitMoveAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new UnitMove
            {
                moveSpeed = authoring.moveSpeed,
                rotationSpeed = authoring.rotationSpeed,
                targetPosition = authoring.targetPosition,
                stopDistance = authoring.stopDistance
            });
        }
    }
}

public struct UnitMove : IComponentData
{
    public float moveSpeed;
    public float rotationSpeed;
    public float3 targetPosition;
    public float stopDistance;
}