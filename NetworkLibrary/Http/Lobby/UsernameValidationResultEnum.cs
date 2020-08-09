using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class UsernameValidationResult
    {
        [Index(0)] public virtual UsernameValidationResultEnum UsernameValidationResultEnum { get; set; }
    }
    public enum UsernameValidationResultEnum
    {
        Ok=1,
        TooLong,
        TooShort,
        InvalidCharacter,
        ContainsSpace,
        DoesNotBeginWithALetter,
        OtherError
    }
}