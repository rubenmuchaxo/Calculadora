using Calculadora.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet] //opcional -> se não se especificar, escolhe sempre o Get
        public IActionResult Index()
        {
            //preparar os dados para serem enviados para a View na primeira interação
            ViewBag.Visor = "0";

            return View();
        }

        [HttpPost] //Quando o formulário for submetido em 'post', ele será acionado
        public IActionResult Index(string botao, string visor, string primeiroOperando, string operador, string limpa)
        {
            

            //testar valor do 'botao'

            switch (botao)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    //pressionou um algarismo
                    //Sugestão -> Fazer de forma algébrica
                    if (visor == "0" || limpa == "sim")
                    {
                        visor = botao;
                    }
                    else { 
                        visor += botao; 
                    }
                    limpa = "nao";
                    break;

                case ",":
                    if(!visor.Contains(',')) 
                    { 
                        visor += botao;
                    }
                    break;

                case "+/-":
                    //vai inverter o valor do visor 
                    //Sugestão -> Fazer de forma algébrica
                    if (!visor.StartsWith("-")) // ou então !visor.Contains("-")
                    {
                        visor = "-" + visor;
                    }
                    else
                    {
                        visor = visor[1..]; //Semelhante a visor.SubString(1) 
                                            //ou então visor.Remove(0, 1);-> Remove do indice 0 ao indice 1 (exclusive)
                    }
                    break;

                case "C":
                    visor = "0";
                    limpa = "sim";
                    primeiroOperando = "";
                    operador = "";
                    break;

                case "+":
                case "-":
                case "x":
                case ":":
                    //pressionado um operador
                    if (!string.IsNullOrEmpty(operador))
                    {
                        //fazer a operação
                        double operandoUm = Convert.ToDouble(primeiroOperando.Replace(",","."));
                        double operandoDois = Convert.ToDouble(visor.Replace(",","."));
                        double result=0;
                        switch (operador)
                        {
                            case "+":
                                result = operandoUm + operandoDois;
                                break;

                            case "x":
                                result = operandoUm * operandoDois;
                                break;

                            case "-":
                                result = operandoUm - operandoDois;
                                break;

                            case ":":
                                result = operandoUm / operandoDois;
                                break;

                        }
                        visor = result + "";
                    }
                        primeiroOperando = visor;
                        operador = botao;
                        limpa = "sim";

                    
                    break;

            }

            //preparar os dados para serem enviados para a View
            ViewBag.Visor = visor;
            ViewBag.PrimeiroOperando = primeiroOperando;
            ViewBag.Operador = operador;
            ViewBag.Limpa = limpa;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}