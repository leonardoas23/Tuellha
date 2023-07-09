using System.ComponentModel.DataAnnotations.Schema;

namespace Tuellha.Models
{
    public class PublicacaoModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
        public DateTime Data { get; set; }

        [NotMapped]
        public IFormFile? Foto { get; set; }

        public byte[]? FotoDB { get; set; }

        public string ToBase64()
        {
            var retorno = FotoDB == null ? string.Empty : Convert.ToBase64String(FotoDB);
            return retorno;
        }

    }
}