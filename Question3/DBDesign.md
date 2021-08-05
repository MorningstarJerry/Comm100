#Question1 Class Design
```
  public interface IChat
    {
        void Join(Agent agent);
        bool Start();
        bool Stop();
        bool Send(User from, User to);
        bool Receive(User from, User to);
    }

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

    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

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

```
#Question2 
```
    public class Calculator
    {
        public static long GetOnLineTime(long start, long end, Log[] logs)
        {
            long total = 0;
            if ((1).Equals(logs.Length))
            {
                if ((0).Equals(logs.Last().Action))
                {//last is logout
                    //total = logs.Last().Time;
                    total = 0;
                }
                else
                {
                    //last is login
                    total = (end - logs.Last().Time);
                }
            }
            else
            {
                if ((0).Equals(logs.Last().Action))
                {//last is logout
                    total += 0;
                }
                else
                {
                    //last is login
                    total += (end - logs.Last().Time);
                }

                end = logs.Last().Time;
                var newLen = logs.Length - 1;
                Log[] newLogs = new Log[newLen];
                Array.ConstrainedCopy(logs, 0, newLogs, 0, newLen);

                total += GetOnLineTime(start, end, newLogs);
            }

            return total;
        }


        public static long[] GetOnlineTimePerDay(long start, long end, Log[] logs)
        {
            List<long> arys = new List<long>();
            GetOnlineTimeLists(start, end, logs, arys);
            arys.RemoveAll(x => x.Equals(0));
            return arys.ToArray().Reverse().ToArray();
        }


        public static void GetOnlineTimeLists(long start, long end, Log[] logs, List<long> arys)
        {
            if ((1).Equals(logs.Length))
            {
                if ((0).Equals(logs.Last().Action))
                {//last is logout
                    //total = logs.Last().Time;
                    arys.Add(0);
                }
                else
                {
                    //last is login
                    arys.Add(end - logs.Last().Time);
                }
            }
            else
            {
                if ((0).Equals(logs.Last().Action))
                {//last is logout
                    arys.Add(0);
                }
                else
                {
                    //last is login
                    arys.Add(end - logs.Last().Time);
                }

                end = logs.Last().Time;
                var newLen = logs.Length - 1;
                Log[] newLogs = new Log[newLen];
                Array.ConstrainedCopy(logs, 0, newLogs, 0, newLen);

                GetOnlineTimeLists(start, end, newLogs, arys);
            }
        }
    }
```
#Question3 DB Design
--------------------------
## TicketsMaster Ticketing 信息主表 
```
CREATE TABLE `ticketsmaster` (
  `Id` char(36) NOT NULL,
  `TenantId` char(36) DEFAULT NULL COMMENT '租户 Id ',
  `Title` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Descroption` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  `Status` char(1) NOT NULL COMMENT '状态',
  `Assignee` char(36) DEFAULT NULL,
  `Department` char(36) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `ticketsdetails` (
  `Id` char(36) NOT NULL,
  `TicketId` char(36) DEFAULT NULL COMMENT '主表 TicketId 关联',
  `CustomMapId` char(36) DEFAULT NULL COMMENT '关联定制表 Id',
  `CustomTypeVal` char(36) NOT NULL,
  `CreationTime` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `ticket_F_index_idx` (`TicketId`),
  KEY `custom_F_index_idx` (`CustomMapId`),
  CONSTRAINT `custom_F_index` FOREIGN KEY (`CustomMapId`) REFERENCES `custommap` (`Id`),
  CONSTRAINT `ticket_F_index` FOREIGN KEY (`TicketId`) REFERENCES `ticketsmaster` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `custommap` (
  `Id` char(36) NOT NULL,
  `TenantId` char(36) DEFAULT NULL COMMENT '租户 ID',
  `TypeId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '外键关联 custom type',
  `CreationTime` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='租户 custom 映射表';

CREATE TABLE `customtype` (
  `Id` char(36) NOT NULL,
  `Name` char(36) DEFAULT NULL,
  `CreationTime` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Custom Type 存储表';

CREATE TABLE `customtypeoptions` (
  `Id` char(36) NOT NULL,
  `TypeId` char(36) DEFAULT NULL,
  `Name` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Type  Option 值 存储列表';
```