using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_SAFEGUARD.Context;
using API_SAFEGUARD.Models;
using Microsoft.AspNetCore.Authorization;

namespace API_SAFEGUARD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EntregaController(AppDbContext context)
        {
            _context = context;
        }
        ///<summary>
        ///  Retorna todos dados das entregas registradas
        /// </summary>
        /// <remarks>
        ///  Exemplo de retorno:
        ///   {
        ///     "idEntrega": 0, ---> Id da entrega enviada
        ///     "idColaborador": 0, --->  Identificação do colaborador que recebeu a entreha
        ///     "idEpi": 0, --->  Identificação do EPI entregada
        ///     "dataDeEntrega": "2024-04-25", ---> Data em que foi realizada a entrega
        ///     "validade": "2024-04-25" ---> Validade do EPI
        ///   }
        /// </remarks>
        /// <response code = "200">Retornado quando a requisição é feita com sucesso</response>
        // GET: api/Entrega
        [HttpGet]
          [Authorize]
        public async Task<ActionResult<IEnumerable<Entrega>>> GetEntregas()
        {
            if (_context.Entregas == null)
            {
                return NotFound();
            }
            return await _context.Entregas.ToListAsync();
        }

        ///<summary>
        ///  Retorna todos dados de  uma entrega específica pelo seu id
        /// </summary>
        /// <remarks>
        ///  Exemplo de retorno:
        ///   {
        ///     "idEntrega": 0, ---> Id da entrega enviada  (mesmo valor passado na chamada)
        ///     "idColaborador": 0, --->  Identificação do colaborador que recebeu a entreha
        ///     "idEpi": 0, --->  Identificação do EPI entregada
        ///     "dataDeEntrega": "2024-04-25", ---> Data em que foi realizada a entrega
        ///     "validade": "2024-04-25" ---> Validade do EPI
        ///   }
        ///   COLOQUE NO CAMPO REQUISITADO {id} O ID DA ENTREGA QUE DESEJA SER RETORNADA
        ///   </remarks>
        /// 
        /// <response code = "200">Retornado quando a requisição é feita com sucesso</response>

        // GET: api/Entrega/5
        [HttpGet("{id}")]
          [Authorize]
        public async Task<ActionResult<Entrega>> GetEntrega(int id)
        {
            if (_context.Entregas == null)
            {
                return NotFound();
            }
            var entrega = await _context.Entregas.FindAsync(id);

            if (entrega == null)
            {
                return NotFound();
            }

            return entrega;
        }

        ///<summary>
        ///  Atualiza os dados  de uma entrega já existente no banco de dados   
        /// </summary>
        /// <remarks>
        ///  Exemplo de retorno:
        ///   {
        ///     "idEntrega": 0, ---> Campo do tipo numerico do Id da entrega enviada  (mesmo valor passado na chamada)
        ///     "idColaborador": 0, --->  Campo do tipo numerico da Identificação do colaborador que recebeu a entrega(ID referente a api colaborador, insira um id que exista)
        ///     "idEpi": 0, --->  Campo do tipo numerico da Identificação do EPI entregada(referente a api dos EPIs, , insira um id que exista)
        ///     "dataDeEntrega": "2024-04-25", ---> Data em que foi realizada a entrega
        ///     "validade": "2024-04-25" ---> Validade do EPI
        ///   }
        ///   COLOQUE NO CAMPO REQUISITADO {id} O ID DA ENTREGA QUE DESEJA SER RETORNADA
        ///   </remarks>
        /// 
        /// <response code = "200">Retornado quando a atualização é feita com sucesso</response>

        // PUT: api/Entrega/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
          [Authorize("Admin")]
        public async Task<IActionResult> PutEntrega(int id, Entrega entrega)
        {
            if (id != entrega.IdEntrega)
            {
                return BadRequest();
            }

            _context.Entry(entrega).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntregaExists(id))
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
        ///  Adiciona novos  dados de uma entrega no banco de dados
        /// </summary>
        /// <remarks>
        ///  Exemplo de retorno:
        ///   {
        ///     "idEntrega": 0, ---> Campo do tipo numerico do Id da entrega enviada  (insira um valor não existente ou deixe vazio)
        ///     "idColaborador": 0, --->  Campo do tipo numerico da Identificação do colaborador que recebeu a entrega(ID referente a api colaborador, , insira um id que exista)
        ///     "idEpi": 0, --->  Campo do tipo numerico da Identificação do EPI entregada(referente a api dos EPIs, , insira um id que exista)
        ///     "dataDeEntrega": "2024-04-25", ---> Data em que foi realizada a entrega
        ///     "validade": "2024-04-25" ---> Validade do EPI
        ///   }
        ///   COLOQUE NO CAMPO REQUISITADO {id} O ID DA ENTREGA QUE DESEJA SER RETORNADA
        ///   </remarks>
        /// 
        /// <response code = "200">Retornado quando os daodos são adicionados com sucesso</response>

        // POST: api/Entrega
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
          [Authorize("Admin")]
        public async Task<ActionResult<Entrega>> PostEntrega(Entrega entrega)
        {
            if (_context.Entregas == null)
            {
                return Problem("Entity set 'AppDbContext.Entregas'  is null.");
            }
            _context.Entregas.Add(entrega);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntrega", new { id = entrega.IdEntrega }, entrega);
        }
         /// <summary>
        /// Deleta  um registro especifico do banco de dados
        /// </summary>
        /// <remarks>
        /// O item do id inserido será deletado para sempre sem possibilidade de recuperaçao, tome cuidado
        /// </remarks>
        /// <response  code="200">Retorna uma mensagem de que foi excluído com sucesso.</response>
        // DELETE: api/Entrega/5
        [HttpDelete("{id}")]
          [Authorize("Admin")]
        public async Task<IActionResult> DeleteEntrega(int id)
        {
            if (_context.Entregas == null)
            {
                return NotFound();
            }
            var entrega = await _context.Entregas.FindAsync(id);
            if (entrega == null)
            {
                return NotFound();
            }

            _context.Entregas.Remove(entrega);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntregaExists(int id)
        {
            return (_context.Entregas?.Any(e => e.IdEntrega == id)).GetValueOrDefault();
        }
    }
}
