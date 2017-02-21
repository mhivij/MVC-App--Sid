using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Orders_Engine_module_2.Models
{
    //Represents dbo.UserAuthentication table
    public class LoginModel
    {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Column("ID")]
            public int ID { get; set; }

            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /*[HiddenInput]
            public string ReturnUrl { get; set; }*/
    }
}