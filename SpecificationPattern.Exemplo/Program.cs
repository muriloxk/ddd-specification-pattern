using System;
using System.Linq.Expressions;
using SpecificationLib;

namespace SpecificationPattern.Exemplo
{
    class Program
    {
        // Specifications: General Guidelines
        // * Avoid ISpecification interface
        // * Make specifications as specific as possible
        // * Make specifications immutable

        static void Main(string[] args)
        {
            var carro = new Carro { AnoModelo = 2015, Carroceria = Carroceria.Cupe, Cor = Cores.Amarelo };
            Console.WriteLine(ValidarSeCarroDeLuxo(carro));
        }

        //private static bool ValidarSeCarroDeLuxo(Carro carro)
        //{
        //    if (carro.AnoModelo < 2018)
        //        return false;

        //    if (carro.Carroceria != Carroceria.Seda)
        //        return false;

        //    if (carro.Cor != Cores.Preto)
        //        return false;

        //    return true;
        //}

        private static bool ValidarSeCarroDeLuxo(Carro carro)
        {
            return new CarrosDeLuxoSpec()
                             .And(new CarrosDoAnoSpec())
                            // Outras opções
                            //.Or(new CarrosDoAnoSpec())
                            //.And(new CarrosDoAnoSpec().Not())
                            .IsSatisfiedBy(carro);
        }

    }

    public class CarrosDeLuxoSpec : Specification<Carro>
    {
        public override Expression<Func<Carro, bool>> ToExpression()
        {
            return carro => carro.Cor == Cores.Preto
                            && carro.AnoModelo >= DateTime.Now.Year - 2
                            && carro.Carroceria == Carroceria.Seda;
        }
    }

    public class CarrosDoAnoSpec : Specification<Carro>
    {
        public override Expression<Func<Carro, bool>> ToExpression()
        {
            return carro => carro.AnoModelo >= DateTime.Now.Year;
        }
    }

    public class Carro
    {
        public int AnoModelo { get; set; }
        public Cores Cor { get; set; }
        public Carroceria Carroceria { get; set; }
    }

    public enum Carroceria
    {
        Seda,
        Cupe,
        SUV,
        Conversivel
    }

    public enum Cores
    {
        Amarelo,
        Azul,
        Verde,
        Vermelho,
        Rosa,
        Preto,
        Roxo
    }
}
