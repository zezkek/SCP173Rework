//#define MoveStop
using Exiled.API.Features;
using System;
using PlyEvents = Exiled.Events.Handlers.Player;
using Scp914Events = Exiled.Events.Handlers.Scp914;
using WarhEvents = Exiled.Events.Handlers.Warhead;

namespace SCP173Rework
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "SCP173Rework";
        public override string Author { get; } = ".fkn_goose & Mydak";
        public override string Prefix => "SCP173Rework";
        public override Version Version => new Version(0, 0, 3);
        public static readonly Lazy<Plugin> LazyInstance = new Lazy<Plugin>(valueFactory: () => new Plugin());
        public static Plugin PluginItem => LazyInstance.Value;
        private Events events;
        public override void OnEnabled()
        {
            events = new Events(this);
#if MoveStop
            Scp173Events.Blinking += events.OnBlinking;
#endif
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
#if MoveStop
            Scp173Events.Blinking -= events.OnBlinking;
#endif
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
