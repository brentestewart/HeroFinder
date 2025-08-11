using HeroFinder.ComponentLibrary;
using HeroFinder.Shared.Models;

namespace HeroFinder.Tests.Components;

/// <summary>
/// These tests are written entirely in C#.
/// Learn more at https://bunit.dev/docs/getting-started/writing-tests.html#creating-basic-tests-in-cs-files
/// </summary>
public class HeroCardCSharpTests : BunitTestContext
{
	private string unfavoritedStrokeColor = "[stroke='#FAFAFA']";

	[Test]
	public void HeroCardStartsWithNoFavoriteCode()
	{
        // Arrange
        var myHero = new Hero { Abilities = ["Test Abilities"], HeroName = "Test Hero", Id = 1, ImageLink = "test.jpg", IsFavorite = false, Universe = "Marvel" };
        var cut = RenderComponent<HeroCard>(parameters => parameters.Add(p => p.Hero, myHero));

		// Assert that content of the paragraph shows counter at zero
		//cut.Find("svg").MarkupMatches(matchingMarkup);
        cut.Find(unfavoritedStrokeColor);
	}

	[Test]
	public void ClickingFavoriteCallsFavoriteCallbackCode()
	{
        // Arrange
        var wasCalled = false;
        var myHero = new Hero { Abilities = ["Test Abilities"], HeroName = "Test Hero", Id = 1, ImageLink = "test.jpg", IsFavorite = false, Universe = "Marvel" };
        var cut = RenderComponent<HeroCard>(parameters => parameters.Add(p => p.Hero, myHero).Add(p => p.OnFavoriteToggle, () => wasCalled = true));

        // Act - click button to increment counter
        cut.Find("button").Click();

        // Assert that the callback was called
        Assert.True(wasCalled);
    }
}
