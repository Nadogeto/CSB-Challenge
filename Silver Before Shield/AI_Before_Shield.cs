using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace AI
{
    // Creates a format of Checkpoint(x,y)
    class Checkpoint
    {
        public int x;
        public int y;

        public Checkpoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    // Runs the main program
    class Program
    {
        static void Main()
        {
            List<Checkpoint> checkpoints = new List<Checkpoint>();
            Checkpoint farthestCheckpoint = new Checkpoint(-1, -1);
            Checkpoint previousCheckpoint = new Checkpoint(-1, -1);
            int lapCount = 1;
            int previousCheckpointAngle = 0;
            double farthestCheckpointDist = double.MinValue;
            string[] inputs;
            bool boost = true;

            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int x = int.Parse(inputs[0]);
                int y = int.Parse(inputs[1]);
                int nextCheckpointX = int.Parse(inputs[2]); // x position of the next check point
                int nextCheckpointY = int.Parse(inputs[3]); // y position of the next check point
                int nextCheckpointDist = int.Parse(inputs[4]); // distance to the next checkpoint
                int nextCheckpointAngle = int.Parse(inputs[5]); // angle between your pod orientation and the direction of the next checkpoint
                inputs = Console.ReadLine().Split(' ');
                int opponentX = int.Parse(inputs[0]);
                int opponentY = int.Parse(inputs[1]);

                // Increases Lap count
                if (previousCheckpoint.x > 0 &&
                previousCheckpoint.y > 0 &&
                previousCheckpoint.x != nextCheckpointX &&
                previousCheckpoint.y != nextCheckpointY &&
                checkpoints[0].x == nextCheckpointX &&
                checkpoints[0].y == nextCheckpointY)
                {
                    lapCount++;
                }

                string thrust = (nextCheckpointAngle > 90 || nextCheckpointAngle < -90) ? "0" : "100";

                // Switches the farthestCheckpoints when passed
                if (farthestCheckpointDist < nextCheckpointDist)
                {
                    farthestCheckpointDist = nextCheckpointDist;
                    farthestCheckpoint.x = nextCheckpointX;
                    farthestCheckpoint.y = nextCheckpointY;
                }

                // Checks for the right time (last lap) to activate the boost
                if (boost == true && lapCount == 3 &&
                thrust.Equals("100") &&
                previousCheckpointAngle == nextCheckpointAngle &&
                nextCheckpointX == farthestCheckpoint.x &&
                nextCheckpointY == farthestCheckpoint.y)
                {
                    boost = false;
                    thrust = "BOOST";
                }

                previousCheckpoint.x = nextCheckpointX;
                previousCheckpoint.y = nextCheckpointY;
                previousCheckpointAngle = nextCheckpointAngle;

                Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " " + thrust);
            }
        }
    }
}