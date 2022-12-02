using System;
using System.Collections.Generic;

namespace SolicitudesMVC.Models;

public partial class Solicitud
{
    public Guid IdSolicitud { get; set; } = Guid.Empty;

    public Guid IdAlumno { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public string CodRegistrante { get; set; } = null!;

    public string Carrera { get; set; } = null!;

    public string Periodo { get; set; } = null!;

    public virtual ICollection<DetalleSolicitud>? DetalleSolicitudes { get; } = new List<DetalleSolicitud>();

    public virtual Alumno? IdAlumnoNavigation { get; set; } = null!;

}
