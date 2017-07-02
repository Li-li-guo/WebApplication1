using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Filters
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class TableAttribute:Attribute
    {
        public TableAttribute()
        {
        }
        public TableAttribute(string tablename)
        {
            this.TableName = tablename;
        }
        public string TableName { get; set; }
    }
}