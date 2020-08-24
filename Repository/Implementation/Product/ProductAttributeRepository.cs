using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementation
{
    public class ProductAttributeRepository : Repository<ProductAttribute>, IProductAttributeRepository
    {
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }
        public ProductAttributeRepository(DbContext _db) : base(_db)
        {

        }
        public IEnumerable<ProductAttributeViewModel> GetByProductID(int? id)
        {
            var ProductAttributes = (from prodAttr in Context.ProductAttributes
                                     where prodAttr.ProductID == id
                                     
                                     join door in Context.DoorType
                                     on prodAttr.DoorTypeID equals door.DoorTypeID into prodAttrDoors
                                     from door in prodAttrDoors.DefaultIfEmpty()  // Left Outer Join of Product Attributes to Doors

                                     join supplier in Context.Suppliers
                                     on prodAttr.SupplierID equals supplier.SupplierID into prodAttrSupplier
                                     from supplier in prodAttrSupplier.DefaultIfEmpty()  // Left Outer Join of Product Attributes to Supplier

                                     join currency in Context.Currencies
                                     on prodAttr.CurrencyID equals currency.CurrencyID into prodAttrCurrency
                                     from currency in prodAttrCurrency.DefaultIfEmpty()  // Left Outer Join of Product Attributes to Supplier

                                     select new ProductAttributeViewModel
                                     {
                                         ProductAttributeID = prodAttr.ProductAttributeID,
                                         ProductID = prodAttr.ProductID,
                                         DoorTypeID = door.DoorTypeID,
                                         DoorTypeName = door.DoorTypeName,
                                         SupplierID = supplier.SupplierID,
                                         SupplierName = supplier.SupplierName,
                                         CurrencyID = currency.CurrencyID,
                                         CurrencyName = currency.CurrencyName

                                     }).ToList();

            return ProductAttributes;
        }

        public ProductAttributeViewModel GetProductAttrWithDoorName(int? id)
        {
            var ProductAttribute = (from prodAttr in Context.ProductAttributes
                                    where prodAttr.ProductAttributeID == id
                                    join door in Context.DoorType
                                    on prodAttr.DoorTypeID equals door.DoorTypeID into prodAttrDoors
                                    from door in prodAttrDoors.DefaultIfEmpty()  // Left Outer Join

                                    select new ProductAttributeViewModel
                                    {

                                        ProductAttributeID = prodAttr.ProductAttributeID,
                                        ProductID = prodAttr.ProductID,
                                        DoorTypeID = door.DoorTypeID,
                                        DoorTypeName = door.DoorTypeName
                                    }).FirstOrDefault();

            return ProductAttribute;
        }
    }
}