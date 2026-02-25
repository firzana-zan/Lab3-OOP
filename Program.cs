//#Task 6

using System;
using System.Collections.Generic;

public class Matrix
{
    private int row;
    private int column;
    private double[,] elements;
    
    public Matrix(int givenRow,int givenColumn)
    {
        row = givenRow;
        column = givenColumn;
        elements = new double [givenRow,givenColumn];
    }
    public void enterData()
    {
        Console.WriteLine("Enter the row and column data :- ");
        
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Console.Write("Row " + i + ", Column " + j + " : ");
                elements[i, j] = double.Parse(Console.ReadLine());
            }
        }
    }
    public void viewData()
    {
        Console.WriteLine("Matrix : ");
        
        for(int i=0;i<row;i++)
        {
            for(int j=0;j<column;j++)
            {
                Console.Write(elements[i,j] + " ");
            }
            Console.WriteLine();
        }
    }
    public int GetRow()
    {
        return row;
    }
    public int GetColumn()
    {
        return column;
    }
    public double GetElements(int givenRow,int givenColumn)
    {
        return elements[givenRow,givenColumn];
    }
    public void SetElements(int givenRow,int givenColumn,double value)
    {
        elements[givenRow,givenColumn] = value;
    }
}

public class Vector
{
    private int length;
    private double[] elements;
    
    public Vector(int givenLength)
    {
        length = givenLength;
        elements = new double [givenLength];
    }
    public void enterData()
    {
        Console.WriteLine("Enter the length data :- ");
        
        for (int i = 0; i < length; i++)
        {
            Console.Write("Length " + i + ": ");
            elements[i] = double.Parse(Console.ReadLine());
        }
    }
    public void viewData()
    {
        Console.WriteLine("Vector : ");
        
        for(int i=0;i<length;i++)
        {
            Console.Write(elements[i] + " ");
        }
        Console.WriteLine();
    }
    public int GetLength()
    {
        return length;
    }
    public double GetElements(int givenLength)
    {
        return elements[givenLength];
    }
    public void SetElements(int givenLength,double value)
    {
        elements[givenLength] = value;
    }
}

public class DimensionValidator
{
    public bool verifyDimensionalCompatibility(Matrix m1,Matrix m2)
    {
        return m1.GetRow() == m2.GetRow() && m1.GetColumn() == m2.GetColumn();
    }
    public bool verifyDimensionalCompatibility(Vector v1,Vector v2)
    {
        return v1.GetLength() == v2.GetLength();
    }
}

public class Calculator
{
    private DimensionValidator validator;
    
    public Calculator(DimensionValidator validation)
    {
        validator = validation;
    }
    
    public Matrix Add(Matrix m1,Matrix m2)
    {
        if(!validator.verifyDimensionalCompatibility(m1,m2))
        {
            Console.WriteLine("Error:Matrix dimensions are incompatible!");
            return null;
        }
        
        Matrix result = new Matrix(m1.GetRow(),m1.GetColumn());
        
        for(int i=0;i<m1.GetRow();i++)
        {
            for(int j=0;j<m1.GetColumn();j++)
            {
                result.SetElements(i,j, m1.GetElements(i,j) + m2.GetElements(i,j));
            }
        }
        return result;
    }
    public Vector Add(Vector v1,Vector v2)
    {
        if(!validator.verifyDimensionalCompatibility(v1,v2))
        {
            Console.WriteLine("Error:Vector dimensions are incompatible");
            return null;
        }
        
        Vector result = new Vector(v1.GetLength());
        
        for(int i=0;i<v1.GetLength();i++)
        {
            result.SetElements(i, v1.GetElements(i) + v2.GetElements(i));
        }
        return result;
    }
}

public class Repository
{
    private List<Matrix> MatrixRepository = new List<Matrix>();
    private List<Vector> VectorRepository = new List<Vector>();
    
    public void storeObjects(Matrix m)
    {
        MatrixRepository.Add(m);
        Console.WriteLine("Matrix successfully stored in Repository");
    }
    public void storeObjects(Vector v)
    {
        VectorRepository.Add(v);
        Console.WriteLine("Vector successfully stored in Repository");
    }
}

public class InputHandler
{
    private double data;
    
    public Matrix createMatrixObject()
    {
        Console.Write("Enter number of row : ");
        int givenRow = int.Parse(Console.ReadLine());
        Console.Write("Enter number of column : ");
        int givenColumn = int.Parse(Console.ReadLine());
        
        Matrix m = new Matrix(givenRow,givenColumn);
        m.enterData();
        return m;
    }
    
    public Vector createVectorObject()
    {
        Console.Write("Enter vector length : ");
        int givenLength = int.Parse(Console.ReadLine());
        
        Vector v = new Vector(givenLength);
        v.enterData();
        return v;
    }
}

public class Program
{
    public static void Main()
    {
        InputHandler handler = new InputHandler();
        Repository repo = new Repository();
        DimensionValidator val = new DimensionValidator();
        Calculator calc = new Calculator(val);

        Console.WriteLine("--- Matrix Addition ---");
        Matrix m1 = handler.createMatrixObject();
        Matrix m2 = handler.createMatrixObject();
        
        Matrix mResult = calc.Add(m1, m2);

        if (mResult != null)
        {
            Console.WriteLine("Resulting Matrix : ");
            mResult.viewData();
            repo.storeObjects(mResult);
        }
        
        
        Console.WriteLine("\n--- Vector Addition ---");
        Vector v1 = handler.createVectorObject(); 
        Vector v2 = handler.createVectorObject();
    
        Vector vResult = calc.Add(v1, v2);

        if (vResult != null)
        {
            Console.WriteLine("Resulting Vector : ");
            vResult.viewData();     
            repo.storeObjects(vResult); 
        }
    }    
}