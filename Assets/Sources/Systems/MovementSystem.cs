using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
class MovementSystem : ComponentSystem
{

    protected override void OnUpdate()
    {
        var dtTime = Time.deltaTime;
        Entities.ForEach((ref Movement move, ref Translation translation) =>
        {
            translation.Value += move.PositionValue * dtTime;
        });
    }
}