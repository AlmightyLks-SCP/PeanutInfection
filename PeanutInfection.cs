using CustomGamemode;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System.Linq;

namespace PeanutInfection
{
    public class PeanutInfection : IGamemode
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string GitHubRepo { get; set; }
        public string Version { get; set; }
        public PeanutInfection()
        {
            Author = "AlmightyLks";
            Name = "PeanutInfection";
            GitHubRepo = "https://github.com/AlmightyLks/PeanutInfection";
            Version = "1.0.0.0";
        }

        public void Start()
        {
            Synapse.Api.Events.EventHandler.Get.Round.SpawnPlayersEvent += Round_SpawnPlayersEvent;
            Synapse.Api.Events.EventHandler.Get.Player.PlayerDeathEvent += Player_PlayerDeathEvent;
            Synapse.Api.Events.EventHandler.Get.Map.LCZDecontaminationEvent += Map_LCZDecontaminationEvent;
            Synapse.Api.Events.EventHandler.Get.Player.PlayerSetClassEvent += Player_PlayerSetClassEvent;

            SynapseController.Server.Logger.Info("PeanutInfection Started");
        }

        public void End()
        {
            Synapse.Api.Events.EventHandler.Get.Round.SpawnPlayersEvent -= Round_SpawnPlayersEvent;
            Synapse.Api.Events.EventHandler.Get.Player.PlayerDeathEvent -= Player_PlayerDeathEvent;
            Synapse.Api.Events.EventHandler.Get.Map.LCZDecontaminationEvent -= Map_LCZDecontaminationEvent;
            Synapse.Api.Events.EventHandler.Get.Player.PlayerSetClassEvent -= Player_PlayerSetClassEvent;

            SynapseController.Server.Logger.Info("PeanutInfection Ended");
        }

        private void Round_SpawnPlayersEvent(SpawnPlayersEventArgs ev)
        {
            SynapseController.Server.Map.Round.RoundLock = true;

            foreach (var player in ev.SpawnPlayers.Keys.ToArray())
                ev.SpawnPlayers[player] = (int)RoleType.ClassD;

            Player rndPeen = SynapseController.Server.Players[UnityEngine.Random.Range(0, SynapseController.Server.Players.Count)];

            ev.SpawnPlayers[rndPeen] = (int)RoleType.Scp173;

            SynapseController.Server.Map.Round.RoundLock = false;
        }
        private void Player_PlayerDeathEvent(PlayerDeathEventArgs ev)
        {
            if (ev.Victim.RoleID != 0)
                ev.Victim.RoleID = (int)RoleType.Scp173;
        }
        private void Player_PlayerSetClassEvent(PlayerSetClassEventArgs ev)
        {
            if (ev.Role != RoleType.ClassD && ev.Role != RoleType.Scp173)
                ev.Allow = false;
        }
        private void Map_LCZDecontaminationEvent(LCZDecontaminationEventArgs ev)
            => ev.Allow = false;
    }
}
