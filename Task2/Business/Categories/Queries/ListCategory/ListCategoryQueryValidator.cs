using FluentValidation;

namespace Business.Categories.Queries.ListCategory
{
    public class ListCategoryQueryValidator : AbstractValidator<ListCategoryQuery>
    {
        public ListCategoryQueryValidator ()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1);
        }
    }
}
