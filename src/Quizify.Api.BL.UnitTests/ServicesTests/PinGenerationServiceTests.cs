using Quizify.Api.BL.Services;
using Quizify.Api.BL.Services.Interfaces;
using System;

namespace Quizify.Api.BL.UnitTests.ServicesTests;

public class PinGenerationServiceTests
{
    private readonly PinGenerationService _pinGenerationService;
    private readonly Random _random;


    public PinGenerationServiceTests()
    {
        _random = new Random();
        _pinGenerationService = new PinGenerationService(_random);
    }

    [Fact]
    public void Generate_ReturnsRandomString_OfDefaultLength()
    {
        var str = _pinGenerationService.Generate();

        Assert.Equal(_pinGenerationService.PinLength,str.Length);
    }


    [Fact]
    public void GeneratedMoreTimesThenSpecified_ReturnsRandomString_OfLengthAddedByOne()
    {
        _pinGenerationService.TriesBeforePinLengthIncreasing = 5;
        var startingPinLength = _pinGenerationService.PinLength;

        string str = "";
        for (int i = 0; i < 6; i++)
        {
            str = _pinGenerationService.Generate();
        }

        Assert.Equal(startingPinLength + 1,str.Length );
    }


    [Fact]
    public void GeneratedMoreTimesThenSpecified_ReturnsRandomString_OfLengthAddedByTwo()
    {
        _pinGenerationService.TriesBeforePinLengthIncreasing = 5;
        var startingPinLength = _pinGenerationService.PinLength;

        string str = "";
        for (int i = 0; i < 11; i++)
        {
            str = _pinGenerationService.Generate();
        }

        Assert.Equal(startingPinLength + 2, str.Length);
    }

    [Fact]
    public void GeneratedMoreTimesThenSpecifiedAndPinLengthIncreasingNotAllowed_Throws()
    {
        _pinGenerationService.AllowPinLengthIncreasing = false;
        _pinGenerationService.TriesBeforePinLengthIncreasing = 5;

        string str = "";
        for (int i = 0; i < 5; i++)
        {
            str = _pinGenerationService.Generate();
        }

        Assert.Throws<Exception>(() => _pinGenerationService.Generate());

    }
}