﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZonartUsers.Data.Models;

namespace ZonartUsers.Data
{
    public class ZonartUsersDbContext : IdentityDbContext<User>
    {
        public ZonartUsersDbContext(DbContextOptions<ZonartUsersDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Template> Templates { get; set; }

        public DbSet<Bag> Bags { get; set; }

        public DbSet<Client> Clients { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            


            base.OnModelCreating(builder);
        }
    }
}
