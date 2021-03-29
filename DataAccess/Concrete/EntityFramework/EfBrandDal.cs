using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBrandDal : EfEntityRepositoryBase<Brand, CarRentalCompanyContext> , IBrandDal
    {
        public List<CarBrandDetailDto> GetCarAndBrandDetails()
        {
            using (CarRentalCompanyContext context = new CarRentalCompanyContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join r in context.Rentals
                             on c.Id equals r.CarId
                             select new CarBrandDetailDto
                             {
                                 CarID = c.Id,
                                 BrandID = b.BrandId,
                                 RentalID = r.Id,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate
                             };

                return result.ToList();

            }
        }
        
        public bool DeleteBrandIfNotReturnDateNull(Brand brand)
        {
            using (CarRentalCompanyContext context = new CarRentalCompanyContext())
            {
                var find = GetCarAndBrandDetails().Any(i => i.BrandID == brand.BrandId && i.ReturnDate == null);
                if (!find)
                {
                    context.Remove(brand);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
