--информация про корабли аккаунта
select a.*, w.*, wt.*, wcr.*,
(
    select coalesce(
       (
            select coalesce(sum(I."Amount"),0)
            from "Increments" I
                 inner join "IncrementTypes" IT on I."IncrementTypeId" = IT."Id"
            where I."WarshipId" = w."Id" and IT."Name"='WarshipRating'
       )
   -
       (
            select coalesce(sum(D."Amount"),0)
            from "Decrements" D
                inner join "DecrementTypes" DT on D."DecrementTypeId" = DT."Id"
            where D."WarshipId" = w."Id" and DT."Name"='WarshipRating'
       )
        ,0
    ) as "WarshipRating"
),
(
    select coalesce(
          (
              select sum(I."Amount")
              from "Increments" I
                       inner join "IncrementTypes" IT on I."IncrementTypeId" = IT."Id"
              where I."WarshipId" = w."Id" and IT."Name"='WarshipPowerPoints'
          )
          ,0
    ) as "WarshipPowerPoints" 
),
(
    select coalesce(
          (
              select count(I."Amount")
              from "Increments" I
                       inner join "IncrementTypes" IT on I."IncrementTypeId" = IT."Id"
              where I."WarshipId" = w."Id" and IT."Name"='WarshipLevel'
          )
          ,0
    ) as "WarshipLevel" 
)

from "Accounts" A
         join "Warships" w on a."Id" = w."AccountId"
         join "WarshipTypes" wt on w."WarshipTypeId" = wt."Id"
         join "WarshipCombatRoles" wcr on wt."WarshipCombatRoleId" = wcr."Id"
where a."ServiceId" = 'serviceId'
group by a."Id", w."Id", wt."Id", wcr."Id";




select        
(
    coalesce((select sum(I."Amount") 
      from "Accounts" A
        join "Transactions" T on A."Id" = T."AccountId"
        join "Increments"  I on T."Id" = I."TransactionId"
        join "IncrementTypes" IT on I."IncrementTypeId" = IT."Id"    
        where A."ServiceId" = 'serviceId' and it."Name" = 'SoftCurrency'
        
        ), 0)
    
    -
 coalesce((select sum(D."Amount")
    from "Accounts" A
        join "Transactions" T on A."Id" = T."AccountId"
        join "Decrements"  D on T."Id" = D."TransactionId"
        join "DecrementTypes" DT on D."DecrementTypeId" = DT."Id"
           where A."ServiceId" = 'serviceId' and DT."Name" = 'SoftCurrency'
    ), 0)

    
    ) as "SoftCurrency",

(coalesce((select sum(I."Amount")
    from "Accounts" A
        join "Transactions" T on A."Id" = T."AccountId"
        join "Increments"  I on T."Id" = I."TransactionId"
        join "IncrementTypes" IT on I."IncrementTypeId" = IT."Id"
           where A."ServiceId" = 'serviceId' and it."Name" = 'HardCurrency'
    ),0)
    -
    coalesce((select sum(D."Amount")
    from "Accounts" A
         join "Transactions" T on A."Id" = T."AccountId"
        join "Decrements"  D on T."Id" = D."TransactionId"
         join "DecrementTypes" DT on D."DecrementTypeId" = DT."Id"
              where A."ServiceId" = 'serviceId' and DT."Name" = 'HardCurrency'
    ),0)) as "HardCurrency",
    

 (coalesce(
         (select sum(I."Amount")
          from "Accounts" A
           join "Transactions" T on A."Id" = T."AccountId"
           join "Increments"  I on T."Id" = I."TransactionId"
           join "IncrementTypes" IT on I."IncrementTypeId" = IT."Id"
          where A."ServiceId" = 'serviceId' and it."Name" = 'LootboxPoints'
         )
     , 0) 
     -
  coalesce(
          (select sum(D."Amount")
           from "Accounts" A
                join "Transactions" T on A."Id" = T."AccountId"
                join "Decrements" D on T."Id" = D."TransactionId"
                join "DecrementTypes" DT on D."DecrementTypeId" = DT."Id"
           where A."ServiceId" = 'serviceId' and DT."Name" = 'LootboxPoints'
        
          )
      , 0))
     as "LootboxPoints"
;

