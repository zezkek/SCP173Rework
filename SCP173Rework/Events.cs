using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.API.Enums;

namespace SCP173Rework
{
    public class Events
    {
        private readonly Plugin plugin;
        public Events(Plugin plugin)
        {
            this.plugin = plugin;
        }
        public void OnBlinking(BlinkingEventArgs ev)
        {
            if (ev.Player.Role == RoleType.Scp173)
                if (ev.Targets.Count >= plugin.Config.Watchers)
                {
                    ev.Player.EnableEffect(EffectType.Ensnared, 2);
                    ev.Player.EnableEffect(EffectType.Asphyxiated, 2);
                    //ev.Player.ShowHint("На вас смотрят <color=red>{int}</color> человек, вы не можете двигаться".Replace("{int}", ev.Targets.Count.ToString()), 5);
                }
                else
                    return;
                    //ev.Player.ShowHint("На вас смотрят <color=red>{int}</color> человек".Replace("{int}", ev.Targets.Count.ToString()), 5);
        }
        public void OnDamage(HurtingEventArgs ev)
        {
            if (ev.Target.Role != RoleType.Scp173) return;
            else if (ev.DamageType == DamageTypes.Nuke || ev.DamageType == DamageTypes.Wall || ev.DamageType == DamageTypes.Decont) return;
            if (plugin.Config.Debug)
                Log.Debug($"OnDamage event has been taken.\nTarget: {ev.Target.Nickname}\nRole: {ev.Target.Role}" +
                    $"\nAmount of damage: {ev.Amount}\nDamageType: {ev.DamageType.name}");
            if (ev.DamageType == DamageTypes.Grenade) ev.Amount *= 0.1f;
            else
                ev.IsAllowed = false;
        }
    }
}
