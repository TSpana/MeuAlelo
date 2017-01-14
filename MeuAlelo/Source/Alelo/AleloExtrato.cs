using System.Collections.Generic;

namespace MeuAlelo.Source.Alelo
{
    public class AleloExtrato : List<ExtratoDados>
    {
        public string DataProximaRecarga { get; internal set; }
        public string GastoMedio { get; internal set; }
        public string Saldo { get; internal set; }
        public string ValorProximaRecarga { get; internal set; }

    }

    public class ExtratoDados
    {
        public ExtratoDados()
        {

        }
        public ExtratoDados(string data, string descricao, string valor)
        {
            this.Data = data;
            this.Descricao = descricao;
            this.Valor = valor;
        }
        public string Data { get; set; }
        public string Descricao { get; set; }
        public string Valor { get; set; }
    }
}