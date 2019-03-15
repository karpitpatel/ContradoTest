using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contrado.Core.Data.Context;

namespace Contrado.Common.Data.Context
{
    /// <summary>
    /// Common Entities Database Context
    /// </summary>
    public class CommonEntities : BaseDbContext, ICommonEntities
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CommonEntities()
        {
        }

        /// <summary>
        /// Ties this context to the given connection string.
        /// </summary>
        /// <param name="connectionString">the connection string for this context</param>
        public CommonEntities(string connectionString)
            : base(connectionString)
        {
        }

        //public IDbSet<ActiveToken> ActiveToken { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null) return;

           // modelBuilder.Configurations.Add(new ActiveTokenMap());
            
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
