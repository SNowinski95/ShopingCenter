﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Infrastructure.Queue
{
    public class QueueConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string QueueName { get; set; }
        public string Key { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
