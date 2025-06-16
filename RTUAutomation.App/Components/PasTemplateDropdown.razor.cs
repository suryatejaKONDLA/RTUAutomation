namespace RTUAutomation.App.Components;

public partial class PasTemplateDropdown : ComponentBase
{
    [Parameter]
    public string Value { get; set; }

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    private async Task OnValueChanged(string value)
    {
        await ValueChanged.InvokeAsync(value);
    }
}