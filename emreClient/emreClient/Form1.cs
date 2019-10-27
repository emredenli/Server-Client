using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP.Client
{
    public partial class Form1 : Form
    {
        
        public TcpClient _client;
        private NetworkStream _network;
        private StreamReader _reader;
        private StreamWriter _writer;

        public Form1()
        {
            InitializeComponent();
        }
    

        private void SendButton_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please, enter your message", "Warning");
                    textBox1.Focus();
                    return;
                }

                string serverMessage;
                _writer.WriteLine(textBox1.Text);
                _writer.Flush();

                serverMessage = _reader.ReadLine();
                MessageBox.Show(serverMessage, "Server Message");
            }

            catch
            {
                MessageBox.Show("Server Connection Error!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _client = new TcpClient("localhost", 9999);
                //"localhost"
            }
            catch
            {
                Console.WriteLine("Cannot connection!");
                return;
            }
            
            _network = _client.GetStream();
            _reader = new StreamReader(_network);
            _writer = new StreamWriter(_network);
        }

        
        public void form1_kapatma(object o, CancelEventArgs ec)
        {
            try
            {
                _writer.Close();
                _reader.Close();
                _network.Close();
            }

            catch
            {
                MessageBox.Show("Server cannot close correctly!");
            }
        }
    }
}
