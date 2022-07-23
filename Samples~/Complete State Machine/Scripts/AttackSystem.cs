using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(TransformSystemGroup))]
[UpdateAfter(typeof(StateMachineEventsSystem))]
[DisableAutoCreation]
public partial class AttackSystem : SystemBase
{
    protected override void OnUpdate()
    {
        // Entities.ForEach((in AttackWindow attackWindow) =>
        // {
        //     FixedString32Bytes open = attackWindow.IsOpen ? "Open" : "Closed";
        //     Debug.Log(FixedString.Format("Attack window is: {0}", open));
        // }).Schedule();
    }
}