using FluentValidation;

namespace Business.Items.Queries.ListItem
{
    public class ListItemQueryValidator : AbstractValidator<ListItemQuery>
    {
        public ListItemQueryValidator ()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1);
        }
    }
}
