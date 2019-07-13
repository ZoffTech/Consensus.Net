namespace consensus.net.bus
{
    public interface IConsensusOptions
    {
        bool PingEnable { get; }
        IEventBusOptions EventBusOptions { get; set; }
    }
}