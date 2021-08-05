using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comm100.Models
{
    public class Chat:IChat
    {
        public Guid Id { get; set; }
        private int _status = 0;  /*0:INIT 1:START 2:STOP */
        public Visitor Visitor { get; set; }
        public List<Agent> Agents { get; set; }
        private readonly ILogger<Worker> _logger;

        public Chat(Visitor visitor, ILogger<Worker> logger)
        {
            this.Visitor = visitor;
            this.Agents = new List<Agent>();
            this.Id = Guid.NewGuid();
            this._logger = logger;
            _logger.LogInformation("INIT-chat:{chatId} init at: {time}",this.Id , DateTimeOffset.Now);
        }

        public bool Start()
        {
            _logger.LogInformation("START-chat:{chatId} Start at: {time}", this.Id, DateTimeOffset.Now);
            this._status = 1;
            return true;
        }

        public bool Stop()
        {
            this._status = 2;
            return true;
        }

        public bool Send(User from, User to)
        {
            _logger.LogInformation("SEND-chat:{chatId} Send from:{from} to:{to} at: {time}", this.Id, from.Id, to.Id, DateTimeOffset.Now);
            return true;
        }

        public bool Receive(User from, User to)
        {
            _logger.LogInformation("RECEIVE:chat:{chatId} Receive from:{from} to:{to} at: {time}", this.Id, from.Id, to.Id, DateTimeOffset.Now);
            return true;
        }

        public void Join(Agent agent)
        {
            this.Agents.Add(agent);
            _logger.LogInformation("JOIN:chat:{chatId} add Agent: {Agent} at: {time}", this.Id, agent.Id, DateTimeOffset.Now);
        }
    }
}
