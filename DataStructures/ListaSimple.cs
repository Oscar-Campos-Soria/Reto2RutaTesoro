using System;
using System.Collections.Generic;
using Reto2RutaTesoro.Models;

namespace Reto2RutaTesoro.DataStructures
{
    /// <summary>
    /// Estructura de datos: Lista Simplemente Enlazada programada desde cero.
    /// Cumple estrictamente con la restricción de no usar colecciones nativas (.NET) como almacenamiento interno.
    /// </summary>
    public class ListaSimple
    {
        private Nodo? cabeza; // Inicio de la lista
        private int contador; // Cantidad total de nodos

        public Nodo? Cabeza => cabeza;
        public int Cantidad => contador;

        public ListaSimple()
        {
            cabeza = null;
            contador = 0;
        }

        /// <summary>
        /// Inserta un nuevo nodo al final de la lista.
        /// Devuelve false si el ID ya existe.
        /// </summary>
        public bool Insertar(int id, string nombre, string pista, int peligro)
        {
            // Validar que el ID no esté duplicado
            if (Buscar(id) != null)
            {
                return false;
            }

            Nodo nuevo = new Nodo(id, nombre, pista, peligro);

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }

            contador++;
            return true;
        }

        /// <summary>
        /// Busca un nodo por su ID recorriendo secuencialmente la lista desde Inicio hasta NULL.
        /// </summary>
        public Nodo? Buscar(int id)
        {
            Nodo? actual = cabeza;
            while (actual != null)
            {
                if (actual.Id == id)
                {
                    return actual;
                }
                actual = actual.Siguiente;
            }
            return null;
        }

        /// <summary>
        /// Modifica la información de un nodo existente identificado por su ID.
        /// </summary>
        public bool Modificar(int id, string nuevoNombre, string nuevaPista, int nuevoPeligro)
        {
            Nodo? nodo = Buscar(id);
            if (nodo == null)
            {
                return false;
            }

            nodo.Nombre = nuevoNombre;
            nodo.Pista = nuevaPista;
            nodo.Peligro = nuevoPeligro;
            return true;
        }

        /// <summary>
        /// Elimina un nodo de la lista ajustando las referencias del nodo anterior.
        /// </summary>
        public bool Eliminar(int id)
        {
            if (cabeza == null)
            {
                return false;
            }

            // Caso 1: El nodo a eliminar es el primero (Cabeza)
            if (cabeza.Id == id)
            {
                cabeza = cabeza.Siguiente;
                contador--;
                return true;
            }

            // Caso 2: El nodo a eliminar está en medio o al final
            Nodo actual = cabeza;
            while (actual.Siguiente != null && actual.Siguiente.Id != id)
            {
                actual = actual.Siguiente;
            }

            if (actual.Siguiente != null)
            {
                // Reenlazamos ignorando el nodo a eliminar
                actual.Siguiente = actual.Siguiente.Siguiente;
                contador--;
                return true;
            }

            return false; // No se encontró
        }

        /// <summary>
        /// Recorre la lista desde la Cabeza hasta NULL y genera una proyección plana
        /// con la única función de alimentar el DataGrid para visualización gráfica.
        /// </summary>
        public List<NodoDTO> RecorrerParaVisualizacion()
        {
            var vista = new List<NodoDTO>();
            Nodo? actual = cabeza;

            while (actual != null)
            {
                vista.Add(new NodoDTO
                {
                    Id = actual.Id,
                    Nombre = actual.Nombre,
                    Pista = actual.Pista,
                    Peligro = actual.Peligro
                });
                actual = actual.Siguiente;
            }

            return vista;
        }
    }

    /// <summary>
    /// Objeto plano (DTO) utilizado exclusivamente para binding de lectura en el DataGrid de Avalonia.
    /// </summary>
    public class NodoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Pista { get; set; } = string.Empty;
        public int Peligro { get; set; }
    }
}