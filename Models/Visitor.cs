using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comm100.Models
{
    public class Visitor: User
    {
        private readonly ILogger<Worker> _logger;

        public Visitor(ILogger<Worker> logger)
        {
            this._logger = logger;
            this.Id = Guid.NewGuid();
            _logger.LogInformation("Visitor: {Visitor} created at: {time}", this.Id, DateTimeOffset.Now);
        }
    }
}
