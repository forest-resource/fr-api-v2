using fr.Core.Timing;
using System;

namespace fr.AppServer.Models
{
    public abstract class ResponseModel
    {
        public int Status { get; set; }
        public string Code { get; set; }
        public DateTime ResponseTime { get => Clock.Now; }
    }
}
