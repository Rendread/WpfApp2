using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDemo.Models
{
    public class FormTableName
    {
        
        public string Email { get; set; } //email
        //public string Name { get; set; }

        public string Subject { get; set; }
        public string Course { get; set; }
        public List<string> Groups { get; set; }

    }

    class ProfessorRegisterModel
    {

        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessage = "Значение имени должно быть установлено")]
        public string userName { get; set; }

        [Required]

        public string Role { get; set; } = "Преподаватель";


        [Display(Name = "Адрес электронной почты")]
        [Required(ErrorMessage = "Значение EMAIL должно быть установлено")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Значение специальности должно быть установлено")]
        [Display(Name = "Специальность")]
        public string SpecOrGroup { get; set; }

        [Required(ErrorMessage = "Значение пароля должно быть установлено")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен быть не короче 6 символов.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Подтверждение пароля")]
        //[Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        //public string ConfirmPassword { get; set; }

    }

}
