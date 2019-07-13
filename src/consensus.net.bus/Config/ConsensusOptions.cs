using System.Collections.Generic;

namespace consensus.net.bus
{
    public class ConsensusOptions : IConsensusOptions
    {
        public ConsensusOptions()
        {
        }

        public IEventBusOptions EventBusOptions { get; set; }

        public List<IListener> Listeners = new List<IListener>();

        public bool AcceptPingResponses { get; internal set; }
    }
}