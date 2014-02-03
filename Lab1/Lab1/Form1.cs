using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Lab1
{
    public partial class Form1 : Form
    {
        Thread currentOp;

        DateTime startTime;

        delegate void SetButtonState(Button b, bool state);
        delegate void MsgBox(string text);

        SetButtonState btnDelegate;
        MsgBox msgDelegate;

        public void SetBtnState(Button b,bool state)
        {
            b.Enabled = state;
        }
        public void ShowMsgBox(string s)
        {
            MessageBox.Show(s);
        }

        public void DisableButtons()
        {
            this.Invoke(btnDelegate, button1, false);
            this.Invoke(btnDelegate, button2, false);
            this.Invoke(btnDelegate, button3, false);
        }
        public void EnableButtons()
        {
            this.Invoke(btnDelegate, button1, true);
            this.Invoke(btnDelegate, button2, true);
            this.Invoke(btnDelegate, button3, true);
        }
        public void ClearLabels()
        {
            labelState.Text = "";
            labelState2.Text = "";
        }


        public void GenerateAndMultiplyMbyV(object multData)
        {
            MultiplyData data = (MultiplyData)multData;
            data.GenerateMatrix();
            data.GenerateVector();
            data.MultiplyMatrixByVector();
        }
        public void GenerateAndMultiplyVbyM(object multData)
        {
            MultiplyData data = (MultiplyData)multData;
            data.GenerateMatrix();
            data.GenerateVector();
            data.MultiplyVectorByMatrix();
        }

        public void Button1Cycle(object multData)
        {
            startTime = DateTime.Now;
            DisableButtons();
            GenerateAndMultiplyMbyV(multData);
            EnableButtons();
            this.Invoke(msgDelegate, "Time elapsed: " + DateTime.Now.Subtract(startTime).TotalMilliseconds + " ms");
        }
        public void Button2Cycle(object multData)
        {
            startTime = DateTime.Now;
            DisableButtons();
            GenerateAndMultiplyVbyM(multData);
            EnableButtons();
            this.Invoke(msgDelegate, "Time elapsed: " + DateTime.Now.Subtract(startTime).TotalMilliseconds + " ms");
        }
        public void Button3Cycle()
        {
            MultiplyData data1 = new MultiplyData(this, labelState);
            MultiplyData data2 = new MultiplyData(this, labelState2);
            startTime = DateTime.Now;
            DisableButtons();

            Thread part1 = new Thread(GenerateAndMultiplyMbyV);
            part1.IsBackground = true;
            part1.Start(data1);

            Thread part2 = new Thread(GenerateAndMultiplyVbyM);
            part2.IsBackground = true;
            part2.Start(data2);

            part1.Join();
            part2.Join();

            EnableButtons();

            this.Invoke(msgDelegate, "Time elapsed: " + DateTime.Now.Subtract(startTime).TotalMilliseconds + " ms");
        }
        
        public Form1()
        {
            btnDelegate = SetBtnState;
            msgDelegate = ShowMsgBox;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearLabels();
            currentOp = new Thread(Button1Cycle);
            currentOp.IsBackground = true;
            currentOp.Start(new MultiplyData(this,labelState));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearLabels();
            currentOp = new Thread(Button2Cycle);
            currentOp.IsBackground = true;
            currentOp.Start(new MultiplyData(this, labelState));
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            ClearLabels();
            currentOp = new Thread(Button3Cycle);
            currentOp.IsBackground = true;
            currentOp.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lab1, Babkin Vladislav, PS-12-1");
        }
    }
}
