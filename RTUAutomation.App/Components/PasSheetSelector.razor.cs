namespace RTUAutomation.App.Components;

public partial class PasSheetSelector : ComponentBase
{
    [Parameter] public List<string> SheetNames { get; set; } = [];

    [Parameter] public string SelectedSheet { get; set; }

    [Parameter] public EventCallback<string> SelectedSheetChanged { get; set; }

    private async Task OnSelectedChanged(string value)
    {
        SelectedSheet = value;
        await SelectedSheetChanged.InvokeAsync(value);
    }
}