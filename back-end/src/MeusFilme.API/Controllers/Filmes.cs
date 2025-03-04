﻿using MeusFilme.API.Data;
using MeusFilme.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MeusFilme.API.Controllers
{
    [Authorize]
    [Route("api/filmes")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public FilmesController(AppDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        [HttpGet("todos-os-filmes")]
        public async Task<ActionResult<IEnumerable<Filme>>> ObterTodos()
        {
            if (ControllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = _accessor.HttpContext.User.FindFirst(ClaimTypes.Email);
            }
            return await _context.Filme.OrderBy(f=>f.AnoLancamento).ToListAsync();
        }
    }
}