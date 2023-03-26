using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Vista
{
    public partial class TicketsForm : Form
    {
        public TicketsForm()
        {
            InitializeComponent();
        }

        Cliente miCliente = null;
        ClienteDB clienteDB = new ClienteDB();
        Ticket miticket = null;

        List<DetalleTicket> listaDetalles = new List<DetalleTicket>();
        TicketDB ticketDB = new TicketDB();
        decimal isv = 0;
        decimal totalAPagar = 0;
        decimal descuento = 0;


        private void PrecioTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(PrecioTextBox.Text))
            {
                DetalleTicket detalle = new DetalleTicket();
                detalle.DescripcionSolicitud = DescripcionSolicitudTextBox.Text;
                detalle.DescripcionRespuesta = DescripcionRespuestaTextBox.Text;
                detalle.TipoSoporte = TipoSoporteComboBox.Text;
                detalle.Precio = Convert.ToDecimal(PrecioTextBox.Text);


                isv = Convert.ToDecimal(PrecioTextBox.Text) * 0.15M;
                descuento = (Convert.ToDecimal(PrecioTextBox.Text) + isv) * 0.10M;
                totalAPagar = Convert.ToDecimal(PrecioTextBox.Text) + isv - descuento;
                detalle.Total = Convert.ToDecimal(PrecioTextBox.Text) + isv - descuento;

                listaDetalles.Add(detalle);
                DetalleDataGridView.DataSource = null;
                DetalleDataGridView.DataSource = listaDetalles;


                ISVTextBox.Text = isv.ToString("N2");
                DescuentoTextBox.Text = descuento.ToString("N2");
                TotalTextBox.Text = totalAPagar.ToString("N2");


                miticket = null;
                PrecioTextBox.Clear();
                DescripcionSolicitudTextBox.Clear();
                DescripcionRespuestaTextBox.Clear();
                TipoSoporteComboBox.Text = null;
                PrecioTextBox.Focus();

            }

        }

        private void IdentidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(IdentidadTextBox.Text))
            {
                miCliente = new Cliente();
                miCliente = clienteDB.DevolverClientePorIdentidad(IdentidadTextBox.Text);
                NombreClienteTextBox.Text = miCliente.Nombre;
            }
            else
            {
                miCliente = null;
                NombreClienteTextBox.Clear();
            }
        }

        private void BuscarClienteButton_Click(object sender, System.EventArgs e)
        {
            BuscarClienteForm form = new BuscarClienteForm();
            form.ShowDialog();
            miCliente = new Cliente();
            miCliente = form.cliente;
            IdentidadTextBox.Text = miCliente.Identidad;
            NombreClienteTextBox.Text = miCliente.Nombre;
        }

        private void TicketsForm_Load(object sender, System.EventArgs e)
        {
            UsuarioTextBox.Text = System.Threading.Thread.CurrentPrincipal.Identity.Name;
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            Ticket miticket = new Ticket();
            miticket.Fecha = FechaDateTimePicker.Value;
            miticket.CodigoUsuario = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            miticket.IdentidadCliente = miCliente.Identidad;
            miticket.ISV = isv;
            miticket.Descuento = descuento;
            miticket.Total = totalAPagar;

            bool inserto = ticketDB.Guardar(miticket, listaDetalles);

            if (inserto)
            {
                LimpiarControles();
                IdentidadTextBox.Focus();
                MessageBox.Show("Ticket registrado exitosamente");
            }
            else
                MessageBox.Show("No se pudo registrar la Solicitud de Ticket");
        }

        private void LimpiarControles()
        {
            miCliente = null;
            listaDetalles = null;
            FechaDateTimePicker.Value = DateTime.Now;
            IdentidadTextBox.Clear();
            NombreClienteTextBox.Clear();
            DescripcionSolicitudTextBox.Clear();
            DescripcionRespuestaTextBox.Clear();
            PrecioTextBox.Clear();
            TipoSoporteComboBox = null;
            DetalleDataGridView.DataSource = null;
            isv = 0;
            ISVTextBox.Clear();
            descuento = 0;
            DescuentoTextBox.Clear();
            totalAPagar = 0;
            TotalTextBox.Clear();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

    }
}
