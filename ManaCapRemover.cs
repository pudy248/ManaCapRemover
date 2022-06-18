using Terraria.ModLoader;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ID;
using Mono.Cecil.Cil;

namespace ManaCapRemover
{
	public class ManaCapRemover : Mod
	{
		public override void Load()
		{
			IL.Terraria.Player.Update += HookUpdateManaLimit;
			base.Load();
		}

		//Swaps the first occurance of the number 400 to 10000, which happens to be in the line  if(this.statManaMax2 > 400), which is the culprit for our unfortunate capping.
		private void HookUpdateManaLimit(ILContext il)
		{
			var c = new ILCursor(il);

			if (!c.TryGotoNext(i => i.MatchLdcI4(400)))
				return;

			c.Remove();
			c.Emit(OpCodes.Ldc_I4, 10000);

		}
	}
}