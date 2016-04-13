using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UOKO.Payment.Web.TongBao.Models
{
    public class ResultModel<T> : BaseResultModel
    {

        public T SingleModel { get; set; }
        public List<T> Model { get; set; }
    }

    public class BaseResultModel
    {
        public string Code { get; set; }

        public string SysToken { get; set; }
        public bool Flag { get; set; }
        public string AccessToken { get; set; }

        public string UserId { get; set; }
    }
}