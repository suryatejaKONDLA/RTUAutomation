namespace RTUAutomation.App.Pages;

public partial class Home : ComponentBase
{
    private readonly Dictionary<string, Type> SheetModelMap = new()
    {
        { "Analogs", typeof(PasStandardizationTemplateAnalogSheetModel) },
        { "Status", typeof(PasStandardizationTemplateStatusSheetModel) },
        { "Controls", typeof(PasStandardizationTemplateControlsSheetModel) }
    };

    private List<PasStandardizationTemplateAnalogSheetModel> AnalogData = [];
    private List<PasStandardizationTemplateControlsSheetModel> ControlsData = [];
    private string SelectedFileName = string.Empty;
    private string SelectedSheetName = string.Empty;
    private List<string> SheetsNames = [];
    private List<PasStandardizationTemplateStatusSheetModel> StatusData = [];

    protected override void OnInitialized() { SheetsNames = SheetModelMap.Keys.ToList(); }

    private async Task OnFileSelectedAsync(string fileName)
    {
        SelectedFileName = fileName;
        await LoadAndRenderSheetAsync();
    }

    private async Task OnSheetSelectedAsync(string sheetName)
    {
        SelectedSheetName = sheetName;
        await LoadAndRenderSheetAsync();
    }

    private async Task LoadAndRenderSheetAsync()
    {
        if (string.IsNullOrWhiteSpace(SelectedFileName) || string.IsNullOrWhiteSpace(SelectedSheetName))
        {
            return;
        }

        // Reset all
        AnalogData = [];
        StatusData = [];
        ControlsData = [];

        var jsonPath = $"templatesJson/{SelectedFileName}_{SelectedSheetName}.json";
        var json = await JsRuntime.InvokeAsync<string>("readLocalJson", jsonPath);

        switch (SelectedSheetName)
        {
            case "Analogs":
                AnalogData = JsonSerializer.Deserialize<List<PasStandardizationTemplateAnalogSheetModel>>(json) ?? [];
                break;
            case "Status":
                StatusData = JsonSerializer.Deserialize<List<PasStandardizationTemplateStatusSheetModel>>(json) ?? [];
                break;
            case "Controls":
                ControlsData = JsonSerializer.Deserialize<List<PasStandardizationTemplateControlsSheetModel>>(json) ?? [];
                break;
        }

        await InvokeAsync(StateHasChanged);
        await Task.Delay(1000); // Ensure DOM is rendered
        await BindToAgGridAsync("myGrid", GetCurrentSheetData());
    }

    private List<object> GetCurrentSheetData()
    {
        return SelectedSheetName switch
        {
            "Analogs" => AnalogData.Cast<object>().ToList(),
            "Status" => StatusData.Cast<object>().ToList(),
            "Controls" => ControlsData.Cast<object>().ToList(),
            _ => []
        };
    }

    private async Task BindToAgGridAsync(string divId, List<object> data)
    {
        if (data?.Any() != true)
        {
            return;
        }

        var json = JsonSerializer.Serialize(data);
        await JsRuntime.InvokeVoidAsync("AgGridInterop.renderGrid", divId, JsonDocument.Parse(json).RootElement);
    }
}