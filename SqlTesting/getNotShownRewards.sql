select
    (sum(mr."RegularCurrencyDelta") + sum(prRc."Quantity")) as "RegularCurrencyDelta",
    (sum(mr."PointsForSmallLootbox") + sum(prSl."Quantity")) as "PointsForSmallLootboxes",
    (sum(prWpp."Quantity")) as "WarshipPowerPoints"

from "Accounts" a

         inner join "Warships" w on w."AccountId" = a."Id"
         inner join "MatchResultForPlayers" mr on mr."WarshipId" = w."Id"

         inner join "Lootbox" lootbox on lootbox."AccountId" = a."Id"
         inner join "LootboxPrizePointsForSmallLootboxes" prSl on prSl."LootboxId" = lootbox."Id"
         inner join "LootboxPrizeRegularCurrencies" prRc on prRc."LootboxId"=lootbox."Id"
         inner join "LootboxPrizeWarshipPowerPoints" prWpp on prWpp."LootboxId"=lootbox."Id"

where a."ServiceId" = 'serviceId_13:12:05' and  mr."WasShown"=false and lootbox."WasShown"=false;