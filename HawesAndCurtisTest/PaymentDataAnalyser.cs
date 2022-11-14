namespace HawesAndCurtisTest
{
    class PaymentDataAnalyser
    {
        public PaymentDataAnalyser(List<PaymentData> paymentDataSet)
        {
            this.paymentDataSet = paymentDataSet;
            developmentYears = new List<int>();
            products = new List<Product>();
            run();
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public int GetEarliestOriginYear()
        {
            return earliestOriginYear;
        }

        public List<int> GetDevelopmentYears()
        {
            return developmentYears;
        }

        private void run()
        {
            extractEarliestOriginYear();
            extractDevelopmentYears();
            extractProducts();
        }

        private void extractEarliestOriginYear()
        {
            for (int i = 0; i < paymentDataSet.Count; i++)
            {
                if (i == 0)
                {
                    earliestOriginYear = paymentDataSet[i].insuarancePolicyBlock.OriginYear;
                }
                else
                {
                    if (earliestOriginYear > paymentDataSet[i].insuarancePolicyBlock.OriginYear)
                    {
                        earliestOriginYear = paymentDataSet[i].insuarancePolicyBlock.OriginYear;
                    }
                }
            }
        }

        private void extractDevelopmentYears()
        {
            for (int i = 0; i < paymentDataSet.Count; i++)
            {
                if (!developmentYears.Contains(paymentDataSet[i].insuarancePolicyBlock.DevelopmentYear))
                {
                    developmentYears.Add(paymentDataSet[i].insuarancePolicyBlock.DevelopmentYear);
                }
            }
            developmentYears.Sort();
            if (developmentYears.Count > 0)
            {
                int lowerBoundofDevelopmentYear = developmentYears[0];
                int upperBoundofDevelopmentYear = developmentYears[developmentYears.Count - 1];
                developmentYears.Clear();
                for (int i = lowerBoundofDevelopmentYear; i <= upperBoundofDevelopmentYear; i++)
                {
                    developmentYears.Add(i);
                }
            }
        }

        private void extractProducts()
        {
            for (int i = 0; i < paymentDataSet.Count; i++)
            {
                bool isContain = false;
                for (int j = 0; j < products.Count; j++)
                {
                    if (paymentDataSet[i].ProductName == products[j].GetProductName())
                    {
                        isContain = true;
                        products[j].AddPolicyBlock(new InsuarancePolicyBlock { DevelopmentYear = paymentDataSet[i].insuarancePolicyBlock.DevelopmentYear, OriginYear = paymentDataSet[i].insuarancePolicyBlock.OriginYear }, paymentDataSet[i].IncrementalValue);
                    }
                }
                if (!isContain)
                {
                    Product currentProduct = new Product(paymentDataSet[i].ProductName, developmentYears);
                    currentProduct.AddPolicyBlock(new InsuarancePolicyBlock { DevelopmentYear = paymentDataSet[i].insuarancePolicyBlock.DevelopmentYear, OriginYear = paymentDataSet[i].insuarancePolicyBlock.OriginYear }, paymentDataSet[i].IncrementalValue);
                    products.Add(currentProduct);
                }
            }
        }

        ~PaymentDataAnalyser()
        {
            if (paymentDataSet != null)
            {
                paymentDataSet.Clear();
            }
            if (developmentYears != null)
            {
                developmentYears.Clear();
            }
            if (products != null)
            {
                products.Clear();
            }
        }

        private int earliestOriginYear;
        private List<PaymentData> paymentDataSet;
        private List<int> developmentYears;
        private List<Product> products;
    }
}
