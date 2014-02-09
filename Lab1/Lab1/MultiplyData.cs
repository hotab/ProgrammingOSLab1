using System;
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
            UpdateState("Multiplying matrix by vector: 0%");
            for (int i = 0; i < M_SZ; i++)
            {
                vector_res[i] = 0;
                for (int j = 0; j < M_SZ; j++)
                    vector_res[i] += matrix[i, j] * vector[j];

                int percentage = (i * M_SZ) / M_SZ;
                UpdateState("Multiplying matrix by vector: " + (((double)percentage) / 100).ToString() + "%");
            }
            UpdateState("Multiplying matrix by vector completed.");
        }
        public void MultiplyVectorByMatrix()
        {
            UpdateState("Multiplying vector by matrix: 0%");
            for (int i = 0; i < M_SZ; i++)
            {
                vector_res[i] = 0;
            }
            for (int j = 0; j < M_SZ; j++)
            {
                for (int i = 0; i < M_SZ; i++)
                    vector_res[i] += matrix[j, i] * vector[j];

                int percentage = (j * M_SZ) / M_SZ;
                UpdateState("Multiplying vector by matrix: " + (((double)percentage) / 100).ToString() + "%");
            }
            UpdateState("Multiplying vector by matrix completed.");
        }

        public void GenerateMatrix()
        {

            UpdateState("Generating matrix: 0%");
            Random r = new Random();
            for (int i = 0; i < M_SZ; i++)
            {
                for (int j = 0; j < M_SZ; j++)
                    matrix[i, j] = r.Next(Max_Elem + 1);
                int percentage = (i * M_SZ) / M_SZ;
                UpdateState("Generating matrix: " + (((double)percentage) / 100).ToString() + "%");
            }
            UpdateState("Completed generating matrix");
        }
        public void GenerateVector()
        {
            UpdateState("Generating vector...");
            Random r = new Random();
            for (int i = 0; i < M_SZ; i++)
                vector[i] = r.Next(Max_Elem + 1);
            UpdateState("Completed generating vector");
        }

        public void UpdateState(string s)
        {
            try 
            { 
                owner.Invoke(textDelegate, s); 
            }
            catch 
            { }
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
