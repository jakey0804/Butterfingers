using System.Collections;
using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;
using Comfort.Common;
using EFT.InventoryLogic;
using UnityEngine;

namespace Butterfingers.Patches;

public class Patches
{
    
    static IEnumerator WaitThenDrop(Player player)
    {
        //God forbid I try to use OnDropWeapon without waiting this took me way too long to figure out
        yield return new WaitForSeconds(0.1f);
        player.HandsController.OnDropWeapon();
    }
    
   
    internal class ButterfingersP : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Player.FirearmController.GClass2029), nameof(Player.FirearmController.GClass2029.InternalOnFireEvent));
        }
        [PatchPostfix]
        static void Postfix(Player.FirearmController.GClass2029 __instance)
        {
            
            
            var state = __instance.Weapon_0.MalfState.State;
            if (state != Weapon.EMalfunctionState.None)
            { 
                if (Random.Range(0f, 1f) <= Plugin.DropChance.Value)
                {
                    Plugin.Instance.StartCoroutine(WaitThenDrop(__instance.Player_0));   
                }
            }
            
        }
    }
}