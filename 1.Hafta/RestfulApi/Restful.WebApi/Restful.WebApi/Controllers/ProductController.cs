﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restful.WebApi.DAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Restful.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private List<Product> products;

        public PersonController()
        {
            products = new();
            products.Add(new Product { ID = 1, Category = "Kalem", Name = "Kalem 1", Barcod = 123, Price = 100 });
            products.Add(new Product { ID = 2, Category = "Kalem", Name = "Kalem 2", Barcod = 124, Price = 200 });
            products.Add(new Product { ID = 3, Category = "Kalem", Name = "Kalem 3", Barcod = 125, Price = 300 });
            products.Add(new Product { ID = 4, Category = "Silgi", Name = "Silgi 1", Barcod = 345, Price = 100 });
            products.Add(new Product { ID = 5, Category = "Silgi", Name = "Silgi 2", Barcod = 346, Price = 200 });
            products.Add(new Product { ID = 6, Category = "Silgi", Name = "Silgi 3", Barcod = 347, Price = 300 });
        }

        [HttpGet]
        public IActionResult GetProduct()
        {
            if (products == null)
            {
                return NotFound(); // 404 Not Found yanıtı
            }

            return Ok(products); // Ürünleri döndürme
        }

        [HttpGet("{id}")]
        public IActionResult GetProductByID(int id)
        {
            var productValue = products.Find(x => x.ID == id); // Ürünü arama

            if (productValue == null)
            {
                return NotFound(); // 404 Not Found yanıtı
            }

            return Ok(productValue); // Ürünü döndürme
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            product.ID = products.Count() + 1;

            products.Add(product);

            return Created("", product); // 201 Created yanıtı
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            var productValue = products.FirstOrDefault(x => x.ID == id);

            if (productValue == null)
            {
                return NotFound(); // 404 Not Found yanıtı
            }

            products.Remove(productValue);
            productValue.ID = id;
            products.Add(product);

            return Ok(products); // Güncellenmiş ve diğer ürünleri döndürme
        }


        [HttpPatch("{id}")] // Kişi Kısmi Güncelleme (Patch)
        public IActionResult PatchPerson(int id, [FromBody] Product product)
        {
            var productValue = products.FirstOrDefault(x => x.ID == id);

            if (productValue == null)
            {
                return NotFound(); // 404 Not Found yanıtı
            }

            productValue.Name = product.Name ?? productValue.Name;
            productValue.Category = product.Category ?? productValue.Category;

            return Ok(products); // Kısmi güncellenmiş ve diğer ürünleri döndürme
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            var productValue = products.FirstOrDefault(x => x.ID == id);

            if (productValue == null)
            {
                return NotFound(); // 404 Not Found yanıtı
            }

            products.Remove(productValue);

            return Ok(products); // Silinmiş ürünsüz listeyi döndürme
        }

        // ------------------------------------------------------------------------------------------

        [HttpGet("list")]
        public IActionResult GetProductsByName(string name)
        {
            var filteredProduct = products.Where(p => p.Name.Contains(name)).ToList(); //
            return Ok(filteredProduct);
        }

        [HttpGet("sort")]
        public IActionResult SortProducts(string sortBy, string sortDirection)
        {
            IQueryable<Product> sortedProducts;

            switch (sortBy)
            {
                case "name":
                    sortedProducts = sortDirection == "desc" ? products.OrderByDescending(p => p.Name).AsQueryable() : products.OrderBy(p => p.Name).AsQueryable();
                    break;
                case "price":
                    sortedProducts = sortDirection == "desc" ? products.OrderByDescending(p => p.Price).AsQueryable() : products.OrderBy(p => p.Price).AsQueryable();
                    break;
                default:
                    sortedProducts = products.AsQueryable();
                    break;
            }

            return Ok(sortedProducts.ToList());
        }
    }
}