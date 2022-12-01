using System;
using System.Collections.Generic;

namespace SolicitudesMVC.Models;

public partial class DetalleSolicitud
{
    public Guid IdDetalleSol { get; set; }

    public Guid IdSolicitud { get; set; }

    public Guid IdCurso { get; set; }

    public string Profesor { get; set; } = null!;

    public string Aula { get; set; } = null!;

    public string Sede { get; set; } = null!;

    public string Observación { get; set; } = null!;

    public virtual Curso IdCursoNavigation { get; set; } = null!;

    public virtual Solicitud IdSolicitudNavigation { get; set; } = null!;
}
