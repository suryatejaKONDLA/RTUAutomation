namespace RTUAutomation.Models.MasterModels;

internal static class V
{
    internal const string Email = "Invalid Email";
    internal const string GreaterThan = "{PropertyName} should be greater than {ComparisonValue}.";
    internal const string GreaterThanOrEqual = "{PropertyName} should be greater than or equal to {ComparisonValue}.";
    internal const string LesserThan = "{PropertyName} should be less than {ComparisonValue}.";
    internal const string LesserThanOrEqual = "{PropertyName} should be less than or equal to {ComparisonValue}.";
    internal const string MaxLength = "Maximum length is {MaxLength}.";
    internal const string MinLength = "Minimum length is {MinLength}.";
    internal const string PasswordMatch = "Passwords do not match.";
    internal const string Range = "Value must be between {From} and {To}.";
    internal const string Required = "This field is required.";
    internal const string Space = "Spaces are not allowed.";
    internal const string StringLength = "Length must be between {MinLength} and {MaxLength}.";
    internal const string StrongPassword = "Please enter a strong password.";
    internal const string StrongPasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\d\s\W]).{8,}$";
    internal const string NoSpaceAndNoSymbolsPattern = "^[a-zA-Z0-9]+$";
    internal const string NoSpaceAndNoSymbols = "Spaces and symbols are not allowed.";
}