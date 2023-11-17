using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System.Net;
using Newtonsoft.Json;

namespace SK_JoinLeave

{
    public class SK_JoinLeave : RocketPlugin<SK_JoinLeaveConfig>
    {
        internal SK_JoinLeave Instance;
        public UnityEngine.Color JoinMessageColor { get; private set; }
        public UnityEngine.Color LeaveMessageColor { get; private set; }
        protected override void Load()
        {
            Instance = this;
            if (Instance.Configuration.Instance.JoinMessageEnable)
            {
                JoinMessageColor = UnturnedChat.GetColorFromName(Configuration.Instance.JoinMessageColor, UnityEngine.Color.green);
                U.Events.OnPlayerConnected += Events_OnPlayerConnected;
            }
            if (Instance.Configuration.Instance.LeaveMessageEnable)
            {
                LeaveMessageColor = UnturnedChat.GetColorFromName(Configuration.Instance.LeaveMessageColor, UnityEngine.Color.red);
                U.Events.OnPlayerDisconnected += Events_OnPlayerDisconnected;
            }
            Instance.Configuration.Save();
            Rocket.Core.Logging.Logger.Log("SK_JoinLeave plugin active!");
            Rocket.Core.Logging.Logger.Log("Would you like more free plugins? Join now: https://discord.gg/y3rYs7ZXFs");
        }

        protected override void Unload()
        {
            if (Instance.Configuration.Instance.JoinMessageEnable)
                U.Events.OnPlayerConnected -= Events_OnPlayerConnected;
            if (Instance.Configuration.Instance.LeaveMessageEnable)
                U.Events.OnPlayerDisconnected -= Events_OnPlayerDisconnected;

            Rocket.Core.Logging.Logger.Log("SK_JoinLeave plugin disabled!");
            Rocket.Core.Logging.Logger.Log("Would you like more free plugins? Join now: https://discord.gg/y3rYs7ZXFs");
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList
                {
                    { "connect_message", "{0} connected to the server." },
                    { "disconnect_message", "{0} disconnected from the server." }
                };
            }
        }

      
        private void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            Message(player, true);
        }

        private void Events_OnPlayerDisconnected(UnturnedPlayer player)
        {
            if (player != null)
                Message(player, false);
            else
                Logger.LogWarning("Uyarı: The DC message for this player did not work because the player data is not working.");
        }
        private void Message(UnturnedPlayer player, bool join)
        {
            UnturnedChat.Say(Translate(join ? "connect_message" : "disconnect_message", player.CharacterName), join == true ? JoinMessageColor : LeaveMessageColor);
        }

    }
}

