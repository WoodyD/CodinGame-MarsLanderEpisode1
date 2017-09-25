using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
	static void Main(string[] args)
	{

		string[] inputs;
		int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
        SurfacePoint[] allPoints = new SurfacePoint[surfaceN];
        for (int i = 0; i < surfaceN; i++)
		{
			inputs = Console.ReadLine().Split(' ');
			int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
			int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
            allPoints[i] = new SurfacePoint(landX, landY);
        }

        List<SurfacePoint> allPairsOfStraightPoints = new List<SurfacePoint>();
        for (int cur = 0; cur < (allPoints.Length - 1); cur++){
            if (allPoints[cur].CheckThisSurface(allPoints[cur + 1])) {
                allPairsOfStraightPoints.Add(allPoints[cur]);
                allPairsOfStraightPoints.Add(allPoints[cur + 1]);
            }
        }

        MarsLander mLander = new MarsLander();
        bool closestLandingSurfaceChecked = false;
        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int X = int.Parse(inputs[0]);
            int Y = int.Parse(inputs[1]);
            int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
            int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
            int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
            int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
            int power = int.Parse(inputs[6]); // the thrust power (0 to 4).

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            if(!closestLandingSurfaceChecked)
                

            if (vSpeed > -40){
				// 2 integers: rotate power. rotate is the desired rotation angle (should be 0 for level 1), power is the desired thrust power (0 to 4).
				Console.WriteLine("0 0");
			}
            else{
				Console.WriteLine("0 4");
			}
		}

	}

    class MarsLander{
        SurfacePoint startPoint;
        SurfacePoint endingPoint;
        SurfacePoint currentPoint;
        int _hSpeed;
        int _vSpeed;
        //int _fuel;
        int _rotate;
        int _power;

        public MarsLander(){
            
        }

        public void MarsLanderNewData(int positionX, int positionY, int hSpeed, int vSpeed, int rotate, int power){
            this.currentPoint = new SurfacePoint(positionX, positionY);
			this._hSpeed = hSpeed;
			this._vSpeed = vSpeed;
            this._rotate = rotate;
            this._power = power;
        }

        public string LanderNextMove(){
            
            return "0 0";
        }
    }

    class SurfacePoint{
        int _x;
        int _y;

        //static HashSet<KeyValuePair<SurfacePoints, SurfacePoints>> allStraightSurfaces = new HashSet<KeyValuePair<SurfacePoints, SurfacePoints>>(); 

        public SurfacePoint(int landX, int landY){
            this._x = landX;
            this._y = landY;
        }

        public bool CheckThisSurface(SurfacePoint nextPoint){
            bool isStraight = this._y == nextPoint._y;
            return isStraight;
        }

        public float ClosestPosition(SurfacePoint ladderPoint){
            float powOfX = MathF.Pow((this._x - ladderPoint._x), 2);
            float powOfY = MathF.Pow((this._y - ladderPoint._y), 2);
            float distance = MathF.Sqrt(powOfX + powOfY);

            return distance;

        }
    }
}
