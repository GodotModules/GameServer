global using System.Collections.Generic;

using Godot;
using System;
using System.Collections.Concurrent;

namespace GodotModules.Netcode.Server
{
    public class ServerManager : Node
    {
        private static ConcurrentQueue<GodotCmd> GodotCmds { get; set; }

        public override async void _Ready()
        {
            GodotCmds = new ConcurrentQueue<GodotCmd>();

            NetworkManager.StartServer(25565, 256);
            NetworkManager.GameServer.Lobby = new DataLobby
            {
                Name = "DefaultName",
                Description = "DefaultDescription",
                HostId = 0
            };
            await WebClient.UpdateIsAlive();
            await WebClient.AddLobbyAsync(new LobbyListing {
                Ip = WebClient.ExternalIp,
                Description = "DefaultDescription",
                LobbyHost = "DefaultLobbyHost",
                MaxPlayerCount = 256,
                Name = "DefaultName",
                Port = 25565
            });
        }

        public override void _Process(float delta)
        {
            Logger.Dequeue();

            while (GodotCmds.TryDequeue(out GodotCmd cmd))
            {
                switch (cmd.Opcode)
                {
                    case GodotOpcode.LogMessage:
                        var message = (GodotMessage)cmd.Data;
                        var text = message.Text;
                        var color = message.Color;

                        Console.ForegroundColor = color;
                        GD.Print(text);

                        if (!string.IsNullOrWhiteSpace(message.Path))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            GD.Print($"   at ({message.Path})");
                        }

                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}