using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WersDictionary.Models
{
    public class WersItem
    {
        public int ID { get; set; }
        public string FamilyCode { get; set; }
        public string FamilyDescription { get; set; }
    }

    public class WersItemDBContext : DbContext
    {
        public DbSet<WersItem> Movies { get; set; }
    }
}