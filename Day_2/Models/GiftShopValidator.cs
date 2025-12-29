namespace AOC_2025.Day_2.Models;

public static class GiftShopValidator
{
    public static bool IsValid(long number)
    {
        long mod = 10;
        while (true)
        {
            long div = number / mod;
            long rem = number % mod;
            
            if(div == rem) 
                return true;
            
            mod *= 10;

            if (mod * mod / 10 >= number)
                return false;
        }
        
        return false;
    }

    public static bool IsValid2(long number)
    {
        long mod = 10;
        while (mod  * mod / 10 <= number)
        {
            long pattern = number % mod;

            if (pattern >= mod / 10)
            {
                long repeat = pattern * mod + pattern;

                while (repeat < number)
                {
                    repeat = repeat * mod + pattern;
                }

                if (repeat == number)
                    return true;
            }

            mod *= 10;
        }
        
        
        return false;
    }
}