namespace HawesAndCurtisTest
{
    class TextPaymentFileWriter : IDisposable
    {
        public TextPaymentFileWriter(string filePath)
        {
            this.filePath = filePath;
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                }
                textfile = new StreamWriter(filePath);
            }
            catch (IOException ex)
            {
                Console.WriteLine("File path error");
                System.Environment.Exit(0);
            }
        }

        public void Write(int earliestOriginYear, int developmentYearsCount, List<Product> products)
        {
            Console.WriteLine("Writting output file: " + filePath);
            string str1 = earliestOriginYear + ", " + developmentYearsCount;
            try
            {
                textfile.WriteLine(str1);
                for (int i = 0; i < products.Count; i++)
                {
                    string outputString = products[i].GetProductName() + ", ";
                    for (int j = 0; j < products[i].GetTriangleAccumulatedValues().Count; j++)
                    {
                        outputString += products[i].GetTriangleAccumulatedValues()[j];
                        if (j != products[i].GetTriangleAccumulatedValues().Count - 1)
                        {
                            outputString += ", ";
                        }
                    }
                    textfile.WriteLine(outputString);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("File writing error");
                System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("File writing error");
                System.Environment.Exit(0);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // tell the GC not to finalize
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed) // only dispose once!
            {
                if (disposing)
                {
                    // Managed code dispose
                }
                // Unmanaged code dispose
                if (textfile != null)
                {
                    textfile.Close();
                    textfile.Dispose();
                }
            }
            this.isDisposed = true;
        }

        ~TextPaymentFileWriter()
        {
            Dispose(false);
        }

        private bool isDisposed = false;
        private string filePath;
        private TextWriter textfile;
    }
}
