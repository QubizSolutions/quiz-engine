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