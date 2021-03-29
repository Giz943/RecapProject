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
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalCompanyContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (CarRentalCompanyContext context = new CarRentalCompanyContext())
            {
                var result = from c in filter == null ? context.Cars : context.Cars.Where(filter)
                             join b in context.Brands
                                 on c.BrandId equals b.BrandId
                             join co in context.Colors
                                 on c.ColorId equals co.Id
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 BrandId = b.BrandId,
                                 ColorId = c.ColorId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ImagePath = (from i in context.CarImages where i.CarId == c.Id select i.ImagePath).ToList(),
                                 IsRentable = context.Rentals.Any(r => r.CarId == c.Id && r.ReturnDate == null)
                                 //IsRentable = (from r in context.Rentals where r.CarID == c.CarID && r.ReturnDate == null ).Any()
                             };
                return result.ToList();
            }
        }



        public bool DeleteCarIfNotReturnDateNull(Car car)
        {
            using (CarRentalCompanyContext context = new CarRentalCompanyContext())
            {
                var find = context.Rentals.Any(i => i.CarId == car.Id && i.ReturnDate == null);
                if (!find)
                {
                    context.Remove(car);
                    context.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public List<CarDetailDto> GetCarDetailsFatih(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (CarRentalCompanyContext context = new CarRentalCompanyContext())
            {
                var result = from c in context.Cars 
                             join b in context.Brands
                                 on c.BrandId equals b.BrandId
                             join co in context.Colors
                                 on c.ColorId equals co.Id
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 BrandId = b.BrandId,
                                 ColorId = c.ColorId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ImagePath = (from i in context.CarImages where i.CarId == c.Id select i.ImagePath).ToList(),
                                 IsRentable = !context.Rentals.Any(r=>r.CarId == c.Id) || context.Rentals.Any(r => r.CarId == c.Id && (r.ReturnDate == null || (r.ReturnDate.HasValue && r.ReturnDate > DateTime.Now )))
                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
           
            }
        }
    }
}