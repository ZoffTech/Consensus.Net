using consensus.net.bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consensus.net.core
{
    public class ConsensusOptions : IConsensusOptions
    {

        public ConsensusOptions()
        {

        }

        public IEventBusOptions EventBusOptions { get; set; } = new EventBusOptions();


        public List<IListener> Listeners = new List<IListener>();

        public bool AcceptPingResponses { get; internal set; }
    }
}
