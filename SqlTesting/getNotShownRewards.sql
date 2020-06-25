select T."Id",
    (coalesce(sum(I."SoftCurrency"),0)-coalesce(sum(D."SoftCurrency"),0)) as "SoftCurrencyDelta",
    (coalesce(sum(I."HardCurrency"),0)-coalesce(sum(D."HardCurrency"),0)) as "HardCurrencyDelta",       
    (coalesce(sum(I."LootboxPoints"),0)-coalesce(sum(D."LootboxPoints"),0)) as "LootboxPointsDelta",       
    (coalesce(sum(I."WarshipRating"),0)-coalesce(sum(D."WarshipRating"),0)) as "AccountRatingDelta"
from "Accounts" A
     inner join "Transactions" T on T."AccountId" = A."Id"
     inner join "Resources" R on T."Id" = R."TransactionId"   
     inner join "Increments" I on R."Id" = I."ResourceId"   
     inner join "Decrements" D on R."Id" = D."ResourceId"
where A."ServiceId" = 'serviceId' and  T."WasShown" = false
group by T."Id";