using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qubiz.QuizEngine.Services;
using Qubiz.QuizEngine.Areas.M.Controllers.Api;
using Moq;
using System.Threading.Tasks;
using Qubiz.QuizEngine.Services.Models;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Qubiz.QuizEngine.Infrastructure;

namespace Qubiz.QuizEngine.UnitTesting.Web.Api
{
	[TestClass]
	public class QuestionControllerTest
	{
		private Mock<IQuestionService> questionServiceMock;
		private QuestionController questionController;

		[TestInitialize]
		public void TestInitialize()
		{
			questionServiceMock = new Mock<IQuestionService>(MockBehavior.Strict);
			questionController = new QuestionController(questionServiceMock.Object);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			questionServiceMock.VerifyAll();
		}

		[TestMethod]
		public async Task Get_WhenGettingAllQuestions_ThenReturnListOfQuestions()
		{
			QuestionListItem[] questions = new QuestionListItem[]
			{
				new QuestionListItem {ID = Guid.NewGuid(), Number = 1, Section = Guid.NewGuid().ToString()},
				new QuestionListItem {ID = Guid.NewGuid(), Number = 2, Section = Guid.NewGuid().ToString()},
			};

			PagedResult<QuestionListItem> questionList = new PagedResult<QuestionListItem> { Items = questions, TotalCount = questions.Length };

			questionController.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/Qubiz.QuizEngine/M/api/Question/Get/?itemsPerPage=2&pageNumber=1");
			questionController.Configuration = new HttpConfiguration();

			questionServiceMock.Setup(x => x.GetQuestionsByPageAsync(2, 1)).Returns(Task.FromResult(questionList));

			IHttpActionResult actionResult = await questionController.Get(2, 1);

			var response = actionResult as OkNegotiatedContentResult<PagedResult<QuestionListItem>>;

			Assert.AreEqual(typeof(OkNegotiatedContentResult<PagedResult<QuestionListItem>>), actionResult.GetType());
			Assert.AreEqual(questions[0].ID, response.Content.Items[0].ID);
			Assert.AreEqual(questions[0].Number, response.Content.Items[0].Number);
			Assert.AreEqual(questions[0].Section, response.Content.Items[0].Section);
			Assert.AreEqual(questions[1].ID, response.Content.Items[1].ID);
			Assert.AreEqual(questions[1].Number, response.Content.Items[1].Number);
			Assert.AreEqual(questions[1].Section, response.Content.Items[1].Section);
		}

		[TestMethod]
		public async Task Get_WhenGettingUnexistingQuestions_ThenReturnEmptyList()
		{
			PagedResult<QuestionListItem> questions = new PagedResult<QuestionListItem>();

			questionController.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/Qubiz.QuizEngine/M/api/Question/Get/?itemsPerPage=2&pageNumber=1");
			questionController.Configuration = new HttpConfiguration();

			questionServiceMock.Setup(x => x.GetQuestionsByPageAsync(2, 1)).Returns(Task.FromResult(questions));

			IHttpActionResult actionResult = await questionController.Get(2, 1);

			var response = actionResult as OkNegotiatedContentResult<PagedResult<QuestionListItem>>;

			Assert.AreEqual(typeof(OkNegotiatedContentResult<PagedResult<QuestionListItem>>), actionResult.GetType());
			Assert.IsNull(response.Content.Items);
		}

		[TestMethod]
		public async Task Get_WhenGettingQuestionByID_ThenReturnQuestionByID()
		{
			Guid questionID = Guid.NewGuid();

			OptionDefinition[] options = new OptionDefinition[]
			{

				new OptionDefinition {ID = Guid.NewGuid(), Answer = "This is a test", IsCorrectAnswer = true, Order =  1, QuestionID = questionID},
				new OptionDefinition {ID = Guid.NewGuid(), Answer = "This is a test 2", IsCorrectAnswer = false, Order =  2, QuestionID = questionID},
			};

			QuestionDetail question = new QuestionDetail { ID = questionID, Complexity = 1, Number = 1, QuestionText = "This is a test", SectionID = Guid.NewGuid(), Type = QuestionType.SingleSelect, Options = options };

			questionController.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/Qubiz.QuizEngine/M/api/Question/Get/?" + question.ID);
			questionController.Configuration = new HttpConfiguration();

			questionServiceMock.Setup(x => x.GetQuestionByID(question.ID)).Returns(Task.FromResult(question));

			IHttpActionResult actionResult = await questionController.Get(question.ID);

			var response = actionResult as OkNegotiatedContentResult<QuestionDetail>;

			Assert.AreEqual(typeof(OkNegotiatedContentResult<QuestionDetail>), actionResult.GetType());
			AssertAreEqual(question, response.Content);
		}

		[TestMethod]
		public async Task Get_WhenGettingUnexistentQuestion_ThenReturnNull()
		{
			Guid newId = Guid.NewGuid();

			questionController.Request = new HttpRequestMessage(HttpMethod.Get, "localhost/Qubiz.QuizEngine/M/api/Question/Get/?" + newId);
			questionController.Configuration = new HttpConfiguration();

			questionServiceMock.Setup(x => x.GetQuestionByID(newId)).Returns(Task.FromResult((QuestionDetail)null));

			IHttpActionResult actionResult = await questionController.Get(newId);

			var response = actionResult as OkNegotiatedContentResult<QuestionDetail>;

			Assert.AreEqual(typeof(OkNegotiatedContentResult<QuestionDetail>), actionResult.GetType());
			Assert.IsNull(response.Content);
		}

