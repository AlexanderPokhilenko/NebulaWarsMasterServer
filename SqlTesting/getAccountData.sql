--Достаёт всю информацию про корабли аккаунта
select a.*, w.*, wt.*, wcr.*,
        (select coalesce(sum(mr."WarshipRatingDelta"), 0)
            from "MatchResultForPlayers" mr
            where mr."WarshipId" = w."Id") as "WarshipRating",
        (select sum(increments."WarshipPowerPoints")
            from "Increments" increments
            inner join "IncrementTypes"   IT
                on increments."IncrementTypeId" = IT."Id"
            where increments."WarshipId" = w."Id" and IT."Name"='WarshipPowerPoints') as "WarshipPowerPoints"
from "Accounts" a
    inner join "Warships" w on a."Id" = w."AccountId"
    inner join "WarshipTypes" wt on w."WarshipTypeId" = wt."Id"
    inner join "WarshipCombatRoles" wcr on wt."WarshipCombatRoleId" = wcr."Id"
    left join "MatchResultForPlayers" matchResult on w."Id" = matchResult."WarshipId"
    where a."ServiceId" = 'serviceId'
    group by a."Id", w."Id", wt."Id", wcr."Id"
;


select        
(coalesce((select sum(MRFP."SoftCurrencyDelta")
        from "Accounts" A1
        join "Warships" W on A1."Id" = W."AccountId"
        join "MatchResultForPlayers" MRFP on W."Id" = MRFP."WarshipId"
     where A1."ServiceId" = 'serviceId'), 0) 
     + 
coalesce((select sum(I."SoftCurrency") 
  from "Accounts" A
    join "Orders" O on A."Id" = O."AccountId"
    join "Products" P on O."Id" = P."OrderId"
    join "Increments"  I on P."Id" = I."ProductId"
    where A."ServiceId" = 'serviceId'), 0)

-
 coalesce((select sum(D."SoftCurrency") 
  from "Accounts" A
    join "Orders" O on A."Id" = O."AccountId"
    join "Products" P on O."Id" = P."OrderId"
    join "Decrements"  D on P."Id" = D."ProductId"
  where A."ServiceId" = 'serviceId' ),0)
    
    
    ) as "SoftCurrency",

(coalesce((select sum(I."HardCurrency")
    from "Accounts" A
        join "Orders" O on A."Id" = O."AccountId"
        join "Products" P on O."Id" = P."OrderId"
        join "Increments"  I on P."Id" = I."ProductId"
    where A."ServiceId" = 'serviceId'),0)
    -
    coalesce((select sum(D."HardCurrency")
    from "Accounts" A
        join "Orders" O on A."Id" = O."AccountId"
        join "Products" P on O."Id" = P."OrderId"
        join "Decrement"  D on P."Id" = D."ProductId"
    where A."ServiceId" = 'serviceId'),0)) as "HardCurrency",
    
(coalesce((select sum(MRFP."SmallLootboxPoints")
          from "Accounts" A1
               join "Warships" W on A1."Id" = W."AccountId"
               join "MatchResultForPlayers" MRFP on W."Id" = MRFP."WarshipId"
          where A1."ServiceId" = 'serviceId'), 0) +
 coalesce(
         (select sum(I."LootboxPowerPoints")
          from "Accounts" A
                   join "Orders" O on A."Id" = O."AccountId"
                   join "Products" P on O."Id" = P."OrderId"
                   join "Increments"  I on P."Id" = I."ProductId"
          where A."ServiceId" = 'serviceId'
         )
     , 0)) as "LootboxPoints"
;

