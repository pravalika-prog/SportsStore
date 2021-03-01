using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;
        public int PageSize = 4;
        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }
        //public IActionResult Index() => View(repository.Products);
        //public ViewResult Index(int productPage = 1)
        //=> View(repository.Products
        //.OrderBy(p => p.ProductID)
        //.Skip((productPage - 1) * PageSize)
        //.Take(PageSize));
        public ViewResult Index(String category, int productPage = 1) => View(new ProductListViewModel
        {
            Products = repository.Products
            .Where(p =>category==null|| p.Category ==category)
.OrderBy(p => p.ProductID)
.Skip((productPage - 1) * PageSize)
.Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = repository.Products.Count()
            },
            CurrentCategory = category
        });
    }
}