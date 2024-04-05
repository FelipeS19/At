using System.ComponentModel.DataAnnotations;
namespace InfnetMVC.Models
{
    public class Funcionario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(50)]
        public string Nome { get; set; }

        public string Endereco { get; set; }

        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Telefone inválido.")]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
    }
}
