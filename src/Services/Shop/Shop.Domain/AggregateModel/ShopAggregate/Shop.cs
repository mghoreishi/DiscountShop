﻿using CSharpFunctionalExtensions;


namespace Shop.Domain.AggregateModel.ShopAggregate
{
    public class Shop : EntityBase
    {

        #region - Constructor - 
        public Shop(ShopName shopName, ShopDescription shopDescription, Address address, Phone phone)
        {
            ShopName = shopName;
            ShopDescription = shopDescription;
            Address = address;
            Phone = phone;
        }

        #endregion

        #region - Properties -
        public ShopName ShopName { get; private set; }
        public ShopDescription ShopDescription { get; private set; }
        public Address Address { get; private set; }
        public Phone Phone { get; private set; }
        #endregion

        #region - Functions -
        #endregion

    }
}