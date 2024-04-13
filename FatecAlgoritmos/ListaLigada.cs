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

                return Anterior = anterior;
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

        public void Adicionar(TValor valor, TipoAdicao tipoAdicao = TipoAdicao.Fim)
        {
            if (!InicioValido())
            {
                InicializarLista(valor);
                return;
            }

            switch (tipoAdicao)
            {
                case TipoAdicao.Inicio: AdicionarInicio(valor); break;
                case TipoAdicao.Fim: AdicionarFim(valor); break;
                default: throw new ArgumentException("Enum inválido", nameof(tipoAdicao));
            }
        }

        public void Remover(TValor valor)
        {
            if (!InicioValido())
                return;

            while (Inicio is not null && !Equals(Inicio.Valor, valor))
                Inicio = Inicio.Proximo;

            if (UltimoNo(Inicio))
            {
                RemoverUltimoNo(Inicio);
                return;
            }

            if (PrimeiroNo(Inicio))
            {
                RemoverPrimeiroNo(Inicio);
                return;
            }

            RemoverNo(Inicio);
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

        private void RemoverUltimoNo(No? noRemocao)
        {
            if (noRemocao is null || !InicioValido())
                return;

            Fim = noRemocao.Anterior;
            Fim!.Proximo = null;

            DecrementarTamanhoLista();
            ReajustarOrdemPeloFim();
        }

        private void RemoverNo(No? noRemocao)
        {
            if (noRemocao is null || !InicioValido())
                return;

            Inicio = noRemocao.Anterior;
            Inicio!.Proximo = noRemocao.Proximo;

            DecrementarTamanhoLista();
            ReajustarOrdemPeloInicio();
        }

        private void RemoverPrimeiroNo(No? noRemocao)
        {
            if (noRemocao is null || !InicioValido())
                return;

            Inicio = noRemocao.Proximo;
            Inicio!.Anterior = null;

            DecrementarTamanhoLista();
            ReajustarOrdemPeloInicio();
        }

        private void ReajustarOrdemPeloInicio()
        {
            if (!InicioValido())
                return;

            while (Inicio!.Anterior is not null)            
                Inicio = Inicio.Anterior;            
        }

        private void ReajustarOrdemPeloFim()
        {
            if (!InicioValido())
                return;

            Inicio = Fim;

            while (Inicio!.Anterior is not null)            
                Inicio = Inicio.Anterior;            
        }

        private void AdicionarInicio(TValor valor)
        {
            var novoNo = new No(valor, proximo: Inicio, anterior: null);
            Inicio = novoNo;

            IncrementarTamanhoLista();
        }

        private void AdicionarFim(TValor valor)
        {
            var novoNo = new No(valor, proximo: null, anterior: Fim);

            Fim!.Proximo = novoNo;
            Fim = novoNo;

            IncrementarTamanhoLista();
        }

        private void InicializarLista(TValor valor)
        {
            var novoNo = new No(valor, proximo: null, anterior: null);

            Inicio = novoNo;
            Fim = novoNo;

            IncrementarTamanhoLista();
        }

        private bool InicioValido() => Inicio is not null;

        private void IncrementarTamanhoLista() => Tamanho++;

        private void DecrementarTamanhoLista() => Tamanho--;
    }
}
