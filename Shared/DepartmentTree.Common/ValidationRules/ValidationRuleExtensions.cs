namespace DepartmentTree.Settings;

using FluentValidation;

public static class ValidationRuleExtensions
{
    public static IRuleBuilderOptions<T, string> GadgetTitle<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(1).WithMessage("Minimum length is 1")
            .MaximumLength(100).WithMessage("Maximum length is 100");
    }

    public static IRuleBuilderOptions<T, double> GadgetPrice<T>(this IRuleBuilder<T, double> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Price cannot be empty")
            .GreaterThan(0).WithMessage("Price cannot be lower than 0");
    }

    public static IRuleBuilderOptions<T, string> GadgetDescription<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(1000).WithMessage("Maximum length is 1000");
    }

}