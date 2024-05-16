using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_SAFEGUARD.Models;

public partial class Entrega
{
    public int IdEntrega { get; set; }

    public int IdColaborador { get; set; }

    public int IdEpi { get; set; }

    public DateOnly DataDeEntrega { get; set; }

    public DateOnly? Validade { get; set; }

    [JsonIgnore]

    public virtual Colaborador? IdColaboradorNavigation { get; set; }
    [JsonIgnore]

    public virtual Epi? IdEpiNavigation { get; set; }
}
