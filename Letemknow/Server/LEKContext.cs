using Letemknow.Shared;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Letemknow.Server;

public class LEKContext : DbContext
{
    public LEKContext(DbContextOptions<LEKContext> opt) : base(opt)
    {
    }

    public DbSet<MailLink> MailLinks { get; set; }

}
