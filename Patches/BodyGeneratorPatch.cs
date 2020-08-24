﻿using CharacterCreation.Manager;
using HarmonyLib;
using System;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace CharacterCreation.Patches
{
    public static class BodyGeneratorPatch
    {
        [HarmonyPatch(typeof(BodyGenerator), nameof(BodyGenerator.SaveCurrentCharacter))]
        private static class SaveCurrentCharacter
        {
            private static readonly TextObject ErrorText = new TextObject("{=CharacterCreation_ErrorText}Error:");
            static bool Prefix(BodyGenerator __instance)
            {
                try
                {
                    __instance.Character.UpdatePlayerCharacterBodyProperties(__instance.CurrentBodyProperties, __instance.IsFemale);
                    if (__instance.Character is CharacterObject characterObject)
                    {
                        CharacterBodyManager.ResetBirthDayForAge(characterObject, __instance.CurrentBodyProperties.DynamicProperties.Age);
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ErrorText.ToString()}\n{ex.Message} \n\n{ex.InnerException?.Message}");
                    return true;
                }
            }
        }
    }
}