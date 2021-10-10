// #define MoveStop
// #define Hurt
namespace SCP173Rework
{
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;

    public class Events
    {
        private readonly Plugin plugin;

        public Events(Plugin plugin)
        {
            this.plugin = plugin;
        }

#if MoveStop
        private IEnumerator<float> CantMove(Player ev)
        {
            Log.Info("173 cant move right now");
            ev.CanSendInputs = false;
            yield return Timing.WaitForSeconds(2f);
            ev.CanSendInputs = true;
        }
        public void OnBlinking(BlinkingEventArgs ev)
        {
            if (ev.Player.Role == RoleType.Scp173)
                if (ev.Targets.Count >= plugin.Config.Watchers)
                {
                    CantMove(ev.Player);
                    //ev.Player.EnableEffect(EffectType.Ensnared, 2);
                    ev.Player.EnableEffect(EffectType.Asphyxiated, 2);
                    //ev.Player.ShowHint("На вас смотрят <color=red>{int}</color> человек, вы не можете двигаться".Replace("{int}", ev.Targets.Count.ToString()), 5);
                }
                else
                    return;
                    //ev.Player.ShowHint("На вас смотрят <color=red>{int}</color> человек".Replace("{int}", ev.Targets.Count.ToString()), 5);
        }
#endif
#if Hurt
        public void OnDamage(HurtingEventArgs ev)
        {
            if (ev.Target.Role != RoleType.Scp173)
            {
                return;
            }

            Log.Debug(
                $"OnDamage event has been taken.\nTarget: {ev.Target.Nickname}\nRole: {ev.Target.Role}" +
                $"\nAmount of damage: {ev.Amount}\nDamageType: {ev.DamageType.Name}", this.plugin.Config.Debug);
            if (ev.DamageType == DamageTypes.Nuke || ev.DamageType == DamageTypes.Wall || ev.DamageType == DamageTypes.Decont)
            {
                return;
            }
            else
            {
                ev.IsAllowed = false;
            }
        }
#endif
    }
}
