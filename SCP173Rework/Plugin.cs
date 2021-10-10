// #define MoveStop
namespace SCP173Rework
{
    using System;
    using Exiled.API.Features;
    using PlayerEvents = Exiled.Events.Handlers.Player;
    using Scp049Events = Exiled.Events.Handlers.Scp049;
    using Scp914Events = Exiled.Events.Handlers.Scp914;
    using ServerEvents = Exiled.Events.Handlers.Server;
    using WarheadEvents = Exiled.Events.Handlers.Warhead;

    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "SCP173Rework";

        public override string Author { get; } = ".fkn_goose & Mydak";

        public override string Prefix => "SCP173Rework";

        public override Version Version => new Version(0, 1, 0);

        private static readonly Plugin InstanceValue = new Plugin();

        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 4);

        public Doors Doors;

        public Generators Generators;

        public SCP914 Scp914;

        public Warhead Warhead;

        public Workstation Workstation;

        private Plugin()
        {
        }

        public static Plugin Instance => InstanceValue;

        public override void OnEnabled()
        {
            this.Doors = new Doors(this);
            this.Generators = new Generators(this);
            this.Scp914 = new SCP914(this);
            this.Warhead = new Warhead(this);
            this.Workstation = new Workstation(this);
#if MoveStop
            Scp173Events.Blinking += events.OnBlinking;
#endif
#if Hurt
            PlyEvents.Hurting += this.events.OnDamage;
#endif

            PlayerEvents.InteractingElevator += this.Doors.OnInteractingElevator;
            PlayerEvents.InteractingLocker += this.Doors.OnInteractingLocker;
            PlayerEvents.InteractingDoor += this.Doors.OnInteractingDoor;

            PlayerEvents.UnlockingGenerator += this.Generators.OnUnlockingGenerator;
            PlayerEvents.OpeningGenerator += this.Generators.OnOpeningGenerator;
            PlayerEvents.ActivatingGenerator += this.Generators.OnActivatingGenerator;
            PlayerEvents.StoppingGenerator += this.Generators.OnStoppingGenerator;
            PlayerEvents.ClosingGenerator += this.Generators.OnClosingGenerator;

            Scp914Events.Activating += this.Scp914.OnActivatingScp914;
            Scp914Events.ChangingKnobSetting += this.Scp914.OnKnobChangingScp914;

            PlayerEvents.ActivatingWarheadPanel += this.Warhead.OnActivatingWarheadPanel;
            WarheadEvents.ChangingLeverStatus += this.Warhead.OnChangingLeverStatus;
            WarheadEvents.Starting += this.Warhead.OnStarting;
            WarheadEvents.Stopping += this.Warhead.OnStopping;

            PlayerEvents.ActivatingWorkstation += this.Workstation.OnActivatingWorkstation;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
#if MoveStop
            Scp173Events.Blinking -= events.OnBlinking;
#endif
#if Hurt
            PlyEvents.Hurting -= this.events.OnDamage;
#endif
            PlayerEvents.InteractingElevator -= this.Doors.OnInteractingElevator;
            PlayerEvents.InteractingLocker -= this.Doors.OnInteractingLocker;
            PlayerEvents.InteractingDoor -= this.Doors.OnInteractingDoor;

            PlayerEvents.UnlockingGenerator -= this.Generators.OnUnlockingGenerator;
            PlayerEvents.OpeningGenerator -= this.Generators.OnOpeningGenerator;
            PlayerEvents.ActivatingGenerator -= this.Generators.OnActivatingGenerator;
            PlayerEvents.StoppingGenerator -= this.Generators.OnStoppingGenerator;
            PlayerEvents.ClosingGenerator -= this.Generators.OnClosingGenerator;

            Scp914Events.Activating -= this.Scp914.OnActivatingScp914;
            Scp914Events.ChangingKnobSetting -= this.Scp914.OnKnobChangingScp914;

            PlayerEvents.ActivatingWarheadPanel -= this.Warhead.OnActivatingWarheadPanel;
            WarheadEvents.ChangingLeverStatus -= this.Warhead.OnChangingLeverStatus;
            WarheadEvents.Starting -= this.Warhead.OnStarting;
            WarheadEvents.Stopping -= this.Warhead.OnStopping;

            PlayerEvents.ActivatingWorkstation -= this.Workstation.OnActivatingWorkstation;
            this.Doors = null;
            this.Generators = null;
            this.Scp914 = null;
            this.Warhead = null;
            this.Workstation = null;

            base.OnDisabled();
        }
    }
}
