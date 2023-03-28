﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company obj)
        {

            _db.Companys.Update(obj);
            //var objFromDb = _db.Companys.FirstOrDefault(u=> u.Id == obj.Id);

            //if (objFromDb != null)
            //{
            //    objFromDb.Name = obj.Name;
            //    objFromDb.StreetAddress = obj.StreetAddress;
            //    objFromDb.City = obj.City;
            //    objFromDb.State = obj.State;
            //    objFromDb.PostalCode = obj.PostalCode;
            //    objFromDb.PhoneNumber = obj.PhoneNumber;
            //}
        }
    }
}
