using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace U1_evaluacion_sumativa.Models;

public partial class Proyecto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// 0: Finalizado ; 1: Proceso; 2: Cancelado
    /// </summary>
    public int Estado { get; set; }

    public int Tipo { get; set; }

    public int UsuarioId { get; set; }

    [ValidateNever]
    public virtual Usuario Usuario { get; set; } = null!;
}