		[TestMethod]
		public async Task Put_WhenUpdateExistingQuestion_ReturnOk()
		{
			Guid questionID = Guid.NewGuid();
			OptionDefinition[] options = new OptionDefinition[]
			{

				new OptionDefinition {ID = Guid.NewGuid(), Answer = "This is a test", IsCorrectAnswer = true, Order =  1, QuestionID = questionID},
				new OptionDefinition {ID = Guid.NewGuid(), Answer = "This is a test 2", IsCorrectAnswer = false, Order =  2, QuestionID = questionID},
			};
			QuestionDetail question = new QuestionDetail { ID = questionID, Complexity = 1, Number = 1, QuestionText = "This is a test", SectionID = Guid.NewGuid(), Type = QuestionType.SingleSelect, Options = options };

			questionController.Request = new HttpRequestMessage(HttpMethod.Put, "http://localhost/Qubiz.QuizEngine/M/api/Question/Put/");
			questionController.Configuration = new HttpConfiguration();

			questionServiceMock.Setup(x => x.UpdateQuestionAsync(It.Is<QuestionDetail>(s => (HaveEqualState(s, question))))).Returns(Task.FromResult(question));

			IHttpActionResult actionResult = await questionController.Put(question.DeepCopyTo<Qubiz.QuizEngine.Areas.M.Models.QuestionDetail>());

			Assert.AreEqual(typeof(OkResult), actionResult.GetType());
		}

		[TestMethod]
		public async Task Post_WhenAddingQuestion_ThenReturnOk()
		{
			Guid questionID = Guid.NewGuid();
			OptionDefinition[] options = new OptionDefinition[]
			{

				new OptionDefinition {ID = Guid.NewGuid(), Answer = "This is a test", IsCorrectAnswer = true, Order =  1, QuestionID = questionID},
				new OptionDefinition {ID = Guid.NewGuid(), Answer = "This is a test 2", IsCorrectAnswer = false, Order =  2, QuestionID = questionID},
			};
			QuestionDetail question = new QuestionDetail { ID = questionID, Complexity = 1, Number = 1, QuestionText = "This is a test", SectionID = Guid.NewGuid(), Type = QuestionType.SingleSelect, Options = options };

			questionController.Request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/Qubiz.QuizEngine/M/api/Question/Post/");
			questionController.Configuration = new HttpConfiguration();

			questionServiceMock.Setup(x => x.AddQuestionAsync(It.Is<QuestionDetail>(s => (HaveEqualState(s, question))))).Returns(Task.FromResult(question));

			IHttpActionResult actionResult = await questionController.Post(question.DeepCopyTo<Qubiz.QuizEngine.Areas.M.Models.QuestionDetail>());

			Assert.AreEqual(typeof(OkResult), actionResult.GetType());
		}

		[TestMethod]
		public async Task Delete_WhenDeletingQuestion_ThenReturnOk()
		{
			Guid questionID = Guid.NewGuid();
			OptionDefinition[] options = new OptionDefinition[]
			{

				new OptionDefinition {ID = Guid.NewGuid(), Answer = "This is a test", IsCorrectAnswer = true, Order =  1, QuestionID = questionID},
				new OptionDefinition {ID = Guid.NewGuid(), Answer = "This is a test 2", IsCorrectAnswer = false, Order =  2, QuestionID = questionID},
			};
			QuestionDetail question = new QuestionDetail { ID = questionID, Complexity = 1, Number = 1, QuestionText = "This is a test", SectionID = Guid.NewGuid(), Type = QuestionType.SingleSelect, Options = options };

			questionController.Request = new HttpRequestMessage(HttpMethod.Delete, "http://localhost/Qubiz.QuizEngine/M/api/Question/Delete/");
			questionController.Configuration = new HttpConfiguration();

			questionServiceMock.Setup(x => x.DeleteQuestionAsync(question.ID)).Returns(Task.FromResult(question));

			IHttpActionResult actionResult = await questionController.Delete(question.ID);

			Assert.AreEqual(typeof(OkResult), actionResult.GetType());
		}

		private void AssertAreEqual(QuestionDetail expected, QuestionDetail actual)
		{
			Assert.AreEqual(expected.ID, actual.ID);
			Assert.AreEqual(expected.Number, actual.Number);
			Assert.AreEqual(expected.QuestionText, actual.QuestionText);
			Assert.AreEqual(expected.SectionID, actual.SectionID);
			Assert.AreEqual(expected.Type.ToString(), actual.Type.ToString());
			CollectionAssertAreEqual(expected.Options, actual.Options);
		}

		private void CollectionAssertAreEqual(OptionDefinition[] expected, OptionDefinition[] actual)
		{
			Assert.AreEqual(expected[0].ID, actual[0].ID);
			Assert.AreEqual(expected[0].Answer, actual[0].Answer);
			Assert.AreEqual(expected[0].IsCorrectAnswer, actual[0].IsCorrectAnswer);
			Assert.AreEqual(expected[0].Order, actual[0].Order);
			Assert.AreEqual(expected[0].QuestionID, actual[0].QuestionID);

			Assert.AreEqual(expected[1].ID, actual[1].ID);
			Assert.AreEqual(expected[1].Answer, actual[1].Answer);
			Assert.AreEqual(expected[1].IsCorrectAnswer, actual[1].IsCorrectAnswer);
			Assert.AreEqual(expected[1].Order, actual[1].Order);
			Assert.AreEqual(expected[1].QuestionID, actual[1].QuestionID);
		}

		private bool HaveEqualState(QuestionDetail expected, QuestionDetail actual)
		{
			return expected.ID == actual.ID
				&& expected.Number == actual.Number
				&& expected.QuestionText == actual.QuestionText
				&& expected.SectionID == actual.SectionID
				&& expected.Type.ToString() == actual.Type.ToString()
				&& expected.Options.Length == actual.Options.Length;
		}
	}
}