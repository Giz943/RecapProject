using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfColorDal : EfEntityRepositoryBase<Color, CarRentalCompanyContext>, IColorDal
    {
        public List<CarColorDetailDto> GetCarColorDetails()
        {
            using (CarRentalCompanyContext context = new CarRentalCompanyContext())
            {
                var result = from c in context.Cars
                    join co in context.Colors
                        on c.ColorId equals co.Id
                    join r in context.Rentals
                        on c.Id equals r.CarId
                    select new CarColorDetailDto()
                    {
                        ColorID = co.Id,
                        CarID = c.Id,
                        RentalID = r.Id,
                        RentDate = r.RentDate,
                        ReturnDate = r.ReturnDate
                    };
                return result.ToList();
            }
        }
        
        public bool DeleteColorIfNotReturnDateNull(Color color)
        {
            using (CarRentalCompanyContext context = new CarRentalCompanyContext())
            {
                var find = GetCarColorDetails().Any(i => i.ColorID == color.Id && i.ReturnDate == null);
                if (!find)
                {
                    context.Remove(color);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
