using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACTIVIDAD6_6
{
    public partial class Form1 : Form
    {
        static int contador;
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblnumero.Text = generarnumero();
        }
        //Implementacion de expresion lambda
        Func<string> generarnumero = () =>
        {
            contador++;
            return contador.ToString("0000");
        };
        //Metodo que capturan los valores
        int getNumero()
        {
            return int.Parse(lblnumero.Text);
        }
        string getTitulo()
        {
            return txtTitulo.Text;
        }
        double getCosto()
        {
            return double.Parse(txtCosto.Text);
        }
        string getCategoria()
        {
            return cboCategoria.Text;
        }
        Func<string, double, double> asignaDescuento = (categoria, costo) =>
        {
            double descuento = 0;
            switch (categoria)
            {
                case "Gestion": descuento = 10.0 / 100 * costo; break;
                case "Ingenieria": descuento = 12.0 / 100 * costo; break;
                case "Programacion": descuento = 20.0 / 100 * costo; break;
                case "Base de datos": descuento = 15.0 / 100 * costo; break;
            }
            return descuento;
        };
        Func<double, double, double> calculaPrecioVenta = (costo, descuento) => costo - descuento;
        //Calculando las estadisticas
        //Monto total de descuentos
        double calculaTotalDescuentos()
        {
            double total = 0;
            for(int i=0; i < lvLibro.Items.Count; i++)
            {
                total += double.Parse(lvLibro.Items[i].SubItems[4].Text);
            }
            return total;
        }
        //Libro con el precio mas alto
        string libroMasAlto()
        {
            double mayor = double.Parse(lvLibro.Items[0].SubItems[5].Text);
            int posicion = 0;
            for(int i = 0; i < lvLibro.Items.Count; i++)
            {
                if(double.Parse(lvLibro.Items[i].SubItems[5].Text)> mayor)
                {
                    posicion = i;
                }
            }
            return lvLibro.Items[posicion].SubItems[1].Text;
        }
        //Imprimir el registro de ventas
        void imprimirRegistro(double descuento,double precioVenta)
        {
            ListViewItem fila = new ListViewItem(getNumero().ToString());
            fila.SubItems.Add(getTitulo());
            fila.SubItems.Add(getCategoria());
            fila.SubItems.Add(getCosto().ToString("0.00"));
            fila.SubItems.Add(descuento.ToString("0.00"));
            fila.SubItems.Add(precioVenta.ToString("0.00"));
            lvLibro.Items.Add(fila);
        }
        //Imprimir estadisticas
        void imprimirEstadisticas(double totalDescuentos,string LibroAlto)
        {
            lvEstadistica.Items.Clear();
            string[] elementosFila = new string[2];
            ListViewItem row;

            elementosFila[0] = "Monto total acumulado de descuentos";
            elementosFila[1] = totalDescuentos.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadistica.Items.Add(row);

            elementosFila[0] = "El libro con el precio de venta mas caro";
            elementosFila[1] = LibroAlto;
            row = new ListViewItem(elementosFila);
            lvEstadistica.Items.Add(row);

        }
        //Metodo para la validacion de datos
        string valida()
        {
            if (txtTitulo.Text.Trim().Length == 0)
            {
                txtTitulo.Focus();
                return "Titulo del libro";
            }else if (cboCategoria.SelectedIndex == -1)
            {
                cboCategoria.Focus();
                return "Categoria del libro";
            }else if (txtCosto.Text.Trim().Length == 0)
            {
                txtCosto.Focus();
                return "Costo del libro";
            }
            return "";
        }

        private void btnregistrar_Click(object sender, EventArgs e)
        {
            if (valida() == "")
            {
                //Capturando los datos de los formularios
                double costo = getCosto();
                string categoria = getCategoria();
                double descuento = asignaDescuento(categoria, costo);
                double precioVenta = calculaPrecioVenta(costo, descuento);
                //Enviando a la impresion 
                imprimirRegistro(descuento, precioVenta);

                lblnumero.Text = generarnumero();
            }
            else
                MessageBox.Show("El error se encuentra en " + valida());
        }

        private void btnestadisticas_Click(object sender, EventArgs e)
        {
            double totalDescuentos = calculaTotalDescuentos();
            string libroalto = libroMasAlto();

            imprimirEstadisticas(totalDescuentos, libroalto);
        }
    }
}
