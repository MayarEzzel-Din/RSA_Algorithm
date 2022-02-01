using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BigInt
{
    public List<int> BigList;
    //O(1)
    public BigInt()
    {
        BigList = new List<int>();       //O(1)
    }
    //O(1)
    public void Input(int X)
    {
        BigList.Add(X);                  //O(1)
    }
    //O(N)
    public BigInt RemoveZeros(BigInt Result)
    {
        for (int i = 0; i < Result.BigList.Count; i++)
        {
            if (Result.BigList.Count == 1 || Result.BigList[i] > 0)
                break;
            else
            {
                Result.BigList.RemoveAt(i);
                i--;
            }
        }
        return Result;
    }
    //O(N)
    public void Display()
    {
        //N*O(1) = O(N)
        for (int i = 0; i < BigList.Count; i++)
            Console.Write(BigList[i]);   //O(1)
        Console.WriteLine();             //O(1)
    }
    //O(N)
    public string tostr()
    {
        string str = "";                 //O(1)
        //N*O(1) = O(N)
        for (int i = 0; i < BigList.Count; i++)
            str += BigList[i].ToString();//O(1)
        return str;                      //O(1)
    }
    //O(N)
    private BigInt Min(BigInt x , BigInt y)
    {
        x = RemoveZeros(x);
        y = RemoveZeros(y);
        BigInt result = new BigInt();
        result.Input(-1);
        if (x.BigList.Count < y.BigList.Count)
            return x;
        else if (x.BigList.Count > y.BigList.Count)
            return y;
        else if (x.BigList[0] < y.BigList[0])
            return x;
        else if (x.BigList[0] > y.BigList[0])
            return y;
        else
        {
            for (int i = 1; i < x.BigList.Count; i++)
            {
                if (x.BigList[i] < y.BigList[i])
                    return x;
                else if (x.BigList[i] > y.BigList[i])
                    return y;
            }
            return result;
        }
    }
    //O(N)
    public BigInt Sum(BigInt X, BigInt Y)
    {
        BigInt BI;                   //O(1)
        if (X.BigList.Count < Y.BigList.Count)
            BI = Addition(X, Y);     //O(N)
        else
            BI = Addition(Y, X);     //O(N)
        return BI;                   //O(1)
    }
    /*
     * Total Complexity of Addition Function:-
     * O(N) 
     */
    private BigInt Addition(BigInt small, BigInt big)
    {
        BigInt result = new BigInt();
        result.Input(0);    //O(1)                                                               
        result = AddZeros(result, big.BigList.Count-1,'L');       //O(N)
        int Zeros = big.BigList.Count - small.BigList.Count;    //O(1)
        BigInt helper = AddZeros(small, Zeros,'L');             //O(N)

        int sum, Carry = 0;                                     //O(1)
        //#Iterations*O(Body)= N*O(1)=O(N)
        for (int i = big.BigList.Count - 1; i >= 0; i--)
        {
            sum = big.BigList[i] + helper.BigList[i] + Carry;   //O(1)
            //Complexity of conditional statements is the complexity of the maximum conditional body= O(1)
            if (sum > 9)
            {
                Carry = 1;                                      //O(1)
                sum %= 10;                                      //O(1)
            }
            else
                Carry = 0;                                      //O(1)
            result.BigList[i] = sum;                            //O(1)
        }
        helper.BigList.Clear();                                 //O(N)
        // Complexity of conditional statements is the complexity of the maximum conditional body= O(N)
        if (Carry == 1)
        {
            helper.Input(1);                                    //O(1)
            helper.BigList.AddRange(result.BigList);            //O(N)
            result = helper;                                    //O(1)
        }
        return result;                                          //O(1)
    }
    /*
     * Total Complexity of Subtraction Function:-
     * O(N)
    */
    public BigInt Subtraction(BigInt First, BigInt Second)
    {
        BigInt result = new BigInt();                                             //O(1)
        BigInt helper = new BigInt();                                             //O(1)
        int size = Math.Abs(Second.BigList.Count - First.BigList.Count);          //O(1)
        //Maximum Complexity: O(N)
        if (First.BigList.Count < Second.BigList.Count)
        {
            helper = AddZeros(First, size,'L');                                   //O(N)
            First = helper;                                                       //O(1)
        }
        else
        {
            helper = AddZeros(Second, size,'L');                                  //O(N)
            Second = helper;                                                      //O(1)
        }
        result.Input(0);                                                          //O(N)
        result = AddZeros(result, First.BigList.Count-1,'L');                     //O(N)
        int sub = 0, Carry = 0;                                                   //O(1)
        //#Iterations*O(Body) = N*O(1) = O(N)
        for (int i = First.BigList.Count - 1; i >= 0; i--)
        {
            sub = First.BigList[i] - Second.BigList[i] - Carry;                   //O(1)
            //Maximum complexity: O(1)
            if (sub < 0 && (i - 1) >= 0)
            {
                sub = sub + 10;                                                   //O(1)
                Carry = 1;                                                        //O(1)
            }
            else
                Carry = 0;                                                        //O(1)
            result.BigList[i] = sub;                                              //O(1)
        }
        int equal = 0;                                                            //O(1)
        // #Iterations*O(Body) = N*O(1) = O(N) 
        for (int i = result.BigList.Count - 1; i >= 0; i--)
        {
            //Maximum complexity: O(1)
            if (result.BigList[i] == 0)
                equal++;                                                          //O(1)
            else
                break;
        }
        //Maximum complexity: O(N)
        if (equal == result.BigList.Count)
        {
            result.BigList.Clear();                                               //O(N)
            result.Input(0);                                                      //O(N)
        }
        //Maximum complexity: O(1)
        if (result.BigList[0] < 0)
            result.BigList[0] *= -1;                                              //O(1)
        return result;
    }

    //O(N^(1.58))
    public BigInt Mul(BigInt X, BigInt Y)
    {
        BigInt BI;                        //O(1)
        if (X.BigList.Count < Y.BigList.Count)
            BI = Multiplication(X, Y);//O(N^(1.58))
        else
            BI = Multiplication(Y, X);//O(N^(1.58))
        return BI;                        //O(1)
    }
    /*
 * Total Complexity of Multiplication Function:-
 * T(N) = 3T(N/2)+ O(N)
 * N^(log a base b) VS N
 * N^(log 3 base 2) VS N
 * N^(1.58) VS N
 * Master method case 1
 * ∴ The total complexity of Multiplication function is
 * N^(1.58)
*/
    private BigInt Multiplication(BigInt First, BigInt Second)
    {
        BigInt Result = new BigInt();   //O(1)
        /*    If statment is O(N):       */
        if (First.BigList.Count == 1 && Second.BigList.Count == 1) //O(1)
        {
            int res = First.BigList[0] * Second.BigList[0]; //O(1)
            /* If statment O(N)*/
            if (res > 9) //O(1)
            {
                Result.Input(res / 10); //O(N)
                Result.Input(res % 10); //O(N)
            }
            else
                Result.Input(res); //O(N)
            return Result; //O(1)
        }
        
        /*    If statment is O(N):       */
        if (Second.BigList.Count % 2 != 0) //O(1)
            Second = AddZeros(Second, 1,'L');  //O(N) 
        int N = Second.BigList.Count; //O(1)

        int size = Second.BigList.Count - First.BigList.Count; //O(1)
        First = AddZeros(First, size,'L'); //O(N)
        BigInt a = Split(First, 0, N / 2); BigInt b = Split(First, N / 2, N);      //O(N) for each statment
        BigInt c = Split(Second, 0, N / 2); BigInt d = Split(Second, N / 2, N);   //O(N) for each statment

        BigInt M1 = Mul(a, c); //T(N/2)
        BigInt M2 = Mul(b, d); //T(N/2)
        BigInt Z = Mul(Sum(a, b), Sum(c, d)); //T(N)
        BigInt S = Subtraction(Subtraction(Z, M1), M2); //O(N)
        M1 = AddZeros(M1, N,'R');    //O(N)
        S  = AddZeros(S, N / 2,'R'); //O(N)

        Result = Sum(Sum(M1, S), M2); //O(N)
        RemoveZeros(Result);          //O(N)
        return Result;        //O(1)
    }
    private BigInt Split(BigInt Number, int start, int End)
    {
        BigInt Result = new BigInt();
        for (int i = start; i < End; i++)
            Result.Input(Number.BigList[i]);
        return Result;
    }
    private BigInt AddZeros(BigInt small, int Zeros,char direction)
    {
        if (Zeros == 0)
            return small;
        BigInt helper = new BigInt();
        if (small.BigList.Count == 0)
        {
            small.BigList.Add(0);
        }
        if (direction == 'L')
        {
            int ArrSize = Zeros + small.BigList.Count;
            int[] Arr = new int[ArrSize];
            for (int i = 0; i < Zeros; i++)     // O(N)
                Arr[i] = 0;                     // O(1)   
            Arr.CopyTo(Arr, 0);                 // O(N)
            small.BigList.ToArray().CopyTo(Arr, Zeros); // O(N)
            helper.BigList = Arr.ToList(); //O(N)
        }
        if(direction == 'R')//124 //00 ==> 12400
        {
            int size = small.BigList.Count + Zeros;
            int[] Arr = new int[size];
            small.BigList.ToArray().CopyTo(Arr, 0); 
            for (int i = small.BigList.Count; i < size; i++) 
                Arr[i] = 0; 
            helper.BigList = Arr.ToList();
        }
        return helper;
    }
    private bool IsZero(BigInt num)
    {
        num = RemoveZeros(num); // 0002 
        if (num.BigList.Count == 1 && num.BigList[0] == 0)
            return true;
        return false;
    }
    public Tuple<BigInt, BigInt> Div(BigInt a, BigInt b)
    {
        BigInt q = new BigInt();
        BigInt r = new BigInt();
        Tuple <BigInt, BigInt> result;
        if (Min(a, b) == a)
        {
            q.Input(0);
            result = new Tuple<BigInt, BigInt>(q, a);
            return result;
        }
        result = Div(a, Sum(b, b));
        r = result.Item2;
        q = result.Item1;
        q = Sum(q, q);
        if (Min(r, b) == r)
        {
            result = new Tuple<BigInt, BigInt>(q, r);
            return result;
        }
        else
        {
            BigInt one = new BigInt();
            one.Input(1);
            result = new Tuple<BigInt, BigInt>(Sum(q, one), Subtraction(r, b));
            return result;
        }
    }
    private BigInt Mod (BigInt a, BigInt b)
    {
        BigInt Result = Div(a, b).Item2;
        return Result;
    }
    public BigInt ModOfPower(BigInt Base, BigInt Power, BigInt M)
    {
        BigInt result = new BigInt(); 
        if(IsZero(Power)) // O(P) 
        {
            result.Input(1);
            return result;
        }
        BigInt Two = new BigInt();  
        Tuple<BigInt, BigInt> test;
        Two.Input(2);
        test = Div(Power, Two);
        if (IsZero(test.Item2))
        {
            result = ModOfPower(Base, test.Item1, M); //T(P/2)
            return Div((Mul(result, result)), M).Item2; //T(N)
        }
        else
        {
            BigInt X = Mod(Base, M);
            BigInt one = new BigInt();
            one.Input(1);
            BigInt Y = Mul(X, ModOfPower(Base, Subtraction(Power, one), M));
            result = Mod(Y, M);
            return result;
        }
    }
}

public class E_RSA
{
    public BigInt e, n;
    private BigInt d, M;
    public BigInt Result;
    public E_RSA(BigInt n, BigInt key, BigInt M, int choice)
    {
        Result = new BigInt();
        this.n = n;
        this.M = M;
        if (choice == 0)
        {
            e = key;
            Result = Encrypt(n, key, M);
            Result = Result.RemoveZeros(Result);
        }
        else
        {
            d = key;
            Result = Decrypt(n, key, M);
            Result = Result.RemoveZeros(Result);
        }
    }
    private BigInt Encrypt(BigInt n, BigInt e, BigInt M)
    {
        BigInt R = Result.ModOfPower(M, e, n);
        return R;
    }
    private BigInt Decrypt(BigInt n, BigInt d, BigInt EM)
    {
        BigInt R = Result.ModOfPower(EM, d, n);
        return R;
    }

}



