namespace SCP173Rework.Commands.Cuff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using CommandSystem;
    using Exiled.API.Enums;
    using Exiled.API.Extensions;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;

    using UnityEngine;

    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]

    public class Vent : ParentCommand
    {
        /// <summary>
        /// Gets or sets time, when feature was used last time.
        /// </summary>
        public static float LastTimeUsed { get; set; } = Time.time;

        /// <inheritdoc/>
        public override string Command { get; } = "vent";

        /// <inheritdoc/>
        public override string[] Aliases { get; } = new string[] { };

        /// <inheritdoc/>
        public override string Description { get; } = "Перемещение объектом 173 через вентиляцию";

        /// <inheritdoc/>
        public override void LoadGeneratedCommands()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vent"/> class.
        /// </summary>
        public Vent() => this.LoadGeneratedCommands();

        /// <inheritdoc/>
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player playerRequester = Player.Get((sender as CommandSender)?.SenderId);

            CommandMethods.LogCommandUsed((CommandSender)sender, CommandMethods.FormatArguments(arguments, 0));
            if (!((CommandSender)sender).CheckPermission("SCP173Rework.vent"))
            {
                response = "\n<color=#C1B5B5>СТАТУС: </color><color=#990000>ОШИБКА</color>\n<color=#C1B5B5>ВЫВОД: В ДОСТУПЕ ОТКАЗАНО</color>";
                return false;
            }

            if (playerRequester.Role != RoleType.Scp173)
            {
                response = "\n<color=#C1B5B5>СТАТУС: </color><color=#990000>ОШИБКА</color>\n<color=#C1B5B5>ВЫВОД: В ДОСТУПЕ ОТКАЗАНО</color>";
                return false;
            }

            if (LastTimeUsed > Time.time)
            {
                response = "\n<color=#C1B5B5>СТАТУС: </color><color=#990000>ОШИБКА</color>\n<color=#C1B5B5>ВЫВОД: СПОСОБНОСТЬ ВРЕМЕННО НЕДОСТУПНА</color>";
                return false;
            }
            else
            {
                List<RoomType> ignoredRoomsBy173 = new List<RoomType> { RoomType.Unknown, RoomType.Pocket, RoomType.Lcz173, RoomType.Lcz012, RoomType.Lcz914, RoomType.LczArmory, RoomType.HczArmory, RoomType.HczTesla, RoomType.Hcz939, RoomType.EzShelter, RoomType.Surface };
                if (Map.IsLczDecontaminated)
                {
                    ignoredRoomsBy173.AddRange(new List<RoomType> {
                RoomType.LczAirlock, RoomType.LczCafe, RoomType.LczChkpA, RoomType.LczChkpB,
                RoomType.LczClassDSpawn, RoomType.LczCrossing, RoomType.LczCurve, RoomType.LczGlassBox,
                RoomType.LczPlants, RoomType.LczStraight, RoomType.LczTCross, RoomType.LczToilets,
                RoomType.HczArmory, RoomType.Hcz939, RoomType.Hcz079,  RoomType.Hcz096, RoomType.HczTesla,
                RoomType.EzShelter, });
                }

                List<Room> targetRooms = new List<Room> { };

                foreach (var player in Player.List.Where(x => x.Team != Team.SCP && x.IsAlive))
                {
                    if (!ignoredRoomsBy173.Contains(player.CurrentRoom.Type) && playerRequester.CurrentRoom != player.CurrentRoom)
                    {
                        targetRooms.Add(player.CurrentRoom);
                    }
                }

                string randomRoom = string.Empty;
                if (targetRooms.IsEmpty())
                {
                    playerRequester.SendConsoleMessage("\n<color=#C1B5B5>СТАТУС: </color><color=#C1B5B5>ИНФО</color>\n<color=#C1B5B5>ВЫВОД: ЦЕЛЕЙ НЕ НАЙДЕНО.</color>", "white");
                    randomRoom = "В СЛУЧАЙНОЕ ПОМЕЩЕНИЕ";
                    foreach (var room in Map.Rooms)
                    {
                        if (!ignoredRoomsBy173.Contains(room.Type) && playerRequester.CurrentRoom != room)
                        {
                            targetRooms.Add(room);
                        }
                    }
                }

                if (targetRooms.IsEmpty())
                {
                    response = "\n<color=#C1B5B5>СТАТУС: </color><color=#990000>ОШИБКА</color>\n<color=#C1B5B5>ВЫВОД: ЦЕЛЕЙ НЕ НАЙДЕНО</color>";
                    // LastTimeUsed = Time.time + 20;
                    return false;
                }

                targetRooms.ShuffleList();
                Room target = targetRooms.FirstOrDefault();
                playerRequester.Position = (2 * Vector3.up) + target.Position;
                playerRequester.ShowHint($"{target.Type}");

                // PlayableScps.Scp173.Get173FromPlayerObject(playerRequester.GameObject).ServerDoBreakneckSpeeds();
                // PlayableScps.Scp173 script = (PlayableScps.Scp173)playerRequester.CurrentScp;
                // LastTimeUsed = Time.time + 20;
                response = $"\n<color=#C1B5B5>СТАТУС: </color><color=#6aa84f>УСПЕШНО</color>\n<color=#C1B5B5>ВЫВОД: ПЕРЕМЕЩЕНИЕ {randomRoom} ...</color>";
                return true;
            }
        }
    }
}
