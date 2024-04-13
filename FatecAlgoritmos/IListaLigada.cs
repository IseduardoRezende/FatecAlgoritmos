namespace FatecAlgoritmos
{
    public interface IListaLigada<TValor>
    {
        void Listar();

        void Adicionar(TValor valor, TipoAdicao tipoAdicao = TipoAdicao.Fim);
        
        void Remover(TValor valor);

        void Limpar();

        int Tamanho { get; }
    }
}
