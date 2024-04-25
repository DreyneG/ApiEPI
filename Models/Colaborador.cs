using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_SAFEGUARD.Models;

public partial class Colaborador
{
    public int IdColaborador { get; set; }

    public string NomeColaborador { get; set; } = null!;

    public int Ctps { get; set; }

    public DateOnly DataDeAdmisao { get; set; }

    public int Telefone { get; set; }

    public decimal Cpf { get; set; }

    public string? TipoFuncionario { get; set; }

    public string? Email { get; set; }

    [JsonIgnore]
    public virtual Entrega? Entrega { get; set; }
}
