using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace FasterEmployees;

[HarmonyPatch]
internal static class RandomStoragePatch
{
    /*[HarmonyPrefix]
    [HarmonyPatch(typeof(NPC_Manager), nameof(NPC_Manager.GetFreeStorageContainer))]
    private static void RandomizeFreeStorageContainer(NPC_Manager __instance, out int __result, out bool __runOriginal)
    {
        __runOriginal = false;
        if (__instance.storageOBJ.transform.childCount == 0)
        {
            __result = -1;
            return;
        }
        int[] range = Enumerable.Range(0, __instance.shelvesOBJ.transform.childCount).ToArray();
        range.Shuffle();
        foreach (int i in range)
        {
            int[] productInfoArray = __instance.storageOBJ.transform.GetChild(i).GetComponent<Data_Container>().productInfoArray;
            int num = productInfoArray.Length / 2;
            for (int j = 0; j < num; j++)
            {
                if (productInfoArray[j * 2] == -1)
                {
                    __result = i;
                    return;
                }
            }
        }

        __result = -1;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(NPC_Manager), nameof(NPC_Manager.GetFreeStorageRow))]
    private static void RandomizeFreeStorageRow(NPC_Manager __instance, out int __result, out bool __runOriginal, int storageContainerIndex)
    {
        __runOriginal = false;
        int[] productInfoArray = __instance.storageOBJ.transform.GetChild(storageContainerIndex).GetComponent<Data_Container>().productInfoArray;
        int num = productInfoArray.Length / 2;
        int[] range = Enumerable.Range(0, num).ToArray();
        range.Shuffle();
        foreach (int i in range)
        {
            if (productInfoArray[i * 2] == -1)
            {
                __result = i;
                return;
            }
        }
        __result = -1;
    }*/

    [HarmonyPrefix]
    [HarmonyPatch(typeof(NPC_Manager), nameof(NPC_Manager.CheckProductAvailability))]
    private static void RandomizeProductAvailability(NPC_Manager __instance, out int[] __result, out bool __runOriginal)
    {
        __runOriginal = false;
        int[] array =
        [
            -1,
            -1,
            -1,
            -1,
            -1,
            -1
        ];
        if (__instance.storageOBJ.transform.childCount == 0)
        {
            __result = array;
            return;
        }
        int[] range = Enumerable.Range(0, __instance.shelvesOBJ.transform.childCount).ToArray();
        range.Shuffle();
        foreach (int i in range)
        {
            int[] productInfoArray = __instance.shelvesOBJ.transform.GetChild(i).GetComponent<Data_Container>().productInfoArray;
            int num = productInfoArray.Length / 2;
            for (int j = 0; j < num; j++)
            {
                int num2 = productInfoArray[j * 2];
                if (num2 >= 0)
                {
                    int num3 = productInfoArray[j * 2 + 1];
                    int num4 = Mathf.FloorToInt(__instance.GetMaxProductsPerRow(i, num2) * 0.78f);
                    if (num3 == 0 || num3 <= num4)
                    {
                        for (int k = 0; k < __instance.storageOBJ.transform.childCount; k++)
                        {
                            int[] productInfoArray2 = __instance.storageOBJ.transform.GetChild(k).GetComponent<Data_Container>().productInfoArray;
                            int num5 = productInfoArray2.Length / 2;
                            for (int l = 0; l < num5; l++)
                            {
                                int num6 = productInfoArray2[l * 2];
                                if (num6 >= 0 && num6 == num2 && productInfoArray2[l * 2 + 1] > 0)
                                {
                                    array[0] = i;
                                    array[1] = j * 2;
                                    array[2] = k;
                                    array[3] = l * 2;
                                    array[4] = num2;
                                    array[5] = num6;
                                    __result = array;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        __result = array;
    }

    private static void Shuffle<T>(this IList<T> ts) {
        int count = ts.Count;
        int last = count - 1;
        for (int i = 0; i < last; ++i) {
            int r = Random.Range(i, count);
            (ts[i], ts[r]) = (ts[r], ts[i]);
        }
    }
}
