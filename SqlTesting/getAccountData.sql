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