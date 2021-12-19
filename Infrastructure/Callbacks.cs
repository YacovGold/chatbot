using System;
using BasePlugin.Interfaces;

namespace Infrastructure
{
    public class Callbacks : ICallbacks
    {
        // public IPlugin Plugin { get; set; }
        // public bool IsActive { get; private set; }

        // public Action StartSession => this.IsActive = true;

        // public Action EndSession => throw new NotImplementedException();
        public Action StartSession { get; set; }
        public Action EndSession { get; set; }
    }
}