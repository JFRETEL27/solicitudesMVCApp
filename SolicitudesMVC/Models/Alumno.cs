using System;
using System.Collections.Generic;

namespace SolicitudesMVC.Models;

public partial class Alumno
{
    public Guid IdAlumno { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public virtual ICollection<Solicitud> Solicitudes { get; } = new List<Solicitud>();
}
