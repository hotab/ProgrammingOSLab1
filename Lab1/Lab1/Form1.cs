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
        public const bool enableOutput = false;
        public const bool useRandom = false;
        public const bool multiplyVectorByMatrixColByCol = false;

        delegate void SetButtonState(Button b, bool state);
        SetButtonState btnDelegate;
        public void SetBtnState(Button b,bool state)
        {
            b.Enabled = state;
        }

        delegate void MsgBox(string text);
        MsgBox msgDelegate;
        public void ShowMsgBox(string s)
        {
            MessageBox.Show(s);
        }

        public void DisableButtons()
        {
            if (enableOutput)
            {
                this.Invoke(btnDelegate, button1, false);
                this.Invoke(btnDelegate, button2, false);
                this.Invoke(btnDelegate, button3, false);
            }
             
        }
        public void EnableButtons()
        {
            if (enableOutput)
            {
                this.Invoke(btnDelegate, button1, true);
                this.Invoke(btnDelegate, button2, true);
                this.Invoke(btnDelegate, button3, true);
            }
        }
        public void ClearLabels()
        {
            labelState.Text = "";
            labelState2.Text = "";
        }

        public void GenerateAndMultiplyMbyV(object multData)
        {
            MultiplyData data = (MultiplyData)multData;
            data.GenerateMatrix(useRandom);
            data.GenerateVector(useRandom);
            data.MultiplyMatrixByVector();
        }
        public void GenerateAndMultiplyVbyM(object multData)
        {
            MultiplyData data = (MultiplyData)multData;
            data.GenerateMatrix(useRandom);
            data.GenerateVector(useRandom);
            data.MultiplyVectorByMatrix();
        }

        public void Button1Cycle(object multData)
        {
            DateTime startTime = DateTime.Now;
            DisableButtons();
            GenerateAndMultiplyMbyV(multData);
            EnableButtons();
            this.Invoke(msgDelegate, "Time elapsed: " + DateTime.Now.Subtract(startTime).TotalMilliseconds + " ms");
        }
        public void Button2Cycle(object multData)
        {
            DateTime startTime = DateTime.Now;
            DisableButtons();
            GenerateAndMultiplyVbyM(multData);
            EnableButtons();
            this.Invoke(msgDelegate, "Time elapsed: " + DateTime.Now.Subtract(startTime).TotalMilliseconds + " ms");
        }
        public void Button3Cycle()
        {
            MultiplyData data1 = new MultiplyData(this, labelState);
            MultiplyData data2 = new MultiplyData(this, labelState2);
            DateTime startTime = DateTime.Now;
            DisableButtons();

            Thread part1 = new Thread(GenerateAndMultiplyMbyV) { IsBackground = true };
            part1.Start(data1);

            Thread part2 = new Thread(GenerateAndMultiplyVbyM) { IsBackground = true };
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
            Thread currentOp = new Thread(Button1Cycle) { IsBackground = true };
            currentOp.Start(new MultiplyData(this,labelState));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ClearLabels();
            Thread currentOp = new Thread(Button2Cycle) { IsBackground = true };
            currentOp.Start(new MultiplyData(this, labelState));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ClearLabels();
            Thread currentOp = new Thread(Button3Cycle) { IsBackground = true };
            currentOp.Start();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lab1, Babkin Vladislav, PS-12-1");
        }
    }
}
