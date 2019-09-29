using System;
using System.Collections.Generic;
using System.Text;

namespace Donate.Shared.Queues.Settings
{
    public class QueueConnectionSettings
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public int MaxConnectionRetries { get; set; }
        public int ConnectionRetryWaitSeconds { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
