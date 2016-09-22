using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qubiz.QuizEngine.Services;
using Qubiz.QuizEngine.Areas.M.Controllers.Api;
using Moq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Areas.M.Models;

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
			QuestionPagedItem[] questionItems = new QuestionPagedItem[]
			{
				new QuestionPagedItem {ID = Guid.NewGuid(), Number = 1, Section = "Section 1" },
				new QuestionPagedItem {ID = Guid.NewGuid(), Number = 2, Section = "Section 2" }
			};

			QuestionPaged questionsPaged = new QuestionPaged
			{
				Items = questionItems,
				TotalCount = questionItems.Length
			};

			questionController.Request = new HttpRequestMessage(HttpMethod.Get, "api/Question/?itemsPerPage=2&pageNumber=1");
			questionController.Configuration = new HttpConfiguration();


			questionServiceMock.Setup(x => x.GetQuestionsByPageAsync(2, 1)).Returns(Task.FromResult(questionsPaged.DeepCopyTo<Qubiz.QuizEngine.Services.Models.PagedResult<Qubiz.QuizEngine.Services.Models.QuestionListItem>>()));

			IHttpActionResult actionResult = await questionController.Get(2, 1);

			QuestionPaged response = (actionResult as OkNegotiatedContentResult<QuestionPaged>).Content;

			Assert.AreEqual(typeof(OkNegotiatedContentResult<QuestionPaged>), actionResult.GetType());
			AssertAreEqual(questionsPaged.Items[0], response.Items[0]);
			AssertAreEqual(questionsPaged.Items[1], response.Items[1]);
			Assert.AreEqual(questionsPaged.TotalCount, response.TotalCount);
		}

		[TestMethod]
		public async Task Get_WhenNoQuestionsMatchTheSearch_ThenNoQuestionsAreReturned()
		{
			QuestionPaged questions = new QuestionPaged();

			questionController.Request = new HttpRequestMessage(HttpMethod.Get, "api/Question/?itemsPerPage=2&pageNumber=1");
			questionController.Configuration = new HttpConfiguration();

			questionServiceMock.Setup(x => x.GetQuestionsByPageAsync(2, 1)).Returns(Task.FromResult(questions.DeepCopyTo<Qubiz.QuizEngine.Services.Models.PagedResult<Qubiz.QuizEngine.Services.Models.QuestionListItem>>()));

			IHttpActionResult actionResult = await questionController.Get(2, 1);

			QuestionPaged response = (actionResult as OkNegotiatedContentResult<QuestionPaged>).Content;

			Assert.AreEqual(typeof(OkNegotiatedContentResult<QuestionPaged>), actionResult.GetType());
			Assert.AreEqual(0, response.Items.Length);
			Assert.AreEqual(0, response.TotalCount);
		}

		[TestMethod]
		public async Task Get_WhenGettingQuestionByID_ThenReturnQuestionByID()
		{
			Guid questionID = Guid.NewGuid();

			Option[] options = new Option[]
			{

				new Option {ID = Guid.NewGuid(), Answer = "This is a test", IsCorrectAnswer = true, Order =  1, QuestionID = questionID},
				new Option {ID = Guid.NewGuid(), Answer = "This is a test 2", IsCorrectAnswer = false, Order =  2, QuestionID = questionID},
			};

			Question question = new Question { ID = questionID, Complexity = 1, Number = 1, QuestionText = "This is a test", SectionID = Guid.NewGuid(), Type = QuestionType.SingleSelect, Options = options };

			questionController.Request = new HttpRequestMessage(HttpMethod.Get, "api/Question/?" + question.ID);
			questionController.Configuration = new HttpConfiguration();

			questionServiceMock.Setup(x => x.GetQuestionByID(question.ID)).Returns(Task.FromResult(question.DeepCopyTo<Qubiz.QuizEngine.Services.Models.QuestionDetail>()));

			IHttpActionResult actionResult = await questionController.Get(question.ID);

			Question response = (actionResult as OkNegotiatedContentResult<Question>).Content;

			Assert.AreEqual(typeof(OkNegotiatedContentResult<Question>), actionResult.GetType());
			AssertAreEqual(question, response);
			AssertAreEqual(question.Options[0], response.Options[0]);
			AssertAreEqual(question.Options[1], question.Options[1]);
		}

		[TestMethod]
		public async Task Get_WhenNoQuestionMatchTheSearch_ThenReturnNull()
		{
			Guid questionId = Guid.NewGuid();

			questionController.Request = new HttpRequestMessage(HttpMethod.Get, "api/Question/?" + questionId);
			questionController.Configuration = new HttpConfiguration();

			questionServiceMock.Setup(x => x.GetQuestionByID(questionId)).Returns(Task.FromResult((Qubiz.QuizEngine.Services.Models.QuestionDetail)null));

			IHttpActionResult actionResult = await questionController.Get(questionId);

			Question response = (actionResult as OkNegotiatedContentResult<Question>).Content;

			Assert.AreEqual(typeof(OkNegotiatedContentResult<Question>), actionResult.GetType());
			Assert.IsNull(response);
		}

		[TestMethod]
		public async Task Put_WhenUpdatingAnExistingQuestion_ThenOkStatusCodeIsReturned()
		{
			Guid questionID = Guid.NewGuid();
			Option[] options = new Option[]
			{

				new Option {ID = Guid.NewGuid(), Answer = "This is a test", IsCorrectAnswer = true, Order =  1, QuestionID = questionID},
				new Option {ID = Guid.NewGuid(), Answer = "This is a test 2", IsCorrectAnswer = false, Order =  2, QuestionID = questionID},
			};
			Question question = new Question { ID = questionID, Complexity = 1, Number = 1, QuestionText = "This is a test", SectionID = Guid.NewGuid(), Type = QuestionType.SingleSelect, Options = options };

			questionController.Request = new HttpRequestMessage(HttpMethod.Put, "api/Question/");
			questionController.Configuration = new HttpConfiguration();

			questionServiceMock.Setup(x => x.UpdateQuestionAsync(It.Is<Qubiz.QuizEngine.Services.Models.QuestionDetail>(s => (HaveEqualState(s, question))))).Returns(Task.CompletedTask);

			IHttpActionResult actionResult = await questionController.Put(question.DeepCopyTo<Question>());

			Assert.AreEqual(typeof(OkResult), actionResult.GetType());
		}

		[TestMethod]
		public async Task Post_WhenAddingQuestion_ThenOkStatusCodeIsReturned()
		{
			Guid questionID = Guid.NewGuid();
			Option[] options = new Option[]
			{

				new Option {ID = Guid.NewGuid(), Answer = "This is a test", IsCorrectAnswer = true, Order =  1, QuestionID = questionID},
				new Option {ID = Guid.NewGuid(), Answer = "This is a test 2", IsCorrectAnswer = false, Order =  2, QuestionID = questionID},
			};
			Question question = new Question { ID = questionID, Complexity = 1, Number = 1, QuestionText = "This is a test", SectionID = Guid.NewGuid(), Type = QuestionType.SingleSelect, Options = options };

			questionController.Request = new HttpRequestMessage(HttpMethod.Post, "api/Question/");
			questionController.Configuration = new HttpConfiguration();

			questionServiceMock.Setup(x => x.AddQuestionAsync(It.Is<Qubiz.QuizEngine.Services.Models.QuestionDetail>(s => (HaveEqualState(s, question))))).Returns(Task.CompletedTask);

			IHttpActionResult actionResult = await questionController.Post(question.DeepCopyTo<Question>());

			Assert.AreEqual(typeof(OkResult), actionResult.GetType());
		}

		[TestMethod]
		public async Task Delete_WhenDeletingQuestion_ThenOkStatusCodeIsReturned()
		{
			Guid questionID = Guid.NewGuid();
			Option[] options = new Option[]
			{

				new Option {ID = Guid.NewGuid(), Answer = "This is a test", IsCorrectAnswer = true, Order =  1, QuestionID = questionID},
				new Option {ID = Guid.NewGuid(), Answer = "This is a test 2", IsCorrectAnswer = false, Order =  2, QuestionID = questionID},
			};
			Question question = new Question { ID = questionID, Complexity = 1, Number = 1, QuestionText = "This is a test", SectionID = Guid.NewGuid(), Type = QuestionType.SingleSelect, Options = options };

			questionController.Request = new HttpRequestMessage(HttpMethod.Delete, "api/Question/");
			questionController.Configuration = new HttpConfiguration();

			questionServiceMock.Setup(x => x.DeleteQuestionAsync(question.ID)).Returns(Task.CompletedTask);

			IHttpActionResult actionResult = await questionController.Delete(question.ID);

			Assert.AreEqual(typeof(OkResult), actionResult.GetType());
		}

		private void AssertAreEqual(Question expected, Question actual)
		{
			Assert.AreEqual(expected.ID, actual.ID);
			Assert.AreEqual(expected.Number, actual.Number);
			Assert.AreEqual(expected.QuestionText, actual.QuestionText);
			Assert.AreEqual(expected.SectionID, actual.SectionID);
			Assert.AreEqual(expected.Type.ToString(), actual.Type.ToString());
		}

		private void AssertAreEqual(QuestionPagedItem expected, QuestionPagedItem actual)
		{
			Assert.AreEqual(expected.ID, actual.ID);
			Assert.AreEqual(expected.Number, actual.Number);
			Assert.AreEqual(expected.Section, actual.Section);
		}

		private void AssertAreEqual(Option expected, Option actual)
		{
			Assert.AreEqual(expected.ID, actual.ID);
			Assert.AreEqual(expected.IsCorrectAnswer, actual.IsCorrectAnswer);
			Assert.AreEqual(expected.Order, actual.Order);
			Assert.AreEqual(expected.QuestionID, actual.QuestionID);
			Assert.AreEqual(expected.Answer, actual.Answer);
		}

		private bool HaveEqualState(Qubiz.QuizEngine.Services.Models.QuestionDetail expected, Question actual)
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