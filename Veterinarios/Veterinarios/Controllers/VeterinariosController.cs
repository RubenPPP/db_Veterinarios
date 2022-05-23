#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Veterinarios.Data;
using Vets.Models;

namespace Veterinarios.Controllers
{
    [Authorize]
    public class VeterinariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VeterinariosController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Veterinarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Veterinarios.ToListAsync());
        }

        // GET: Veterinarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarios = await _context.Veterinarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinarios == null)
            {
                return NotFound();
            }

            return View(veterinarios);
        }

        // GET: Veterinarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veterinarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,NumCedulaProf,Fotografia")] Vets.Models.Veterinarios veterinarios, IFormFile fotoVet)
        {
            /*
             * Algoritmo para processar as imagens
             * 
             * Se ficheiro imagem nulo
             *      atribuir uma imagem genérica ao veterinário
             * Else
             *      será que o ficheiro é uma imagem
             *      se sim
             *          definir o nome a atribuir à imagem
             *          atribuir aos dados do novo veterinario, o nome do ficheiro da imagem
             *          guardar a imagem no disco rígido do servidor
             *      se não for
             *          criar mensagem de erro
             *          devolver o controlo da app à view
             *      
             * 
             */

            if (fotoVet == null)
            {
                veterinarios.Fotografia = "noVet.png";
            } else
            {
                if (fotoVet.ContentType == "image/png" || fotoVet.ContentType == "image/jpeg")
                {
                    // Definir o nome da foto
                    Guid g = Guid.NewGuid();
                    string nomeFoto = veterinarios.NumCedulaProf + g.ToString();
                    string extensaoFoto = Path.GetExtension(fotoVet.FileName);
                    nomeFoto += extensaoFoto;
                    // Atribuir ao vet o nome da sua foto
                    veterinarios.Fotografia = nomeFoto;
                } else
                {
                    // Criar mensagem de erro
                    ModelState.AddModelError("", "Por favor, adicione um ficheiro .png ou .jpg");
                    // Devolver o controlo da app à view
                    // Fornecendo-lhe os dados que o utilizador já tinha preenchido no formulário
                    return View(veterinarios);
                }
            }
            // Avaliar se os dados fornecidos pelo utilizador estão de acordo com as regras do Model
            if (ModelState.IsValid)
            {
                try
                {
                    // Adicionar os dados à BD
                    _context.Add(veterinarios);
                    // Consolidar esses dados (commit)
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    // É da nossa responsabilidade tratarmos da exceção!

                    // Registar no disco rígido do servidor todos os dados da operação:
                    // data + hora
                    // nome do utilizador
                    // nome do controller + método
                    // dados do erro
                    // outros dados considerados úteis

                    // eventualmente, tentar guardar na (numa) base de dados os dados do erro
                    // eventualmente, notificar o Administrador da app do erro

                    // Criar msg de erro
                    ModelState.AddModelError("", "Ocorreu um erro com a operação de guardar os dados do Veterinário " + veterinarios.Nome);
                    // Devolver controlo à View
                    return View(veterinarios);
                }
                // +++Concretizar a ação de guardar o ficheiro da foto
                if (fotoVet != null)
                {
                    // Local a guardar ficheiro
                    string nomeLocalizacaoFicheiro = _webHostEnvironment.WebRootPath;
                    nomeLocalizacaoFicheiro = Path.Combine(nomeLocalizacaoFicheiro, "Fotos");
                    // Avaliar se a pasta 'Fotos' existe
                    if (!Directory.Exists(nomeLocalizacaoFicheiro))
                        Directory.CreateDirectory(nomeLocalizacaoFicheiro);
                    // Nome do documento a guardar
                    string nomeDaFoto = Path.Combine(nomeLocalizacaoFicheiro, veterinarios.Fotografia);
                    // Criar o objeto que vai manipular o ficheiro
                    using var stream = new FileStream(nomeDaFoto, FileMode.Create);
                    // Guardar no disco rígido
                    fotoVet.CopyTo(stream);
                    // ---Concretizar a ação de guardar o ficheiro da foto
                } 
                // Devolver o controlo da app à View
                return RedirectToAction(nameof(Index));
            }
            return View(veterinarios);
        }

        // GET: Veterinarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            /* 
             * Só se altera a foto do Vet, se for carregado
             * algum ficheiro, e mesmo assim, só se for
             * uma imagem
             * 
             * Se há uma nova imagem, qual o seu nome?
             * Vamos manter o nome da foto antiga ou dar um novo?
             * Se mantiver, pode haver problemas com a cache do browser
             * Se for novo, tenho de apahar o ficheiro antigo
             *  Se não for o 'noVet.png'
             */
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var veterinario = await _context.Veterinarios.FindAsync(id);
            if (veterinario == null)
            {
                return RedirectToAction("Index");
            }

            // Preservar, para memória futura, os dados que não devem ser adulterados pelo utilizador no browser
            // Vamos usar 'variáveis de sessão'
            // Podemos guardar INT e STRING
            // Quero guardar o ID do médico veterinário
            HttpContext.Session.SetInt32("VetID", (int) id);
            return View(veterinario);
        }

        // POST: Veterinarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,NumCedulaProf,Fotografia")] Vets.Models.Veterinarios veterinario)
        {
            if (id != veterinario.Id)
            {
                return NotFound();
            }

            /* Confirmar se não houve adulteração de dados no browser
             * para que isto aconteça:
             * - Recuperar o valor da varíavel de sessão
             * - Comparar este valor com os dados que chegam do browser
             * - Se forem diferentes, temos um problema...
             */

            var idMedicoVeterinario = HttpContext.Session.GetInt32("VetID");
            // Se a variável 'idMedicoVeterinario' for nula, o que aconteceu?
            // Houve injeção de dados através de uma ferramenta externa
            // Demorou-se demasiado tempo na execução da tarefa
            if (idMedicoVeterinario == null)
            {
                ModelState.AddModelError("", "Demorou demasiado tempo a executar a tarefa de edição");
                return View(veterinario);
            }

            if (idMedicoVeterinario != veterinario.Id)
            {
                // Temos problemas... o que vamos fazer?
                return RedirectToAction("Index");
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinariosExists(veterinario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(veterinario);
        }

        // GET: Veterinarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarios = await _context.Veterinarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinarios == null)
            {
                return NotFound();
            }

            return View(veterinarios);
        }

        // POST: Veterinarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var veterinario = await _context.Veterinarios.FindAsync(id);
                _context.Veterinarios.Remove(veterinario);
                await _context.SaveChangesAsync();
                // Remover o ficheiro com a foto do Veterinário se a foto não for a 'noVet.png'
            } catch (Exception ex)
            {
                // Não esquecer, tratar da exceção
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinariosExists(int id)
        {
            return _context.Veterinarios.Any(e => e.Id == id);
        }
    }
}
