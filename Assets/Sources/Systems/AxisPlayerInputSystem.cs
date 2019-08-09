using UnityEngine;
using Unity.Entities;
class PlayerAxisInputSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        // Debug.Log("input system");
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Entities.ForEach((ref AxisInput axisInput) =>
        {
            axisInput.InputVector = input;
        });
    }
}