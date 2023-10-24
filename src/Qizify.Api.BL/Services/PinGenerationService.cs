using Quizify.Api.BL.Services.Interfaces;

namespace Quizify.Api.BL.Services;

public class PinGenerationService : IPinGenerationService
{
    public int PinLength { get; set; } = 4;
    public string AllowedChars { get; init;} = "0123456789";
    public bool AllowPinLengthIncreasing { get; set; } = true;
    public int TriesBeforePinLengthIncreasing { get; set; } = 3;

    private readonly Random _random;
    private int Tries = 0;
    public PinGenerationService(Random random)
    {
        _random = random;
    }

    public string Generate()
    {

        if (TriesBeforePinLengthIncreasing - Tries == 0)
        {
            if (!AllowPinLengthIncreasing)
            {
                throw new Exception("Too many tries of pin generation." +
                                    " Try to increase TriesBeforePinLengthIncreasing field in: " + this);
            }

            PinLength++;
            Tries = 0;

        }

        Tries++;
        return new string(Enumerable.Repeat(AllowedChars, PinLength)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}