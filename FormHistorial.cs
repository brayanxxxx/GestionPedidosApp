//formulario de historial de pedidos con filtros
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
    public partial class FormHistorial : Form
    {
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.ComboBox cmbFiltro;
        private System.Windows.Forms.Button btnMostrarTodos;

        public FormHistorial()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CargarDatos();
        }

        private void InitializeComponent()
        {
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.cmbFiltro = new System.Windows.Forms.ComboBox();
            this.btnMostrarTodos = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.SuspendLayout();
           
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Name = "FormHistorial";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.ResumeLayout(false);
        }

        private void ConfigurarFormulario()
        {
            this.Text = "TechExpress - Historial de Pedidos";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(700, 400);

            dgvPedidos.Location = new Point(12, 60);
            dgvPedidos.Size = new Size(660, 280);
            dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPedidos.AllowUserToAddRows = false;
            dgvPedidos.AllowUserToDeleteRows = false;
            dgvPedidos.ReadOnly = true;
            dgvPedidos.RowHeadersVisible = false;
            dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPedidos.MultiSelect = false;

            dgvPedidos.Columns.Add("Cliente", "Cliente");
            dgvPedidos.Columns.Add("Producto", "Producto");
            dgvPedidos.Columns.Add("Urgente", "Urgente");
            dgvPedidos.Columns.Add("Peso", "Peso (kg)");
            dgvPedidos.Columns.Add("Distancia", "Distancia (km)");
            dgvPedidos.Columns.Add("MetodoEntrega", "Método de Entrega");
            dgvPedidos.Columns.Add("Costo", "Costo ($)");

            lblFiltro = new Label();
            lblFiltro.AutoSize = true;
            lblFiltro.Location = new Point(12, 20);
            lblFiltro.Text = "Filtrar por tipo de entrega:";

            cmbFiltro = new ComboBox();
            cmbFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltro.Location = new Point(160, 17);
            cmbFiltro.Size = new Size(150, 21);
            cmbFiltro.Items.Add("Todos");
            cmbFiltro.Items.Add("Dron");
            cmbFiltro.Items.Add("Motocicleta");
            cmbFiltro.Items.Add("Camión");
            cmbFiltro.Items.Add("Bicicleta");
            cmbFiltro.SelectedIndex = 0;
            cmbFiltro.SelectedIndexChanged += new EventHandler(cmbFiltro_SelectedIndexChanged);

            btnMostrarTodos = new Button();
            btnMostrarTodos.Text = "Mostrar Todos";
            btnMostrarTodos.Location = new Point(320, 17);
            btnMostrarTodos.Size = new Size(100, 23);
            btnMostrarTodos.Click += new EventHandler(btnMostrarTodos_Click);

            this.Controls.Add(dgvPedidos);
            this.Controls.Add(lblFiltro);
            this.Controls.Add(cmbFiltro);
            this.Controls.Add(btnMostrarTodos);
        }

        private void CargarDatos(string filtro = null)
        {
            dgvPedidos.Rows.Clear();

            var pedidos = RegistroPedidos.Instancia.Pedidos;

            foreach (var pedido in pedidos)
            {
                if (!string.IsNullOrEmpty(filtro) && filtro != "Todos" && pedido.MetodoEntrega.TipoEntrega() != filtro)
                    continue;

                dgvPedidos.Rows.Add(
                    pedido.Cliente,
                    pedido.Producto,
                    pedido.Urgente ? "Sí" : "No",
                    pedido.Peso.ToString("0.00"),
                    pedido.Distancia,
                    pedido.MetodoEntrega.TipoEntrega(),
                    pedido.ObtenerCosto().ToString("$0.00")
                );
            }
        }

        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtro = cmbFiltro.SelectedItem.ToString();
            CargarDatos(filtro);
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            cmbFiltro.SelectedIndex = 0; 
            CargarDatos();
        }
    }
}