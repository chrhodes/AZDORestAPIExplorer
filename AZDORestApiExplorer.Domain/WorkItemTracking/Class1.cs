using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    class Class1
    {
    }


    public class Rootobject
    {
        public Closed[] Closed { get; set; }
        public Requested[] Requested { get; set; }
        public Child[] _ { get; set; }
    }

    public class Closed
    {
        public string to { get; set; }
        public object actions { get; set; }
    }

    public class Requested
    {
        public string to { get; set; }
        public string[] actions { get; set; }
    }

    public class Child
    {
        public string to { get; set; }
        public object actions { get; set; }
    }

    //-- 


    //public class Rootobject
    //{
    //    public Child[] _ { get; set; }
    //}

    //public class Child
    //{
    //    public string to { get; set; }
    //    public object actions { get; set; }
    //}

    ////


    //public class Rootobject
    //{
    //    public string to { get; set; }
    //    public object actions { get; set; }
    //}


}
