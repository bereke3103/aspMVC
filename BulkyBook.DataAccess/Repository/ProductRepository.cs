﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        //public void Save()
        //{
        //    _db.SaveChanges();
        //}

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u=> u.Id == obj.Id);

            if (objFromDb != null)
            {
                objFromDb.Title= objFromDb.Title;
                objFromDb.ISBN= objFromDb.ISBN;
                objFromDb.Description= objFromDb.Description;
                objFromDb.ListPrice= objFromDb.ListPrice;
                objFromDb.Price = objFromDb.Price;
                objFromDb.Price50 = objFromDb.Price50;
                objFromDb.Price100 = objFromDb.Price100;
                objFromDb.CategoryId= objFromDb.CategoryId;
                objFromDb.Author = objFromDb.Author;
                objFromDb.CoverTypeId = objFromDb.CoverTypeId;

                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
