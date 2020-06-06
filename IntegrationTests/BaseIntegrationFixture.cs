using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NUnit.Framework;

namespace IntegrationTests
{
    /// <summary>
    /// Отвечает за очистку БД после каждого теста.
    /// </summary>
    internal class BaseIntegrationFixture
    {
        protected ApplicationDbContext Context => SetUpFixture.DbContext;
        protected AccountDbReaderService AccountDbReaderService => SetUpFixture.AccountReaderService;
        
        [SetUp]
        public void ResetChangeTracker()
        {
            //Удаляет все сохранения в модели БД, которые не были закоммичены
            //или нет
            IEnumerable<EntityEntry> changedEntriesCopy = Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted
                );
            foreach (EntityEntry entity in changedEntriesCopy)
            {
                Context.Entry(entity.Entity).State = EntityState.Detached;
            }
            
            SetUpFixture.TruncateAccountsTable();
        }
    }
}