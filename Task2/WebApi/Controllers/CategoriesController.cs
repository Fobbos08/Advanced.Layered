using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Categories.Commands.AddCategory;
using Business.Categories.Commands.DeleteCategory;
using Business.Categories.Commands.UpdateCategory;
using Business.Categories.Queries.GetCategory;
using Business.Categories.Queries.ListCategory;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseApiController
    {
        [HttpGet]
        //[Authorize(Roles = "manager")]//just for easy demonstration
        //[Authorize]
        public async Task<List<Category>> GetCategories([FromQuery]ListCategoryQuery query)
       {
           var categories = await Mediator.Send(query);
            return categories;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetCategory(int id)
        {
            var category = await Mediator.Send(new GetCategoryQuery(){ Id = id });
            return new JsonResult(new { Content = category, Links = CreateLinks(id) });
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> CreateCategory(AddCategoryCommand command)
        {
            var id = await Mediator.Send(command);
            return new JsonResult(new { Content = new {categoryId = id } , Links = CreateLinks(id)});
        }

        [HttpPut]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> UpdateCategory(UpdateCategoryCommand command)
        {
            await Mediator.Send(command);
            return new JsonResult(new { Content = "", Links = "" });
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await Mediator.Send(new DeleteCategoryCommand(){Id = id});

            return Ok();
        }

        private List<LinkDto> CreateLinks(int id)
        {
            var list = new List<LinkDto>();
            list.Add(new LinkDto($"api/categories/${id}", "delete", "DELETE"));
            list.Add(new LinkDto($"api/categories/", "update", "PUT"));
            
            return list;
        }
    }
}
