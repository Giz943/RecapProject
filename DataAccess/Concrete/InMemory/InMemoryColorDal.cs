using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Entities.DTOs;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryColorDal : IColorDal
    {
        List<Color> _colors;
        public InMemoryColorDal()
        {
            _colors = new List<Color>
            {
                new Color{Id = 1, ColorName = "White"},
                new Color{Id = 2, ColorName = "Black"},
                new Color{Id= 3, ColorName= "Grey"},
                new Color{Id= 4, ColorName= "Blue" },
                new Color{Id= 5, ColorName= "Red" }
            };
        }
        public void Add(Color entity)
        {
            _colors.Add(entity);
        }

        public void Delete(Color entity)
        {
            Color colorToDelete = _colors.SingleOrDefault(c => c.Id == entity.Id);
            _colors.Remove(colorToDelete);
        }

        public Color Get(Func<Color, bool> filter)
        {
            return _colors.SingleOrDefault(filter);
        }

        public List<Color> GetAll(Func<Color, bool> filter = null)
        {
            return _colors.Where(filter).ToList();
        }

        public void Update(Color entity)
        {
            Color colorToUpdate = _colors.SingleOrDefault(c => c.Id == entity.Id);
            colorToUpdate.Id = entity.Id;
            colorToUpdate.ColorName = entity.ColorName;
        }

        public List<CarColorDetailDto> GetCarColorDetails()
        {
            throw new NotImplementedException();
        }

        public bool DeleteColorIfNotReturnDateNull(Color color)
        {
            throw new NotImplementedException();
        }
    }
}
