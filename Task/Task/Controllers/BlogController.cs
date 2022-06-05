using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.DAL;
using Task.Models;

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
            List<Blog> blogs = _context.Blogs.Include(b => b.Comments).ToList();

            return View(blogs);
        }
        public IActionResult Details(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Blog blog = _context.Blogs.Include(b=>b.Comments).Include(b => b.Tags).FirstOrDefault(b => b.Id == id);
            if (blog==null)
            {
                return NotFound();
            }

            return View(blog);
        }
    }
}
