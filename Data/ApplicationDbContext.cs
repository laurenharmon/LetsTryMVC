using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using LetsTryMVC.Models;

namespace LetsTryMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LetsTryMVC.Models.Product> Product { get; set; }
        public DbSet<LetsTryMVC.Models.Photo> Photo { get; set; }
    }
}
