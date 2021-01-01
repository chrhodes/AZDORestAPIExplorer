using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.Domain
{
    public class StatesRoot
    {
        public int count { get; set; }
        public State[] value { get; set; }
    }

    public class State
    {
        public string id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string stateCategory { get; set; }
        public int order { get; set; }
        public string url { get; set; }
        public string customizationType { get; set; }
    }
}
