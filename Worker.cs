using Comm100.Models;
using Comm100.Question2;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Comm100
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                /*Question 1. Class Design*/

                _logger.LogInformation("********************************Question1-Class Design***********************************");
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var visitor = new Visitor(_logger);
                var chat = new Chat(visitor, _logger);

                var agent1 = new Agent(_logger);
                agent1.Accept(chat);

                var agent2 = new Agent(_logger);
                agent2.Join(chat);

                var agent3 = new Agent(_logger);
                agent3.Join(chat);

                chat.Send(agent1, agent3);
                chat.Receive(agent3, agent1);


                /*Question 2. Programming */
                Log[] logsGroup1 = new Log[2] { new Log(0, 1), new Log(20, 0) };
                Log[] logsGroup2 = new Log[2] { new Log(10, 0), new Log(20, 1) };
                Log[] logsGroup3 = new Log[3] { new Log(0, 1), new Log(10, 0), new Log(20, 0) };
                Log[] logsGroup4 = new Log[3] { new Log(0, 1), new Log(30, 1), new Log(50, 0) };
                _logger.LogInformation("********************************Question2-1***********************************");
                var result1 = Calculator.GetOnLineTime(0, 100, logsGroup1);
                _logger.LogInformation("Worker logsGroup1: {result1}", result1);

                var result2 = Calculator.GetOnLineTime(0, 100, logsGroup2);
                _logger.LogInformation("Worker logsGroup2: {result2}", result2);

                var result3 = Calculator.GetOnLineTime(0, 100, logsGroup3);
                _logger.LogInformation("Worker logsGroup3: {result3}", result3);

                var result4 = Calculator.GetOnLineTime(0, 100, logsGroup4);
                _logger.LogInformation("Worker logsGroup3: {result3}", result4);


                _logger.LogInformation("********************************Question2-2***********************************");
                Log[] logsQ2Group1 = new Log[4] { new Log(0, 1), new Log(20, 0), new Log(86400000, 1), new Log(86400030, 0) };
                Log[] logsQ2Group2 = new Log[3] { new Log(0, 1), new Log(86400000, 1), new Log(86400030, 0) };
                var arraysQ21 = Calculator.GetOnlineTimePerDay(0, 86400000 * 2, logsQ2Group1);
                var arraysQ22 = Calculator.GetOnlineTimePerDay(0, 86400000 * 2, logsQ2Group2);
                _logger.LogInformation("Worker logsQ2Group1: [{result}]", string.Join(",", arraysQ21));
                _logger.LogInformation("Worker logsQ2Group1: [{result}]", string.Join(",", arraysQ22));

                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}
