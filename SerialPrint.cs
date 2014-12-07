using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using ThermalDotNet; //Don't forget to add the dll reference

namespace serialprint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Replace /dev/tty with the name of your serial port.
        // Can be COM1, COM2... on Windows, /dev/ttyUSB0 on Linux.
        
        private void button1_Click(object sender, EventArgs e)
        {
            int baud = (int)numericUpDown1.Value;
            SerialPort printerPort = new SerialPort(this.textBox1.Text, baud);
            printerPort.Open();

            // Connection to the printer
            ThermalPrinter printer = new ThermalPrinter(printerPort);

            printer.WakeUp();   // Let's wake up the printer
            printer.WriteLine(this.richTextBox1.Text);
            this.richTextBox1.Text = null;
            printer.LineFeed(); // Feed paper
            printer.Sleep();    // Save some energy
            printerPort.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SerialPort printerPort = new SerialPort(this.textBox1.Text, 57600);
            printerPort.Open();
            ThermalPrinter printer = new ThermalPrinter(printerPort);
            printer.WakeUp();
            string sEsc = "\x1b";
            string cut = sEsc+"i";
            printerPort.WriteLine(cut);
            printer.Sleep();
            printerPort.Close();
        }
    }
}
