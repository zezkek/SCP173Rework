using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events = Exiled.Events.EventArgs;
using PlyEvents = Exiled.Events.Handlers.Player;
using SvEvents = Exiled.Events.Handlers.Server;
using ScpEvents = Exiled.Events.Handlers.Scp106;
using MapEvents = Exiled.Events.Handlers.Map;
using WarhEvents = Exiled.Events.Handlers.Warhead;
using Scp914Events = Exiled.Events.Handlers.Scp914;
using Scp049Events = Exiled.Events.Handlers.Scp049;

namespace SCP049Rework
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "SCP049Rework";
        public override string Author { get; } = "Mydak";
        public override Version Version => new Version(0, 0, 1);
        public static readonly Lazy<Plugin> LazyInstance = new Lazy<Plugin>(valueFactory: () => new Plugin());
        public static Plugin PluginItem => LazyInstance.Value;
        private Events events;
        public override void OnEnabled()
        {
            events = new Events(this);
            PlyEvents.Dying += events.OnDeath;
            PlyEvents.ChangingRole += events.OnSetClass;
            PlyEvents.Left += events.OnLeave;
            SvEvents.RoundEnded += events.OnRoundEnd;
            Scp049Events.StartingRecall += events.OnStartingRecall;
            PlyEvents.InteractingElevator += events.OnLiftInteracting;

            Scp914Events.Activating += events.OnActivatingScp914;
            Scp914Events.ChangingKnobSetting += events.OnKnobChangingScp914;
            WarhEvents.ChangingLeverStatus += events.OnChangingWarheadStatus;
            PlyEvents.ClosingGenerator += events.OnClosingGenerator;
            PlyEvents.EjectingGeneratorTablet += events.OnEjectingTabletGenerator;
            PlyEvents.InteractingLocker += events.OnLockerInteract;
            WarhEvents.Starting += events.OnStartingWarhead;
            WarhEvents.Stopping += events.OnStoppingWarhead;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            PlyEvents.Dying -= events.OnDeath;
            PlyEvents.ChangingRole -= events.OnSetClass;
            PlyEvents.Left -= events.OnLeave;
            SvEvents.RoundEnded -= events.OnRoundEnd;
            Scp049Events.StartingRecall -= events.OnStartingRecall;
            PlyEvents.InteractingElevator -= events.OnLiftInteracting;

            Scp914Events.Activating -= events.OnActivatingScp914;
            Scp914Events.ChangingKnobSetting -= events.OnKnobChangingScp914;
            WarhEvents.ChangingLeverStatus -= events.OnChangingWarheadStatus;
            PlyEvents.ClosingGenerator -= events.OnClosingGenerator;
            PlyEvents.EjectingGeneratorTablet -= events.OnEjectingTabletGenerator;
            PlyEvents.InteractingLocker -= events.OnLockerInteract;
            WarhEvents.Starting -= events.OnStartingWarhead;
            WarhEvents.Stopping -= events.OnStoppingWarhead;
            events = null;
            base.OnDisabled();
        }
    }
}
