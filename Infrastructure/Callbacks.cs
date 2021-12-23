using System;
using BasePlugin.Interfaces;

namespace Infrastructure
{
    public class Callbacks : ICallbacks
    {
        public Action StartSession { get; set; }
        public Action EndSession { get; set; }
    }
}