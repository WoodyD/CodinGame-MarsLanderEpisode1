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
        List<SurfacePoints> surface = new List<SurfacePoints>();

		string[] inputs;
		int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
		for (int i = 0; i < surfaceN; i++)
		{
			inputs = Console.ReadLine().Split(' ');
			int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
			int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
            surface.Add(new SurfacePoints(landX, landY));
        }


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
        int _xToGo;
        int _yToGo;
        int _positionX;
        int _positionY;
        int _hSpeed;
        int _vSpeed;
        int _fuel;
        int _rotate;
        int _power;

        public MarsLander(){
            
        }

        public void LanderNewPosition(int positionX, int positionY){
            this._positionX = positionX;
            this._positionY = positionY;
        }

        public void LanderNewSpeed(int hSpeed, int vSpeed){
            this._hSpeed = hSpeed;
            this._vSpeed = vSpeed;
        }

        public string LanderNextMove(){
            
            return "0 0";
        }
    }

    class SurfacePoints{
        int _landX;
        int _landY;
        bool _canLand;

        public SurfacePoints(int landX, int landY){
            this._landX = landX;
            this._landY = landY;
            _canLand = landX == landY;
        }

        public int[] ClosestPosition(){
            int[] position = new int[2];

            return position;

        }
    }
}
