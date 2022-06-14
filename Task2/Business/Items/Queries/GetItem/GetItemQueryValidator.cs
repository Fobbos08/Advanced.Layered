using FluentValidation;

namespace Business.Items.Queries.GetItem
{
    public class ListItemQueryValidator : AbstractValidator<GetItemQuery>
    {
        public ListItemQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0);
        }
    }
}
