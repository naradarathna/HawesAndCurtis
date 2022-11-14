namespace HawesAndCurtisTest
{
    class Product
    {
        public Product(string productName, List<int> developmentYears)
        {
            this.productName = productName;
            this.developmentYears = developmentYears;
            insuarencePolicyBlocks = new Dictionary<InsuarancePolicyBlock, double>();
            triangleAccumulatedValues = new List<double>();
        }

        public string GetProductName()
        {
            return productName;
        }

        /// <summary> add policy blocks for product</summary>
        /// <param name="policyBlock">policy blocks data of origin date and development date</param>
        /// <param name="value">policy block value</param>
        /// <returns>void</returns>
        public void AddPolicyBlock(InsuarancePolicyBlock policyBlock, double value)
        {
            if (insuarencePolicyBlocks != null)
            {
                insuarencePolicyBlocks.Add(policyBlock, value);
            }
            extractTriangleAccumulatedValues();
        }

        public List<double> GetTriangleAccumulatedValues()
        {
            return triangleAccumulatedValues;
        }

        private void extractTriangleAccumulatedValues()
        {
            triangleAccumulatedValues.Clear();
            for (int i = 0; i < developmentYears.Count; i++)
            {
                double value = 0.0;
                for (int j = i; j < developmentYears.Count; j++)
                {
                    InsuarancePolicyBlock currentInsuarancePolicyBlock = new InsuarancePolicyBlock() { DevelopmentYear = developmentYears[j], OriginYear = developmentYears[i] };
                    if (insuarencePolicyBlocks.ContainsKey(currentInsuarancePolicyBlock))
                    {
                        value += insuarencePolicyBlocks[currentInsuarancePolicyBlock];
                    }
                    triangleAccumulatedValues.Add(value);
                }
            }
        }

        ~Product()
        {
            if (developmentYears != null)
            {
                developmentYears.Clear();
            }
            if (triangleAccumulatedValues != null)
            {
                triangleAccumulatedValues.Clear();
            }
            if (insuarencePolicyBlocks != null)
            {
                insuarencePolicyBlocks.Clear();
            }
        }

        private string productName;
        private List<int> developmentYears;
        private List<double> triangleAccumulatedValues;
        private Dictionary<InsuarancePolicyBlock, double> insuarencePolicyBlocks;
    }
}
