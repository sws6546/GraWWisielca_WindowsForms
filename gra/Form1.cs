namespace gra
{
    public partial class Form1 : Form
    {
        Dictionary<int, string> hasla = new Dictionary<int, string>();
        string haslo;
        List<int> zgadniete = new List<int>() { };
        Dictionary<int, string> obrazki = new Dictionary<int, string>()
        {
            {0, "0.jpg"},
            {1, "1.jpg"},
            {2, "2.jpg"},
            {3, "3.jpg"},
            {4, "4.jpg"},
            {5, "5.jpg"},
            {6, "6.jpg"},
            {7, "7.jpg"},
            {8, "8.jpg"},
            {9, "9.jpg"}
        };

        List<TextBox> textboxy = new List<TextBox>();
        int obrazek = 0;
        int proby = 0;
        string sprawdzoneLitery = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hasla.Add(1, "komputer");
            hasla.Add(2, "repozytorium");
            hasla.Add(3, "szybkiplik");
            hasla.Add(4, "siszarp");

            Random r = new Random();
            haslo = hasla[r.Next(1, 4)];
            MessageBox.Show(haslo); //  DO TESTOWANIA #######################################################

            int locationX = 148;
            for (int i = 0; i < haslo.Length; i++)
            {
                char c = ' ';
                foreach (int j in zgadniete)
                {
                    if (i == j)
                    {
                        c = haslo[i];
                    }
                }

                TextBox t = new TextBox();
                t.Text = c.ToString();
                t.Width = 50;
                t.Height = 50;
                t.Location = new Point(locationX, 365);
                locationX += t.Width + 10;
                Controls.Add(t);
                pictureBox1.Image = Image.FromFile(obrazki[obrazek]);
                textboxy.Add(t);
            }

            label2.Text = "Próby: " + proby;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ok = true;
            string e_message = "";

            if (textBox1.Text.Length < 1 || textBox1.Text.Length > 1)
            {
                ok = false;
                e_message = "Podaj jedn¹ literkê";
            }

            if (ok)
            {
                char literka = textBox1.Text.ToCharArray()[0];

                bool literaByla = false;
                foreach (char c in sprawdzoneLitery)
                {
                    if (c == literka)
                    {
                        literaByla = true;
                    }
                }
                if (!literaByla)
                {
                    proby++;
                    label2.Text = "Próby: " + proby;
                    sprawdzoneLitery += literka;
                }

                bool zgadnieto = false;
                for (int i = 0; i < haslo.Length; i++)
                {
                    if (haslo[i] == literka)
                    {
                        bool juzBylo = false;
                        foreach (int j in zgadniete)
                        {
                            if (j == i)
                            {
                                juzBylo = true;
                            }
                        }
                        if (!juzBylo)
                        {
                            zgadniete.Add(i);
                        }
                        zgadnieto = true;
                    }
                }

                for (int i = 0; i < zgadniete.Count; i++)
                {
                    textboxy[zgadniete[i]].Text = haslo[zgadniete[i]].ToString();
                }

                if (zgadnieto)
                {
                    if (zgadniete.Count == haslo.Length)
                    {
                        MessageBox.Show("WYGRANA!!! \nPróby:" + proby);
                        Application.Exit();
                    }
                }
                else
                {
                    try
                    {
                        obrazek++;
                        pictureBox1.Image = Image.FromFile(obrazki[obrazek]);
                    }
                    catch
                    {
                        MessageBox.Show("Przegrana. \nIloœæ prób: " + proby + ". \nHas³o to: " + haslo);
                        Application.Exit();
                    }

                }
            }
            else
            {
                MessageBox.Show(e_message);
            }
        }
    }
}