using System.Globalization;

namespace HawesAndCurtisTest
{
    class TextPaymentFileReader : IDisposable
    {
        public TextPaymentFileReader(string filePath)
        {
            this.filePath = filePath;
            try
            {
                textfile = new StreamReader(filePath);
            }
            catch (IOException ex)
            {
                Console.WriteLine("File path error");
                System.Environment.Exit(0);
            }
        }

        public List<PaymentData> Read()
        {
            List<PaymentData> paymentDataSet = new List<PaymentData>();
            try
            {
                bool isFileReadingError = false;
                string line;
                int lineCount = 0;
                while ((line = textfile.ReadLine()) != null)
                {
                    string[] words = line.Split(',');
                    if (words.Length == 4)
                    {
                        int originYear;
                        int developmentYear;
                        DateTime dateValue;
                        double incrementalValue;
                        bool isOriginYearNumeric = int.TryParse(words[1], out originYear) && DateTime.TryParseExact(Convert.ToString(originYear), "yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out dateValue);
                        bool isDevelopmentYearNumeric = int.TryParse(words[2], out developmentYear) && DateTime.TryParseExact(Convert.ToString(developmentYear), "yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out dateValue);
                        bool isIncrementalValueDouble = double.TryParse(words[3], out incrementalValue);
                        if (isOriginYearNumeric && isDevelopmentYearNumeric && isIncrementalValueDouble)
                        {
                            paymentDataSet.Add(new PaymentData
                            {
                                ProductName = words[0],
                                insuarancePolicyBlock = new InsuarancePolicyBlock { OriginYear = originYear, DevelopmentYear = developmentYear },
                                IncrementalValue = incrementalValue
                            });
                        }
                        else
                        {
                            if (words[0] != "Product")
                            {
                                isFileReadingError = true;
                                Console.WriteLine("Data set error in line : " + lineCount);
                            }
                        }
                    }
                    if (words.Length > 1 && words.Length < 4)
                    {
                        isFileReadingError = true;
                        Console.WriteLine("Data set error in line : " + lineCount);
                    }
                    lineCount++;
                }
                if (isFileReadingError)
                {
                    Console.WriteLine("File data set error");
                    System.Environment.Exit(0);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("File reading error");
                System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("File reading error");
                System.Environment.Exit(0);
            }
            return paymentDataSet;
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

        ~TextPaymentFileReader()
        {
            Dispose(false);
        }

        private bool isDisposed = false;
        private string filePath;
        private StreamReader textfile;
    }
}
