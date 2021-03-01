using System;
using System.Collections.Generic;
using System.Text;

namespace Final_project
{
    class Program
    {
        static void Main(string[] args)
        {
            // Question1
            Dictionary<int, int> vendingMachine = new Dictionary<int, int>()
            {
                {1, 10 }, {2, 10}, {5,20}, {10,8}, { 50,7 },{100,5}
            };
            List<int> money = GetInMoney();
            int bill = GetBIllValue();
            Dictionary<int, int> result = VendingTransaction(vendingMachine, bill, money);

            if (AddMoney(result) == AddMoney(money) - bill && AddMoney(result) != 0 )
            {
                Console.WriteLine("\nHere is your refund. Thank you for using the machine. :) See you again! ");
                PrintResult(result);
            }



            // Question2
            //Console.WriteLine(CompressString("RTFFFFYYUPPPEEEUUU"));
            //Console.WriteLine(CompressString("RTFFFFYYUPPPEEEUUUXXX"));
            //Console.WriteLine(CompressString("RTFFFFYYUPPPEEEUUUXX"));
            //Console.WriteLine(CompressString("RTFFFFYYUPPPEEEUUUX"));
            //Console.WriteLine(CompressString("RRRRTFFFFYYUPPPEEEUUU"));
            //Console.WriteLine(CompressString("RRRTFFFFYYUPPPEEEUUU"));
            //Console.WriteLine(CompressString("RRTFFFFYYUPPPEEEUUU"));
            //Console.WriteLine(CompressString("RTFFFFYYUPPPEEEUUUUUUU"));
            //Console.WriteLine(CompressString(""));
            /*
             * Expected answers
             * 
             * RTF4YYUP3E3U3
              RTF4YYUP3E3U3X3
              RTF4YYUP3E3U3XX
              RTF4YYUP3E3U3X
              R4TF4YYUP3E3U3
              R3TF4YYUP3E3U3
              RRTF4YYUP3E3U3
              RTF4YYUP3E3U7
              Sorry, you entered invalid string to compress
            */

            //Console.WriteLine(DecompressString("RTF4YYUP3E3U3B7"));
            //Console.WriteLine(DecompressString("RTF4YYUP3E3U3"));
            //Console.WriteLine(DecompressString("RTF4YYUP3E3U3X"));
            //Console.WriteLine(DecompressString("RRTF4YYUP3E3U3XX"));
            //Console.WriteLine(DecompressString("R7TF4YYUP3E3U3"));
            //Console.WriteLine(DecompressString("R"));
            //Console.WriteLine(DecompressString(null));
            //Console.WriteLine(DecompressString(""));

            /*
             * Expected answers 
             * 
             * RTFFFFYYUPPPEEEUUUBBBBBBB
               RTFFFFYYUPPPEEEUUU
               RTFFFFYYUPPPEEEUUUX
               RRTFFFFYYUPPPEEEUUUXX
               RRRRRRRTFFFFYYUPPPEEEUUU
               R
               Sorry, you entered invalid string to decompress
               Sorry, you entered invalid string to decompress
             */
        }

        //Question1
        static Dictionary<int, int> VendingTransaction(Dictionary<int, int> vendingMachine, int bill, List<int> moneyAdded)
        {
            Dictionary<int, int> balanceDict = new Dictionary<int, int>();
            int totalMoney = AddMoney(moneyAdded);
            int totalBalance = totalMoney - bill;

            if (AddMoney(vendingMachine) < totalBalance)
                Apologize(totalMoney);

            else if (bill == AddMoney(moneyAdded))
                Console.WriteLine("Thank you for using the machine. :) See you again! ");

            else if(totalBalance < 0)
                Console.WriteLine($"You entered less money than needed. Here is your money back : ${totalMoney}");

            else
            {
                while (AddMoney(balanceDict) != totalBalance)
                {
                    int balanceLeft = totalBalance - AddMoney(balanceDict);
                    int currCoin = FindSuitableCurrencyValue(vendingMachine, balanceLeft);

                    if (currCoin == 0) 
                    {
                        Apologize(totalMoney);
                        break;
                    }
                    else if (vendingMachine.ContainsKey(currCoin))
                    {
                        InsertCoin(balanceDict, currCoin);
                        RemoveCoin(vendingMachine, currCoin);

                    }
                }
                    InsertMoneyIntoMachine(vendingMachine, moneyAdded);
            }

            return balanceDict;
        }

