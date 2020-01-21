using Microsoft.EntityFrameworkCore;
using PoliticPolls.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoliticPolls.Web.Services
{
    public class SqlUtility
    {
        public static void ExecuteStoredProcedure(ApplicationDbContext db, string procedureName, params object[] procedureParameters)
        {
            db.Database.ExecuteSqlCommand($"BEGIN {procedureName}; END;", procedureParameters);
        }
    }
}
