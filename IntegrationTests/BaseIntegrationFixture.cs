﻿using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer;
using NUnit.Framework;

namespace IntegrationTests
{
    /// <summary>
    /// Отвечает за очистку БД после каждого теста.
    /// </summary>
    internal class BaseIntegrationFixture
    {
        protected ApplicationDbContext Context => SetUpFixture.DbContext;
        protected AccountDbReaderService Service => SetUpFixture.Service;
        
        [TearDown]
        public void ResetChangeTracker()
        {
            SetUpFixture.TruncateAccountsTable();
        }
    }
}