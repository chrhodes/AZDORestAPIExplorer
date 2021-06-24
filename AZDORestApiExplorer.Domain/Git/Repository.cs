using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.Domain.Git
{
    public class RepositoriesRoot
    {
        public int count { get; set; }
        public Repository[] value { get; set; }
    }

    public class Repository
    {
    }
    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Rename classes to use TYPEsRoot and TYPE classes

}
