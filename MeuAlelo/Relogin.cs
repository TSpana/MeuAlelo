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
    public partial class Relogin : Form
    {
        public Relogin(AleloDados cartao)
        {
            InitializeComponent();
            this.Cartao = cartao;
            textBox_cartao.Text = cartao.CartaoNumero;
            textBox_cartao.ReadOnly = true;
        }

        private AleloDados Cartao;
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.DialogResult = DialogResult.None;
               var cartao = await Alelo.Current.LoadCartao(textBox_cartao.Text, textBox_captcha.Text);
                Cartao.Ticket = cartao.Ticket;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                button1.DialogResult = DialogResult.None;
            }
        }
        

        private async void pictureBox_captcha_Click(object sender, EventArgs e)
        {
            pictureBox_captcha.Image = await Alelo.Current.NewCaptcha();
        }

        private async void Relogin_Load(object sender, EventArgs e)
        {
            pictureBox_captcha.Image = await Alelo.Current.NewCaptcha();
        }
    }
}
