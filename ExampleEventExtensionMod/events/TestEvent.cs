using GameNetcodeStuff;
using LuckyDice.custom.monobehaviour.attributes;
using LuckyDice.custom.monobehaviour.def;
using LuckyDice.custom.network;
using Unity.Netcode;

namespace ExampleEventExtensionMod.events
{
    [MountAtRegistry] // Mounts the event right after registering it
    [NeedsRemoval] // Let's me know the event needs to be manually removed at the end of the level
    [OneTimeEvent] // Makes the event be removed after it's been triggered once from all pools
    public class TestEvent : BasePlayerEvent
    {
        public override void AddPlayer(PlayerControllerB player)
        {
            EventManager.Instance.DisplayMessageClientRPC(new NetworkObjectReference(),
                "This is a test event! (it will now be removed)",
                $"Player: {player.playerUsername} has joined the event!");
        }
    }
}