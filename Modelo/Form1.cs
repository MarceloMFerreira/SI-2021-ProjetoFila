using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Modelo
{
    public partial class Form1 : Form
    {
        Queue<Pessoa> filaNormal = new Queue<Pessoa>();
        Queue<Pessoa> filaPref = new Queue<Pessoa>();
        int cont = 0;
        public Form1()
        {
            
            InitializeComponent();
            carregar();
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void BtnSobre_Click(object sender, EventArgs e)
        {

        }

        void salvar()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open("fila1.txt", FileMode.Create)))
            {
                writer.Write(filaNormal.Count());
                foreach (Pessoa p in filaNormal)
                {
                    writer.Write(p.Nome);
                    writer.Write(p.Rg);
                    writer.Write(p.Idade);
                }
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open("fila2.txt", FileMode.Create)))
            {
                writer.Write(filaPref.Count());
                foreach (Pessoa p in filaPref)
                {
                    writer.Write(p.Nome);
                    writer.Write(p.Rg);
                    writer.Write(p.Idade);
                }
            }
        }

        void carregar()
        {
            if (File.Exists("fila1.txt"))
            {
                using (BinaryReader reader = new BinaryReader(File.Open("fila1.txt", FileMode.Open)))
                {
                    int qtd = reader.ReadInt32();
                    for (int i = 0; i < qtd; i++)
                    {
                        Pessoa p = new Pessoa();
                        p.Nome = reader.ReadString();
                        p.Rg = reader.ReadInt32();
                        p.Idade = reader.ReadInt32();
                        filaNormal.Enqueue(p);
                    }// fim for
                }

            }// Fila normal

            if (File.Exists("fila2.txt"))
            {
                using (BinaryReader reader = new BinaryReader(File.Open("fila2.txt", FileMode.Open)))
                {
                    int qtd = reader.ReadInt32();
                    for (int i = 0; i < qtd; i++)
                    {
                        Pessoa p = new Pessoa();
                        p.Nome = reader.ReadString();
                        p.Rg = reader.ReadInt32();
                        p.Idade = reader.ReadInt32();
                        filaPref.Enqueue(p);
                    }// fim for
                }

            }// Fila normal

            mostra();
        }

        void mostra()
        {
            listNormal.Items.Clear();
            foreach(Pessoa s in filaNormal)
            {
               listNormal.Items.Add(s.Nome + " - Idade:" +  s.Idade);
            }

           listPref.Items.Clear();
            foreach (Pessoa s in filaPref)
            {
               listPref.Items.Add(s.Nome + " - Idade:" + s.Idade);
            }
        }
        //---------------
        void limpa()
        {
            txtNome.Clear();
            txtIdade.Clear();
            txtRG.Clear();
            txtNome.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Pessoa p = new Pessoa();
            p.Nome = txtNome.Text;
            p.Idade = Convert.ToInt32(txtIdade.Text);
            p.Rg = Convert.ToInt32(txtRG.Text);

            if(p.Idade < 60)
                filaNormal.Enqueue(p);
            else
                filaPref.Enqueue(p);

            mostra();
            limpa();


        }


        private void btnRemove_Click(object sender, EventArgs e)
        {
            Pessoa p = new Pessoa();
            if (filaPref.Count > 0 && filaNormal.Count > 0)
            {
                if (cont < 3)
                {
                    p = filaPref.Peek();
                    lblProx.Text = p.Nome;
                    filaPref.Dequeue();
                    cont = cont+1;
                   

                }
                else
                {
                    p = filaNormal.Peek();
                    lblProx.Text = p.Nome;
                    filaNormal.Dequeue();
                    cont = 0;
                }
            }else if(filaPref.Count == 0 && filaNormal.Count > 0)
            {
                p = filaNormal.Peek();
                lblProx.Text = p.Nome;
                filaNormal.Dequeue();
                cont = 0;

            }else if(filaPref.Count > 0 && filaNormal.Count==0){
                p = filaPref.Peek();
                lblProx.Text = p.Nome;
                filaPref.Dequeue();

            }else if(filaPref.Count==0 && filaNormal.Count == 0)
            {
                MessageBox.Show("Todos foram atendidos!");
            }
            

            mostra();
           


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            salvar();
        }

        private void lblProx_Click(object sender, EventArgs e)
        {

        }
    }
}
