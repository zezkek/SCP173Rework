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
    public class Handler
    {
        public void OnBlinking(BlinkingEventArgs ev)
        {
            if (ev.Player.Role == RoleType.Scp173)
                if (ev.Targets.Count >= 4)
                {
                    ev.Player.EnableEffect(EffectType.Ensnared,2);
                    ev.Player.ShowHint("На вас смотрят <color=red>{int}</color> человек, вы не можете двигаться".Replace("{int}", ev.Targets.Count.ToString()), 5);
                }
                else
                    ev.Player.ShowHint("На вас смотрят <color=red>{int}</color> человек".Replace("{int}", ev.Targets.Count.ToString()), 5);
        }
    }
}
