using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using WebApplication1.Models;

namespace WebApplication1
{
    public class PollContext : DbContext
    {
        public DbSet<Poll> Polls { get; set; }

        public string DbPath { get; set; }

        public PollContext(DbContextOptions<PollContext> options) : base(options)
        {
        }
    }
}
