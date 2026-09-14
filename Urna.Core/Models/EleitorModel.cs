using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Urna.Core.Models;

public class EleitorModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    public string ?Cpf { get; set; }
    public bool JaVotou { get; set; }
}