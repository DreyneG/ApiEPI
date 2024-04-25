using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_SAFEGUARD.Context;
using API_SAFEGUARD.Models;

namespace API_SAFEGUARD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EpiController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        ///  Retorna todos dados da EPIs registradas
        /// </summary>
        /// <remarks>
        ///  Exemplo de retorno:
        ///  {
        ///    "idEpi": 0, ----> ID da EPI registrada,
        ///    "nomeEpi: "string", -- > Nome da EPI
        ///    "instrucoes": "string", --> Instruções para o usuário utilizar
        ///    "qtd": 0, --> Quantidade de unidades disponíveis da EPI
        ///  }
        ///  </remarks>
        /// 
        /// <response code = "200">Retornado quando a requisição é feita com sucesso</response>
        // GET: api/Epi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Epi>>> GetEpis()
        {
            if (_context.Epis == null)
            {
                return NotFound();
            }
            return await _context.Epis.ToListAsync();
        }
        /// <summary>
        ///  Retorna o dados do id especificado da EPI
        /// </summary>
        /// <remarks>
        ///  Exemplo de retorno:
        ///  {
        ///    "idEpi": 0, ----> ID da EPI registrada  (mesmo que tenha sido passado no parametro)
        ///    "nomeEpi: "string", -- > Nome da EPI
        ///    "instrucoes": "string", --> Instruções para o usuário utilizar
        ///    "qtd": 0, --> Quantidade de unidades disponíveis da EPI
        ///  }
        ///  COLOQUE NO CAMPO {ID}  O ID DA EPI QUE DESEJA BUSCAR
        ///  </remarks>
        /// 
        /// <response code = "200">Retornado quando a requisição é feita com sucesso</response>
        // GET: api/Epi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Epi>> GetEpi(int id)
        {
            if (_context.Epis == null)
            {
                return NotFound();
            }
            var epi = await _context.Epis.FindAsync(id);

            if (epi == null)
            {
                return NotFound();
            }

            return epi;
        }
        /// <summary>
        ///  Modifica e atualiza as informações do EPI do id inserido
        /// </summary>
        /// <remarks>
        ///  Exemplo de estrutura pra atualizar:
        ///  {
        ///    "idEpi": 0, ----> informação do tipo numerico inserir o id da EPI que for alterar no banco de dados
        ///    "nomeEpi: "string", -- > informação do tipo string do nome da EPI
        ///    "instrucoes": "string", --> informação do tipo string Instruções para o usuário utilizar
        ///    "qtd": 0, --> informação do tipo numerico da quantidade de unidades disponíveis da EPI
        ///  } </remarks>
        /// 
        /// <response code = "200">Retornado quando a atualização é feita com sucesso</response>

        // PUT: api/Epi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEpi(int id, Epi epi)
        {
            if (id != epi.IdEpi)
            {
                return BadRequest();
            }

            _context.Entry(epi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        ///<summary>
        ///  Adiciona um novo dado no banco de dados 
        /// </summary>
        /// <remarks>
        ///  Exemplo de estrutura pra adicionar:
        ///  {
        ///    "idEpi": 0, ----> informação do tipo numerico, inserir um id não existente ou deixar vazio
        ///    "nomeEpi: "string", -- > informação do tipo string do nome da EPI
        ///    "instrucoes": "string", --> informação do tipo string Instruções para o usuário utilizar
        ///    "qtd": 0, --> informação do tipo numerico da quantidade de unidades disponíveis da EPI
        ///  }
        ///  </remarks>
        /// 
        /// <response code = "200">Retornado quando os dados são adicionados com sucesso</response>
        // POST: api/Epi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Epi>> PostEpi(Epi epi)
        {
            if (_context.Epis == null)
            {
                return Problem("Entity set 'AppDbContext.Epis'  is null.");
            }
            _context.Epis.Add(epi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEpi", new { id = epi.IdEpi }, epi);
        }
        /// <summary>
        /// Deleta  um registro especifico do banco de dados
        /// </summary>
        /// <remarks>
        /// O item do id inserido será deletado para sempre sem possibilidade de recuperaçao, tome cuidado
        /// </remarks>
        /// <response  code="200">Retorna uma mensagem de que foi excluído com sucesso.</response>

        // DELETE: api/Epi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpi(int id)
        {
            if (_context.Epis == null)
            {
                return NotFound();
            }
            var epi = await _context.Epis.FindAsync(id);
            if (epi == null)
            {
                return NotFound();
            }

            _context.Epis.Remove(epi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EpiExists(int id)
        {
            return (_context.Epis?.Any(e => e.IdEpi == id)).GetValueOrDefault();
        }
    }
}
