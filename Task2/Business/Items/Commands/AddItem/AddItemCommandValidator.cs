using FluentValidation;

namespace Business.Items.Commands.AddItem
{
    public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
    {
        public AddItemCommandValidator ()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
