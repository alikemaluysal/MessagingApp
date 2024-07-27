using Microsoft.EntityFrameworkCore;

namespace FileAPI.Data;

public class FileDbContext : DbContext
{
    public DbSet<FileRecord> FileRecords { get; set; }
    public FileDbContext(DbContextOptions<FileDbContext> options) : base(options) { }
}
