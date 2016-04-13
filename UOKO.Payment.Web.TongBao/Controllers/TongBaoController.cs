using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using UOKO.Payment.Web.TongBao.Models;
using System.Configuration;
using System.Collections.Generic;

namespace UOKO.Payment.Web.TongBao.Controllers
{
    public class TongBaoController : Controller
    {

        public async Task<ActionResult> Index(string token)
        {

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Can not be empty by token.");
            }

            var tenantInfo = await GetTenantInfo(token);

            if (null == tenantInfo)
            {
                throw new Exception("Token 无效,没有找到对应的用户");
            }

            using (var client = new HttpClient())
            {
                var apiAddress = ConfigurationManager.AppSettings["TongBaoApi"].ToString();
                //client.BaseAddress = new Uri("http://localhost:58013/");
                client.BaseAddress = new Uri(apiAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent.ParseAdd(Request.UserAgent);
                // New code:
                HttpResponseMessage response = await client.PostAsJsonAsync("/api/tanentInfo", tenantInfo);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<BaseResultModel>().Result;

                    if (result.Flag && result.UserId == null)
                    {
                        var url = string.Format("~/app/info.html?systoken={0}&userid={1}&accestoken={2}", result.SysToken, result.UserId, result.AccessToken);
                        return Redirect(url);

                    }
                    else
                    {
                        var url = string.Format("~/app/register.html?systoken={0}&tenantId={1}", result.SysToken, tenantInfo.TenantId);
                        return Redirect(url);

                    }
                }
            }
            return View();

        }

        /// <summary>
        /// 得到用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// 
        [NonAction]
        private async Task<TenantInfo> GetTenantInfo(string token)
        {
            using (var client = new HttpClient())
            {
                var apiAddress = ConfigurationManager.AppSettings["BusinessApi"].ToString();

                client.BaseAddress = new Uri(apiAddress);    `
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent.ParseAdd(Request.UserAgent);
                // New code:
                HttpResponseMessage response = await client.PostAsJsonAsync("api/gettenantbankinfo?token=" + token, "");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<ResultModel<TenantInfo>>().Result;

                    if (result.Code == "0")
                    {
                        return result.SingleModel as TenantInfo;
                    }
                }
            }
            return null;
        }
    }
}