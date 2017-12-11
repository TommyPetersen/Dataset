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
    class testMNIST
    {
        static Random random;

        static String trainingImagesFileName =
            "C:\\Users\\Tommy\\Documents\\" + 
            "MachineLearning\\Dataset\\MNIST\\train-images.idx3-ubyte";
        static String trainingLabelsFileName =
            "C:\\Users\\Tommy\\Documents\\" +
            "MachineLearning\\Dataset\\MNIST\\train-labels.idx1-ubyte";
        static String testImagesFileName =
            "C:\\Users\\Tommy\\Documents\\" +
            "MachineLearning\\Dataset\\MNIST\\t10k-images.idx3-ubyte";
        static String testLabelsFileName =
            "C:\\Users\\Tommy\\Documents\\" +
            "MachineLearning\\Dataset\\MNIST\\t10k-labels.idx1-ubyte";

        static void Main(string[] args)
        {
            random = new Random();
            writeImage();
            //testClassMNIST();
            //testRandomImage();
        }

        static void writeImage()
        {
            MNIST mnist = new MNIST();

            mnist.loadTrainingSetFiles(trainingImagesFileName, trainingLabelsFileName);

            int i;

            while (Console.ReadLine().Equals(""))
            {
                i = random.Next(60000);
                Console.WriteLine("trainingImages[" + i + "]:");
                Console.WriteLine(mnist.trainingImages[i]);
                Console.WriteLine("trainingLabels[" + i + "]:");
                Console.WriteLine(mnist.trainingLabels[i]);

                Console.WriteLine("Enter non-empty line to quit!");
            }

        }

        static void testClassMNIST()
        {
            MNIST mnist = new MNIST();

            mnist.loadTrainingSetFiles(trainingImagesFileName, trainingLabelsFileName);
            mnist.loadTestSetFiles(testImagesFileName, testLabelsFileName);

            int i, j;

            while (Console.ReadLine().Equals(""))
            {
                i = random.Next(60000);
                Console.WriteLine("trainingImages[" + i + "]:");
                Console.WriteLine(mnist.trainingImages[i]);
                Console.WriteLine("trainingLabels[" + i + "]:");
                Console.WriteLine(mnist.trainingLabels[i]);

                j = random.Next(10000);
                Console.WriteLine("testImages[" + j + "]:");
                Console.WriteLine(mnist.testImages[j]);
                Console.WriteLine("testLabels[" + j + "]:");
                Console.WriteLine(mnist.testLabels[j]);

                Console.WriteLine("Enter non-empty line to quit!");
            }

            for (int k = 0; k < 60000; k++)
            {
                if (mnist.trainingLabels[k] < 0 || mnist.trainingLabels[k] > 9)
                {
                    throw new Exception("testMNIST: Invalid training label. " +
                        "trainingLabels[" + k + "]: " + mnist.trainingLabels[k]);
                }
            }

            Console.WriteLine("trainingLabels OK!");

            for (int k = 0; k < 10000; k++)
            {
                if (mnist.testLabels[k] < 0 || mnist.testLabels[k] > 9)
                {
                    throw new Exception("testMNIST: Invalid test label. " +
                        "testLabels[" + k + "]: " + mnist.testLabels[k]);
                }
            }

            Console.WriteLine("testLabels OK!");
        }

        static void testRandomImage()
        {
            MNISTImage image = new MNISTImage();

            byte[] bytes = new byte[28 * 28];

            random.NextBytes(bytes);

            for (int m = 0; m < 28; m++)
            {
                for (int n = 0; n < 28; n++)
                {
                    image[m, n] = bytes[28 * m + n];
                }
            }

            Console.WriteLine("MNIST image:");
            Console.WriteLine(image.ToString());
            Console.ReadLine();
        }
    }
}
