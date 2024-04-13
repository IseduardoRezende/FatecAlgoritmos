namespace FatecAlgoritmos
{
    public class ListaLigada<TValor> : IListaLigada<TValor> 
    {
        private protected class No
        {            
            private TValor _value = default!;

            public TValor Valor
            {
                get
                {
                    return _value;
                }
                set
                {
                    if (value is null)
                        throw new ArgumentNullException(nameof(Valor), "Valor nulo é inválido");

                    _value = value;
                }
            }

            public No? Proximo { get; set; }

            public No? Anterior { get; set; }

            public No(TValor valor, No? proximo, No? anterior)
            {
                Valor = valor;
                Anterior = AtualizaAnterior(anterior);
                Proximo = AtualizaProximo(proximo);
            }

            private No? AtualizaProximo(No? proximo)
            {
                if (proximo == null)
                    return null;

                Proximo = proximo;
                Proximo.Anterior = this;

                return Proximo;
            }

            private No? AtualizaAnterior(No? anterior)
            {
                if (anterior == null)
                    return null;

                Anterior = anterior;
                Anterior.Proximo = this;

                return Anterior;
            }
        }

        private No? Inicio { get; set; }

        private No? Fim { get; set; }

        public int Tamanho { get; private set; }

        public void Listar()
        {
            Console.WriteLine("\nListar Itens: ");
            Console.WriteLine($"Tamanho da Lista: {Tamanho}");

            var nos = Inicio;

            while (nos != null)
            {
                Console.WriteLine($"Valor: {nos.Valor}");
                nos = nos.Proximo;
            }
        }

        public void Limpar()
        {
            Inicio = null;
            Fim = null;
            Tamanho = default;
        }

        public void Adicionar(TValor valorExistente, TValor novoValor, TipoPosicao tipoPosicao)
        {
            if (!InicioValido())
                throw new ArgumentException("Valor inexistente", nameof(valorExistente));

            switch (tipoPosicao)
            {
                case TipoPosicao.Antes: AdicionarAntes(valorExistente, novoValor); break;
                case TipoPosicao.Depois: AdicionarDepois(valorExistente, novoValor); break;
                default: throw new ArgumentException("Enum inválido", nameof(tipoPosicao));
            }

            IncrementarTamanhoLista();
            AtualizarInicio();
        }

        private void AdicionarAntes(TValor valorExistente, TValor novoValor)
        {           
            var possuiValor = PossuiValor(valorExistente, out No? inicio);

            if (!possuiValor)
                throw new ArgumentException("Valor inexistente", nameof(valorExistente));

            var novoNo = new No(novoValor, proximo: inicio, anterior: inicio!.Anterior);
            Inicio = novoNo;
        }

        private void AdicionarDepois(TValor valorExistente, TValor novoValor)
        {            
            var possuiValor = PossuiValor(valorExistente, out No? inicio);

            if (!possuiValor)
                throw new ArgumentException("Valor inexistente", nameof(valorExistente));

            var novoNo = new No(novoValor, proximo: inicio!.Proximo, anterior: inicio);
            Inicio = novoNo;
            
            AtualizarFim();
        }

        public void Adicionar(TValor valor, TipoAdicao tipoAdicao)
        {
            if (!InicioValido())
            {
                InicializarLista(valor);
                IncrementarTamanhoLista();
                return;
            }

            switch (tipoAdicao)
            {
                case TipoAdicao.Inicio: AdicionarInicio(valor); break;
                case TipoAdicao.Fim: AdicionarFim(valor); break;
                default: throw new ArgumentException("Enum inválido", nameof(tipoAdicao));
            }

            IncrementarTamanhoLista();
        }

        private void InicializarLista(TValor valor)
        {
            var novoNo = new No(valor, proximo: null, anterior: null);

            Inicio = novoNo;
            Fim = novoNo;
        }

        private void AdicionarInicio(TValor valor)
        {
            var novoNo = new No(valor, proximo: Inicio, anterior: null);
            Inicio = novoNo;
        }

        private void AdicionarFim(TValor valor)
        {
            var novoNo = new No(valor, proximo: null, anterior: Fim);
            Fim = novoNo;
        }

        private bool PrimeiroNo(No? no)
        {
            if (no is null || !InicioValido())
                throw new ArgumentNullException(nameof(no), "Nó inexistente");

            return no.Anterior is null;
        }

        private bool UltimoNo(No? no)
        {
            if (no is null || !InicioValido())
                throw new ArgumentNullException(nameof(no), "Nó inexistente");

            return no.Proximo is null;
        }

        public void Remover(TValor valor)
        {
            var possuiValor = PossuiValor(valor, out No? inicio);

            if (!possuiValor)
                throw new ArgumentException("Valor inexistente", nameof(valor));

            RemoverNo(inicio);
            DecrementarTamanhoLista();
            AtualizarInicio();
        }

        private void RemoverNo(No? noRemocao)
        {
            if (noRemocao is null || !InicioValido())
                return;

            if (PrimeiroNo(noRemocao))
            {
                RemoverPrimeiroNo(noRemocao);
                return;
            }

            if (UltimoNo(noRemocao))
            {
                RemoverUltimoNo(noRemocao);
                return;
            }

            Inicio = noRemocao.Anterior;
            Inicio!.Proximo = noRemocao.Proximo;
            Inicio!.Proximo!.Anterior = Inicio;
        }

        private void RemoverPrimeiroNo(No? noRemocao)
        {
            if (noRemocao is null || !InicioValido() || !PrimeiroNo(noRemocao))
                return;

            Inicio = noRemocao.Proximo;
            Inicio!.Anterior = null;
        }

        private void RemoverUltimoNo(No? noRemocao)
        {
            if (noRemocao is null || !InicioValido() || !UltimoNo(noRemocao))
                return;

            Fim = noRemocao.Anterior;
            Fim!.Proximo = null;
            Inicio = Fim;
        }               

        private void AtualizarInicio()
        {
            if (!InicioValido())
                return;

            while (Inicio!.Anterior is not null)
                Inicio = Inicio.Anterior;
        }

        private void AtualizarFim()
        {
            if (!InicioValido())
                return;

            AtualizarInicio();

            var no = Inicio;

            while (no!.Proximo is not null)
                no = no.Proximo;

            Fim = no;
        }

        private bool PossuiValor(TValor valor, out No? inicio)
        {
            if (!InicioValido())
                throw new ArgumentException("Valor inexistente", nameof(valor));

            inicio = Inicio;
            var no = Inicio;

            while (no is not null && !Equals(no.Valor, valor))
                no = no.Proximo;

            if (no is null)
                return false;

            inicio = no;
            return true;
        }

        private bool InicioValido() => Inicio is not null;

        private void IncrementarTamanhoLista() => Tamanho++;

        private void DecrementarTamanhoLista() => Tamanho--;        
    }
}
