using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.User
{
    public class UserDtoCreat
    {
        // Indica que o campo "Name" é obrigatório.
        [Required(ErrorMessage = "Nome é campo obrigatório")]
        // Define o comprimento máximo do campo "Name" para 60 caracteres.
        [StringLength(60, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
        public string Name { get; set; }

        // Indica que o campo "Email" é obrigatório.
        [Required(ErrorMessage = "Email é campo obrigatório")]
        // Verifica se o campo "Email" tem um formato válido.
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        // Define o comprimento máximo do campo "Email" para 100 caracteres.
        [StringLength(100, ErrorMessage = "Email deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }
    }
}