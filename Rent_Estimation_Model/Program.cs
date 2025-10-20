namespace Rent_Estimation_Model {
    internal class Program {

        /* Create dataSet which is contains DataShell. Read data from given csv file path. */
        public static List<DataShell> createDataSets(string path, bool useAgeFeature) {

            var dataSet = new List<DataShell>();
            var lines = File.ReadAllLines(path);
            string[] parts;

            for (int i = 0; i < lines.Length; i++) {
                parts = lines[i].Split(',');

                dataSet.Add(
                    new DataShell(
                        int.Parse(parts[0]), // Room Count
                        double.Parse(parts[1]), // Distance to Center
                        int.Parse(parts[2]), // Building Age
                        int.Parse(parts[3]), // Rent
                        useAgeFeature // Whether to include Building Age as a feature
                    )
                );

            }

            return dataSet;

        }
            static void Main(string[] args) {
            
        }
    }
}
