using MartianRobots.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobotsTests.UnitTests
{
    public  class BaseTest
    {
        public readonly ApplicationDbContext applicationDbContext;
        public BaseTest()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("InmemoryDB");
            this.applicationDbContext = new ApplicationDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
