using System;
using System.Collections.Generic;
using Application.Data.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Application.Data.Models.Auth;

namespace Application.Data.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<Applications> Applications { get; set; } = null!;
        public DbSet<ApplicationCustomer> ApplicationCustomers { get; set; } = null!;
        public DbSet<BusinessArea> BusinessAreas { get; set; } = null!;
        public DbSet<BusinessAreaRelationship> BusinessAreaRelationships { get; set; } = null!;
        public DbSet<BusinessAreaType> BusinessAreaTypes { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Users> Users { get; set; } = null!;

        public DbSet<BusinessAreaTypeRelationship> BusinessAreaTypeRelationships { get; set; }
    }
}
