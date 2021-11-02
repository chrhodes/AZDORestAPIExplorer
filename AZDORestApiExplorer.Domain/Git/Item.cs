using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Git.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;
namespace AZDORestApiExplorer.Domain.Git
{
    namespace Events
    {
        public class GetItemsEvent : PubSubEvent<GetItemsEventArgs> { }

        public class GetItemsEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;

            public Domain.Git.GitRepository Repository;

            public string ScopePath;

            public string RecursionLevel;
        }

        public class SelectedItemChangedEvent : PubSubEvent<Item> { }
    }
    public class ItemsRoot
    {
        public int count { get; set; }
        public Item[] value { get; set; }
    }

    public class Item
    {
        public string objectId { get; set; }
        public string gitObjectType { get; set; }
        public string commitId { get; set; }
        public string path { get; set; }
        public bool isFolder { get; set; }
        public string url { get; set; }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Item

}
