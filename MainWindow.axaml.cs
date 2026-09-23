using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Reto2RutaTesoro.DataStructures;
using Reto2RutaTesoro.Models;

namespace Reto2RutaTesoro
{
    public partial class MainWindow : Window
    {
        // Instancia única de la lista simplemente enlazada
        private readonly ListaSimple listaRuta;

        public MainWindow()
        {
            InitializeComponent();
            listaRuta = new ListaSimple();

            // Suscripción a eventos de los botones
            btnAgregar.Click += BtnAgregar_Click;
            btnBuscar.Click += BtnBuscar_Click;
            btnModificar.Click += BtnModificar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnLimpiar.Click += BtnLimpiar_Click;

            ActualizarTabla();
        }

        /// <summary>
        /// Recorre la lista enlazada desde el inicio (Cabeza) hasta NULL
        /// y actualiza la visualización del DataGrid.
        /// </summary>
        private void ActualizarTabla()
        {
            dgNodos.ItemsSource = null;
            dgNodos.ItemsSource = listaRuta.RecorrerParaVisualizacion();
        }

        private void MostrarMensaje(string mensaje, bool esError = false)
        {
            lblMensaje.Text = $"Estado: {mensaje}";
            lblMensaje.Foreground = esError 
                ? Brushes.OrangeRed 
                : Brushes.LightGreen;
        }

        private void BtnAgregar_Click(object? sender, RoutedEventArgs e)
        {
            int id = (int)(numId.Value ?? 1);
            string nombre = txtNombre.Text?.Trim() ?? string.Empty;
            string pista = txtPista.Text?.Trim() ?? string.Empty;
            int peligro = (int)(numPeligro.Value ?? 1);

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MostrarMensaje("Debe ingresar el nombre de la ubicación.", true);
                return;
            }

            if (listaRuta.Insertar(id, nombre, pista, peligro))
            {
                ActualizarTabla();
                MostrarMensaje($"Ubicación '{nombre}' (ID: {id}) agregada a la lista enlazada.");
                LimpiarFormulario(incrementarId: true);
            }
            else
            {
                MostrarMensaje($"El ID {id} ya existe en la ruta.", true);
            }
        }

        private void BtnBuscar_Click(object? sender, RoutedEventArgs e)
        {
            int id = (int)(numId.Value ?? 1);
            Nodo? nodo = listaRuta.Buscar(id);

            if (nodo != null)
            {
                txtNombre.Text = nodo.Nombre;
                txtPista.Text = nodo.Pista;
                numPeligro.Value = nodo.Peligro;
                MostrarMensaje($"Nodo encontrado: ID {id} -> {nodo.Nombre}");
            }
            else
            {
                MostrarMensaje($"No se encontró la ubicación con ID {id}.", true);
            }
        }

        private void BtnModificar_Click(object? sender, RoutedEventArgs e)
        {
            int id = (int)(numId.Value ?? 1);
            string nombre = txtNombre.Text?.Trim() ?? string.Empty;
            string pista = txtPista.Text?.Trim() ?? string.Empty;
            int peligro = (int)(numPeligro.Value ?? 1);

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MostrarMensaje("Debe ingresar un nombre válido.", true);
                return;
            }

            if (listaRuta.Modificar(id, nombre, pista, peligro))
            {
                ActualizarTabla();
                MostrarMensaje($"Ubicación con ID {id} modificada en la lista.");
            }
            else
            {
                MostrarMensaje($"No se encontró la ubicación con ID {id} para modificar.", true);
            }
        }

        private void BtnEliminar_Click(object? sender, RoutedEventArgs e)
        {
            int id = (int)(numId.Value ?? 1);

            if (listaRuta.Eliminar(id))
            {
                ActualizarTabla();
                MostrarMensaje($"Ubicación con ID {id} eliminada de la lista.");
                LimpiarFormulario();
            }
            else
            {
                MostrarMensaje($"No existe la ubicación con ID {id} para eliminar.", true);
            }
        }

        private void BtnLimpiar_Click(object? sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
            MostrarMensaje("Campos limpiados.");
        }

        private void LimpiarFormulario(bool incrementarId = false)
        {
            if (incrementarId)
            {
                numId.Value = (numId.Value ?? 1) + 1;
            }
            txtNombre.Text = string.Empty;
            txtPista.Text = string.Empty;
            numPeligro.Value = 1;
        }
    }
}