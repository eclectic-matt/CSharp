namespace Numbers;

public static class Numbers 
{
	public static Complex GetComplexNumber(int real, int imaginary)
	{
		return new Complex(real, imaginary);
	}
}

public class Complex(float realPart, float imaginaryPart)
{
	//private fields
	private float real = realPart, imaginary = imaginaryPart;
	
	//public properties with get/set methods
	public float Real
	{
		get { return real; }
		set { real = value; }
	}
	public float Imaginary
	{
		get { return imaginary; }
		set { imaginary = value; }
	}

	/**
	 * Adds another Complex number to this number.
	 * @param Complex c2 The complex number to add.
	 * @return Complex The resulting Complex number.
	 */
    public Complex Add(Complex c2)
	{
		float r = real + c2.Real;
		float i = imaginary + c2.Imaginary;
		return new Complex(r, i);
	}

	public Complex Subtract(Complex c2)
	{
		float r = real - c2.real;
		float i = this.imaginary - c2.imaginary;
		return new Complex(r,i);
	}

	public Complex Multiply(Complex c2)
	{
		float r = (real * c2.real) - (imaginary * c2.imaginary);
		float i = (real * c2.imaginary) + (imaginary * c2.real);
		return new Complex(r, i);
	}

	public Complex Divide(Complex c2)
	{
		float rA = (real * c2.real) + (imaginary * c2.imaginary);
		float i = (imaginary * c2.real) - (real * c2.imaginary);
		float rB = (c2.real * c2.real) + (c2.imaginary * c2.imaginary);
		return new Complex(rA / rB, i / rB);
	}

	public double Modulo()
	{
		float r = (real * real) + (imaginary * imaginary);
		return Math.Sqrt(r);
	}

	public string Print()
	{
		char sign;
		string imaginaryPart = "";
		//If the imaginary part is > 0
		if (imaginary > 0){
			//Its sign will be positive
			sign = '+';
		}else{
			//Its sign will be negative
			sign = '-';
		}
		//If the imaginary part is not 0
		if(imaginary != 0){
			//Output the imaginary part
			imaginaryPart = sign + " " + Math.Abs(imaginary) + "i";
		}
		//Return the real + imaginary parts
		return real + " " + imaginaryPart;
	}
}