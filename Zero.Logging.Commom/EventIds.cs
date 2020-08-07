using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Geexbox.Logging.ElasticSearch.ZeroLoggingCommom
{
    public struct GeexboxEventId
    {
        public EventId val;

        public GeexboxEventId(EventId eventId)
        {
            this.val = eventId;
        }

        public static GeexboxEventId RemoteEndPointRequest
        {
            get
            {
                return new GeexboxEventId(new EventId(nameof(RemoteEndPointRequest).GetHashCode(), nameof(RemoteEndPointRequest)));
            }
        }

        public static GeexboxEventId RemoteEndPointResponse
        {
            get
            {
                return new GeexboxEventId(new EventId(nameof(RemoteEndPointResponse).GetHashCode(), nameof(RemoteEndPointResponse)));
            }
        }

        public static EventId AuditLog
        {
            get
            {
                return new GeexboxEventId(new EventId(nameof(AuditLog).GetHashCode(), nameof(AuditLog)));
            }
        }

        public override string ToString()
        {
            return val.ToString();
        }
        // .... and so on....

        // User-defined conversion from MyColor to Color
        public static implicit operator EventId(GeexboxEventId c)
        {
            return c.val;
        }
        //  User-defined conversion from Color to MyColor
        public static implicit operator GeexboxEventId(EventId c)
        {
            return new GeexboxEventId(c);
        }
    }
}
