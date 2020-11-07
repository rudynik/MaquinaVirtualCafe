using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CafeVirtual.Models;
using CafeVirtual.Repositorios;
using Microsoft.AspNetCore.Http;
using System.Xml.Serialization;
using System.IO;
using CafeVirtual.Utilidades;

namespace CafeVirtual.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProdutoRepository _repo;

        public HomeController(ILogger<HomeController> logger, IProdutoRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public IActionResult Index()
        {
            Produto produto1 = new Produto("Cappuccino", "Experimente nosso delicioso cappuccino",3.50m, "cappuccino.jpeg");
            Produto produto2 = new Produto("Mocha", "Nossa mocha com chocolate é uma delícia", 4.00m, "mocha.jpeg");
            Produto produto3 = new Produto("Café com leite", "Tradicional café com leite da família brasileira", 3.00m, "cafeComLeite.jpeg");

            if (!_repo.InicialAdd(produto1.Nome))
            {
                _repo.AddProduto(produto1);
            }

            if (!_repo.InicialAdd(produto2.Nome))
            {
                _repo.AddProduto(produto2);
            }

            if (!_repo.InicialAdd(produto3.Nome))
            {
                _repo.AddProduto(produto3);
            }


            return View(_repo.GetProdutos());
        }

        public IActionResult Pagamento(int id)
        {
            return View(_repo.FindProduto(id));
        }
        [HttpPost]
        public JsonResult DadosPagamento(Produto produto)
        {
            return Json(produto.CalcularProduto());
        }

        public JsonResult DadosFinalizar(Produto produto)
        {
            HttpContext.Session.SetString("lista", Persistencia.SerializaParaString<List<string>>(produto.CalculaTroco()));

            return Json(produto.Id);
        }

        public IActionResult Resumo(int id)
        {
            var produto = _repo.FindProduto(id);
            var lista = (List<string>)Persistencia.DeserializaParaObjeto(HttpContext.Session.GetString("lista"), new List<string>().GetType());

            ViewBag.moedas = lista;

            return View(produto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
