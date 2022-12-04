using System;
using System.Collections.Generic;

namespace SolicitudesMVC.Models;

public partial class Curso
{
    public Guid IdCurso { get; set; }

    public string Nombre { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int NroCreditos { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<DetalleSolicitud> DetalleSolicitudes { get; } = new List<DetalleSolicitud>();
}
