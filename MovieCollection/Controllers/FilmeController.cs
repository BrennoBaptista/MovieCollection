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
    public class FilmeController : Controller
    {
        FilmeRepository _repository = new FilmeRepository();

        // GET: Filme
        public ActionResult Index()
        {
            var filmes = _repository.ListarFilmes();

            return View(filmes);
        }

        // GET: Filme/Details/5
        public ActionResult Details(int id)
        {
            var filme = _repository.DetalharFilme(id);

            if (filme == null)
            {
                return StatusCode(404);
            }

            return View(filme);
        }

        // GET: Filme/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Filme/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Filme filme)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.CriarFilme(filme);
                    return RedirectToAction(nameof(Index));
                }
                return View(filme);
            }
            catch
            {
                return View();
            }
        }

        // GET: Filme/Edit/5
        public ActionResult Edit(int id)
        {
            var filme = _repository.DetalharFilme(id);

            if (filme == null)
            {
                return StatusCode(404);
            }

            return View(filme);
        }

        // POST: Filme/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Filme filme)
        {
            if (ModelState.IsValid)
            {
                _repository.AtualizarFilme(new Filme
                {
                    Id = filme.Id,
                    Titulo = filme.Titulo,
                    TituloOriginal = filme.TituloOriginal,
                    Ano = filme.Ano
                });
                return RedirectToAction("Index");
            }
            else
            {
                return View(filme);
            }
        }

        // GET: Filme/Delete/5
        public ActionResult Delete(int id)
        {
            var filme = _repository.DetalharFilme(id);

            if (filme == null)
            {
                return StatusCode(404);
            }

            return View(filme);
        }

        // POST: Filme/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Filme filme)
        {
            _repository.ExcluirFilme(filme.Id);
            return RedirectToAction("Index");
        }
    }
}