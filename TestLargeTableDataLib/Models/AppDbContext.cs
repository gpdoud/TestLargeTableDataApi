using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestLargeTableDataLib.Models
{
	public class AppDbContext : DbContext
	{
		public DbSet<Number> Numbers {  get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
	}
}
