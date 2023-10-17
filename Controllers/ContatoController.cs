using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using moduloApi.Context;
using moduloApi.Entities;

namespace moduloApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = contato.id }, contato);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var contato = _context.Contatos.Find(id);
            if (contato == null) return NotFound();
            return Ok(contato);
        }

        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));
            return Ok(contatos);
        }

        [HttpPut("{id}")]
        public IActionResult atualizar(int id, Contato contato)
        {
            var contatoDb = _context.Contatos.Find(id);
            if (contatoDb == null) return NotFound();
            contatoDb.Nome = contato.Nome;
            contatoDb.Telefone = contato.Telefone;
            contatoDb.Ativo = contato.Ativo;
            _context.Contatos.Update(contatoDb);
            _context.SaveChanges();
            return Ok(contatoDb);
        }

        [HttpDelete("{id}")]
        public IActionResult deletar(int id)
        {
            var contatoDb = _context.Contatos.Find(id);
            if (contatoDb == null) return NotFound();
            _context.Contatos.Remove(contatoDb);
            _context.SaveChanges();
            return NoContent();
        }
    }
}