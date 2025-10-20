namespace Rent_Estimation_Model {
    internal class DataShell {
        
        public double[] inputs;
        public double target;

        /*Divide all Room Count values in Table 1 by 5, 
         * all Distance from Center values by 20, 
         * Building Age values to 30, 
         * and Rent values to 10,000. 
        */
        public DataShell(double roomCount, double distanceToCenter, double buildingAge, double rent, bool calculateWithAge) {

            if (calculateWithAge) {
                inputs = new double[] { roomCount / 5.0, distanceToCenter / 20.0, buildingAge / 30.0 };
            } else {
                inputs = new double[] { roomCount / 5.0, distanceToCenter / 20.0 };
            }

            this.target = rent / 10000.0;

        }

    }
}
