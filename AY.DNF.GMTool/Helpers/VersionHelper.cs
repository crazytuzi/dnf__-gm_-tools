using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace AY.DNF.GMTool.Helpers
{
    internal class VersionHelper
    {
        const string ReleasesApi = "https://gitee.com/api/v5/repos/AsakuraYou/dnf__-gm_-tools/releases/latest?access_token=7f7b1f6a4b0608f95c628843c31204b7";

        /// <summary>
        /// 获取最后版本信息
        /// </summary>
        /// <returns></returns>
        public static LasetReleaseVersionStruct? Get()
        {
            try
            {
                var client = new HttpClient();
                var task = client.GetAsync(ReleasesApi);
                var result = task.Result;
                var resultStr = result.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<LasetReleaseVersionStruct>(resultStr);
            }
            catch
            {
                return null;
            }
        }

        public static bool HasUpdate(string curVer, string lastVer)
        {
            curVer = curVer.Replace("v", "").Replace("V", "");
            lastVer = lastVer.Replace("v", "").Replace("V", "");

            var curArr = curVer.Split('.');
            var lastArr = lastVer.Split('.');

            for (var i = 0; i < curArr.Length; i++)
            {
                if (i == 2)
                {
                    var curDt = DateTime.ParseExact(curArr[i], "yyyyMMdd", CultureInfo.InvariantCulture);
                    var lastDt = DateTime.ParseExact(lastArr[i], "yyyyMMdd", CultureInfo.InvariantCulture);
                    if (curDt < lastDt) return true;

                    return false;
                }

                if (int.Parse(curArr[i]) < int.Parse(lastArr[i])) return true;
            }

            return false;
        }
    }

    struct LasetReleaseVersionStruct
    {
        /// <summary>
        /// 标签（发布版本号）
        /// </summary>
        public string tag_name { get; set; }

        /// <summary>
        /// 是否预览版
        /// </summary>
        public bool prerelease { get; set; }

        /// <summary>
        /// 发布名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 描述体
        /// </summary>
        public string body { get; set; }

        public List<AssetsStruct>? Assets { get; set; }

        public AssetsStruct? ReleaseAssets => Assets == null || Assets.Count <= 0 ? null : Assets[0];
    }

    struct AssetsStruct
    {
        public string browser_download_url { get; set; }
        public string name { get; set; }
    }
}
