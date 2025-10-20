using System.Transactions;

namespace Rent_Estimation_Model {
    internal class Neuron {

        private double[] weights;
        public double lambda;
        Random r;

        public Neuron(int weightCount, double lambda) {
            this.weights = new double[weightCount];
            this.lambda = lambda;
            r = new Random();

            for (int i = 0; i < weightCount; i++) { weights[i] = r.NextDouble() * 2 - 1; }
        }

        public double[] getWeights() { return this.weights; }

        public double activationFormula(double v) { return 1.0 / (1.0 + Math.Exp(-v)); }

        public double calculateY(double[] inputs) { 
            
            double v = 0.0;
            for (int i = 0; i < inputs.Length; i++) { v += inputs[i] * weights[i]; }

            return activationFormula(v);
        }

        public void Train(double[]inputs, double target) {
            double y = calculateY(inputs);
            double error = target - y;

            for (int i = 0; i < inputs.Length; i++) { weights[i] += this.lambda * error * inputs[i]; }
        }


    }
}
