using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

partial struct UnitMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        UnitMoveJob job = new UnitMoveJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };

        // job.Run(); // 主线程执行
        job.ScheduleParallel(); // 多核执行

        // 主线程执行
        // foreach ((RefRW<LocalTransform> localTransform,
        //              RefRW<UnitMove> unitMove,
        //              RefRW<PhysicsVelocity> velocity) in SystemAPI.Query
        //          <RefRW<LocalTransform>,
        //              RefRW<UnitMove>,
        //              RefRW<PhysicsVelocity>>())
        // {
        //     float3 targetPosition = unitMove.ValueRO.targetPosition;
        //     float3 dir = targetPosition - localTransform.ValueRO.Position;
        //     dir = math.normalize(dir);
        //
        //     localTransform.ValueRW.Rotation = math.slerp(localTransform.ValueRO.Rotation,
        //         quaternion.LookRotation(dir, math.up()),
        //         SystemAPI.Time.DeltaTime * unitMove.ValueRO.rotationSpeed);
        //
        //     velocity.ValueRW.Linear = dir * unitMove.ValueRO.moveSpeed;
        //     velocity.ValueRW.Angular = float3.zero;
        // }
    }
}

[BurstCompile]
public partial struct UnitMoveJob : IJobEntity
{
    public float deltaTime;

    public void Execute(ref LocalTransform localTransform, in UnitMove unitMove, ref PhysicsVelocity velocity)
    {
        float3 targetPosition = unitMove.targetPosition;
        float3 moveDir = targetPosition - localTransform.Position;

        if (math.lengthsq(moveDir) < unitMove.stopDistance)
        {
            velocity.Linear = float3.zero;
            velocity.Angular = float3.zero;
            return;
        }

        moveDir = math.normalize(moveDir);

        localTransform.Rotation = math.slerp(localTransform.Rotation,
            quaternion.LookRotation(moveDir, math.up()),
            deltaTime * unitMove.rotationSpeed);

        velocity.Linear = moveDir * unitMove.moveSpeed;
        velocity.Angular = float3.zero;
    }
}