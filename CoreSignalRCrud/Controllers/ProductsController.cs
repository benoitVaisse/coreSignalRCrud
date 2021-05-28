using CoreSignalRCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSignalRCrud.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<SignalRServer> _signalR;
        

        public ProductsController(ApplicationDbContext context, IHubContext<SignalRServer> signalR)
        {
            this._context = context;
            this._signalR = signalR;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            Products Products = await this._context.Products.FindAsync(Id);
            if(Products == null)
            {
                return NotFound();
            }

            return View(Products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Category,Price,StockQty")] Products products)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(products);
                await _context.SaveChangesAsync();
                await this._signalR.Clients.All.SendAsync("LoadProducts");
                RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Products product = await this._context.Products.FindAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? Id, [Bind("Id,Name,Category,Price,StockQty")] Products products)
        {
            if(Id != products.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                this._context.Products.Update(products);
                await this._context.SaveChangesAsync();
                await this._signalR.Clients.All.SendAsync("LoadProducts");
                RedirectToAction(nameof(Index));
            }
            return View(products);
        }
    }
}
