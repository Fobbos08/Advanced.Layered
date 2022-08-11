using FluentValidation;

namespace Business.Categories.Queries.GetCategory
{
    public class ListCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
    {
        public ListCategoryQueryValidator ()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0);
        }
    }
}
