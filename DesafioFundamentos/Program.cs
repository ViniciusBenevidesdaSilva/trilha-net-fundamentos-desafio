using DesafioFundamentos.Models;
using DesafioFundamentos.Utils;
using System;

namespace DesafioFundamentos
{
    class Program
    {
        static void Main(string[] args)
        {
            // Coloca o encoding para UTF8 para exibir acentuação
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            decimal precoInicial = 0;
            decimal precoPorHora = 0;

            Console.WriteLine("Seja bem vindo ao sistema de estacionamento!\n");
            precoInicial = LeituraConsole.LeConsoleDecimal("Digite o preço inicial: R$ ");
            precoPorHora = LeituraConsole.LeConsoleDecimal("Agora digite o preço por hora: R$ ");

            // Instancia a classe Estacionamento, já com os valores obtidos anteriormente
            Estacionamento es = new Estacionamento(precoInicial, precoPorHora);

            // Realiza o loop do menu
            Menu(ref es);

            Console.WriteLine("O programa se encerrou!");
        }

       
        public static void Menu(ref Estacionamento es)
        {
            bool exibirMenu = true;
            string opcao = string.Empty;

            while (exibirMenu)
            {
                Console.Clear();
                Console.WriteLine("===================================" +
                    "\nDigite a sua opção:" +
                    "\n 1 - Cadastrar veículo" +
                    "\n 2 - Remover veículo" +
                    "\n 3 - Listar veículos" +
                    "\n 4 - Encerrar");

                opcao = Console.ReadLine();

                Console.WriteLine("===================================\n");

                switch (opcao)
                {
                    case "1":
                        es.AdicionarVeiculo();
                        break;

                    case "2":
                        es.RemoverVeiculo();
                        break;

                    case "3":
                        es.ListarVeiculos();
                        break;

                    case "4":
                        exibirMenu = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

                Console.WriteLine("\n\nPressione uma tecla para continuar...");
                Console.ReadLine();
            }
        }
    }
}
