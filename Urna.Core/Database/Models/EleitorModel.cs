using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Urna.Core.Database.Models;

public class EleitorModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    public string Cpf { get; set; } = string.Empty;
    public bool JaVotou { get; set; }
}