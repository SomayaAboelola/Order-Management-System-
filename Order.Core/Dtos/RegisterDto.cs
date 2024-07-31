using System.ComponentModel.DataAnnotations;

namespace Order_Management_System.Dtos
{
    public class RegisterDto 
    {
      
        public string UserName { get; set; } = null!;
       
        [EmailAddress]
        public string Email { get; set; } = null!;
      
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
            ErrorMessage = "password must have at least :1 uppercase ,1 lowercase ,1 number ,one special character: @, $, !, %, , ?, &, or similar symbols")]

        public string Password { get; set; } = null!;
     
        public string Phone { get; set; } = null!;
    }
}
