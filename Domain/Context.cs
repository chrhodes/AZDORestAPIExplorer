using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.Domain
{
    public class Context
    {
        public Project SelectedProject { get; set; }
        public Team SelectedTeam { get; set; }
        public Process SelectedProcess { get; set; }
    }
}
