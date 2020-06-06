select a.*, w."Id", "AccountId", "WarshipTypeId", (sum(mr."WarshipRatingDelta")) as warship_rating, wt.*
from "Accounts" a
         inner join "Warships" w on a."Id" = w."AccountId"
         inner join "WarshipTypes" wt on w."WarshipTypeId" = wt."Id"
         inner join "MatchResultForPlayers" mr on w."Id" = mr."WarshipId"
where a."ServiceId" = 'serviceId_15:16:37'
group by a."Id",w."Id", wt."Id";


select w."Id", "AccountId", "WarshipTypeId",
       sum(mr."WarshipRatingDelta") as "WarshipRating"
from "Accounts" a
         inner join "Warships" w on a."Id" = w."AccountId"
         inner join "WarshipTypes" wt on w."WarshipTypeId" = wt."Id"
         inner join "MatchResultForPlayers" mr on w."Id" = mr."WarshipId"
where a."ServiceId" = 'serviceId_15:16:37'
group by w."Id";



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


select a.*, w.*, (sum(mr."WarshipRatingDelta")) as "WarshipRating", wt.*
from "Accounts" a
    inner join "Warships" w on a."Id" = w."AccountId"
    inner join "WarshipTypes" wt on w."WarshipTypeId" = wt."Id"
    inner join "MatchResultForPlayers" mr on w."Id" = mr."WarshipId" 
group by a."Id",w."Id", wt."Id"
limit 1;


select a.*, w.*, wt.*, 
       (sum(matchResult."WarshipRatingDelta"))                                             as "WarshipRating",
       (sum(matchResult."RegularCurrencyDelta") + sum(prizeRegulatCurrency."Quantity"))    as "RegularCurrency",
       (sum(prizeWarshipPowerPoints."Quantity"))                                           as "WarshipPowerPoints",
       (sum(prizeSmallLootboxPoints."Quantity")+ sum(matchResult."PointsForSmallLootbox")) as "PointsForSmallLootboxes"
from "Accounts" a
    inner join "Warships" w on a."Id" = w."AccountId"
    inner join "WarshipTypes" wt on w."WarshipTypeId" = wt."Id"
    inner join "MatchResultForPlayers" matchResult on w."Id" = matchResult."WarshipId"
    inner join "Lootbox" lootbox on lootbox."AccountId" = a."Id"
    inner join "LootboxPrizePointsForSmallLootboxes" prizeSmallLootboxPoints on prizeSmallLootboxPoints."LootboxId" = lootbox."Id"
    inner join "LootboxPrizeRegularCurrencies" prizeRegulatCurrency on prizeRegulatCurrency."LootboxId"=lootbox."Id"
    inner join "LootboxPrizeWarshipPowerPoints" prizeWarshipPowerPoints on prizeWarshipPowerPoints."LootboxId"=lootbox."Id"

group by a."Id",w."Id", wt."Id"
limit 2;


select a.*, w.*, wt.*,
       (select sum(mr."WarshipRatingDelta")  
           from "MatchResultForPlayers" mr
           where mr."WarshipId" = w."Id")                                                  as "WarshipRating",
       (sum(matchResult."RegularCurrencyDelta") + sum(prizeRegularCurrency."Quantity"))    as "RegularCurrency",
       (select sum(wpp."Quantity")
           from "LootboxPrizeWarshipPowerPoints" wpp
           where wpp."WarshipId" = w."Id")                                                 as "WarshipPowerPoints",
       (sum(prizeSmallLootboxPoints."Quantity")+ sum(matchResult."PointsForSmallLootbox")) as "PointsForSmallLootboxes"
from "Accounts" a
         inner join "Warships" w on a."Id" = w."AccountId"
         inner join "WarshipTypes" wt on w."WarshipTypeId" = wt."Id"
         left join "MatchResultForPlayers" matchResult on w."Id" = matchResult."WarshipId"
         left join "Lootbox" lootbox on lootbox."AccountId" = a."Id"
         left join "LootboxPrizePointsForSmallLootboxes" prizeSmallLootboxPoints on prizeSmallLootboxPoints."LootboxId" = lootbox."Id"
         left join "LootboxPrizeRegularCurrencies" prizeRegularCurrency on prizeRegularCurrency."LootboxId"=lootbox."Id"
         left join "LootboxPrizeWarshipPowerPoints" prizeWarshipPowerPoints on prizeWarshipPowerPoints."LootboxId"=lootbox."Id"

group by a."Id",w."Id", wt."Id"
limit 2;


