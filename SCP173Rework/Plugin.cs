using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events = Exiled.Events.EventArgs;
using Scp173Events = Exiled.Events.Handlers.Scp173;
using PlyEvents = Exiled.Events.Handlers.Player;
using SvEvents = Exiled.Events.Handlers.Server;
using ScpEvents = Exiled.Events.Handlers.Scp106;
using MapEvents = Exiled.Events.Handlers.Map;
using WarhEvents = Exiled.Events.Handlers.Warhead;
using Scp914Events = Exiled.Events.Handlers.Scp914;

namespace SCP173Rework
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "SCP173Rework";
        public override string Author { get; } = ".fkn_goose & Mydak";
        public override Version Version => new Version(0, 0, 2);
        public static readonly Lazy<Plugin> LazyInstance = new Lazy<Plugin>(valueFactory: () => new Plugin());
        public static Plugin PluginItem => LazyInstance.Value;
        private Events events;
        public override void OnEnabled()
        {
            events = new Events(this);
            Scp173Events.Blinking += events.OnBlinking;
            PlyEvents.Hurting += events.OnDamage;
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
            Scp173Events.Blinking -= events.OnBlinking;
            PlyEvents.Hurting -= events.OnDamage;
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
