using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InfnetMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class InfnetDbContexto : IdentityDbContext
    {
        public InfnetDbContexto (DbContextOptions<InfnetDbContexto> options)
            : base(options)
        {
        }

        public DbSet<InfnetMVC.Models.Funcionario> Funcionario { get; set; } = default!;

        public DbSet<InfnetMVC.Models.Departamento> Departamento { get; set; } = default!;
    }
