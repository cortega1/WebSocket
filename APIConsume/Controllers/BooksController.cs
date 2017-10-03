using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using APIConsume.Models;
using System.Collections;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace APIConsume.Controllers
{
    public class BooksController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Ticker(string book)
        {
            return View();
        }
    }
}