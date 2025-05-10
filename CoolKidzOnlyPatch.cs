using GorillaLocomotion;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace GorillaStepsButWorks
{
    [HarmonyPatch(typeof(GorillaStats.Main))]
    [HarmonyPatch("Update", MethodType.Normal)]
    internal class CoolKidzOnlyPatch
    {
        public static float fontSizeTarget;
        public static TextAlignmentOptions alignmentTarget;
        private static bool toReturn = true;
        private static bool wasCollidingLastL, wasCollidingLastR;
        private static int counter;
        private static bool isPressed;
        private static float lastTime;
        
        private static bool Prefix(GorillaStats.Main __instance)
        {
            if (ControllerInputPoller.instance.rightControllerSecondaryButton && !isPressed)
            {
                toReturn = !toReturn;
            }
            isPressed = ControllerInputPoller.instance.rightControllerSecondaryButton;
            
            if (GTPlayer.Instance.wasLeftHandColliding && !wasCollidingLastL ||
                GTPlayer.Instance.wasRightHandColliding && !wasCollidingLastR)
            {
                if (Time.time > lastTime)
                {
                    lastTime = Time.time + 0.15f;
                    counter++;
                }
            }
            wasCollidingLastL = GTPlayer.Instance.wasLeftHandColliding;
            wasCollidingLastR = GTPlayer.Instance.wasRightHandColliding;
            
            if (toReturn)
            {
                if (__instance.watchText != null)
                {
                    __instance.watchText.fontSize = fontSizeTarget;
                    __instance.watchText.alignment = alignmentTarget;
                }
                return true;
            }
            else
            {
                if (__instance.watchText != null)
                {
                    __instance.watchText.fontSize = 48f;
                    __instance.watchText.alignment = TextAlignmentOptions.Center;
                    __instance.watchText.text = $"Steps:\n{counter}";
                }
                return false;
            }
        }
    }

    [HarmonyPatch(typeof(GorillaStats.Main))]
    [HarmonyPatch("Init", MethodType.Normal)]
    internal class IPatchInit
    {
        private static void Postfix(GorillaStats.Main __instance)
        {
            CoolKidzOnlyPatch.fontSizeTarget = __instance.watchText.fontSize;
            CoolKidzOnlyPatch.alignmentTarget = __instance.watchText.alignment;
        }
    }
}