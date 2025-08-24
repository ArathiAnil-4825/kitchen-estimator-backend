namespace KitchenEstimatorAPI.Models
{
    public class KitchenEstimatorDatabaseSettings
    {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public string DatabaseName { get; set; } = "KitchenEstimatorDB";

        public string ProjectsCollection { get; set; } = "Projects";
        public string MaterialsCollection { get; set; } = "Materials";
        public string ProductsCollection { get; set; } = "Products";
        public string LaborRatesCollection { get; set; } = "LaborRates";
        public string ProjectComponentsCollection { get; set; } = "ProjectComponents";
        public string ApprovalsCollection { get; set; } = "Approvals";
        public string EstimationRulesCollection { get; set; } = "EstimationRules";
    }
}
