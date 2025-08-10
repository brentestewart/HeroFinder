using Microsoft.AspNetCore.Components;
using HeroFinder.Shared.Services;

namespace HeroFinder.ComponentLibrary.ViewModels;

public class ViewModelComponentBase<TViewModel> : ComponentBase where TViewModel : ViewModelBase
{
    public ViewModelComponentBase()
    {
    }

    [Inject]
    public TViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        ViewModel.ToastOk = Ok;
        ViewModel.ToastInfo = Info;
        ViewModel.ToastSuccess = Success;
        ViewModel.ToastWarn = Warn;
        ViewModel.ToastError = Error;
        ViewModel.StateHasChanged = StateHasChanged;
        await ViewModel.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {

        if (firstRender)
        {
        }

        await ViewModel.OnAfterRenderAsync(firstRender);
    }

    protected override async Task OnParametersSetAsync()
    {
        await ViewModel.OnParametersSetAsync();
    }

    public virtual void Ok(string message) { ShowMessage(message, Severity.Normal); }
    public virtual void Info(string message) { ShowMessage(message, Severity.Info); }
    public virtual void Success(string message = "Success") { ShowMessage(message, Severity.Success); }
    public virtual void Warn(string message) { ShowMessage(message, Severity.Warning); }
    public virtual void Error(string message = "Error") { ShowMessage(message, Severity.Error); }

    protected virtual void ShowMessage(string message, Severity severity)
    {
        //TODO: Show message in UI
    }

}

public enum Severity
{
    Normal,
    Info,
    Success,
    Warning,
    Error
}