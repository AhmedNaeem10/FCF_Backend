﻿using Microsoft.EntityFrameworkCore;
using FCF.Entities;

namespace FCF.Data
{
    public class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions<MainDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
    }
}