using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HighLandCoffeeWebsite.Models
{
    public class User_Admin
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Họ tên không để trống")]
        public string fullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Sai định dạng số điện thoại")]
        public string phone { get; set; }
        [Required(ErrorMessage = "Email không để trống")]
        [EmailAddress()]
        public string email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string confirmPassword { get; set; }
        [Required(ErrorMessage = "Địa chỉ không để trống")]
        public string address { get; set; }


        [Required(ErrorMessage = "The product type can't be blank!")]
        public int roleID { get; set; }
        public User_Admin()
        {

        }
        public User_Admin(int userId, string fullName, string phone, string email, string password, string address, int roleID)
        {
            UserId = userId;
            this.fullName = fullName;
            this.phone = phone;
            this.email = email;
            this.password = password;
            this.address = address;
            this.roleID = roleID;
        }
    }
}