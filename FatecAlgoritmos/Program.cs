using FatecAlgoritmos;

IListaLigada<Student> listaLigada2 = new ListaLigada<Student>();
listaLigada2.Adicionar(new Student("Edu", 18));
listaLigada2.Adicionar(new Student("Livia", 19));
listaLigada2.Adicionar(new Student("João", 17), TipoAdicao.Inicio);

var juca = new Student("Juca", 17);
listaLigada2.Adicionar(juca, TipoAdicao.Inicio);

listaLigada2.Listar();

listaLigada2.Remover(juca);

listaLigada2.Listar();

class Student
{
    public Student(string nome, int idade)
    {
        this.nome = nome;
        this.idade = idade;
    }

    public string nome;
    public int idade;

    public override string ToString()
    {
        return $"Nome: {nome} | Idade: {idade}";
    }
}