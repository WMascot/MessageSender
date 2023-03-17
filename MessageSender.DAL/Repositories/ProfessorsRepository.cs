﻿using MessageSender.DAL.Context;
using MessageSender.DAL.Models;
using MessageSender.DAL.Repositories.Base;

namespace MessageSender.DAL.Repositories
{
    public class ProfessorsRepository : Repository<Professor>
    {
        public ProfessorsRepository(ApplicationDbContext db) : base (db) { }
    }
}
