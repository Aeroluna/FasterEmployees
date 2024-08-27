using HarmonyLib;
using UnityEngine.AI;

namespace FasterEmployees;

[HarmonyPatch(typeof(NPC_Manager), nameof(NPC_Manager.SpawnEmployee))]
internal static class NoCollideEmployeePatch
{
    [HarmonyPostfix]
    private static void DisableCollision(NPC_Manager __instance)
    {
        foreach (NavMeshAgent agent in __instance.employeeParentOBJ.GetComponentsInChildren<NavMeshAgent>())
        {
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        }
    }
}
