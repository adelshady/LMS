﻿using LMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        public DbSet<Register> registers { get; set; }
        public DbSet<Lesson> lessons { get; set; }
        public DbSet<Chapter> chapters { get; set; }
        public DbSet<Section> sections { get; set; }
        public DbSet<Stage> stages { get; set; }
        public DbSet<Level> levels { get; set; }

        public DbSet<User> users{ get; set; }


    }
}
