namespace Reto2RutaTesoro.Models
{
    /// <summary>
    /// Representa un nodo individual en la lista simplemente enlazada.
    /// Contiene los datos de la ubicación y la referencia al siguiente nodo.
    /// </summary>
    public class Nodo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Pista { get; set; }
        public int Peligro { get; set; }

        /// <summary>
        /// Puntero/Referencia al siguiente nodo de la lista.
        /// Es null si es el último elemento.
        /// </summary>
        public Nodo? Siguiente { get; set; }

        public Nodo(int id, string nombre, string pista, int peligro)
        {
            Id = id;
            Nombre = nombre;
            Pista = pista;
            Peligro = peligro;
            Siguiente = null;
        }
    }
}