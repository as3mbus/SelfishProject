using Unity.Entities;
using UnityEngine;
class MovementInputSystem : ComponentSystem
{

    protected override void OnUpdate()
    {
        var dtTime = Time.deltaTime;
        Entities.ForEach((ref Movement move, ref AxisInput input) =>
        {
            move.MoveVal = input.InputVector;
        });
    }
}