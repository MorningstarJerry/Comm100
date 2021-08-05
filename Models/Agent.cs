using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comm100.Models
{
    public class Agent : User
    {
        private readonly ILogger<Worker> _logger;

        public Agent(ILogger<Worker> logger)
        {
            this._logger = logger;
            this.Id = Guid.NewGuid();
            _logger.LogInformation("Agent: {Agent} created at: {time}", this.Id, DateTimeOffset.Now);
        }

        public void Join(IChat chat)
        {
            chat.Join(this);
        }

        public void Accept(IChat chat)
        {
            chat.Start();
        }
    }
}
