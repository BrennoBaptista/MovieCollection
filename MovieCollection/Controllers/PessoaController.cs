using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCollection.Domain;
using MovieCollection.Repository;

namespace MovieCollection.Controllers
{
    public class PessoaController : Controller
    {
        PessoaRepository _repository = new PessoaRepository();

        // GET: Pessoa
        public ActionResult Index()
        {
            var pessoas = _repository.ListarPessoas();
            return View(pessoas);
        }

        // GET: Pessoa/Details/5
        public ActionResult Details(int id)
        {
            var pessoa = _repository.DetalharPessoa(id);
            if (pessoa == null)
            {
                return StatusCode(404);
            }
            return View(pessoa);
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pessoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] Pessoa pessoa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.CriarPessoa(pessoa);
                    return RedirectToAction("Index");
                }
                return View(pessoa);
            }
            catch
            {
                return View();
            }
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(int id)
        {
            var pessoa = _repository.DetalharPessoa(id);

            if (pessoa == null)
            {
                return StatusCode(404);
            }

            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _repository.AtualizarPessoa(new Pessoa
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome,
                    Sobrenome = pessoa.Sobrenome,
                    DataNascimento = pessoa.DataNascimento
                });
                return RedirectToAction("Index");
            }
            else
            {
                return View(pessoa);
            }
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(int id)
        {
            var pessoa = _repository.DetalharPessoa(id);

            if (pessoa == null)
            {
                return StatusCode(404);
            }

            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Pessoa pessoa)
        {
            _repository.ExcluirPessoa(pessoa.Id);
            return RedirectToAction("Index");
        }
    }
}