using UnityEngine;
using Unity.Entities;

public class PlayerAxisInput : AxisInput
{ }
class PlayerAxisInputSystem : ComponentSystem
{
    struct ReqComponent
    {
        public PlayerAxisInput axis;
    }
    protected override void OnUpdate()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), 0);
        foreach (var e in GetEntities<ReqComponent>())
        {
            e.axis.InputVector = input;
        }
    }
}