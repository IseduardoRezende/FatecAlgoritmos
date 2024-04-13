namespace FatecAlgoritmos
{
    public interface IListaLigada<TValor>
    {
        void Listar();

        void Adicionar(TValor valor, TipoAdicao tipoAdicao);

        void Adicionar(TValor valorExistente, TValor novoValor, TipoPosicao tipoPosicao);

        void Remover(TValor valor);

        void Limpar();

        int Tamanho { get; }
    }
}
