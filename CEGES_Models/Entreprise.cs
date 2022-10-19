﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Models
{
    public class Entreprise
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public List<Groupe> Groupes { get; set; } = new();

    }
}
