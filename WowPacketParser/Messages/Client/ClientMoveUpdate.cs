using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdate
    {
        public CliMovementStatus Status;
    }
}