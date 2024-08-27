using HarmonyLib;

namespace FasterEmployees;

[HarmonyPatch(typeof(UpgradesManager), nameof(UpgradesManager.OnStartClient))]
internal static class ImprovedEmployeePatch
{
    [HarmonyPostfix]
    private static void AddEmployees()
    {
        NPC_Manager instance = NPC_Manager.Instance;
        instance.extraEmployeeSpeedFactor += 0.3f;
        instance.maxEmployees += 3;
        instance.UpdateEmployeesNumberInBlackboard();
        instance.UpdateEmployeeStats();
    }
}
