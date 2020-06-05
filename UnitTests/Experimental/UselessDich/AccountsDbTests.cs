// ﻿using System.Threading.Tasks;
// using DataLayer;
// using MatchmakerTest.Utils;
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
//         /// <summary>
//         /// Id аккаунтов начинается с 1, а не с ноля.
//         /// </summary>
//         [TestMethod]
//         public async Task Test1()
//         {
//             //Arrange
//             var account = TestsAccountFactory.CreateUniqueAccount();
//             var dbContext = new InMemoryDbContextFactory(nameof(AccountsDbTests)).Create();
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