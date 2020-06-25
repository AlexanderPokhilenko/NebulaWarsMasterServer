--Достаёт всю информацию про корабли аккаунта
select a.*, w.*, wt.*, wcr.*,
(
    select coalesce(
    
        (select coalesce(sum(I."WarshipRating"), 0)
            from "Increments" I
            where I."WarshipId" = w."Id")
        -
        (select coalesce(sum(D."WarshipRating"), 0)
            from "Decrements" D
            where D."WarshipId" = w."Id")

    ,0)) as "WarshipRating",
       
    (select sum(increments."WarshipPowerPoints")
        from "Increments" increments
        where increments."WarshipId" = w."Id"
    ) as "WarshipPowerPoints"


from "Accounts" a
    inner join "Warships" w on a."Id" = w."AccountId"
    inner join "WarshipTypes" wt on w."WarshipTypeId" = wt."Id"
    inner join "WarshipCombatRoles" wcr on wt."WarshipCombatRoleId" = wcr."Id"
    left join "MatchResults" matchResults on w."Id" = matchResults."WarshipId"
    where a."ServiceId" = 'serviceId'
    group by a."Id", w."Id", wt."Id", wcr."Id"
;


select        
(
    coalesce((select sum(I."SoftCurrency") 
      from "Accounts" A
        join "Transactions" O on A."Id" = O."AccountId"
        join "Resources" P on O."Id" = P."TransactionId"
        join "Increments"  I on P."Id" = I."ResourceId"
        where A."ServiceId" = 'serviceId'), 0)
    
    -
 coalesce((select sum(D."SoftCurrency")
    from "Accounts" A
        join "Transactions" O on A."Id" = O."AccountId"
        join "Resources" P on O."Id" = P."TransactionId"
        join "Decrements"  D on P."Id" = D."ResourceId"
    where A."ServiceId" = 'serviceId'), 0)

    
    ) as "SoftCurrency",

(coalesce((select sum(I."HardCurrency")
    from "Accounts" A
        join "Transactions" T on A."Id" = T."AccountId"
        join "Resources" R on T."Id" = R."TransactionId"
        join "Increments"  I on R."Id" = I."ResourceId"
    where A."ServiceId" = 'serviceId'),0)
    -
    coalesce((select sum(D."HardCurrency")
    from "Accounts" A
         join "Transactions" T on A."Id" = T."AccountId"
         join "Resources" R on T."Id" = R."TransactionId"
        join "Decrements"  D on R."Id" = D."ResourceId"
    where A."ServiceId" = 'serviceId'),0)) as "HardCurrency",
    

 (coalesce(
         (select sum(I."LootboxPoints")
          from "Accounts" A
           join "Transactions" T on A."Id" = T."AccountId"
           join "Resources" R on T."Id" = R."TransactionId"
           join "Increments"  I on R."Id" = I."ResourceId"
          where A."ServiceId" = 'serviceId'
         )
     , 0) 
     -
  coalesce(
          (select sum(D."LootboxPoints")
           from "Accounts" A
                join "Transactions" T on A."Id" = T."AccountId"
                join "Resources" R on T."Id" = R."TransactionId"
                join "Decrements" D on R."Id" = D."ResourceId"
           where A."ServiceId" = 'serviceId'
          )
      , 0))
     as "LootboxPoints"
;

