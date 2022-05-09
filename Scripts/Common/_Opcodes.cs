﻿namespace GodotModules.Netcode
{
    // Received from Game Client
    public enum ClientPacketOpcode
    {
        Ping,
        Lobby,
        PlayerPosition,
        PlayerMovementDirections,
        PlayerRotation,
        PlayerShoot
    }

    // Sent to Game Client
    public enum ServerPacketOpcode
    {
        Pong,
        Lobby,
        PlayerTransforms
    }

    public enum LobbyOpcode
    {
        LobbyCreate,
        LobbyJoin,
        LobbyLeave,
        LobbyKick,
        LobbyInfo,
        LobbyChatMessage,
        LobbyReady,
        LobbyCountdownChange,
        LobbyGameStart
    }

    public enum DisconnectOpcode
    {
        Disconnected,
        Timeout,
        Maintenance,
        Restarting,
        Stopping,
        Kicked,
        Banned
    }
}