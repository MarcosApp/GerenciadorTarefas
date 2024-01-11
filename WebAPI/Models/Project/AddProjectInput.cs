﻿using System;

namespace WebAPI.Models.Project
{
    public class AddProjectInput
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

    }
}
