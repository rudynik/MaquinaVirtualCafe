using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CafeVirtual.Models
{
    public class Produto
    {
        #region Construtores
        public Produto()
        {
        }

        public Produto(string nome, string descricao, decimal valor, string imagem)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            Imagem = imagem;
        }

        public Produto(decimal valor, decimal total, decimal moeda)
        {
            Valor = valor;
            Total = total;
            Moeda = moeda;
        }
        #endregion

        #region Atributos
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }
        [NotMapped]
        public decimal Moeda { get; set; }
        [NotMapped]
        public decimal Troco { get; set; }
        [NotMapped]
        public decimal Total { get; set; }
        [NotMapped]
        private int contador { get; set; }
        #endregion

        #region Métodos
        public Produto CalcularProduto()
        {
            this.Total = Decimal.Round(this.Total + this.Moeda, 2);

            if (this.Total == this.Valor)
            {
                this.Troco = 0;
            }
            else if (this.Total < this.Valor)
            {
                this.Troco = -1;
            }
            else
            {
                this.Troco = Decimal.Round(this.Total - this.Valor, 2);
            }


            return this;
        }

        public List<string> CalculaTroco()
        {
            List<string> lista = new List<string>();

            this.Troco = this.Total - this.Valor;

            while (this.Troco > 0)
            {
                if (this.Troco >= 1)
                {
                    this.Troco = Valores(this.Troco, 1);
                    lista.Add($"Quantidade: {this.contador} - R$1,00 ");
                    this.contador = 0;
                }

                if (this.Troco >= 0.50m)
                {
                    this.Troco = Valores(this.Troco, 0.50m);
                    lista.Add($"Quantidade: {this.contador} - R$0,50 ");
                    this.contador = 0;
                }

                if (this.Troco >= 0.25m)
                {
                    this.Troco = Valores(this.Troco, 0.25m);
                    lista.Add($"Quantidade: {this.contador} - R$0,25 ");
                    this.contador = 0;
                }

                if (this.Troco >= 0.10m)
                {
                    this.Troco = Valores(this.Troco, 0.10m);
                    lista.Add($"Quantidade: {this.contador} - R$0,10 ");
                    this.contador = 0;
                }

                if (this.Troco >= 0.05m)
                {
                    this.Troco = Valores(this.Troco, 0.05m);
                    lista.Add($"Quantidade: {this.contador} - R$0,05 ");
                    this.contador = 0;
                }

                if (this.Troco >= 0.01m)
                {
                    this.Troco = Valores(this.Troco, 0.01m);
                    lista.Add($"Quantidade: {this.contador} - R$0,01 ");
                    this.contador = 0;
                }
            }

            return lista;
        }

        private decimal Valores(decimal troco, decimal tipo)
        {
            while (troco >= tipo)
            {
                this.contador = (int)(troco / tipo);
                troco = troco % tipo;
            }

            return troco;
        }
        #endregion
    }
}
