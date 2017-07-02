using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Filters;

namespace WebApplication1.Models
{
    [Table("fsfds")]
    public class eeeModel
    {
        //private int -id
        //public int Id
        //{
        //    get { return _id; }
        //    set { Id = _id; }
        //}

        //private int _id;
        //public int Id
        //{
        //    get { return _id; }
        //    set { if (Id > 0) { _id = Id; } }
        //}

        public int Id { get; set; }
        //[Required(AllowEmptyStrings=false,ErrorMessage="名称不能为空")]
        //[DisplayName("名称")]
        public string Name { get; set; }
        //[StringLength(8)]
        //[DisplayName("优势")]
        public string Advantage { get; set; }
        //[DisplayName("劣势")]
        // [StringLength(10,MinimumLength =6)]
        public string Disadvantage { get; set; }
        //[DisplayName("国家")]
        public string Country { get; set; }
        //[DisplayName("推荐")]
        public string Recommend_Models { get; set; }
        public int State { get; set; }
    }
}