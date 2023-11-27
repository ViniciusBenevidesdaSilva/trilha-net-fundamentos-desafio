namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            // feat: Pedir para o usuário digitar uma placa (ReadLine) e adicionar na lista "veiculos"
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = Console.ReadLine().Trim().ToUpper();

            if(string.IsNullOrEmpty(placa))
            {
                Console.WriteLine("Placa inválida!");
                return;
            }
            
            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine("Veículo já está estacionado!");
                return;
            }

            veiculos.Add(placa);
        }

        public void RemoverVeiculo()
        {

            // feat: Pedir para o usuário digitar a placa e armazenar na variável placa
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = Console.ReadLine().Trim().ToUpper();

            // Verifica se o veículo existe
            if (!veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
                return;
            }

            // feat: Pedir para o usuário digitar a quantidade de horas que o veículo permaneceu estacionado,
            int horas = 0;
            decimal valorTotal = 0; 

            Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
            int.TryParse(Console.ReadLine(), out horas);
            
            // feat: Realizar o seguinte cálculo: "precoInicial + precoPorHora * horas" para a variável valorTotal                
            valorTotal = precoInicial + precoPorHora * horas;
            
            // feat: Remover a placa digitada da lista de veículos
            veiculos.Remove(placa);

            Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");

                // feat: Realizar um laço de repetição, exibindo os veículos estacionados
                foreach(string veiculo in veiculos)
                {
                    Console.WriteLine(veiculo);
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
