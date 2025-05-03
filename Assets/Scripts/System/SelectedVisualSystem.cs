using Unity.Entities;
using Unity.Transforms;

public partial struct SelectedVisualSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var selected in SystemAPI.Query<RefRW<Selected>>().WithDisabled<Selected>())
        {
            var localTransform = SystemAPI.GetComponentRW<LocalTransform>(selected.ValueRW.visualEntity);
            localTransform.ValueRW.Scale = 0;
        }

        foreach (var selected in SystemAPI.Query<RefRW<Selected>>())
        {
            var localTransform = SystemAPI.GetComponentRW<LocalTransform>(selected.ValueRW.visualEntity);
            localTransform.ValueRW.Scale = selected.ValueRO.showScale;
        }
    }
}

public partial struct SelectedVisualJob1 : IJobEntity
{
    public void Execute(ref LocalTransform localTransform, in Selected selected)
    {
    }
}

public partial struct SelectedVisualJob2 : IJobEntity
{
    public void Execute(ref LocalTransform localTransform, in Selected selected)
    {
    }
}