
Console.WriteLine("Love this.");

var listaLigada = new ListaLigada();

listaLigada.Inserir(10, TipoAdicao.Inicio);
listaLigada.Inserir(11, TipoAdicao.Fim);
listaLigada.Inserir(9, TipoAdicao.Inicio);
listaLigada.Inserir(8, TipoAdicao.Fim);

listaLigada.Listar();

public class ListaLigada
{
    private protected class No
    {
        public short Valor { get; set; }

        public No? Proximo { get; set; }

        public No? Anterior { get; set; }

        public No(short valor, No? proximo, No? anterior)
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

    public short Tamanho { get; private set; }

    public void Listar()
    {
        Console.WriteLine($"Tamanho da Lista: {Tamanho}");

        while (Inicio != null)
        {
            Console.WriteLine($"Valor: {Inicio.Valor}");
            Inicio = Inicio.Proximo;
        }
    }

    public void Inserir(short valor, TipoAdicao tipoAdicao)
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
            default: MensagemErro(nameof(tipoAdicao), tipoAdicao.ToString()); break;
        }
    }

    private void AdicionarInicio(short valor)
    {
        var novoNo = new No(valor, Inicio, null);
        Inicio = novoNo;

        IncrementarTamanhoLista();
    }

    private void AdicionarFim(short valor)
    {
        var novoNo = new No(valor, null, Fim);

        Fim!.Proximo = novoNo;
        Fim = novoNo;

        IncrementarTamanhoLista();
    }

    private void InicializarLista(short valor)
    {
        var novoNo = new No(valor, null, null);

        Inicio = novoNo;
        Fim = novoNo;

        IncrementarTamanhoLista();
    }

    private bool InicioValido() => Inicio is not null;

    private void IncrementarTamanhoLista() => Tamanho++;

    private static void MensagemErro(string parametro, string valor) => Console.WriteLine($"Valor: '{valor}' em Parâmetro: '{parametro}' é inválido");
}

public enum TipoAdicao
{
    Inicio,
    Fim
}