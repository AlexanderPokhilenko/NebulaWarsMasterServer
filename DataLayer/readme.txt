SELECT  a.Id,
       (
           select sum(mr1.WarshipRatingDelta)
               from MatchResultForPlayers mr1
               where mr1.WarshipId  = mr.WarshipId 
       )
    from Accounts  a
    inner join Warships  w
       on w.AccountId  = a.Id 
    inner join WarshipTypes  wt
       on w. WarshipTypeId  = wt.Id 
    inner join MatchResultForPlayers  mr
       on mr.WarshipId =w.Id 
    where a.Id = @accountId;