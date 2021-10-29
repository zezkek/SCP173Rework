#define MoveStop
#define Hurt
namespace SCP173Rework
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;

    public class Events
    {
        private readonly Plugin plugin;

        public Events(Plugin plugin)
        {
            this.plugin = plugin;
        }

#if MoveStop
        public void OnBlinking(BlinkingEventArgs ev)
        {
            if (ev.Player.Role == RoleType.Scp173)
            {
                if (ev.Targets.Count >= this.plugin.Config.Watchers)
                {
                    ev.IsAllowed = false;
                }
            }
        }

#endif
        public void OnDamage(HurtingEventArgs ev)
        {
            if (ev.Target.Role != RoleType.Scp173)
            {
                return;
            }

            Log.Debug(
                $"OnDamage event has been taken.\nTarget: {ev.Target.Nickname}\nRole: {ev.Target.Role}" +
                $"\nAmount of damage: {ev.Amount}\nDamageType: {ev.DamageType.Name}", this.plugin.Config.Debug);
            if (ev.DamageType == DamageTypes.Nuke || ev.DamageType == DamageTypes.Wall)
            {
                return;
            }
            else if (ev.DamageType == DamageTypes.Grenade)
            {
                ev.Amount *= 0.1f;
            }
            else
            {
                ev.IsAllowed = false;
            }
        }
    }
}
