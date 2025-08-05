namespace RTUAutomation.Models.MasterModels.StatusDescriptionMasterModels;

public class StatusDescriptionMasterModelValidator : AbstractValidator<StatusDescriptionMasterModel>
{
    public StatusDescriptionMasterModelValidator()
    {
        RuleFor(x => x.Sl07StatusDescriptionName)
            .NotEmpty().WithMessage(V.Required)
            .Length(3, 40).WithMessage(V.StringLength);
    }
}