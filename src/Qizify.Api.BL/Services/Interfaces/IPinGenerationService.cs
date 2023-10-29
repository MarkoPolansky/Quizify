namespace Quizify.Api.BL.Services.Interfaces;

public interface IPinGenerationService
{
     string Generate();
     int PinLength { get; set; }
     string AllowedChars { get; init; }
     bool AllowPinLengthIncreasing { get; set; }
     int TriesBeforePinLengthIncreasing { get; set; }
}