using System;
using UnityEngine;
using RD = System.Random;

namespace Alter {

    public class RandomService {

        RD random;
        public int seed;

        public RandomService() {
            this.seed = (int)DateTime.Now.Ticks;
            random = new RD(seed);
        }

        public int NextIntRange(int min, int max) {
            return random.Next(min, max);
        }

    }

}