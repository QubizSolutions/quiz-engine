using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qubiz.QuizEngine.Infrastructure;
using System;
using System.Collections.Generic;

namespace Qubiz.QuizEngine.UnitTesting.Infrastructure
{
	[TestClass]
	public class MapperTest
	{
		[TestMethod]
		public void DeepCopyTo_SourceAndDestinationTypeAreTheSame_DestinationCopied()
		{
			Cow source = new Cow()
			{
				Bool = true,
				DateTime = new DateTime(2010, 01, 02),
				Decimal = 10.10M,
				Guid = new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"),
				Int = 123,
				String = "123"
			};

			Cow destination = source.DeepCopyTo<Cow>();

			Assert.AreNotEqual(source, destination);
			Assert.AreEqual(true, destination.Bool);
			Assert.AreEqual(new DateTime(2010, 01, 02), destination.DateTime);
			Assert.AreEqual(10.10M, destination.Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"), destination.Guid);
			Assert.AreEqual(123, destination.Int);
			Assert.AreEqual("123", destination.String);
		}

		[TestMethod]
		public void DeepCopyTo_SourceAndDestinationAreDifferentTypeButContainTheSameProperties_DestinationCopied()
		{
			Cow source = new Cow()
			{
				Bool = true,
				DateTime = new DateTime(2010, 01, 02),
				Decimal = 10.10M,
				Guid = new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"),
				Int = 123,
				String = "123"
			};

			Mule destination = source.DeepCopyTo<Mule>();

			Assert.AreNotEqual(source, destination);
			Assert.AreEqual(true, destination.Bool);
			Assert.AreEqual(new DateTime(2010, 01, 02), destination.DateTime);
			Assert.AreEqual(10.10M, destination.Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"), destination.Guid);
			Assert.AreEqual(123, destination.Int);
			Assert.AreEqual("123", destination.String);
		}

		[TestMethod]
		public void DeepCopyTo_ArrayToArrayOfSameType_DestinationCopied()
		{
			Cow[] source = new Cow[1]
			{
				 new Cow()
				{
					Bool = true,
					DateTime = new DateTime(2010, 01, 02),
					Decimal = 10.10M,
					Guid = new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"),
					Int = 123,
					String = "123"
				}
			};

			Cow[] destination = source.DeepCopyTo<Cow[]>();

			Assert.AreNotEqual(source, destination);
			Assert.AreEqual(1, destination.Length);
			Assert.AreNotEqual(source[0], destination[0]);
			Assert.AreEqual(true, destination[0].Bool);
			Assert.AreEqual(new DateTime(2010, 01, 02), destination[0].DateTime);
			Assert.AreEqual(10.10M, destination[0].Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"), destination[0].Guid);
			Assert.AreEqual(123, destination[0].Int);
			Assert.AreEqual("123", destination[0].String);
		}

		[TestMethod]
		public void DeepCopyTo_ArrayToArrayOfDifferentTypesButWithSameProperties_DestinationCopied()
		{
			Cow[] source = new Cow[1]
			{
				 new Cow()
				{
					Bool = true,
					DateTime = new DateTime(2010, 01, 02),
					Decimal = 10.10M,
					Guid = new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"),
					Int = 123,
					String = "123"
				}
			};

			Mule[] destination = source.DeepCopyTo<Mule[]>();

			Assert.AreNotEqual(source, destination);
			Assert.AreEqual(1, destination.Length);
			Assert.AreNotEqual(source[0], destination[0]);
			Assert.AreEqual(true, destination[0].Bool);
			Assert.AreEqual(new DateTime(2010, 01, 02), destination[0].DateTime);
			Assert.AreEqual(10.10M, destination[0].Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"), destination[0].Guid);
			Assert.AreEqual(123, destination[0].Int);
			Assert.AreEqual("123", destination[0].String);
		}

		[TestMethod]
		public void DeepCopyTo_ListToListOfSameType_DestinationCopied()
		{
			List<Cow> source = new List<Cow>
			{
				 new Cow()
				{
					Bool = true,
					DateTime = new DateTime(2010, 01, 02),
					Decimal = 10.10M,
					Guid = new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"),
					Int = 123,
					String = "123"
				}
			};

			List<Cow> destination = source.DeepCopyTo<List<Cow>>();

			Assert.AreNotEqual(source, destination);
			Assert.AreEqual(1, destination.Count);
			Assert.AreNotEqual(source[0], destination[0]);
			Assert.AreEqual(true, destination[0].Bool);
			Assert.AreEqual(new DateTime(2010, 01, 02), destination[0].DateTime);
			Assert.AreEqual(10.10M, destination[0].Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"), destination[0].Guid);
			Assert.AreEqual(123, destination[0].Int);
			Assert.AreEqual("123", destination[0].String);
		}

		[TestMethod]
		public void DeepCopyTo_ListToListOfDifferentTypesButWithSameProperties_DestinationCopied()
		{
			List<Cow> source = new List<Cow>
			{
				 new Cow()
				{
					Bool = true,
					DateTime = new DateTime(2010, 01, 02),
					Decimal = 10.10M,
					Guid = new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"),
					Int = 123,
					String = "123"
				}
			};

			List<Mule> destination = source.DeepCopyTo<List<Mule>>();

			Assert.AreNotEqual(source, destination);
			Assert.AreEqual(1, destination.Count);
			Assert.AreNotEqual(source[0], destination[0]);
			Assert.AreEqual(true, destination[0].Bool);
			Assert.AreEqual(new DateTime(2010, 01, 02), destination[0].DateTime);
			Assert.AreEqual(10.10M, destination[0].Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"), destination[0].Guid);
			Assert.AreEqual(123, destination[0].Int);
			Assert.AreEqual("123", destination[0].String);
		}

		[TestMethod]
		public void DeepCopyTo_ArrayToListOfSameType_DestinationCopied()
		{
			Cow[] source = new Cow[1]
			{
				 new Cow()
				{
					Bool = true,
					DateTime = new DateTime(2010, 01, 02),
					Decimal = 10.10M,
					Guid = new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"),
					Int = 123,
					String = "123"
				}
			};

			List<Cow> destination = source.DeepCopyTo<List<Cow>>();

			Assert.AreNotEqual(source, destination);
			Assert.AreEqual(1, destination.Count);
			Assert.AreNotEqual(source[0], destination[0]);
			Assert.AreEqual(true, destination[0].Bool);
			Assert.AreEqual(new DateTime(2010, 01, 02), destination[0].DateTime);
			Assert.AreEqual(10.10M, destination[0].Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"), destination[0].Guid);
			Assert.AreEqual(123, destination[0].Int);
			Assert.AreEqual("123", destination[0].String);
		}

		[TestMethod]
		public void DeepCopyTo_ArrayToListOfDifferentTypesButWithSameProperties_DestinationCopied()
		{
			Cow[] source = new Cow[1]
			{
				 new Cow()
				{
					Bool = true,
					DateTime = new DateTime(2010, 01, 02),
					Decimal = 10.10M,
					Guid = new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"),
					Int = 123,
					String = "123"
				}
			};

			List<Mule> destination = source.DeepCopyTo<List<Mule>>();

			Assert.AreNotEqual(source, destination);
			Assert.AreEqual(1, destination.Count);
			Assert.AreNotEqual(source[0], destination[0]);
			Assert.AreEqual(true, destination[0].Bool);
			Assert.AreEqual(new DateTime(2010, 01, 02), destination[0].DateTime);
			Assert.AreEqual(10.10M, destination[0].Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"), destination[0].Guid);
			Assert.AreEqual(123, destination[0].Int);
			Assert.AreEqual("123", destination[0].String);
		}

		[TestMethod]
		public void DeepCopyTo_ListToArrayOfSameType_DestinationCopied()
		{
			List<Cow> source = new List<Cow>
			{
				 new Cow()
				{
					Bool = true,
					DateTime = new DateTime(2010, 01, 02),
					Decimal = 10.10M,
					Guid = new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"),
					Int = 123,
					String = "123"
				}
			};

			Cow[] destination = source.DeepCopyTo<Cow[]>();

			Assert.AreNotEqual(source, destination);
			Assert.AreEqual(1, destination.Length);
			Assert.AreNotEqual(source[0], destination[0]);
			Assert.AreEqual(true, destination[0].Bool);
			Assert.AreEqual(new DateTime(2010, 01, 02), destination[0].DateTime);
			Assert.AreEqual(10.10M, destination[0].Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"), destination[0].Guid);
			Assert.AreEqual(123, destination[0].Int);
			Assert.AreEqual("123", destination[0].String);
		}

		[TestMethod]
		public void DeepCopyTo_ListToArrayOfDifferentTypesButWithSameProperties_DestinationCopied()
		{
			List<Cow> source = new List<Cow>
			{
				 new Cow()
				{
					Bool = true,
					DateTime = new DateTime(2010, 01, 02),
					Decimal = 10.10M,
					Guid = new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"),
					Int = 123,
					String = "123"
				}
			};

			Mule[] destination = source.DeepCopyTo<Mule[]>();

			Assert.AreNotEqual(source, destination);
			Assert.AreEqual(1, destination.Length);
			Assert.AreNotEqual(source[0], destination[0]);
			Assert.AreEqual(true, destination[0].Bool);
			Assert.AreEqual(new DateTime(2010, 01, 02), destination[0].DateTime);
			Assert.AreEqual(10.10M, destination[0].Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8D82FE84DBD9"), destination[0].Guid);
			Assert.AreEqual(123, destination[0].Int);
			Assert.AreEqual("123", destination[0].String);
		}

		[TestMethod]
		public void DeepCopyTo_WhenSourceAndDestinationHaveDifferentAggregatedTypes_ThenDestinationIsCopied()
		{
			A.BigClass source = new A.BigClass()
			{
				Bool = true,
				DateTime = new DateTime(2011, 01, 02),
				Decimal = 14.10M,
				Guid = new Guid("8DA4C611-A758-4EB7-A352-8D12FE84DBD9"),
				Int = 4141,
				String = "1142",

				FirstClass = new A.FirstClass()
				{
					Bool = true,
					DateTime = new DateTime(2012, 01, 02),
					Decimal = 66.10M,
					Guid = new Guid("8D24C611-A758-4EB7-A352-8D82FE84DBD9"),
					Int = 54,
					String = "8567",

					FirstClassFirstSubClass = new A.FirstClassFirstSubClass()
					{
						Bool = true,
						DateTime = new DateTime(2013, 01, 02),
						Decimal = 324.10M,
						Guid = new Guid("8DA43611-A758-4EB7-A352-8D82FE84DBD9"),
						Int = 7345,
						String = "834"
					},
					FirstClassSecondSubClass = new A.FirstClassSecondSubClass()
					{
						Bool = true,
						DateTime = new DateTime(2014, 01, 02),
						Decimal = 523.10M,
						Guid = new Guid("8DA4C611-A758-4EB7-A352-8482FE84DBD9"),
						Int = 235,
						String = "978"
					}
				},

				SecondClass = new A.SecondClass()
				{
					Bool = true,
					DateTime = new DateTime(2015, 01, 02),
					Decimal = 78.10M,
					Guid = new Guid("8DA4C611-3758-4EB7-A352-8D82FE84DBD9"),
					Int = 2346,
					String = "5",

					SecondClassFirstSubClass = new A.SecondClassFirstSubClass()
					{
						Bool = true,
						DateTime = new DateTime(2016, 01, 02),
						Decimal = 32.10M,
						Guid = new Guid("8DA41611-A758-4EB7-A352-8D82FE84DBD9"),
						Int = 8,
						String = "7089"
					},
					SecondClassSecondSubClass = new A.SecondClassSecondSubClass()
					{
						Bool = true,
						DateTime = new DateTime(2017, 01, 02),
						Decimal = 56.10M,
						Guid = new Guid("8DA44611-A758-4EB7-A352-8D82FE844BD9"),
						Int = 5,
						String = "887"
					}
				}
			};

			B.BigClass destination = source.DeepCopyTo<B.BigClass>();

			Assert.AreNotEqual(source, destination);
			Assert.AreEqual(true, destination.Bool);
			Assert.AreEqual(new DateTime(2011, 01, 02), destination.DateTime);
			Assert.AreEqual(14.10M, destination.Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8D12FE84DBD9"), destination.Guid);
			Assert.AreEqual(4141, destination.Int);
			Assert.AreEqual("1142", destination.String);

			Assert.AreNotEqual(source.FirstClass, destination.FirstClass);
			Assert.AreEqual(true, destination.FirstClass.Bool);
			Assert.AreEqual(new DateTime(2012, 01, 02), destination.FirstClass.DateTime);
			Assert.AreEqual(66.10M, destination.FirstClass.Decimal);
			Assert.AreEqual(new Guid("8D24C611-A758-4EB7-A352-8D82FE84DBD9"), destination.FirstClass.Guid);
			Assert.AreEqual(54, destination.FirstClass.Int);
			Assert.AreEqual("8567", destination.FirstClass.String);

			Assert.AreNotEqual(source.FirstClass.FirstClassFirstSubClass, destination.FirstClass.FirstClassFirstSubClass);
			Assert.AreEqual(true, destination.FirstClass.FirstClassFirstSubClass.Bool);
			Assert.AreEqual(new DateTime(2013, 01, 02), destination.FirstClass.FirstClassFirstSubClass.DateTime);
			Assert.AreEqual(324.10M, destination.FirstClass.FirstClassFirstSubClass.Decimal);
			Assert.AreEqual(new Guid("8DA43611-A758-4EB7-A352-8D82FE84DBD9"), destination.FirstClass.FirstClassFirstSubClass.Guid);
			Assert.AreEqual(7345, destination.FirstClass.FirstClassFirstSubClass.Int);
			Assert.AreEqual("834", destination.FirstClass.FirstClassFirstSubClass.String);

			Assert.AreNotEqual(source.FirstClass.FirstClassSecondSubClass, destination.FirstClass.FirstClassSecondSubClass);
			Assert.AreEqual(true, destination.FirstClass.FirstClassSecondSubClass.Bool);
			Assert.AreEqual(new DateTime(2014, 01, 02), destination.FirstClass.FirstClassSecondSubClass.DateTime);
			Assert.AreEqual(523.10M, destination.FirstClass.FirstClassSecondSubClass.Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8482FE84DBD9"), destination.FirstClass.FirstClassSecondSubClass.Guid);
			Assert.AreEqual(235, destination.FirstClass.FirstClassSecondSubClass.Int);
			Assert.AreEqual("978", destination.FirstClass.FirstClassSecondSubClass.String);

			Assert.AreNotEqual(source.SecondClass, destination.SecondClass);
			Assert.AreEqual(true, destination.SecondClass.Bool);
			Assert.AreEqual(new DateTime(2015, 01, 02), destination.SecondClass.DateTime);
			Assert.AreEqual(78.10M, destination.SecondClass.Decimal);
			Assert.AreEqual(new Guid("8DA4C611-3758-4EB7-A352-8D82FE84DBD9"), destination.SecondClass.Guid);
			Assert.AreEqual(2346, destination.SecondClass.Int);
			Assert.AreEqual("5", destination.SecondClass.String);

			Assert.AreNotEqual(source.SecondClass.SecondClassFirstSubClass, destination.SecondClass.SecondClassFirstSubClass);
			Assert.AreEqual(true, destination.SecondClass.SecondClassFirstSubClass.Bool);
			Assert.AreEqual(new DateTime(2016, 01, 02), destination.SecondClass.SecondClassFirstSubClass.DateTime);
			Assert.AreEqual(32.10M, destination.SecondClass.SecondClassFirstSubClass.Decimal);
			Assert.AreEqual(new Guid("8DA41611-A758-4EB7-A352-8D82FE84DBD9"), destination.SecondClass.SecondClassFirstSubClass.Guid);
			Assert.AreEqual(8, destination.SecondClass.SecondClassFirstSubClass.Int);
			Assert.AreEqual("7089", destination.SecondClass.SecondClassFirstSubClass.String);

			Assert.AreNotEqual(source.SecondClass.SecondClassSecondSubClass, destination.SecondClass.SecondClassSecondSubClass);
			Assert.AreEqual(true, destination.SecondClass.SecondClassSecondSubClass.Bool);
			Assert.AreEqual(new DateTime(2017, 01, 02), destination.SecondClass.SecondClassSecondSubClass.DateTime);
			Assert.AreEqual(56.10M, destination.SecondClass.SecondClassSecondSubClass.Decimal);
			Assert.AreEqual(new Guid("8DA44611-A758-4EB7-A352-8D82FE844BD9"), destination.SecondClass.SecondClassSecondSubClass.Guid);
			Assert.AreEqual(5, destination.SecondClass.SecondClassSecondSubClass.Int);
			Assert.AreEqual("887", destination.SecondClass.SecondClassSecondSubClass.String);
		}

		[TestMethod]
		public void DeepCopyTo_WhenSourceAndDestinationHaveTheSameAggregatedType_ThenDestinationIsCopied()
		{
			A.BigClass source = new A.BigClass()
			{
				Bool = true,
				DateTime = new DateTime(2011, 01, 02),
				Decimal = 14.10M,
				Guid = new Guid("8DA4C611-A758-4EB7-A352-8D12FE84DBD9"),
				Int = 4141,
				String = "1142",

				FirstClass = new A.FirstClass()
				{
					Bool = true,
					DateTime = new DateTime(2012, 01, 02),
					Decimal = 66.10M,
					Guid = new Guid("8D24C611-A758-4EB7-A352-8D82FE84DBD9"),
					Int = 54,
					String = "8567",

					FirstClassFirstSubClass = new A.FirstClassFirstSubClass()
					{
						Bool = true,
						DateTime = new DateTime(2013, 01, 02),
						Decimal = 324.10M,
						Guid = new Guid("8DA43611-A758-4EB7-A352-8D82FE84DBD9"),
						Int = 7345,
						String = "834"
					},
					FirstClassSecondSubClass = new A.FirstClassSecondSubClass()
					{
						Bool = true,
						DateTime = new DateTime(2014, 01, 02),
						Decimal = 523.10M,
						Guid = new Guid("8DA4C611-A758-4EB7-A352-8482FE84DBD9"),
						Int = 235,
						String = "978"
					}
				},

				SecondClass = new A.SecondClass()
				{
					Bool = true,
					DateTime = new DateTime(2015, 01, 02),
					Decimal = 78.10M,
					Guid = new Guid("8DA4C611-3758-4EB7-A352-8D82FE84DBD9"),
					Int = 2346,
					String = "5",

					SecondClassFirstSubClass = new A.SecondClassFirstSubClass()
					{
						Bool = true,
						DateTime = new DateTime(2016, 01, 02),
						Decimal = 32.10M,
						Guid = new Guid("8DA41611-A758-4EB7-A352-8D82FE84DBD9"),
						Int = 8,
						String = "7089"
					},
					SecondClassSecondSubClass = new A.SecondClassSecondSubClass()
					{
						Bool = true,
						DateTime = new DateTime(2017, 01, 02),
						Decimal = 56.10M,
						Guid = new Guid("8DA44611-A758-4EB7-A352-8D82FE844BD9"),
						Int = 5,
						String = "887"
					}
				}
			};

			A.BigClass destination = source.DeepCopyTo<A.BigClass>();

			Assert.AreNotEqual(source, destination);
			Assert.AreEqual(true, destination.Bool);
			Assert.AreEqual(new DateTime(2011, 01, 02), destination.DateTime);
			Assert.AreEqual(14.10M, destination.Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8D12FE84DBD9"), destination.Guid);
			Assert.AreEqual(4141, destination.Int);
			Assert.AreEqual("1142", destination.String);

			Assert.AreNotEqual(source.FirstClass, destination.FirstClass);
			Assert.AreEqual(true, destination.FirstClass.Bool);
			Assert.AreEqual(new DateTime(2012, 01, 02), destination.FirstClass.DateTime);
			Assert.AreEqual(66.10M, destination.FirstClass.Decimal);
			Assert.AreEqual(new Guid("8D24C611-A758-4EB7-A352-8D82FE84DBD9"), destination.FirstClass.Guid);
			Assert.AreEqual(54, destination.FirstClass.Int);
			Assert.AreEqual("8567", destination.FirstClass.String);

			Assert.AreNotEqual(source.FirstClass.FirstClassFirstSubClass, destination.FirstClass.FirstClassFirstSubClass);
			Assert.AreEqual(true, destination.FirstClass.FirstClassFirstSubClass.Bool);
			Assert.AreEqual(new DateTime(2013, 01, 02), destination.FirstClass.FirstClassFirstSubClass.DateTime);
			Assert.AreEqual(324.10M, destination.FirstClass.FirstClassFirstSubClass.Decimal);
			Assert.AreEqual(new Guid("8DA43611-A758-4EB7-A352-8D82FE84DBD9"), destination.FirstClass.FirstClassFirstSubClass.Guid);
			Assert.AreEqual(7345, destination.FirstClass.FirstClassFirstSubClass.Int);
			Assert.AreEqual("834", destination.FirstClass.FirstClassFirstSubClass.String);

			Assert.AreNotEqual(source.FirstClass.FirstClassSecondSubClass, destination.FirstClass.FirstClassSecondSubClass);
			Assert.AreEqual(true, destination.FirstClass.FirstClassSecondSubClass.Bool);
			Assert.AreEqual(new DateTime(2014, 01, 02), destination.FirstClass.FirstClassSecondSubClass.DateTime);
			Assert.AreEqual(523.10M, destination.FirstClass.FirstClassSecondSubClass.Decimal);
			Assert.AreEqual(new Guid("8DA4C611-A758-4EB7-A352-8482FE84DBD9"), destination.FirstClass.FirstClassSecondSubClass.Guid);
			Assert.AreEqual(235, destination.FirstClass.FirstClassSecondSubClass.Int);
			Assert.AreEqual("978", destination.FirstClass.FirstClassSecondSubClass.String);

			Assert.AreNotEqual(source.SecondClass, destination.SecondClass);
			Assert.AreEqual(true, destination.SecondClass.Bool);
			Assert.AreEqual(new DateTime(2015, 01, 02), destination.SecondClass.DateTime);
			Assert.AreEqual(78.10M, destination.SecondClass.Decimal);
			Assert.AreEqual(new Guid("8DA4C611-3758-4EB7-A352-8D82FE84DBD9"), destination.SecondClass.Guid);
			Assert.AreEqual(2346, destination.SecondClass.Int);
			Assert.AreEqual("5", destination.SecondClass.String);

			Assert.AreNotEqual(source.SecondClass.SecondClassFirstSubClass, destination.SecondClass.SecondClassFirstSubClass);
			Assert.AreEqual(true, destination.SecondClass.SecondClassFirstSubClass.Bool);
			Assert.AreEqual(new DateTime(2016, 01, 02), destination.SecondClass.SecondClassFirstSubClass.DateTime);
			Assert.AreEqual(32.10M, destination.SecondClass.SecondClassFirstSubClass.Decimal);
			Assert.AreEqual(new Guid("8DA41611-A758-4EB7-A352-8D82FE84DBD9"), destination.SecondClass.SecondClassFirstSubClass.Guid);
			Assert.AreEqual(8, destination.SecondClass.SecondClassFirstSubClass.Int);
			Assert.AreEqual("7089", destination.SecondClass.SecondClassFirstSubClass.String);

			Assert.AreNotEqual(source.SecondClass.SecondClassSecondSubClass, destination.SecondClass.SecondClassSecondSubClass);
			Assert.AreEqual(true, destination.SecondClass.SecondClassSecondSubClass.Bool);
			Assert.AreEqual(new DateTime(2017, 01, 02), destination.SecondClass.SecondClassSecondSubClass.DateTime);
			Assert.AreEqual(56.10M, destination.SecondClass.SecondClassSecondSubClass.Decimal);
			Assert.AreEqual(new Guid("8DA44611-A758-4EB7-A352-8D82FE844BD9"), destination.SecondClass.SecondClassSecondSubClass.Guid);
			Assert.AreEqual(5, destination.SecondClass.SecondClassSecondSubClass.Int);
			Assert.AreEqual("887", destination.SecondClass.SecondClassSecondSubClass.String);
		}
	}

	internal class Cow
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }
	}


	internal class Mule
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }
	}
}

namespace A
{
	internal class BigClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }

		public FirstClass FirstClass { get; set; }

		public SecondClass SecondClass { get; set; }
	}

	internal class FirstClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }

		public FirstClassFirstSubClass FirstClassFirstSubClass { get; set; }

		public FirstClassSecondSubClass FirstClassSecondSubClass { get; set; }
	}

	internal class FirstClassFirstSubClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }
	}

	internal class FirstClassSecondSubClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }
	}

