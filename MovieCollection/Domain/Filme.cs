using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollection.Domain
{
    public class Filme
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Título Original")]
        public string TituloOriginal { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Ano de Lançamento")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Gênero")]
        public string Genero { get; set; }
    }
}
