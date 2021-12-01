﻿using fr.Database.Services;
using Microsoft.EntityFrameworkCore;

namespace fr.Database.EntityFramework
{
    public class AppDbContext : AppDbContextBase
    {
        public AppDbContext(DbContextOptions options, IAuditService auditService)
            : base(options, auditService)
        {
        }
    }
}
