using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace Stop_Poking_Me
{
	public class Settings : ModSettings
	{
		//Use Mod.settings.setting to refer to this setting.
		public bool doVoice = true;
		public bool doCritter = true;

		public void DoWindowContents(Rect wrect)
		{
			var options = new Listing_Standard();
			options.Begin(wrect);
			
			options.CheckboxLabeled("Do Thing 1", ref doVoice);
			options.CheckboxLabeled("Do Thing 2", ref doCritter);
			options.Gap();

			options.End();
		}
		
		public override void ExposeData()
		{
			Scribe_Values.Look(ref doVoice, "doVoice", true);
			Scribe_Values.Look(ref doCritter, "doCritter", true);
		}
	}
}