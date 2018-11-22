using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using linkedInTutorial.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace linkedInTutorial.Controllers
{
    public class BlogController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var posts = new[]
            {
                new Post
                {
                    Title = "My blog post",
                Posted = DateTime.Now,
                Author = "Soumalya",
                Body = "This is a good blog isn't it?"
                },

                new Post
            {
                Title = "Ayan blog post",
                Posted = DateTime.Now,
                Author = "Ayan",
                Body = "This is a good blog isn't it?"
            },
         };
            return View(posts);
        }


        [Route("blog/{year:int}/{month:int}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = new Post
            {
                Title = "My blog post",
                Posted = DateTime.Now,
                Author = "Soumalya",
                Body = "This is a good blog isn't it?"
            };
            return View(post);
        }
    }
}




