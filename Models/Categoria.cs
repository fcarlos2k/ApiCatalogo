using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("Categorias")]
public class Categoria
{
    public int CategoriaId { get; set; }
    public string? Nome  { get; set; }
    public string? ImagemUrl{ get; set; }

    public virtual List<Produto>? Produtos { get; set; }
 }
   
  


