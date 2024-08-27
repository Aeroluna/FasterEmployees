using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace FasterEmployees;

[HarmonyPatch]
internal static class EmployeesBrainLagPatch
{
    private static readonly MethodInfo _startWaitState = AccessTools.Method(typeof(NPC_Info), nameof(NPC_Info.StartWaitState));

    [HarmonyTranspiler]
    [HarmonyPatch(typeof(NPC_Manager), nameof(NPC_Manager.EmployeeNPCControl))]
    private static IEnumerable<CodeInstruction> RemoveBrainLag(IEnumerable<CodeInstruction> instructions)
    {
        return new CodeMatcher(instructions)
            .MatchForward(
                false,
                new CodeMatch(OpCodes.Ldc_R4),
                new CodeMatch(OpCodes.Ldc_I4_0),
                new CodeMatch(OpCodes.Callvirt, _startWaitState))
            .Repeat(matcher => matcher
                .SetOperandAndAdvance(0.1f))
            .InstructionEnumeration();
    }
}
