using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class UISelectionManager : MonoBehaviour
{
    public static UISelectionManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            EntityQuery entityQuery = new EntityQueryBuilder(Allocator.Temp).WithAll<UnitMove, Selected>()
                .Build(entityManager);
            NativeArray<UnitMove> unitMoveArray = entityQuery.ToComponentDataArray<UnitMove>(Allocator.Temp);
            for (int i = 0; i < unitMoveArray.Length; i++)
            {
                UnitMove unitMove = unitMoveArray[i];
                unitMove.targetPosition = MouseWorldPosition.Instance.GetPosition();
                unitMoveArray[i] = unitMove;
            }

            entityQuery.CopyFromComponentDataArray(unitMoveArray);
            unitMoveArray.Dispose();
        }
    }
}