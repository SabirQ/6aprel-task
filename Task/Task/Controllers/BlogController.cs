using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.DAL;
using Task.Models;
using Task.ViewModels;

namespace Task.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Blog> blogs = _context.Blogs.Include(b => b.Comments).Include(b => b.Tags).ToList();

            return View(blogs);
        }
        public IActionResult Details(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            BlogVM model = new BlogVM
            {
                Blog = _context.Blogs.Include(b => b.Comments).Include(b => b.Tags).FirstOrDefault(b => b.Id == id),
                Blogs=_context.Blogs.OrderByDescending(b => b.Date).Take(4).ToList()
            };
            
            if (model.Blog==null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
