﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab1
{
    class MultiplyData
    {
        const int M_SZ = 10000;
        const int Max_Elem = 100;

        Form1 owner;
        Label state;

        delegate void UpdateStatusText(string s);
        UpdateStatusText textDelegate;
        public void UpdText(string s)
        {
            state.Text = "State: " + s;
        }

        int[,] matrix;
        int[] vector;
        int[] vector_res;

        public int[,] Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }
        public int[] Vector
        {
            get { return vector; }
            set { vector = value; }
        }
        public int[] VectorRes
        {
            get { return vector_res; }
            set { vector_res = value; }
        }

        public void MultiplyMatrixByVector()
        {
            try{owner.Invoke(textDelegate, "Multiplying matrix by vector: 0%");} catch{return;}
            for (int i = 0; i < M_SZ; i++)
            {
                vector_res[i] = 0;
                for (int j = 0; j < M_SZ; j++)
                    vector_res[i] += matrix[i, j] * vector[j];

                int percentage = (i * 10000) / M_SZ;
                try{owner.Invoke(textDelegate, "Multiplying matrix by vector: " + (((double)percentage) / 100).ToString() + "%");} catch{return;}
            }
            try { owner.Invoke(textDelegate, "Multiplying matrix by vector completed."); }
            catch { return; }
        }
        public void MultiplyVectorByMatrix()
        {
            try { owner.Invoke(textDelegate, "Multiplying vector by matrix: 0%");} catch{return;}
            for (int i = 0; i < M_SZ; i++)
            {
                vector_res[i] = 0;
                for (int j = 0; j < M_SZ; j++)
                    vector_res[i] += matrix[j, i] * vector[j];

                int percentage = (i * 10000) / M_SZ;
                try { owner.Invoke(textDelegate, "Multiplying vector by matrix: " + (((double)percentage) / 100).ToString() + "%");} catch{return;}
            }
            try { owner.Invoke(textDelegate, "Multiplying vector by matrix completed."); }
            catch { return; }
        }

        public void GenerateMatrix()
        {

            try { owner.Invoke(textDelegate, "Generating matrix: 0%"); } catch{return;}
            Random r = new Random();
            for (int i = 0; i < M_SZ; i++)
            {
                for (int j = 0; j < M_SZ; j++)
                    matrix[i, j] = r.Next(Max_Elem + 1);
                int percentage = (i * 10000) / M_SZ;
                 try { owner.Invoke(textDelegate, "Generating matrix: " + (((double)percentage) / 100).ToString() + "%"); } catch{return;}
            }
            try { owner.Invoke(textDelegate, "Completed generating matrix"); }
            catch { return; }
        }
        public void GenerateVector()
        {
            try { owner.Invoke(textDelegate, "Generating vector...");} catch{return;}
            Random r = new Random();
            for (int i = 0; i < M_SZ; i++)
                vector[i] = r.Next(Max_Elem + 1);
            try { owner.Invoke(textDelegate, "Completed generating vector"); }
            catch { return; }
        }

        public MultiplyData(Form1 owningForm, Label stateLabel)
        {
            owner = owningForm;
            state = stateLabel;
            textDelegate = UpdText;

            matrix = new int[M_SZ, M_SZ];
            vector = new int[M_SZ];
            vector_res = new int[M_SZ];

        }
    }
}