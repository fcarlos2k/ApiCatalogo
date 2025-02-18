using APICatalogo.Models;
using APICatalogo.Notifications;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalogo.DTOs
{
    public class CategoriaDto
    {
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<ProdutoDto>? Produtos { get; set; }

        // Método para validar o DTO
        public NotificationContext Validate()
        {
            var context = new NotificationContext();

            // Validação do campo Nome
            if (string.IsNullOrEmpty(Nome))
            {
                context.AddNotification(nameof(Nome), "O nome da categoria é obrigatório.");
            }
            else if (Nome.Length > 80)
            {
                context.AddNotification(nameof(Nome), "O nome da categoria deve ter no máximo 80 caracteres.");
            }

            // Validação do campo ImagemUrl
            if (string.IsNullOrEmpty(ImagemUrl))
            {
                context.AddNotification(nameof(ImagemUrl), "A URL da imagem é obrigatória.");
            }
            else if (ImagemUrl.Length > 300)
            {
                context.AddNotification(nameof(ImagemUrl), "A URL da imagem deve ter no máximo 300 caracteres.");
            }

            return context;
        }
    }
}

