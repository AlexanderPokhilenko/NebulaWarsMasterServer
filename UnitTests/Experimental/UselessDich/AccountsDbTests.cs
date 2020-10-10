// ﻿using System.Threading.Tasks;
// using AmoebaGameMatcherServer.Experimental;
// using DataLayer;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
//
// //TODO in memory базы данных связаны
//
// namespace MatchmakerTest
// {
//     [TestClass]
//     public class AccountsDbTests
//     {
//         private ApplicationDbContext dbContext;
//
//         [TestInitialize]
//         public void SetUp()
//         {
//             var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                 .UseInMemoryDatabase("Test")
//                 .Options;
//
//             dbContext = new ApplicationDbContext(options);
//         }
//         /// <summary>
//         /// Id аккаунтов начинается с 1, а не с ноля.
//         /// </summary>
//         [TestMethod]
//         public async Task Test1()
//         {
//             //Arrange
//             var account = new DefaultAccountFactoryService
//             var dbContext = new InMemoryDbContextFactory(nameof(AccountsDbTests)).CreateAsync();
//             
//             //Act
//             await dbContext.Accounts.AddAsync(account);
//             await dbContext.SaveChangesAsync();
//             
//             //Assert
//             var accountDb = await dbContext.Accounts
//                 .SingleOrDefaultAsync(account1 => account1.Id == 0);
//             Assert.IsNull(accountDb);
//         }
//     }
// }