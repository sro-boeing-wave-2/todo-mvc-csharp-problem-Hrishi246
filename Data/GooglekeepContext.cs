using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Googlekeep.Model;

namespace Googlekeep.Models
{
    public class GooglekeepContext : DbContext
    {
        public GooglekeepContext (DbContextOptions<GooglekeepContext> options)
            : base(options)
        {
        }

        public DbSet<Googlekeep.Model.Note> Note { get; set; }
    }
}
