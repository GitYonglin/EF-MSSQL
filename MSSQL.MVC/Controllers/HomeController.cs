using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using MSSQL.MVC.Models;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.IO;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSSQL.MVC.Controllers
{
    public class HomeController : Controller
    {
        private Model _context;

        public HomeController(Model context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            using (var db = _context)
            {
                var blogs = db.Blogs
                    .Include(blog => blog.Posts)
                    .Include(c => c.BlogCategorys)
                    .Include(img => img.Imgs)
                    .ToList();
                var categorys = db.Categorys.ToList();
                //发送到前台 可以使用 as 转换
                ViewData["categorys"] = categorys;
                return View(blogs);
            }
        }
        /// <summary>
        /// 新建数据保存
        /// </summary>
        /// <param name="env">系统路径获取</param>
        /// <param name="blog">blog类</param>
        /// <param name="categorys">分类集合</param>
        /// <param name="img">图片类集合</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index([FromServices]IHostingEnvironment env, Blog blog, List<int> categorys,List<Imgs> img)
        {
            var imgs = new List<Img>();
            // 图片保存获取路径
            foreach (var item in img)
            {
                var url = await item.Save(env.WebRootPath,@"\updata\");
                imgs.Add(new Img {Title = item.Title, Url = url });
            }
            blog.Imgs = imgs;

            // blog 不可以直接设置 new了一个新的实体
            var dcs = new List<BlogCategory>();
            // 构造好选中的分类
            foreach (var item in categorys)
            {
                dcs.Add(new BlogCategory { CategoryId = item });
            }
            blog.BlogCategorys = dcs;
            using (var db = _context)
            {
                db.Blogs.Add(blog);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 分类显示
        /// </summary>
        /// <returns></returns>
        public IActionResult Category()
        {
            using (var context = _context)
            {
                // 这个BlogCategorys 不可以获取到下面的Blog实例
                var category = context.Categorys
                    .Include(c => c.BlogCategorys)
                    .ToList();

                return View(category);
            }
        }
        [HttpPost]
        public IActionResult Category(Category category)
        {
            using (var db = _context)
            {
                db.Categorys.Add(category);
                db.SaveChanges();
            }
            return RedirectToAction("category", "Home");
        }

    }
}
