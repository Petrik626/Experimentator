using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Experimentator.Models
{
    class ExperimentatorDbContext:IdentityDbContext<ExperimentatorUser>
    {
        public ExperimentatorDbContext(DbContextOptions<ExperimentatorDbContext> options) : base(options) { }
    }
}