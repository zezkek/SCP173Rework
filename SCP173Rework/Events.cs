//#define MoveStop
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

        public void OnActivatingScp914(ActivatingEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp173)
                ev.IsAllowed = false;
        }

        public void OnKnobChangingScp914(ChangingKnobSettingEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp173)
                ev.IsAllowed = false;
        }

        public void OnWarheadActivating(ActivatingWarheadPanelEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp173)
                ev.IsAllowed = false;
        }

        public void OnChangingWarheadStatus(ChangingLeverStatusEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp173)
                ev.IsAllowed = false;
        }

        public void OnClosingGenerator(ClosingGeneratorEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp173)
                ev.IsAllowed = false;
        }

        public void OnEjectingTabletGenerator(EjectingGeneratorTabletEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp173)
                ev.IsAllowed = false;
        }
        public void OnLockerInteract(InteractingLockerEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp173)
                ev.IsAllowed = false;
        }
        public void OnStartingWarhead(StartingEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp173)
                ev.IsAllowed = false;
        }
        public void OnStoppingWarhead(StoppingEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp173)
                ev.IsAllowed = false;
        }
    }
}
