using FluentValidation;

namespace Business.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator ()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
