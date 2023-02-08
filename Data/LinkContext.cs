using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShortenIt.Models;

namespace ShortenIt.Data
{
    public class LinkContext : DbContext
    {
        public LinkContext (DbContextOptions<LinkContext> options)
            : base(options)
        {
        }

        public DbSet<ShortenIt.Models.Link> Link { get; set; } = default!;
    }
}
