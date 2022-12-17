using System.Reflection;
using System.Linq;
using Verse;
using Verse.Sound;
using RimWorld;
using UnityEngine;
using HarmonyLib;

namespace Stop_Poking_Me
{
	public class Mod : Verse.Mod
	{
		public static Settings settings;
		public Mod(ModContentPack content) : base(content)
		{
			// initialize settings
			settings = GetSettings<Settings>();

#if DEBUG
			Harmony.DEBUG = true;
#endif

			Harmony harmony = new Harmony("Uuugggg.rimworld.Stop_Poking_Me.main");	
			harmony.PatchAll();
		}

		public override void DoSettingsWindowContents(Rect inRect)
		{
			base.DoSettingsWindowContents(inRect);
			settings.DoWindowContents(inRect);
		}

		public override string SettingsCategory()
		{
			return "Stop Poking Me";
		}
	}

	[DefOf]
	public static class TDSoundDefOf
	{
		public static SoundDef TD_WhyPoke;
	}

	[HarmonyPatch(typeof(Selector), "SelectUnderMouse")]
	public static class SelectUnderMouseCount
	{
		static int count = 0;
		static Thing lastSingleSelectedThing;
		public static void Postfix(Selector __instance)
		{
			Thing singleThing = __instance.SingleSelectedThing;

			// Start the count on this new thing
			if(singleThing != lastSingleSelectedThing)
			{
				lastSingleSelectedThing = singleThing;
				count = 1;
				return;
			}

			// Count on same thing
			if (++count == 5)
			{
				// At 5, sound off
				count = 0;

				TDSoundDefOf.TD_WhyPoke.PlayOneShotOnCamera();
			}
		}
	}
}