using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Database.Persitance;

namespace NatureCottages.Database
{
    public class UnitOfWork
    {
        private readonly CottageDbContext _cottageDbContext;
        public UnitOfWork(CottageDbContext cottageDbContext)
        {
            _cottageDbContext = cottageDbContext;
        }
        


    }
}
