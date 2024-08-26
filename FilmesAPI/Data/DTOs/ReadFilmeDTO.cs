using FilmesAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs
{
    public class ReadFilmeDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo título é obrigatório")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo diretor é obrigatório")]
        public string Diretor { get; set; }
        [StringLength(30, ErrorMessage = "O gênero tem que ter menos de 30 caracteres")]
        public string Genero { get; set; }
        [Range(1, 600, ErrorMessage = "A duração tem que ser entre 1 e 600 minutos")]
        public int Duracao { get; set; }
        public DateTime HoraConsulta { get; set; }
    }
}
