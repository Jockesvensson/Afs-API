﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace appTechV1.Models
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions<JobContext> options)
            : base(options)
        {
        }

        public DbSet<JobItem> JobItems { get; set; }
    }
}
