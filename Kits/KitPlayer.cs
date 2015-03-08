using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using TShockAPI;

namespace Kits
{
	public static class TSPlayerExtensions
	{
		private static ConditionalWeakTable<TSPlayer, KitPlayer> players = new ConditionalWeakTable<TSPlayer, KitPlayer>();

		public static KitPlayer GetKitPlayer(this TSPlayer tsplayer)
		{
			return players.GetOrCreateValue(tsplayer);
		}
	}

	public class KitPlayer
	{
		public Dictionary<string, DateTime> cooldowns = new Dictionary<string, DateTime>();

		public int GetKitCooldown(Kit k)
		{
			if (cooldowns.ContainsKey(k.name))
			{
				var when = cooldowns[k.name];
				return k.cooldown - (int)(DateTime.Now - when).TotalSeconds;
			}

			return 0;
		}

		public void UseKit(String name)
		{
			cooldowns[name] = DateTime.Now;
		}
	}
}
