using FatecAlgoritmos;

IListaLigada<float> listaLigada = new ListaLigada<float>();

listaLigada.Adicionar(1, TipoAdicao.Inicio);
listaLigada.Adicionar(2, TipoAdicao.Fim);
listaLigada.Adicionar(3, TipoAdicao.Inicio);
listaLigada.Adicionar(4, TipoAdicao.Fim);

listaLigada.Listar();

listaLigada.Remover(1);

listaLigada.Listar();

listaLigada.Adicionar(4, 3.5f, TipoPosicao.Antes);
listaLigada.Adicionar(3, 1, TipoPosicao.Depois);
listaLigada.Adicionar(3, 1.5f, TipoPosicao.Antes);
listaLigada.Adicionar(7, TipoAdicao.Fim);
listaLigada.Adicionar(9, TipoAdicao.Inicio);
listaLigada.Adicionar(7, -2, TipoPosicao.Depois);

listaLigada.Listar();

listaLigada.Remover(-2);
listaLigada.Remover(9);
listaLigada.Remover(3);
listaLigada.Remover(4);

listaLigada.Listar();

listaLigada.Limpar();

listaLigada.Listar();
    