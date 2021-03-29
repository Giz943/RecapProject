using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete
{
    public class InMemoryCarDal //: ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
             //   new Car{CarId=1, BrandId=1, ColorId=5, ModelYear=2018, DailyPrice= 180, Description="5 Adult / 2 Suitcase / Automatic Shift / Diesel"  },
              //  new Car{CarId=2, BrandId=3, ColorId=2, ModelYear=2016, DailyPrice= 220, Description="5 Adult / 3 Suitcase / Manual Shift / Gas"  },
              //  new Car{CarId=1, BrandId=4, ColorId=4, ModelYear=2013, DailyPrice= 230, Description="4 Adult / 3 Suitcase / Automatic Shift / Diesel"  },
               // new Car{CarId=1, BrandId=2, ColorId=3, ModelYear=2014, DailyPrice= 210, Description="5 Adult / 2 Suitcase / Manual Shift / Gas"  },
               // new Car{CarId=1, BrandId=5, ColorId=1, ModelYear=2017, DailyPrice= 240, Description="4 Adult / 3 Suitcase / Automatic Shift / Diesel"  }
            };
        }

        public void Add(Car entity)
        {
            _cars.Add(entity);
        }

        public void Delete(Car entity)
        {
            //Car carToDelete = _cars.SingleOrDefault(c=> c.CarId == entity.CarId);
            //_cars.Remove(carToDelete);

        }

        public Car Get(Func<Car, bool> filter = null)
        {
            return _cars.SingleOrDefault(filter);
        }


        public List<Car> GetAll(Func<Car, bool> filter = null)
        {
            return _cars.Where(filter).ToList();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        
        public bool DeleteCarIfNotReturnDateNull(Car car)
        {
            throw new NotImplementedException();
        }

        public void Update(Car entity)
        {
           //Car carToUpdate = _cars.SingleOrDefault(c => c.CarId == entity.CarId);
          //  carToUpdate.CarId = entity.CarId;
           // carToUpdate.ColorId = entity.ColorId;
           // carToUpdate.BrandId = entity.BrandId;
           // carToUpdate.DailyPrice = entity.DailyPrice;
           // carToUpdate.Description = entity.Description;
        }

        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetailsFatih(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
