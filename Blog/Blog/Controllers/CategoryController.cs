﻿using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {

        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X04 - Falha interna no servidor");
            }
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var categorie = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (categorie == null)
                    return NotFound();
                return Ok(categorie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X02 - Falha interna no servidor");
            }
        }

        [HttpPost("v1/categories/")]
        public async Task<IActionResult> PostAsync([FromBody] Category model, [FromServices] BlogDataContext context)
        {
            try
            {
                await context.Categories.AddAsync(model);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{model.Id}", model);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05X09 - Não foi possivel incluir a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X10 - Falha interna no servidor");
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Category model, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05X08 - Não foi possivel alterar a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X11 - Falha interna no servidor");
            }
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound();

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05X07 - Não foi possivel alterar a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X12 - Falha interna no servidor");
            }
        }

    }
}
