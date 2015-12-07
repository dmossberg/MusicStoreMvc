using System;
using System.Diagnostics.Tracing;

namespace MvcMusicStore.Logging
{
    public class ErrorEvent : EventSource
    {
        public class Keywords
        {
            public const EventKeywords Database = (EventKeywords)1;
            public const EventKeywords OutboundCall = (EventKeywords)2;
            public const EventKeywords Unknown = (EventKeywords)4;
        }

        //create a static field instance that provides access to an instance of this class
        private static readonly Lazy<ErrorEvent> Instance
          = new Lazy<ErrorEvent>(() => new ErrorEvent());
         //create a static property called Log that returns the current value 
        //of the Instance field of the event source. This value is 
        //determined by the custom event methods called in the Customerservices application.
        private ErrorEvent() { }
        
        public static ErrorEvent Log
        {
            get { return Instance.Value; }
        }

        [Event(1, Message = "Database access error", Keywords = Keywords.Database, Level = EventLevel.Error)]
        internal void DatabaseError(string type, string message, string stackTrace)
        {
            this.WriteEvent(1, type, message, stackTrace);
        }

        [Event(2, Message = "unhandled error.", Keywords = Keywords.Unknown, Level = EventLevel.Error)]
        internal void ExcepcionNoManejada(string type, string message, string stackTrace)
        {
            this.WriteEvent(2, type, message, stackTrace);
        }

        [Event(3, Message = "Outbound service call error", Keywords = Keywords.OutboundCall, Level = EventLevel.Error)]
        internal void ServiceCallError(string type, string message, string stackTrace, string url)
        {
            this.WriteEvent(3, type, message, stackTrace, url);
        }
    }
}