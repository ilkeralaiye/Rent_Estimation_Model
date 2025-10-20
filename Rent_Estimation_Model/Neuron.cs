namespace Rent_Estimation_Model
{
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




    }
}
