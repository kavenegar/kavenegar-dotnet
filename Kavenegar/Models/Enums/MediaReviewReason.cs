namespace Kavenegar.Models.Enums
{
    public enum MediaReviewReason : byte
    {
        Unknown = 0,
        InappropriateContent = 1,
        NudityOrSexualContent = 2,
        ViolenceOrGraphicContent = 3,
        HateOrHarassment = 4,
        IllegalContent = 5,
        SelfHarmOrSuicide = 6,
        SpamOrScam = 7,
        MisleadingOrFakeContent = 8,
        Impersonation = 9,
        CopyrightViolation = 10,
        TrademarkViolation = 11,
        PrivacyViolation = 12,
        LowQuality = 13,
        CorruptedFile = 14,
        UnsupportedFormat = 15,
        TooLarge = 16,
        EmptyOrBlankMedia = 17,
        IrrelevantContent = 18,
        DuplicateMedia = 19,
        MissingRequiredInfo = 20,
        PolicyViolation = 21,
        ManualReviewFailed = 22
    }
}
