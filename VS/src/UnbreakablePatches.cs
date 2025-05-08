using Il2CppTLD.BigCarry;

namespace TravoisTether
{
    internal class UnbreakablePatches
    {
        [HarmonyPatch(typeof(TravoisBigCarryItem), nameof(TravoisBigCarryItem.HandleInaccessibleTravois))]
        private static class TravoidPosCheckFailCancel
        {
            internal static bool Prefix(ref TravoisBigCarryItem __instance)
            {
                Log(CC.Red, $"Travois failed position check at {__instance.transform.position} | {__instance.transform.eulerAngles} in {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");

                if (Settings.options.reposition)
                { 
                    AlignToSurface(__instance.transform);
                }

                return false;
            }
        }
    }   
}
 