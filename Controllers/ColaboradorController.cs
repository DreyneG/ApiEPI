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
    public class ColaboradorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ColaboradorController(AppDbContext context)
        {
            _context = context;
        }

        ///<summary>
        ///  Retorna todos dados da EPIs registradas
        ///</summary>
        /// <remarks>Exemplo de retorno:
        ///  {
        ///     "idColaborador": 0, ---> Id do colaborador
        ///     "nomeColaborador": "string", -->  Nome completo do colaborador
        ///     "ctps": 0, --> Numero da carteira de trabalho do colaborador
        ///     "dataDeAdmisao": "2024-04-25", --> Data de admisao do colaborador na empresa
        ///     "telefone": 0, --> Numero de telefone do colaborador
        ///     "cpf": 0, -->  CPF do colaborador
        ///     "tipoFuncionario": "string",  --> Tipo de funcionario 
        ///     "email": "string" -->  Email do colaborador
        ///     }</remarks>
        /// 
        /// <response code = "200">Retornado quando a requisição é feita com sucesso</response>
        // GET: api/Colaborador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colaborador>>> GetColaboradors()
        {
            if (_context.Colaboradors == null)
            {
                return NotFound();
            }
            return await _context.Colaboradors.ToListAsync();
        }
        ///<summary>
        ///  Retorna os dados de um colaborador especificp
        /// </summary>
        /// <remarks>Exemplo de retorno:
        ///  {
        ///     "idColaborador": 0, ---> Id do colaborador que vai ser retornado
        ///     "nomeColaborador": "string", -->  Nome completo do colaborador
        ///     "ctps": 0, --> Numero da carteira de trabalho do colaborador
        ///     "dataDeAdmisao": "2024-04-25", --> Data de admisao do colaborador na empresa
        ///     "telefone": 0, --> Numero de telefone do colaborador
        ///     "cpf": 0, -->  CPF do colaborador
        ///     "tipoFuncionario": "string",  --> Tipo de funcionario 
        ///     "email": "string" -->  Email do colaborador
        ///    }
        /// COLOQUE NO CAMPO REQUISITADO {idColaborador} o ID DO COLABORADOR QUE VAI SER BUSCADO </remarks>
        /// 
        /// <response code = "200">Retornado quando a requisição é feita com sucesso</response>

        // GET: api/Colaborador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colaborador>> GetColaborador(int id)
        {
            if (_context.Colaboradors == null)
            {
                return NotFound();
            }
            var colaborador = await _context.Colaboradors.FindAsync(id);

            if (colaborador == null)
            {
                return NotFound();
            }

            return colaborador;
        }

        ///<summary>
        ///  Atualiza e altera os dados de um campo ja existente num banco de dados
        /// </summary>
        /// <remarks>Exemplo de retorno:
        ///  {
        ///     "idColaborador": 0, ---> Campo do tipo numerico inteiro que representa o ID do Colaborador (insira o mesmo id da requisição)
        ///     "nomeColaborador": "string", -->  Campo do tipo string do nome completo do colaborador
        ///     "ctps": 0, --> Campo do tipo numerico do numero da carteira de trabalho do colaborador
        ///     "dataDeAdmisao": "2024-04-25", --> Campo do tipo numerico da data de admisao do colaborador na empresa
        ///     "telefone": 0, --> Campo do tipo numerico do numero de telefone do colaborador
        ///     "cpf": 0, -->  Campo do tipo numerico do CPF do colaborador
        ///     "tipoFuncionario": "string",  --> Tipo de funcionario 
        ///     "email": "string" -->  Campo do tipo String do Email do colaborador
        ///}
        /// 
        /// COLOQUE NO CAMPO  REQUISITADO {IdColaborador}  O ID DO COLABORADOR QUE VAI SER ALTERADO E OS DADOS A SEREM ALTERADOS 
        /// </remarks>
        /// <response code = "200">Retornado quando a atualização é feita com sucesso</response>
        // PUT: api/Colaborador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColaborador(int id, Colaborador colaborador)
        {
            if (id != colaborador.IdColaborador)
            {
                return BadRequest();
            }

            _context.Entry(colaborador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradorExists(id))
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
        ///  Adiciona novos dados  no banco de dados
        /// </summary>
        /// <remarks>
        ///  Exemplo de retorno:
        ///  {
        ///     "idColaborador": 0, ---> Campo do tipo numerico inteiro que representa o ID do Colaborador (insira um valor não existente, ou deixe vazio)
        ///     "nomeColaborador": "string", -->  Campo do tipo string do nome completo do colaborador
        ///     "ctps": 0, --> Campo do tipo numerico do numero da carteira de trabalho do colaborador
        ///     "dataDeAdmisao": "2024-04-25", --> Campo do tipo numerico da data de admisao do colaborador na empresa
        ///     "telefone": 0, --> Campo do tipo numerico do numero de telefone do colaborador
        ///     "cpf": 0, -->  Campo do tipo numerico do CPF do colaborador
        ///     "tipoFuncionario": "string",  --> Tipo de funcionario 
        ///     "email": "string" -->  Campo do tipo String do Email do colaborador
        /// }
        /// 
        /// COLOQUE NO CAMPO  REQUISITADO {IdColaborador}  O ID DO COLABORADOR QUE VAI SER ALTERADO E OS DADOS A SEREM ALTERADOS 
        /// </remarks>
        /// <response code = "200">Retornado quando o dado é adicionado com sucesso</response>

        // POST: api/Colaborador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Colaborador>> PostColaborador(Colaborador colaborador)
        {
            if (_context.Colaboradors == null)
            {
                return Problem("Entity set 'AppDbContext.Colaboradors'  is null.");
            }
            _context.Colaboradors.Add(colaborador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColaborador", new { id = colaborador.IdColaborador }, colaborador);
        }
         /// <summary>
        /// Deleta  um registro especifico do banco de dados
        /// </summary>
        /// <remarks>O item do id inserido será deletado para sempre sem possibilidade de recuperaçao, tome cuidado</remarks>
        /// <response  code="200">Retorna uma mensagem de que foi excluído com sucesso.</response>

        // DELETE: api/Colaborador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColaborador(int id)
        {
            if (_context.Colaboradors == null)
            {
                return NotFound();
            }
            var colaborador = await _context.Colaboradors.FindAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }

            _context.Colaboradors.Remove(colaborador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColaboradorExists(int id)
        {
            return (_context.Colaboradors?.Any(e => e.IdColaborador == id)).GetValueOrDefault();
        }
    }
}
