
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MSSQL.MVC.Models
{
    public class Imgs
    {
        public string Title { get; set; }
        public IFormFile File { get; set; }


        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="rootPath">系统路径</param>
        /// <param name="savePath">保存文件路径</param>
        /// <returns></returns>
        public async Task<string> Save(string rootPath,string savePath)
        {
            // 获取文件名与扩展名
            var fileName = ContentDispositionHeaderValue
                                .Parse(File.ContentDisposition)
                                .FileName
                                .Trim('"');
            var filePath = $@"{savePath}{fileName}";
            fileName = $@"{rootPath}{filePath}";
            //文件保存
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                await File.CopyToAsync(fs);
                fs.Flush();
            }

            return filePath;
        }
    }


}
