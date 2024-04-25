using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_SAFEGUARD.Models;

public partial class Epi
{
    public int IdEpi { get; set; }

    public string NomeEpi { get; set; } = null!;

    public string? Instrucoes { get; set; }

    public int Qtd { get; set; }

    [JsonIgnore]
    public virtual Entrega? Entrega { get; set; }
}
