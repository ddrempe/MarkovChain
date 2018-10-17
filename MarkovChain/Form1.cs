using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace MarkovChain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PopuniComboBox();
        }

        private void UbaciTestnePodatke() {
            tbAProdajaPocetak.Text = "130";
            tbBProdajaPocetak.Text = "100";
            tbCProdajaPocetak.Text = "70";

            tbSmanjenjeAB.Text = "15";
            tbSmanjenjeAC.Text = "10";

            tbSmanjenjeBA.Text = "7";
            tbSmanjenjeBC.Text = "9";

            tbSmanjenjeCA.Text = "3";
            tbSmanjenjeCB.Text = "6";
        }

        private void PopuniComboBox() {
            for (int i = 1; i <= 6; i++)
            {
                cbOdabirMjeseca.Items.Add(i);
            }
            cbOdabirMjeseca.SelectedIndex = 0;
        }

        private void tbSmanjenjeBA_TextChanged(object sender, EventArgs e)
        {
            tbPovecanjeAB.Text = tbSmanjenjeBA.Text;
        }

        private void tbSmanjenjeCA_TextChanged(object sender, EventArgs e)
        {
            tbPovecanjeAC.Text = tbSmanjenjeCA.Text;
        }

        private void tbSmanjenjeAB_TextChanged(object sender, EventArgs e)
        {
            tbPovecanjeBA.Text = tbSmanjenjeAB.Text;
        }

        private void tbSmanjenjeCB_TextChanged(object sender, EventArgs e)
        {
            tbPovecanjeBC.Text = tbSmanjenjeCB.Text;
        }

        private void tbSmanjenjeAC_TextChanged(object sender, EventArgs e)
        {
            tbPovecanjeCA.Text = tbSmanjenjeAC.Text;
        }

        private void tbSmanjenjeBC_TextChanged(object sender, EventArgs e)
        {
            tbPovecanjeCB.Text = tbSmanjenjeBC.Text;
        }

        private void btnIzracunajLojalne_Click(object sender, EventArgs e)
        {
            int rezultat;

            rezultat = int.Parse(tbAProdajaPocetak.Text) - int.Parse(tbSmanjenjeAB.Text) - int.Parse(tbSmanjenjeAC.Text);
            tbLojalniA.Text = rezultat.ToString();

            rezultat = int.Parse(tbBProdajaPocetak.Text) - int.Parse(tbSmanjenjeBA.Text) - int.Parse(tbSmanjenjeBC.Text);
            tbLojalniB.Text = rezultat.ToString();

            rezultat = int.Parse(tbCProdajaPocetak.Text) - int.Parse(tbSmanjenjeCA.Text) - int.Parse(tbSmanjenjeCB.Text);
            tbLojalniC.Text = rezultat.ToString();

            rezultat = int.Parse(tbLojalniA.Text) + int.Parse(tbPovecanjeAB.Text) + int.Parse(tbPovecanjeAC.Text);
            tbAProdajaKraj.Text = rezultat.ToString();

            rezultat = int.Parse(tbLojalniB.Text) + int.Parse(tbPovecanjeBA.Text) + int.Parse(tbPovecanjeBC.Text);
            tbBProdajaKraj.Text = rezultat.ToString();

            rezultat = int.Parse(tbLojalniC.Text) + int.Parse(tbPovecanjeCA.Text) + int.Parse(tbPovecanjeCB.Text);
            tbCProdajaKraj.Text = rezultat.ToString();
        }

        private void btnIzracunajUdjele_Click(object sender, EventArgs e)
        {

            double p11 = double.Parse(tbLojalniA.Text) / int.Parse(tbAProdajaPocetak.Text);
            double p21 = double.Parse(tbSmanjenjeAB.Text) / int.Parse(tbAProdajaPocetak.Text);
            double p31 = double.Parse(tbSmanjenjeAC.Text) / int.Parse(tbAProdajaPocetak.Text);

            double p12 = double.Parse(tbSmanjenjeBA.Text) / int.Parse(tbBProdajaPocetak.Text);
            double p22 = double.Parse(tbLojalniB.Text) / int.Parse(tbBProdajaPocetak.Text);
            double p32 = double.Parse(tbSmanjenjeBC.Text) / int.Parse(tbBProdajaPocetak.Text);

            double p13 = double.Parse(tbSmanjenjeCA.Text) / int.Parse(tbCProdajaPocetak.Text);
            double p23 = double.Parse(tbSmanjenjeCB.Text) / int.Parse(tbCProdajaPocetak.Text);
            double p33 = double.Parse(tbLojalniC.Text) / int.Parse(tbCProdajaPocetak.Text);
            Matrix<double> P = DenseMatrix.OfArray(new double[,] {
                {p11,p12,p13},
                {p21,p22,p23},
                {p31,p32,p33}});

            tbMatricaP.Text = P.ToMatrixString();

            int suma = int.Parse(tbAProdajaKraj.Text) + int.Parse(tbBProdajaKraj.Text) + int.Parse(tbCProdajaKraj.Text);
            double a11 = double.Parse(tbAProdajaKraj.Text) / suma;
            double a21 = double.Parse(tbBProdajaKraj.Text) / suma;
            double a31 = double.Parse(tbCProdajaKraj.Text) / suma;
            Matrix<double> An = DenseMatrix.OfArray(new double[,] {
                {a11},
                {a21},
                {a31}});

            int brojMjeseci = int.Parse(cbOdabirMjeseca.SelectedItem.ToString());
            tbOcekivaniUdjeli.Text = "";
            Matrix<double> rezMatrica;
            for (int i = 0; i < brojMjeseci; i++)
            {
                tbOcekivaniUdjeli.Text += An.ToMatrixString() + System.Environment.NewLine;
                rezMatrica = P * An;
                An = rezMatrica;
            }
        }

        private void btnTestniPodaci_Click(object sender, EventArgs e)
        {
            UbaciTestnePodatke();
        }

        private void btnItracunajStabilno_Click(object sender, EventArgs e)
        {
            double p11 = double.Parse(tbLojalniA.Text) / int.Parse(tbAProdajaPocetak.Text);
            double p21 = double.Parse(tbSmanjenjeAB.Text) / int.Parse(tbAProdajaPocetak.Text);
            double p31 = double.Parse(tbSmanjenjeAC.Text) / int.Parse(tbAProdajaPocetak.Text);

            double p12 = double.Parse(tbSmanjenjeBA.Text) / int.Parse(tbBProdajaPocetak.Text);
            double p22 = double.Parse(tbLojalniB.Text) / int.Parse(tbBProdajaPocetak.Text);
            double p32 = double.Parse(tbSmanjenjeBC.Text) / int.Parse(tbBProdajaPocetak.Text);

            double p13 = double.Parse(tbSmanjenjeCA.Text) / int.Parse(tbCProdajaPocetak.Text);
            double p23 = double.Parse(tbSmanjenjeCB.Text) / int.Parse(tbCProdajaPocetak.Text);
            double p33 = double.Parse(tbLojalniC.Text) / int.Parse(tbCProdajaPocetak.Text);
            Matrix<double> P = DenseMatrix.OfArray(new double[,] {
                {p11,p12,p13},
                {p21,p22,p23},
                {p31,p32,p33}});

            Matrix<double> A = DenseMatrix.OfArray(new double[,] {
                {p11-1,p12,p13},
                {p21,p22-1,p23},
                {1,1,1}});

            Matrix<double> b = DenseMatrix.OfArray(new double[,] {
                {0},
                {0},
                {1}});

            Matrix<double> x = A.Solve(b);

            tbStabilnoStanje.Text = "A= " + x[0, 0] + System.Environment.NewLine +
                                    "B= " + x[1, 0] + System.Environment.NewLine +
                                    "C= " + x[2, 0];
        }
    }
}
