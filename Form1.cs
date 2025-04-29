
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionPedidosApp;

namespace PedidosApp
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cmbProducto;
        private System.Windows.Forms.CheckBox chkUrgente;
        private System.Windows.Forms.Label lblPeso;
        private System.Windows.Forms.NumericUpDown nudPeso;
        private System.Windows.Forms.Label lblDistancia;
        private System.Windows.Forms.NumericUpDown nudDistancia;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.ListBox lstPedidos;
        private System.Windows.Forms.Label lblHistorial;
        private System.Windows.Forms.Button btnVerHistorial; 

        public Form1()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "TechExpress - Gestión de Pedidos";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(320, 550); 

            lblCliente = new Label();
            txtCliente = new TextBox();
            lblProducto = new Label();
            cmbProducto = new ComboBox();
            chkUrgente = new CheckBox();
            lblPeso = new Label();
            nudPeso = new NumericUpDown();
            lblDistancia = new Label();
            nudDistancia = new NumericUpDown();
            btnCalcular = new Button();
            lblResultado = new Label();
            lstPedidos = new ListBox();
            lblHistorial = new Label();
            btnVerHistorial = new Button(); 

            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(12, 15);
            lblCliente.Text = "Cliente:";

            txtCliente.Location = new Point(83, 12);
            txtCliente.Size = new Size(200, 20);

            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(12, 45);
            lblProducto.Text = "Producto:";

            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.Location = new Point(83, 42);
            cmbProducto.Size = new Size(200, 21);
            cmbProducto.Items.Add("tecnología");
            cmbProducto.Items.Add("accesorio");
            cmbProducto.Items.Add("componente");
            cmbProducto.SelectedIndex = 0;

            chkUrgente.AutoSize = true;
            chkUrgente.Location = new Point(83, 72);
            chkUrgente.Text = "Urgente";

            lblPeso.AutoSize = true;
            lblPeso.Location = new Point(12, 102);
            lblPeso.Text = "Peso (Kg):";

            ((System.ComponentModel.ISupportInitialize)nudPeso).BeginInit();
            nudPeso.DecimalPlaces = 2;
            nudPeso.Location = new Point(83, 100);
            nudPeso.Maximum = 1000;
            nudPeso.Value = 1;
            nudPeso.Size = new Size(200, 20);

            lblDistancia.AutoSize = true;
            lblDistancia.Location = new Point(12, 132);
            lblDistancia.Text = "Distancia (km):";

            ((System.ComponentModel.ISupportInitialize)nudDistancia).BeginInit();
            nudDistancia.Location = new Point(94, 130);
            nudDistancia.Maximum = 1000;
            nudDistancia.Minimum = 1;
            nudDistancia.Value = 1;
            nudDistancia.Size = new Size(189, 20);

            btnCalcular.Location = new Point(83, 160);
            btnCalcular.Size = new Size(200, 23);
            btnCalcular.Text = "Calcular Pedido";
            btnCalcular.Click += new EventHandler(btnCalcular_Click);

            lblResultado.BorderStyle = BorderStyle.FixedSingle;
            lblResultado.Location = new Point(15, 195);
            lblResultado.Size = new Size(268, 100);
            lblResultado.Text = "Resultado";

            lblHistorial.AutoSize = true;
            lblHistorial.Location = new Point(12, 304);
            lblHistorial.Text = "Historial de Pedidos:";

            lstPedidos.Location = new Point(15, 320);
            lstPedidos.Size = new Size(268, 121);

            btnVerHistorial.Location = new Point(83, 450);
            btnVerHistorial.Size = new Size(200, 23);
            btnVerHistorial.Text = "Ver Historial Completo";
            btnVerHistorial.Click += new EventHandler(btnVerHistorial_Click);

            this.Controls.Add(lblCliente);
            this.Controls.Add(txtCliente);
            this.Controls.Add(lblProducto);
            this.Controls.Add(cmbProducto);
            this.Controls.Add(chkUrgente);
            this.Controls.Add(lblPeso);
            this.Controls.Add(nudPeso);
            this.Controls.Add(lblDistancia);
            this.Controls.Add(nudDistancia);
            this.Controls.Add(btnCalcular);
            this.Controls.Add(lblResultado);
            this.Controls.Add(lblHistorial);
            this.Controls.Add(lstPedidos);
            this.Controls.Add(btnVerHistorial); 

            ((System.ComponentModel.ISupportInitialize)nudPeso).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDistancia).EndInit();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCliente.Text))
                {
                    MessageBox.Show("Por favor, ingrese el nombre del cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string cliente = txtCliente.Text;
                string producto = cmbProducto.SelectedItem.ToString();
                bool urgente = chkUrgente.Checked;
                double peso = Convert.ToDouble(nudPeso.Value);
                int distancia = Convert.ToInt32(nudDistancia.Value);

                Pedido pedido = new Pedido(cliente, producto, urgente, peso, distancia);
                RegistroPedidos.Instancia.AgregarPedido(pedido);

                lblResultado.Text = $"Cliente: {pedido.Cliente}\r\n" +
                                    $"Producto: {pedido.Producto}\r\n" +
                                    $"Método de Entrega: {pedido.MetodoEntrega.TipoEntrega()}\r\n" +
                                    $"Costo: ${pedido.ObtenerCosto():0.00}";

                ActualizarListaPedidos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarListaPedidos()
        {
            lstPedidos.Items.Clear();
            foreach (var pedido in RegistroPedidos.Instancia.Pedidos)
            {
                lstPedidos.Items.Add($"{pedido.Cliente} - {pedido.Producto} - {pedido.MetodoEntrega.TipoEntrega()} - ${pedido.ObtenerCosto():0.00}");
            }
        }

        private void btnVerHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                FormHistorial formHistorial = new FormHistorial();
                formHistorial.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el historial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}