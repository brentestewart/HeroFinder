using HeroFinder.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace HeroFinder.ComponentLibrary.ViewModels;

public class ViewModelBase(NavigationManager navigationManager)
{
    public Action StateHasChanged { get; set; } = () => { };
    public Action<string> ToastOk { get; internal set; } = (s) => { };
    public Action<string> ToastInfo { get; internal set; } = (s) => { };
    public Action<string> ToastSuccess { get; internal set; } = (s) => { };
    public Action<string> ToastWarn { get; internal set; } = (s) => { };
    public Action<string> ToastError { get; internal set; } = (s) => { };
    public NavigationManager NavigationManager { get; } = navigationManager;
    private string? _currentLocation;

    public virtual Task OnInitializedAsync()
    {
        NavigationManager.LocationChanged += OnLocationChanged;
        return Task.CompletedTask;
    }

    public virtual Task OnParametersSetAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task OnAfterRenderAsync(bool firstRender)
    {
        return Task.CompletedTask;
    }

    public virtual void OnNavigatedAway() { }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        if (!string.Equals(_currentLocation, e.Location, StringComparison.InvariantCultureIgnoreCase))
        {
            _currentLocation = e.Location;
            OnNavigatedAway();
        }
    }

    protected void ToastServiceFailureException(ServiceFailureException ex)
    {
        switch (ex.Severity)
        {
            case ServiceFailureSeverity.Error:
                ToastError?.Invoke(ex.Message);
                break;
            case ServiceFailureSeverity.Warning:
                ToastWarn?.Invoke(ex.Message);
                break;
            default:
                ToastInfo?.Invoke(ex.Message);
                break;
        }
    }
}
