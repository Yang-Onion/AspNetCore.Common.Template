using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AspNetCore.Common.Infrastructure.Web
{
    public class UtilityHelper
    {
        public Task DeleteFileAsync(IHostingEnvironment host, string fileName) {
            var fullPath = host.WebRootPath + "\\uploads\\" + fileName;
            if (File.Exists(fullPath)) {
                File.Delete(fullPath);
            }
            return Task.CompletedTask;
        }

        public static JsonSerializerSettings JsonSettings() {
            return new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = "yyyy-MM-dd HH:mm"
            };
        }

        public static string ConvertToJsonString(object obj) {
            return JsonConvert.SerializeObject(obj, JsonSettings());
        }

        /// <summary>
        /// 返回文件名
        /// </summary>
        public static async Task<List<string>> UploadFileAsync(IConfiguration cfg, IFormFileCollection files) {
            var fileNameList = new List<string>();
            foreach (var file in files) {
                string fileName = await UploadFileAsync(cfg, file);
                fileNameList.Add(fileName);
            }
            return fileNameList;
        }

        /// <summary>
        /// 返回文件名
        /// </summary>
        public static async Task<string> UploadFileAsync(IConfiguration cfg, IFormFile file) {
            //try {
            //    if (file.Length <= 1024 * 1024 * 10) {
            //        var client = new AwsClient(cfg);
            //        var fileId = Guid.NewGuid().ToString();
            //        var res = await client.PutObject(fileId, file.OpenReadStream());
            //        if (res) {
            //            return fileId;
            //        }
            //        throw new DomainException("文件上传失败，请稍后再试");
            //    }
            //    throw new DomainException("单个文件大小不能超过10MB");
            //}
            //catch (DomainException) {
            //    throw;
            //}
            //catch (Exception) {
            //    return string.Empty;
            //}
            return string.Empty;
        }
    }
}