using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.API.Enums;

namespace SCP049Rework
{
    public class Events
    {
        private readonly Plugin plugin;
        private List<Player> playerForProbableRecall = new List<Player> { };
        public Events(Plugin plugin)
        {
            this.plugin = plugin;
        }
        public void OnDeath(DyingEventArgs ev)
        {
            Log.Debug($"DyingEvent has been taken", plugin.Config.Debug);
            if (ev.Killer.Role == RoleType.Scp049)
            {
                playerForProbableRecall.Add(ev.Target);
                Log.Debug($"Target has been added", plugin.Config.Debug);
            }

        }
        public void OnSetClass(ChangingRoleEventArgs ev)
        {
            Log.Debug($"ChangingRoleEvent has been taken", plugin.Config.Debug);
            if (ev.NewRole == RoleType.Scp0492) return;
            if (playerForProbableRecall.Contains(ev.Player))
            {
                playerForProbableRecall.Remove(ev.Player);
                Log.Debug($"Target has been removed", plugin.Config.Debug);
            }

        }
        public void OnLeave(LeftEventArgs ev)
        {
            Log.Debug($"LeftEvent has been taken", plugin.Config.Debug);
            if (playerForProbableRecall.Contains(ev.Player))
            {
                playerForProbableRecall.Remove(ev.Player);
                Log.Debug($"Target has been removed", plugin.Config.Debug);
            }
        }
        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            Log.Debug($"RoundEndedEvent has been taken", plugin.Config.Debug);
            playerForProbableRecall.Clear();
            Log.Debug($"List of targets has been cleared", plugin.Config.Debug);
        }
        public void OnStartingRecall(StartingRecallEventArgs ev)
        {
            Log.Debug($"StartingRecallEvent has been taken", plugin.Config.Debug);
            if (!playerForProbableRecall.Contains(ev.Target))
            {
                ev.IsAllowed = false;
                if(plugin.Config.Debug)
                    Log.Warn($"Recall is not allowed");
            }
            else
                Log.Debug($"Recall is allowed", plugin.Config.Debug);

        }
        public void OnLiftInteracting(InteractingElevatorEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp0492)
                ev.IsAllowed = false;
        }
        public void OnActivatingScp914(ActivatingEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp0492)
                ev.IsAllowed = false;
        }

        public void OnKnobChangingScp914(ChangingKnobSettingEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp0492)
                ev.IsAllowed = false;
        }

        public void OnWarheadActivating(ActivatingWarheadPanelEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp0492)
                ev.IsAllowed = false;
        }

        public void OnChangingWarheadStatus(ChangingLeverStatusEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp0492)
                ev.IsAllowed = false;
        }

        public void OnClosingGenerator(ClosingGeneratorEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp0492)
                ev.IsAllowed = false;
        }

        public void OnEjectingTabletGenerator(EjectingGeneratorTabletEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp0492)
                ev.IsAllowed = false;
        }
        public void OnLockerInteract(InteractingLockerEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp0492)
                ev.IsAllowed = false;
        }
        public void OnStartingWarhead(StartingEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp0492)
                ev.IsAllowed = false;
        }
        public void OnStoppingWarhead(StoppingEventArgs ev)
        {
            if (!ev.IsAllowed || ev.Player.Role == RoleType.Scp0492)
                ev.IsAllowed = false;
        }
    }
}
