using System;

namespace LoggingMicroservice.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Application { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }

        public string LogSeverity {get; set; }

         public string HostName {get; set; }

         public int AssociateId {get; set; }

         public string Technology {get; set; }

         public string ModuleName {get; set; }

         public string FeatureName {get; set; }

         public string ClasName {get; set; }

         public int ErrorCode {get; set; }

         public string ErrMessage {get ; set;}
        public DateTime CreatedAt { get; set; }
    }
}
