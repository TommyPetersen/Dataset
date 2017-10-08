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
using System.IO;

namespace Dataset
{
    public class MNIST
    {
        public List<MNISTImage> trainingImages { get; set; } = new List<MNISTImage>();
        public List<byte> trainingLabels { get; set; } = new List<byte>();

        public List<MNISTImage> testImages { get; set; } = new List<MNISTImage>();
        public List<byte> testLabels { get; set; } = new List<byte>();

        public void loadTrainingSetFiles(String imagesFileName, String labelsFileName)
        {
            trainingImages = loadImagesfile(imagesFileName,
                4 + 4 + 4 + 4 + (60000 * 28 * 28),
                "0, 0, 8, 3 | 0, 0, 234, 96 | 0, 0, 0, 28 | 0, 0, 0, 28");

            trainingLabels = LoadLabelsfile(labelsFileName,
                4 + 4 + 60000,
                "0, 0, 8, 1 | 0, 0, 234, 96");
        }

        public void loadTestSetFiles(String imagesFileName, String labelsFileName)
        {
            testImages = loadImagesfile(imagesFileName, 
                4 + 4 + 4 + 4 + (10000 * 28 * 28),
                "0, 0, 8, 3 | 0, 0, 39, 16 | 0, 0, 0, 28 | 0, 0, 0, 28");

            testLabels = LoadLabelsfile(labelsFileName,
                4 + 4 + 10000,
                "0, 0, 8, 1 | 0, 0, 39, 16");
        }

        private List<MNISTImage> loadImagesfile(String imagesFileName, int correctSize, String correctHeaderSignature)
        {
            if (imagesFileName == null)
            {
                throw new Exception("MNIST: imagesFileName is null.");
            }

            List<MNISTImage> images = new List<MNISTImage>();
            byte[] bytes = File.ReadAllBytes(imagesFileName);

            int totalSize = bytes.Length;

            if (totalSize != correctSize)
            {
                throw new Exception("MNIST: imagesFile has incorrect size." +
                    " Number of bytes is " + totalSize.ToString());
            }

            String headerSignature =
                bytes[0].ToString() + ", " +
                bytes[1].ToString() + ", " +
                bytes[2].ToString() + ", " +
                bytes[3].ToString() + " | " +
                bytes[4].ToString() + ", " +
                bytes[5].ToString() + ", " +
                bytes[6].ToString() + ", " +
                bytes[7].ToString() + " | " +
                bytes[8].ToString() + ", " +
                bytes[9].ToString() + ", " +
                bytes[10].ToString() + ", " +
                bytes[11].ToString() + " | " +
                bytes[12].ToString() + ", " +
                bytes[13].ToString() + ", " +
                bytes[14].ToString() + ", " +
                bytes[15].ToString();

            if (headerSignature != correctHeaderSignature)
            {
                throw new Exception("MNIST: imagesFile has incorrect header signature." +
                    " Header signature is " + headerSignature);
            }

            int headerSize = 4 + 4 + 4 + 4;
            int nrOfRows = 28;
            int rowSize = 28;
            int imageSize = nrOfRows * rowSize;
            int nrOfImages = (totalSize - headerSize) / imageSize;
            int i0 = headerSize;
            MNISTImage mnistImage = null;

            for (int k = 0; k < nrOfImages; k++)
            {
                mnistImage = new MNISTImage();

                for (int m = 0; m < nrOfRows; m++)
                {
                    for (int n = 0; n < rowSize; n++)
                    {
                        mnistImage[m, n] = bytes[i0 + (k * imageSize) + ((m * rowSize) + n)];
                    }
                }

                images.Add(mnistImage);
            }

            return images;
        }

        private List<byte> LoadLabelsfile(String labelsFileName, int correctSize, String correctHeaderSignature)
        {
            if (labelsFileName == null)
            {
                throw new Exception("MNIST: labelsFileName is null.");
            }

            List<byte> labels = new List<byte>();
            byte[] bytes = File.ReadAllBytes(labelsFileName);

            if (bytes.Length != correctSize)
            {
                throw new Exception("MNIST: labelsFile has incorrect size." +
                    " Number of bytes is " + bytes.Length.ToString());
            }

            int totalSize = bytes.Length;

            String headerSignature =
                bytes[0].ToString() + ", " +
                bytes[1].ToString() + ", " +
                bytes[2].ToString() + ", " +
                bytes[3].ToString() + " | " +
                bytes[4].ToString() + ", " +
                bytes[5].ToString() + ", " +
                bytes[6].ToString() + ", " +
                bytes[7].ToString();

            if (headerSignature != correctHeaderSignature)
            {
                throw new Exception("MNIST: labelsFile has incorrect header signature." +
                    " Header signature is " + headerSignature);
            }

            int headerSize = 4 + 4;
            int nrOfLabels = (totalSize - headerSize);
            int i0 = headerSize;

            for (int k = 0; k < nrOfLabels; k++)
            {
                labels.Add(bytes[i0 + k]);
            }

            return labels;
        }

        public MNIST()
        {
            ;
        }
    }
}
