using System;

namespace Geex.ElasticSearch.Zero.Logging.Commom
{
    public struct LogMessage
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Message { get; set; }
    }
}
