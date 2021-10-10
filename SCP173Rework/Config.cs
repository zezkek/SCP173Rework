namespace SCP173Rework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Exiled.API.Enums;
    using Exiled.API.Interfaces;

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = true;

        // Оставить до переноса
        // [Description("Count of humans needful to stop SCP173")]
        // public sbyte Watchers { get; set; } = 4;

        [Description("Should SCP-173 be be prevented from interacting with opened doors.")]
        public bool OpenedDoorsAccess { get; set; } = true;

        [Description("Should SCP-173 be be prevented from interacting with elevators.")]
        public bool RestrictedElevatorAccess { get; set; } = false;

        [Description("Should SCP-173 be be prevented from interacting with lockers.")]
        public bool RestrictedLockerAccess { get; set; } = true;

        [Description("Should SCP-173 be be prevented from interacting with generators.")]
        public bool RestrictGeneratorsAccess { get; set; } = true;

        [Description("Should SCP-173 be be prevented from interacting with warhead.")]
        public bool RestrictWarheadAccess { get; set; } = true;

        [Description("Should SCP-173 be be prevented from interacting with SCP-914.")]
        public bool RestrictSCP914Access { get; set; } = true;

        [Description("Should SCP-173 be be prevented from interacting with workstation.")]
        public bool RestrictWorkstationAccess { get; set; } = true;

        [Description("Which doors will be prevented from opening by SCP-173")]
        public List<DoorType> SpecDoorAccess { get; set; } = new List<DoorType>()
        {
        };

    }
}