	internal class SecondClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }

		public SecondClassFirstSubClass SecondClassFirstSubClass { get; set; }

		public SecondClassSecondSubClass SecondClassSecondSubClass { get; set; }
	}

	internal class SecondClassFirstSubClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }
	}

	internal class SecondClassSecondSubClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }
	}
}

namespace B
{
	internal class BigClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }

		public FirstClass FirstClass { get; set; }

		public SecondClass SecondClass { get; set; }
	}

	internal class FirstClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }

		public FirstClassFirstSubClass FirstClassFirstSubClass { get; set; }

		public FirstClassSecondSubClass FirstClassSecondSubClass { get; set; }
	}

	internal class FirstClassFirstSubClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }
	}

	internal class FirstClassSecondSubClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }
	}

	internal class SecondClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }

		public SecondClassFirstSubClass SecondClassFirstSubClass { get; set; }

		public SecondClassSecondSubClass SecondClassSecondSubClass { get; set; }
	}

	internal class SecondClassFirstSubClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }
	}

	internal class SecondClassSecondSubClass
	{
		public int Int { get; set; }

		public decimal Decimal { get; set; }

		public string String { get; set; }

		public DateTime DateTime { get; set; }

		public Guid Guid { get; set; }

		public bool Bool { get; set; }
	}
}