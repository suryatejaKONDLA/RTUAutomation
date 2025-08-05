namespace RTUAutomation.Models.MasterModels.PhMasterModels;

public class PhMasterModelValidator : AbstractValidator<PhMasterModel>
{
    public PhMasterModelValidator()
    {
        RuleFor(x => x.Sl01PhName)
            .NotEmpty().WithMessage(V.Required)
            .Length(3, 40).WithMessage(V.StringLength);
    }
}