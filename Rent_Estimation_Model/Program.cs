namespace Rent_Estimation_Model {
    internal class Program {

        /* Create dataSet which is contains DataShell. Read data from given csv file path. */
        public static List<DataShell> createDataSet(string path, bool useAgeFeature) {

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

        public static void forEpoch(Neuron neuron, List<DataShell> data, int epoch) {

            for (int i = 0; i < epoch; i++) { // Train the neuron for given epoch
                foreach (var dataShell in data) { neuron.Train(dataShell.inputs, dataShell.target); }
            }

        }

        static double MSE(Neuron neuron, List<DataShell> data, bool printDetails = false) {
            double sumOfSquaredErrors = 0.0;

            if (printDetails) {
                Console.WriteLine("\n" +
                    "| ----------------- Prediction Results ---------------- |\n" +
                    "| Instance | Target (t) | Predicted (y) | (t-y)^2       |\n" +
                    "|----------|------------|---------------|---------------|"
                    );
            }

            int index = 1;
            foreach (var point in data) {
                double y = neuron.calculateY(point.inputs);
                double error = point.target - y;
                double squaredError = Math.Pow(error, 2);
                sumOfSquaredErrors += squaredError;

                if (printDetails) { Console.WriteLine($"| {index,8} |     {point.target:F4} |        {y:F4} |    {squaredError:F8} |"); }
                index++;
            }
            return sumOfSquaredErrors / data.Count;
        }

        static void Main(string[] args) {
            List<DataShell> trainingSet = null;
            List<DataShell> testingSet = null;

            try { trainingSet = createDataSet("trainingData.csv", true); } catch (Exception e) { Console.WriteLine("Training set error: " + e.Message); }
            try { testingSet = createDataSet("testData.csv", true); } catch (Exception e) { Console.WriteLine("Test set error: " + e.Message); }

            int[] epochs = { 25, 100 };
            double[] lambdas = {0.01, 0.5, 0.1 };

            double lambda = lambdas[1];
            int epoch = epochs[0];

            Neuron neuron = new Neuron(3, lambda);

            Console.WriteLine($"\nCurrent lambda: {lambda}, Epoch: {epoch}");
            Console.WriteLine($"Weights generated randomly: \n | w1:Rooms| w2:Distance| w3:Age|\n |  " + string.Join("  |    ", neuron.getWeights().Select(w => w.ToString("F3"))));

            // First Training with 25 epoch
            forEpoch(neuron, trainingSet, epoch);
            Console.WriteLine($"Epoch training is done for {epoch} epochs");

            Console.WriteLine($"||==========||==========||==========||==========||==========||\nFirst MSE calculations: trainingSet.");
            double trainingMSE = MSE(neuron, trainingSet, printDetails: true);
            Console.WriteLine($"\nTraining MSE: {trainingMSE:F8}\n||==========||==========||==========||==========||==========||");

            Console.WriteLine($"||==========||==========||==========||==========||==========||\nSecond MSE calculations: testingSet.");
            double testMSE = MSE(neuron, testingSet, printDetails: true);
            Console.WriteLine($"\n Test MSE: {testMSE:F8}\n||==========||==========||==========||==========||==========||");

            double[,] matrix = new double[epochs.Length, lambdas.Length];

            for (int i = 0; i < epochs.Length; i++) {

                for (int j = 0; j < lambdas.Length; j++) {
                    Neuron experimentalNeuron = new Neuron(3, lambdas[j]);

                    // Call it for iterate epochs[i] times on training set with given neuron
                    forEpoch(experimentalNeuron, trainingSet, epochs[i]);
                    double mse = MSE(experimentalNeuron, testingSet);
                    matrix[i, j] = mse;

                }
            }







        }

    }
}
