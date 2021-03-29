using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, CarRentalCompanyContext>, IUserDal
    {
        public List<CustomerRentalDetailDto> GetCustomerAndRentalDetails()
        {
            using (CarRentalCompanyContext context = new CarRentalCompanyContext())
            {
                var result = from c in context.Customers
                             join r in context.Rentals
                                 on c.Id equals r.CustomerId
                             join u in context.Users
                                 on c.Id equals u.Id
                             select new CustomerRentalDetailDto()
                             {
                                 RentalID = r.Id,
                                 UserID = u.Id,
                                 CustomerID = c.Id,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate

                             };
                return result.ToList();

            }
        }

        public bool DeleteUserIfNotReturnDateNull(User user)
        {
            using (CarRentalCompanyContext context = new CarRentalCompanyContext())
            {
                var find = GetCustomerAndRentalDetails().Any(i => i.UserID == user.Id && i.ReturnDate == null);
                if (!find)
                {
                    context.Remove(user);
                    context.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (CarRentalCompanyContext context = new CarRentalCompanyContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = userOperationClaim.OperationClaimId,
                                 Name = operationClaim.Name
                             };

                return result.ToList();
            }

        }
    }
}