using HeroFinder.Client.Services;
using HeroFinder.Shared.Models;
using HeroFinder.ComponentLibrary.ViewModels;
using Microsoft.AspNetCore.Components;

namespace HeroFinder.Client.ViewModels;

public class HeroDetailsViewModel(HeroApiService heroApiService, NavigationManager navigationManager) : ViewModelBase(navigationManager)
{
    private HeroApiService HeroApiService { get; } = heroApiService;

    public Hero? Hero { get; private set; }
    public string? ReturnUrl { get; set; }

    public async Task LoadHeroAsync(int id)
    {
        if(id <= 0)
        {
            ToastError("Invalid hero ID.");
            return;
        }

        Hero = await HeroApiService.GetHeroByIdAsync(id);
        await Task.Delay(1000);
        StateHasChanged();
    }

    public void GoBack()
    {
        if (!string.IsNullOrEmpty(ReturnUrl))
        {
            NavigationManager.NavigateTo(Uri.UnescapeDataString(ReturnUrl));
        }
        else
        {
            NavigationManager.NavigateTo("/heroindex"); // fallback
        }
    }

    public void SetReturnUrl(string? returnUrl)
    {
        ReturnUrl = returnUrl ?? "/";
    }

    public async Task ToggleFavorite()
    {
        if (Hero == null) return;

        var newFavoriteStatus = !Hero.IsFavorite;
        var success = await HeroApiService.UpdateHeroFavoriteAsync(Hero.Id, newFavoriteStatus);

        if (success)
        {
            Hero.IsFavorite = newFavoriteStatus;
            StateHasChanged();
        }
    }

    public void RegisterAdCampaign(string adCampaign)
    {
        // Notify that this hit came from adCampaign
    }

    public override void OnNavigatedAway()
    {
        Hero = null; // Clear hero data when navigating away
    }
}