select a.*, w.*, wt.*,
       (
           select sum(mr."WarshipRatingDelta")
           from "MatchResultForPlayers" mr
           where mr."WarshipId" = w."Id"
       )									 		as "WarshipRating",
       (
               sum(matchResult."RegularCurrencyDelta")
               + sum(COALESCE(prizeRegularCurrency."Quantity",0))
           )											as "RegularCurrency",
       (
           select sum(wpp."Quantity")
           from "LootboxPrizeWarshipPowerPoints" wpp
           where wpp."WarshipId" = w."Id"
       )											as "WarshipPowerPoints",
       (
               sum(COALESCE(prizeSmallLootboxPoints."Quantity",0))
               + sum(matchResult."PointsForSmallLootbox")
           )											as "PointsForSmallLootboxes"
from "Accounts" a
         inner join "Warships" w on a."Id" = w."AccountId"
         inner join "WarshipTypes" wt on w."WarshipTypeId" = wt."Id"
         left join "MatchResultForPlayers" matchResult on w."Id" = matchResult."WarshipId"
         left join "Lootbox" lootbox on lootbox."AccountId" = a."Id"
         left join "LootboxPrizePointsForSmallLootboxes" prizeSmallLootboxPoints on prizeSmallLootboxPoints."LootboxId" = lootbox."Id"
         left join "LootboxPrizeRegularCurrencies" prizeRegularCurrency on prizeRegularCurrency."LootboxId"=lootbox."Id"
         left join "LootboxPrizeWarshipPowerPoints" prizeWarshipPowerPoints on prizeWarshipPowerPoints."LootboxId"=lootbox."Id"
where a."ServiceId" = 'serviceId_18:15:49'
group by a."Id",w."Id", wt."Id"
;

--Достаёт всю информацию про корабли аккаунта
select a.*, w.*, wt.*, wcr.*,
        (select coalesce(sum(mr."WarshipRatingDelta"), 0)
            from "MatchResultForPlayers" mr
            where mr."WarshipId" = w."Id")              	as "WarshipRating",
        (select sum(wpp."Quantity")
            from "LootboxPrizeWarshipPowerPoints" wpp
            where wpp."WarshipId" = w."Id")					as "WarshipPowerPoints"
from "Accounts" a
    inner join "Warships" w on a."Id" = w."AccountId"
    inner join "WarshipTypes" wt on w."WarshipTypeId" = wt."Id"
    inner join "WarshipCombatRole" wcr on wt."WarshipCombatRoleId" = wcr."Id"
    left join "MatchResultForPlayers" matchResult on w."Id" = matchResult."WarshipId"
    left join "Lootbox" lootbox on lootbox."AccountId" = a."Id"    
    left join "LootboxPrizeWarshipPowerPoints" prizeWarshipPowerPoints on prizeWarshipPowerPoints."LootboxId"=lootbox."Id"
    where a."ServiceId" = 'serviceId_13:05:24'
    group by a."Id",w."Id", wt."Id", wcr."Id"
;

--Достаёт информацию про ресурсы аккаунта
select 
       --Обычная валюта
(coalesce(
    (select sum(MRFP."SoftCurrencyDelta")
        from "Accounts" A1
        join "Warships" W on A1."Id" = W."AccountId"
        join "MatchResultForPlayers" MRFP on W."Id" = MRFP."WarshipId"
     where A1."ServiceId" = 'serviceId_14:54:06'
    )
    , 0) + 
 coalesce(
     (select sum(LPSC."Quantity") 
      from "Accounts" A
          join "Lootbox" L on A."Id" = L."AccountId"
          join "LootboxPrizeSoftCurrency" LPSC on L."Id" = LPSC."LootboxId"
      where A."ServiceId" = 'serviceId_14:54:06'
     )
, 0)) as "SoftCurrency",
       --Премиум валюта
       --Добавить таблицы с покупками
(
coalesce(
        (select sum(LPHC."Quantity")
         from "Accounts" A
                  join "Lootbox" L on A."Id" = L."AccountId"
                  join "LootboxPrizeHardCurrency" LPHC on L."Id" = LPHC."LootboxId"
         where A."ServiceId" = 'serviceId_14:54:06'
        )
    , 0)) as "HardCurrency",
       --Баллы для маленького сундука
(coalesce(
         (select sum(MRFP."SmallLootboxPoints")
          from "Accounts" A1
                   join "Warships" W on A1."Id" = W."AccountId"
                   join "MatchResultForPlayers" MRFP on W."Id" = MRFP."WarshipId"
          where A1."ServiceId" = 'serviceId_14:54:06'
         )
     , 0) +
 coalesce(
         (select sum(LPSLP."Quantity")
          from "Accounts" A
                   join "Lootbox" L on A."Id" = L."AccountId"
                   join "LootboxPrizeSmallLootboxPoints" LPSLP on L."Id" = LPSLP."LootboxId"
          where A."ServiceId" = 'serviceId_14:54:06'
         )
     , 0)) as "SmallLootboxPoints"
;