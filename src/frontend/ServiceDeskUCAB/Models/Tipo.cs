﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceDeskUCAB.Models
{
    public class Tipo
    {
        public Guid Id { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }

        public string tipo { get; set; }

        public List<Departament> Departamento { get; set; }
        public List<TipoFlujo> Flujo_Aprobacion  { get; set; }

        public int? Minimo_Aprobado { get; set; }

        
    }
    
}
