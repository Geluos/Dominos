using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominos
{
	internal class Program
	{
		static List<Domino> dominoList = new List<Domino>();
		static List<List<Domino>> workingLists = new List<List<Domino>>();
		static List<List<List<Domino>>> dominoSets = new List<List<List<Domino>>>();
		static void Main(string[] args)
		{
			System.Console.WriteLine("Взлетаем...");
			FillDominos();
			ListCreator(new List<Domino>(), 0, 0);
			System.Console.WriteLine("Количество наборов = " + workingLists.Count);
			FindSolvetions();
		}

		static void FillDominos()
		{
			for(int i = 0; i <= 6; ++i)
			{
				for(int j = i; j <= 6; ++j)
				{
					dominoList.Add(new Domino(i, j));
				}
			}
			dominoList.Reverse();
		}

		static void FindSolvetions()
		{
			foreach(List<Domino> list in workingLists)
			{
				Selecter(list, new List<Domino>(), new List<Domino>(), new List<Domino>(), 0, 0, 0, 0);
				System.Console.WriteLine("Набор проверен. Текущее число разбиений = " + dominoSets.Count);
			}
		}

		static void Selecter(List<Domino> list, List<Domino> l1, List<Domino> l2, List<Domino> l3, int sum1, int sum2, int sum3, int index)
		{
			if (index < list.Count)
			{
				Domino domino = list[index];
				{
					if (sum1 + domino.Sum() <= 26 && l1.Count < 6)
					{
						List<Domino> copyList = CopyOfList(l1);
						copyList.Add(domino);
						Selecter(list, copyList, CopyOfList(l2), CopyOfList(l3), sum1 + domino.Sum(), sum2, sum3, index + 1);
					}

					if (sum2 + domino.Sum() <= 26 && l2.Count < 6)
					{
						List<Domino> copyList = CopyOfList(l2);
						copyList.Add(domino);
						Selecter(list, CopyOfList(l1), copyList, CopyOfList(l3), sum1, sum2 + domino.Sum(), sum3, index + 1);
					}

					if (sum3 + domino.Sum() <= 26 && l3.Count < 6)
					{
						List<Domino> copyList = CopyOfList(l3);
						copyList.Add(domino);
						Selecter(list, CopyOfList(l1), CopyOfList(l2), copyList, sum1, sum2, sum3 + domino.Sum(), index + 1);
					}
				}
			}
			else
			{
				if(sum1 == 26 && sum2 == 26 && sum3 == 26)
				{
					List<List<Domino>> result = new List<List<Domino>>();
					result.Add(l1);
					result.Add(l2);
					result.Add(l3);
					dominoSets.Add(result);
				}
			}
		}

		static void ListCreator(List<Domino> list, int index, int sum)
		{
			if (index < dominoList.Count)
			{
				List<Domino> copyList = new List<Domino>();

				copyList = CopyOfList(list);

				ListCreator(copyList, index+1, sum);

				sum += dominoList[index].Sum();

				if (sum <= 13*6)
				{
					list.Add(dominoList[index]);
					if (list.Count == 18 && sum == 13*6)
					{
						workingLists.Add(list);
					}
					else
					{
						if(list.Count < 18)
							ListCreator(list, index + 1, sum);
					}
				}
			}
		}

		static List<Domino> CopyOfList(List<Domino> list)
		{
			List<Domino> result = new List<Domino>();
			foreach (Domino domino in list)
			{
				result.Add(domino);
			}
			return result;
		}

	}

	struct Domino
	{
		int valueOne;
		int valueTwo;

		public Domino(int valueOne, int valueTwo)
		{
			this.valueOne = valueOne;
			this.valueTwo = valueTwo;
		}

		public int Sum()
		{
			return valueOne + valueTwo;
		}
	}
}
