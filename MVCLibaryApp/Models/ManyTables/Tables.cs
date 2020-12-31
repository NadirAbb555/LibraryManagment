using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCLibaryApp.Models.Entity;
namespace MVCLibaryApp.Models.ManyTables
{
    public class Tables
    {
        public IEnumerable<TBLBOOK> book { get; set; }

        public IEnumerable<TBLMEMBERS> memmber { get; set; }
        public IEnumerable<TBLACTION> delivery { get; set; }
        public IEnumerable<TBLCASH> cash { get; set; }
        public IEnumerable<TBLSITEABOUT> about { get; set; }
    }
}