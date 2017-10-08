/*
Copyright 2017 Tommy Petersen.

This file is part of "Dataset".

"Dataset" is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

"Dataset" is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with "Dataset".  If not, see <http://www.gnu.org/licenses/>. 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataset
{
    public class MNISTImage
    {
        private byte[,] image;
        public int M;
        public int N;

        public override String ToString()
        {
            String s = "";

            for (int m = 0; m < M; m++)
            {
                for (int n = 0; n < N; n++)
                {
                    if (image[m, n] > 200) //Foreground
                    {
                        s = s + "*";
                    }
                    else
                    {
                        if (image[m, n] < 55) //Background
                        {
                            s = s + " ";
                        }
                        else
                        {
                            s = s + "+"; //Between
                        }
                    }
                }
                s = s + "\n";
            }

            return s;
        }
        
        public byte this[int m, int n]
        {
            get
            {
                if (m < 0 || m >= M || n < 0 || n >= N)
                {
                    throw new Exception("MNISTImage: Invalid index arguments for get." +
                        " m = " + m.ToString() + ", n = " + n.ToString());
                }

                return image[m, n];
            }

            set
            {
                if (m < 0 || m >= M || n < 0 || n >= N)
                {
                    throw new Exception("MNISTImage: Invalid index arguments for set." +
                        " m = " + m.ToString() + ", n = " + n.ToString());
                }

                image[m, n] = value;
            }
        }

        public MNISTImage()
        {
            M = N = 28;
            image = new byte[M, N];
        }
    }
}
