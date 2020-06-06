select
    (
        coalesce(
                sum(MR."SoftCurrencyDelta")    
            ,0)
         +
        coalesce(
                sum(LPSC."Quantity")
            ,0)
    ) as "SoftCurrencyDelta",
       
    (
     coalesce(
            sum(MR."SmallLootboxPoints")
         ,0)
         +
     coalesce(
         sum(LPSLP."Quantity")
         ,0)
    ) as "SmallLootboxPoints",
    (
            coalesce(
                    sum(MR."WarshipRatingDelta")
                ,0)
            
        ) as "AccountRatingDelta"

from "Accounts" a

         inner join "Warships" W on W."AccountId" = a."Id"
         inner join "MatchResultForPlayers" MR on MR."WarshipId" = W."Id"

         inner join "Lootbox" L on L."AccountId" = a."Id"
         inner join "LootboxPrizeSmallLootboxPoints" LPSLP on LPSLP."LootboxId" = L."Id"
         inner join "LootboxPrizeSoftCurrency" LPSC on LPSC."LootboxId"= L."Id"

where a."ServiceId" = 'serviceId_17:03:38' 
  and  MR."WasShown"=false 
  and L."WasShown"=false;