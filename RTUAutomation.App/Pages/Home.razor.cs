namespace RTUAutomation.App.Pages;

public partial class Home : ComponentBase
{
    [Inject] private IJSRuntime JsRuntime { get; set; }

    private List<PasStandardizationTemplateAnalogSheetModel> AnalogData = [];
    private List<PasStandardizationTemplateStatusSheetModel> StatusData = [];
    private List<PasStandardizationTemplateControlsSheetModel> ControlsData = [];

    private string SelectedFileName = string.Empty;
    private string SelectedSheetName = string.Empty;
    private List<string> SheetsNames = [];

    private readonly Dictionary<string, Type> SheetModelMap = new()
    {
        { "Analogs", typeof(PasStandardizationTemplateAnalogSheetModel) },
        { "Status", typeof(PasStandardizationTemplateStatusSheetModel) },
        { "Controls", typeof(PasStandardizationTemplateControlsSheetModel) }
    };

    protected override void OnInitialized()
    {
        SheetsNames = SheetModelMap.Keys.ToList();
        base.OnInitialized();
    }


    private async Task OnFileSelectedAsync(string fileName)
    {
        SelectedFileName = fileName;
        await LoadSheetDataAsync();
    }

    private async Task OnSheetSelectedAsync(string sheetName)
    {
        SelectedSheetName = sheetName;
        await LoadSheetDataAsync();
    }

    private async Task LoadSheetDataAsync()
    {
        if (string.IsNullOrWhiteSpace(SelectedFileName) || string.IsNullOrWhiteSpace(SelectedSheetName))
        {
            return;
        }
        AnalogData = [];
        StatusData = [];
        ControlsData = [];
        var json = await JsRuntime.InvokeAsync<string>("readLocalJson", $"templatesJson/{SelectedFileName}_{SelectedSheetName}.json");

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

        StateHasChanged();
    }
}