        static int FindSuitableCurrencyValue(Dictionary<int, int> vendingMachine, int balance)
        {
            int coin = 0;

            foreach(var item in vendingMachine)
            {
                if (item.Key <= balance && item.Key > coin)
                {
                    coin = item.Key;
                }
            }

            return coin;
        }

        static void RemoveCoin(Dictionary<int, int> vendingMachine, int coin)
        {
            if (vendingMachine[coin] == 1)
                vendingMachine.Remove(coin);
            else
                vendingMachine[coin]--;
        }

        static void Apologize(int money)
        {
            Console.WriteLine($"Sorry, the machine doesn't have enough money for refund! Here's your total money: ${money}");
        }

        static List<int> GetInMoney()
        {
            List<int> result = new List<int>();
            Dictionary<int, bool> acceptedCoins = new Dictionary<int, bool>()
            { { 1,true },{ 2, true },{ 5,true },{ 10, true },{20,true },{ 50,true },{ 100, true } };

            Console.WriteLine("Please enter a coin and press enter. Enter -1 to finish entering coins.");
            int value = 0;
            do
            {
                bool success = int.TryParse(Console.ReadLine(), out value);
                if (success)
                {
                    if (acceptedCoins.ContainsKey(value))
                        result.Add(value);
                    else if (value == -1)
                        Console.WriteLine("Thank you for entering money in.\n");
                    else
                        Console.WriteLine($"The coin with value {value} you inserted is not valid currency. Please enter a valid value for currency.");
                }
            } while (value != -1 || value >=0);

            return result;
        }

        static int GetBIllValue()
        {
            int bill = 0;
            int count = 0;
            do
            {
                if (count > 0)
                    Console.Write("You entered an invalid bill value. ");

                Console.Write("Please enter the total amount of money for your purchase.\n");
                int.TryParse(Console.ReadLine(), out bill);
                count++;
            } while (bill <= 0);

            return bill;
        }

        static void InsertMoneyIntoMachine(Dictionary<int, int> vendingMachine, List<int> money)
        {
            foreach (var coin in money)
            {
                InsertCoin(vendingMachine, coin);
            }
        }

        static void InsertCoin(Dictionary<int, int> dict, int coin)
        {
            if (dict.ContainsKey(coin))
                dict[coin]++;
            else
                dict.Add(coin, 1);
        }

        static int AddMoney(List<int> list)
        {
            int result = 0;
            foreach (var value in list)
            {
                result += value;
            }
            return result;
        }

        static int AddMoney(Dictionary<int, int> dict)
        {
            int result = 0;
            foreach (var item in dict)
            {
                result += item.Key * item.Value;
            }
            return result;
        }

        static void PrintResult(Dictionary<int, int> money)
        {
            foreach (var coin in money)
            {
                Console.WriteLine($"{coin.Value} coins of ${coin.Key}.");
            }
        }

        // Question2
        static string DecompressString(string str)
        {
            if (String.IsNullOrEmpty(str))
                return "Sorry, you entered invalid string to decompress";

            if (str.Length == 1)
                return str;

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char currChar = str[i];
                if (Char.IsDigit(currChar))
                {
                    char lastLetter = str[i - 1];
                    for (int j = 1; j < Char.GetNumericValue(currChar); j++)
                    {
                        result.Append(lastLetter);
                    }
                }
                else if (Char.IsLetter(currChar))
                    result.Append(currChar);

            }
            return result.ToString();
        }

        static string CompressString(string str)
        {
            if (String.IsNullOrEmpty(str))
                return "Sorry, you entered invalid string to compress";

            if (str.Length <= 2)
                return str;

            StringBuilder result = new StringBuilder();
            char lastchar = ' ';
            int counter = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].Equals(lastchar))
                    counter++;

                if (!str[i].Equals(lastchar) || i == str.Length - 1)
                {
                    if (!str[i].Equals(' '))
                    {
                        if (counter == 2)
                            result.Append(lastchar);
                        else if (counter > 2)
                            result.Append($"{counter}");

                        if (!str[i].Equals(lastchar))
                            result.Append(str[i]);
                    }
                    counter = 1;
                    lastchar = str[i];
                }
            }
            return result.ToString();
        }

        // extraHelper functions
        static void PrintList(List<int> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        static void PrintDictionary(Dictionary<int, int> dict)
        {
            foreach (var item in dict)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
