using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HighLandCoffeeWebsite.Models
{
    public class Admin_Product
    {
            public int productID { get; set; }
            [Required(ErrorMessage = "The product name can't be blank!")]
            public string productName { get; set; }
            public string productDescription { get; set; }

            [Required(ErrorMessage = "The price can't be blank!")]
            public decimal price { get; set; }
            [Required(ErrorMessage = "Image can't be blank!")]
            public string images { get; set; }
            //[Required(ErrorMessage = "The created date product can't be blank!")]
            //public DateTime CreatedDate { get; set; }
            //[Required(ErrorMessage = "The updated date product can't be blank!")]
            //public DateTime UpdatedDate { get; set; }

            [Required(ErrorMessage = "The product type can't be blank!")]
            public int productTypeID { get; set; }

            public Admin_Product()
            {

            }
            public Admin_Product(int productID, string productName, string productDescription, decimal price, string images, int productTypeID)
            {
                this.productID = productID;
                this.productName = productName;
                this.productDescription = productDescription;
                this.price = price;
                this.images = images;
                this.productTypeID = productTypeID;
            }
    }
}