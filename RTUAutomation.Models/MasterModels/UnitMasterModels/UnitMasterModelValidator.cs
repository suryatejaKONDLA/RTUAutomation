namespace RTUAutomation.Models.MasterModels.UnitMasterModels;

public class UnitMasterModelValidator : AbstractValidator<UnitMasterModel>
{
    public UnitMasterModelValidator()
    {
        RuleFor(x => x.Sl03UnitName)
            .NotEmpty().WithMessage(V.Required)
            .Length(3, 40).WithMessage(V.StringLength);
    }
}