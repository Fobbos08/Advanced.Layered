using System;
using System.Collections.Generic;
using System.Linq;

using Business.Categories.Commands.AddCategory;
using Business.Tests.Steps;

using Domain.Entities;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using TechTalk.SpecFlow;

namespace Business.Integration.Tests.Steps
{
    [Binding]
    public sealed class CategorySteps : BaseSteps
    {
        private readonly List<Category> _categories = new List<Category>();
        private readonly List<Exception> _exceptions = new List<Exception>();

        public CategorySteps (ScenarioContext context) : base(context)
        {
        }

        [BeforeScenario]
        public void CleanDatabase ()
        {
            _categories.Clear();
            _exceptions.Clear();
            ((DbContext)_dbContext).Database.EnsureDeleted();
        }

        [Given(@"the Category Name: (\w+)")]
        public void GivenCategoryIs (string name)
        {
            var category = new Category()
            {
                Name = name
            };
            _categories.Add(category);
        }

        [Given(@"the Category Name: (\w+) ImageUrl:(.*)")]
        public void GivenCategoryIs (string name, string url)
        {
            var category = new Category()
            {
                Name = name,
                Image = new Uri(url)
            };
            _categories.Add(category);
        }

        [When(@"defined categories added")]
        public async void WhenDefinedCategoriesAdded ()
        {
            foreach (var category in _categories)
            {
                var command = new AddCategoryCommand()
                {
                    Image = category.Image,
                    Name = category.Name
                };

                try
                {
                    await _mediator.Send(command);
                }
                catch (ValidationException exception)
                {
                    _exceptions.Add(exception);
                }
            }
        }

        [Then(@"should exist (.*) validation errors")]
        public void ThenExistValidationError (int count)
        {
            Assert.AreEqual(_exceptions.Count, count);
        }

        [Then(@"should exist (.*) categories")]
        public void ThenTheResultShouldBe (int count)
        {
            Assert.AreEqual(count, _dbContext.Categories.Count());
        }

        [Then(@"first category Name is (.*)")]
        public void ThenFirstCategoryNameIs (string name)
        {
            var category = _dbContext.Categories.First();
            Assert.AreEqual(name, category.Name);
        }
    }
}
