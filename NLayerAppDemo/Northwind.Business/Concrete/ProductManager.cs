using FluentValidation;
using FluentValidation.Validators;
using Northwind.Business.Abstract;
using Northwind.Business.ValidationRules.FluentValidation;
using Northwind.DataAccess.Abstract;
using Northwind.Entities;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Northwind.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        { 
            ProductValidator productValidator=new ProductValidator();
            var result =productValidator.Validate(product);
            if(result.Errors.Count > 0)
            {
                throw new ValidationException(result.Errors);
            }
              _productDal.Add(product);        
        }

        public void Delete(Product product)
        {
            try
            {
             _productDal.Delete(product);
            }
            catch
            {
                throw new Exception("Silme gerçekleşemedi");
            }
            
        }

        public List<Product> GetAll() 
        { //Business Code
            return _productDal.GetAll();
        }

        public List<Product> GetProductsByCategory(int categoryID)
        {
            return _productDal.GetAll(p=>p.CategoryID == categoryID);
        }

        public List<Product> GetProductsByProductName(string productName)
        {
            return _productDal.GetAll(p => p.ProductName.ToLower().Contains(productName));
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }
    }
}
