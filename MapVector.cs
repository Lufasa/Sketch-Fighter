using System;

public class MapVector
{
	private float x;
	private float y;
	private float divRatio;

	public MapVector (float a, float b, float divR)
	{
		x = a;
		y = b;
		divRatio = divR;
	}

	public float DivRatio
	{
		get { return divRatio;}
		set { divRatio = value;}
	}

	public float A
	{
		get { return x;}
		set { x = value;}
	}

	public float B
	{
		get { return y;}
		set { y = value;}
	}

	public float C
	{
		get { return DivRatio;}
		set { DivRatio = value;}
	}

	public override string ToString ()
	{
		return string.Format ("[MapVector: A={0}, B={1}, C={2}]", A, B, C);
	}

	public override bool Equals (object obj)
	{
		MapVector rhs = (MapVector)obj;
		return rhs.A == A && rhs.B == B && rhs.C == C;
	}

}

