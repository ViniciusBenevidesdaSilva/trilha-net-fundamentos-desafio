using System;

namespace DesafioFundamentos.Utils
{
    public static class LeituraConsole
    {
        public static decimal LeConsoleDecimal(string mensagem = "Digite um valor Decimal: ")
        {
            while (true)
            {
                try
                {
                    Console.Write(mensagem);
                    return Convert.ToDecimal(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Erro: Valor Inválido! Certifique-se de digitar um número decimal válido.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Erro: {e.Message}");
                }
            }
        }

        public static int LeConsoleInt(string mensagem = "Digite um valor Inteiro: ")
        {
            while (true)
            {
                try
                {
                    Console.Write(mensagem);
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Erro: Valor Inválido! Certifique-se de digitar um número inteiro válido.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Erro: {e.Message}");
                }
            }
        }
    }
}
