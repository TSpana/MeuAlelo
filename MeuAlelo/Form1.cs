using MeuAlelo.Source.Alelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MeuAlelo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Cartoes = new aleloDadosCollection();
            label_media.Text = "";
            label_data.Text = "";
            label_recarga.Text ="";
            label_saldo.Text ="";

        }
        public aleloDadosCollection Cartoes { get; set; }
        private void Form1_Load(object sender, EventArgs e)
        {

            Cartoes.Read();

            foreach (var item in Cartoes)
            {
                cartãoToolStripMenuItem.DropDownItems.Add(item.Key).Click += Form1_Click; ;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            
            var t = (sender as ToolStripMenuItem).Text;

            Consultar(t);
        }
        private async void Consultar(string key)
        {
            try {
                var extrato = await Alelo.Current.LoadSaldo(Cartoes[key]);
                SetListView(extrato);

            }
            catch(AleloExpiratedException ex)
            {
                using (Relogin form = new Relogin(Cartoes[key]))
                {
                    if(form.ShowDialog()==DialogResult.OK)
                    {
                        Cartoes.Salvar();
                        Consultar(key);
                        
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormNovoCartao newcard = new FormNovoCartao())
                {
                    if (newcard.ShowDialog() == DialogResult.OK)
                    {
                        Cartoes[newcard.Cartao.CartaoNumero] = newcard.Cartao;
                        Cartoes.Salvar();
                        var tooltip = (sender as ToolStripMenuItem).GetCurrentParent().Items.Add(Cartoes[newcard.Cartao.CartaoNumero].CartaoNumero);
                        tooltip.Click += Form1_Click;
                        Consultar(newcard.Cartao.CartaoNumero);
                     
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetListView(AleloExtrato extrato)
        {
            listView1.Items.Clear();
            if(listView1.Columns.Count==0)
            {
                listView1.Columns.Add("Data", 100);
                listView1.Columns.Add("Descricao", 200);
                listView1.Columns.Add("Valor", 100);
            }

            label_media.Text = extrato.GastoMedio;
            label_data.Text = extrato.DataProximaRecarga;
            label_recarga.Text = extrato.ValorProximaRecarga;
            label_saldo.Text = extrato.Saldo;

            foreach (var item in extrato)
            {
                ListViewItem lvItem = new ListViewItem(item.Data);
                lvItem.SubItems.Add(item.Descricao);
                lvItem.SubItems.Add(item.Valor);

                listView1.Items.Add(lvItem);

            }
        }
    }
}
