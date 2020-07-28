using System.Linq;
using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Services.Database.Seeding.Seaders
{
    public class WarshipImprovementTrigger
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.GameModeTypes.Any())
            {
                // int warshipLevelId = (int) IncrementTypeEnum.WarshipLevel;
                string trigger=
                    @"
CREATE OR REPLACE FUNCTION warship_improvement_check()
    RETURNS trigger AS
$BODY$
DECLARE
    _current_improvements_count          integer;
BEGIN
    IF NEW.""IncrementTypeId"" = 5 THEN
        SELECT COUNT(*) INTO _current_improvements_count
 FROM ""Increments"" 
WHERE ""Amount"" = NEW.""Amount"" 
AND ""WarshipId"" = NEW.""WarshipId""
AND ""IncrementTypeId""=5 ;
        IF _current_improvements_count <> 0 THEN
            RAISE EXCEPTION 'Warship with id % already was improved to level %!', NEW.""WarshipId"", NEW.""Amount"";
        END IF;
    END IF;

    RETURN NEW;
END;
$BODY$ LANGUAGE plpgsql;

CREATE TRIGGER warship_improvement_checking
    BEFORE INSERT OR UPDATE
    ON ""Increments""
    FOR EACH ROW
EXECUTE PROCEDURE warship_improvement_check();
";

                dbContext.Database.ExecuteSqlCommand(trigger);
            }
        }
    }
}