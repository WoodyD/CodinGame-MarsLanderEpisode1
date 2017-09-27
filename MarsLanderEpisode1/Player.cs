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
		for (int cur = 0; cur < (allPoints.Length - 1); cur++)
		{
			if (allPoints[cur].CheckThisSurface(allPoints[cur + 1]))
			{
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

			mLander.MarsLanderNewData(X, Y, hSpeed, vSpeed, rotate, power);

			// Write an action using Console.WriteLine()
			// To debug: Console.Error.WriteLine("Debug messages...");
			if (!closestLandingSurfaceChecked)
			{
				mLander.CheckClosestSurfaceForLander(allPairsOfStraightPoints);
				closestLandingSurfaceChecked = true;
			}

			Console.WriteLine(mLander.LanderNextMove());

		}

	}

	class MarsLander
	{
		SurfacePoint leftSidePoint;
		SurfacePoint rightSidePoint;
		SurfacePoint currentPoint;
		int _hSpeed;
		int _vSpeed;
		//int _fuel;
		int _rotate;
		int _power;

		public MarsLander()
		{
			this.currentPoint = new SurfacePoint(0, 0);
		}

		public void MarsLanderNewData(int positionX, int positionY, int hSpeed, int vSpeed, int rotate, int power)
		{
			this.currentPoint = new SurfacePoint(positionX, positionY);
			this._hSpeed = hSpeed;
			this._vSpeed = vSpeed;
			this._rotate = rotate;
			this._power = power;
		}

		public void CheckClosestSurfaceForLander(List<SurfacePoint> allSurfaces)
		{
			//Check 0-1, 1-2, 2-3 surfaces
			double closestDistance = allSurfaces[0].ClosestDistanceTo(currentPoint);
			leftSidePoint = allSurfaces[0];
			rightSidePoint = allSurfaces[1];
			for (int cur = 0; cur < (allSurfaces.Count() - 2); cur += 2)
			{
				if (closestDistance < allSurfaces[cur].ClosestDistanceTo(currentPoint))
				{
					closestDistance = allSurfaces[cur].ClosestDistanceTo(currentPoint);
					leftSidePoint = allSurfaces[cur];
					rightSidePoint = allSurfaces[cur + 1]; // TODO: Check here for eventualy mistakes
				}
			}
			Console.Error.WriteLine(leftSidePoint._x + " :x1 ; x2: " + rightSidePoint._x);
		}

		public string LanderNextMove()
		{
			string nextMove = "0 0";
			RelativePosition currently = currentPoint.RelativePosition(leftSidePoint, rightSidePoint);
			switch (currently)
			{
				case RelativePosition.Between:
					nextMove = MoveDown();
					break;
				case RelativePosition.Left:
					nextMove = MoveRight();
					break;
				case RelativePosition.Right:
					nextMove = MoveLeft();
					break;
			}

			return nextMove;
		}

		private string MoveDown()
		{
			Console.Error.WriteLine("Move down");
			double distanceToLand = currentPoint.ToLand(leftSidePoint);
			int power = SetPower(_vSpeed, distanceToLand);
			int rotate = StabilizeRotate(_hSpeed);
			return rotate + " " + power;
		}

		private int StabilizeRotate(int hSpeed)
		{
			int rotate = 0;
			if (_rotate == 0 && Math.Abs(hSpeed) < 10)
				return rotate;

			if (hSpeed > 0)
				rotate = 45;
			else if (hSpeed < 0)
				rotate = -45;
			else
				rotate = 0;

			return rotate;
		}

		private int SetPower(int vSpeed, double distanceToLand)
		{
			int power;
			if (Math.Abs(vSpeed) <= 20)
				power = 4;
			else
				power = 4;

			return power;
		}

		private string MoveRight()
		{
			Console.Error.WriteLine("Move right");
			int rotate = 35;
			int power = 4;
			int distanceToLand = currentPoint.ToLand(leftSidePoint);
			int distanceToScy = currentPoint.ToScy();
			if (_hSpeed < -20)
			{
				rotate = 0;
				if (distanceToLand > 2000)
					power = 3;
			}

			if (_hSpeed < -40)
			{
				rotate = -35;
				power = 4;
			}

			if (distanceToScy < 200)
				power = 3;

			return rotate + " " + power;
		}

		private string MoveLeft()
		{
			Console.Error.WriteLine("Move left");
			int rotate = -35;
			int power = 4;
			int distanceToLand = currentPoint.ToLand(leftSidePoint);
			int distanceToScy = currentPoint.ToScy();

			if (_hSpeed > 20)
			{
				rotate = 0;
				if (distanceToLand > 2000)
					power = 3;
			}

			if (_hSpeed > 40)
			{
				rotate = 35;
				power = 4;
			}

			if (distanceToScy < 200)
				power = 3;

			return rotate + " " + power;
		}

	}

	public enum RelativePosition { Left, Between, Right }

	class SurfacePoint
	{
		public int _x;
		public int _y;

		public SurfacePoint(int landX, int landY)
		{
			this._x = landX;
			this._y = landY;
		}

		public bool CheckThisSurface(SurfacePoint nextPoint)
		{
			bool isStraight = this._y == nextPoint._y;
			return isStraight;
		}

		public double ClosestDistanceTo(SurfacePoint ladderPoint)
		{
			double powOfX = Math.Pow((this._x - ladderPoint._x), 2);
			double powOfY = Math.Pow((this._y - ladderPoint._y), 2);
			double distance = Math.Sqrt(powOfX + powOfY);

			return distance;
		}

		public RelativePosition RelativePosition(SurfacePoint leftSide, SurfacePoint rightSide)
		{
			if (this._x > leftSide._x && this._x < rightSide._x)
				return Player.RelativePosition.Between;
			else if (this._x >= leftSide._x)
				return Player.RelativePosition.Left;
			else
				return Player.RelativePosition.Right;
		}

		internal int ToLand(SurfacePoint landPoint)
		{
			int distanceToLand = this._y - landPoint._y;
			return distanceToLand;
		}

		internal int ToScy()
		{
			int distance = 3000 - this._y;
			return distance;
		}
	}
}
