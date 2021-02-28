﻿using Vintagestory.API.Client;

namespace VintageMods.Core.Helpers.Extensions
{
    public static class CoreClientApiEx
    {
        public static void AddWaypointAtCurrentPos(
            this ICoreClientAPI api, string icon, string colour, string title, bool pinned)
        {
            var blockPos = api.World?.Player?.Entity?.Pos.AsBlockPos.RelativeToSpawn(api);
            if (blockPos is null) return;
            api.SendChatMessage(
                $"/waypoint addati {icon} {blockPos.X} {blockPos.Y} {blockPos.Z} {(pinned ? "true" : "false")} {colour} {title}");
        }
    }
}