using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class StateMachineStart : MonoBehaviour
{
    private List<SystemBase> systems = new List<SystemBase>();
    private void Awake()
    {
        systems.Add(World.DefaultGameObjectInjectionWorld.CreateSystem<StateMachineExampleUISystem>());
        systems.Add(World.DefaultGameObjectInjectionWorld.CreateSystem<StateMachineEventsSystem>());
        systems.Add(World.DefaultGameObjectInjectionWorld.CreateSystem<AttackSystem>());
    }

    private void Update()
    {
        foreach (var systemBase in systems)
        {
            systemBase.Update();
        }
    }
}
