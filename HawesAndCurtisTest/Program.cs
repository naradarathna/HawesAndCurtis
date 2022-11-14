namespace HawesAndCurtisTest
{
    struct InsuarancePolicyBlock
    {
        public int OriginYear;
        public int DevelopmentYear;
    }

    struct PaymentData
    {
        public string ProductName;
        public InsuarancePolicyBlock insuarancePolicyBlock;
        public double IncrementalValue;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Input File Path");
            var inputFilePath = @"" + Console.ReadLine();
            using (TextPaymentFileReader textPaymentFileReader = new TextPaymentFileReader(inputFilePath))
            {
                List<PaymentData> paymentDataSet = textPaymentFileReader.Read();
                PaymentDataAnalyser paymentDataAnalyser = new PaymentDataAnalyser(paymentDataSet);
                var outputFilePath = Path.GetDirectoryName(inputFilePath) + "\\output.txt";
                using (TextPaymentFileWriter textPaymentFileWriter = new TextPaymentFileWriter(outputFilePath))
                {
                    textPaymentFileWriter.Write(paymentDataAnalyser.GetEarliestOriginYear(), paymentDataAnalyser.GetDevelopmentYears().Count, paymentDataAnalyser.GetProducts());
                }
            }
        }
    }
}
