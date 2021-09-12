using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events = Exiled.Events.EventArgs;
using Scp173 = Exiled.Events.Handlers.Scp173;
using PlyEv = Exiled.Events.Handlers.Player;

namespace SCP173Rework
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "SCP173Rework";
        public override string Author { get; } = ".fkn_goose";
        public override Version Version => new Version(0, 0, 1);
        public static readonly Lazy<Plugin> LazyInstance = new Lazy<Plugin>(valueFactory: () => new Plugin());
        public static Plugin PluginItem => LazyInstance.Value;
        private Events events;
        public override void OnEnabled()
        {
            events = new Events(this);
            Scp173.Blinking += events.OnBlinking;
            PlyEv.Hurting += events.OnDamage;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Scp173.Blinking -= events.OnBlinking;
            PlyEv.Hurting -= events.OnDamage;
            events = null;
            base.OnDisabled();
        }
    }
}
