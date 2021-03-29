using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalCompanyContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (CarRentalCompanyContext context = new CarRentalCompanyContext())
            {
                var result = from c in context.Cars
                             join r in context.Rentals
                             on c.Id equals r.CarId
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join color in context.Colors
                             on c.ColorId equals color.Id
                             join cstmr in context.Customers
                             on r.CustomerId equals cstmr.Id
                             join user in context.Users
                             on cstmr.UserId equals user.Id
                             select new RentalDetailDto 
                             {
                                 RentalId = r.Id,
                                 CustomerId=cstmr.Id,
                                 CarName = b.BrandName,
                                 ColorName = color.ColorName,
                                 CustomerInfo = $"{user.FirstName} {user.LastName}",
                                 CompanyName = cstmr.CompanyName,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate

                             };
                return result.ToList();

            }
        }


        public bool DeleteRentalIfNotReturnDateNull(Rental rental)
        {
            using (CarRentalCompanyContext context = new CarRentalCompanyContext())
            {
                var find = context.Rentals.Any(i => i.Id == rental.Id && i.ReturnDate == null);
                if (!find)
                {
                    context.Remove(rental);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }

        }
    }
}
