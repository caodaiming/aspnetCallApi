using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UOKO.Payment.Web.TongBao.Models
{
    public class TenantInfo
    {
        /// <summary>
        /// 平台类型
        /// </summary>
        public string Platform { get; set; }
        /// <summary>
        /// 租客Id
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 真实名字
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 银行预留手机号
        /// </summary>
        public string ResverdPhone { get; set; }

        /// <summary>
        /// 银行代码
        /// </summary>
        public string BankCode { get; set; }
        /// <summary>
        /// 银行名字
        /// </summary>
        public string BankName { get; set; }
    }
}