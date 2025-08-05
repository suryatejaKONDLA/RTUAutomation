namespace RTUAutomation.Models.MasterModels.FullScaleCountMasterModels;

public class FullScaleCountMasterModelValidator : AbstractValidator<FullScaleCountMasterModel>
{
    public FullScaleCountMasterModelValidator()
    {
        RuleFor(x => x.Sl05FullScaleCountName)
            .NotEmpty().WithMessage(V.Required)
            .Length(3, 40).WithMessage(V.StringLength);
    }
}