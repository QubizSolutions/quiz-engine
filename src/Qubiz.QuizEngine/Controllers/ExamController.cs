using Qubiz.QuizEngine.Core;
using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Qubiz.QuizEngine.Controllers
{
    public class ExamController : ApiController
    {
		private readonly IExamRepository examRepository;
		private readonly ITestRepository testRepository;
		private readonly IExamService examService;

        public ExamController(IExamService examService, IExamRepository examRepository, ITestRepository testRepository)
        {
            this.examRepository = examRepository;
            this.testRepository = testRepository;
            this.examService = examService;
        }

        [Route("api/Exam/Get")]
        public IHttpActionResult Get(int pageNumber = 0, SortType? dateSort = null, SortType? scoreSort = null, Guid? testIDFilter = null, string nameFilter = "")
        {
            return Ok(examRepository.GetExamsFiltered(nameFilter, dateSort, scoreSort, testIDFilter, pageNumber));
        }
        [Route("api/Exam/GetExam")]
        public IHttpActionResult Get(Guid id)
        {
                ViewModels.ExamResult exam = examRepository.GetExamResult(id);

                TimeSpan duration = exam.EndDate - exam.StartDate;
                //var examPercentage = Regex.Match(exam.TotalScore ?? "", @"[\d\d]+%").Value;
                return Ok(new { ExamID = id, ExamDuration = duration.ToString(@"hh\:mm\:ss"), Title = exam.Title, CandidateName = exam.CandidateName, AllQuestions = exam.AllQuestions, TotalScore = exam.TotalScore, StartDate = exam.StartDate, EndDate = exam.EndDate, ExamAnswers = exam.ResultsPerSection, AnswersPerSection = exam.AnswersPerSection });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/Exam/TakeExam")]
        public IHttpActionResult TakeExam(Exam exam)
        {
            if (!Authorizer.IsAdmin(HttpContext.Current.User.Identity.Name) && string.IsNullOrWhiteSpace(exam.CandidateName))
                return StatusCode(HttpStatusCode.Unauthorized);

            exam.CandidateName = exam.CandidateName ?? HttpContext.Current.User.Identity.Name;

            ViewModels.TestDefinitionDetail test = testRepository.GetTestByID(exam.TestID);

            if (!test.IsPublished)
                return StatusCode(HttpStatusCode.Unauthorized);

            ViewModels.ExamDetail examCreated = examService.CreateExam(test, exam.CandidateName);

            examRepository.UpdateExam(examCreated);

            return Ok(new { CandidateName = examCreated.CandidateName, TestID = examCreated.TestID, ID = examCreated.ID, Answers = examCreated.Answers.Select(a => new { QuestionID = a.QuestionID }) });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/Exam/EndExam")]
        public IHttpActionResult EndExam(ViewModels.ExamDetail exam)
        {
            ViewModels.ExamDetail existingExam = examRepository.GetExamByID(exam.ID);

            if (existingExam == null)
                return StatusCode(HttpStatusCode.NotFound);

            if (existingExam.EndDate < DateTime.Now || existingExam.IsEnded)
                return StatusCode(HttpStatusCode.NotAcceptable);

            if (existingExam.Answers.Length != exam.Answers.Length || !existingExam.Answers.All(ea => exam.Answers.Any(a => a.QuestionID == ea.QuestionID)))
                return StatusCode(HttpStatusCode.NotAcceptable);

            examService.EvaluateAnswers(existingExam, exam);
            existingExam.EndDate = DateTime.Now;
            examRepository.UpdateExam(existingExam);

            ViewModels.TestDefinitionDetail test = testRepository.GetTestByID(exam.TestID);

            return Ok(test.ShowScoreWhenCompleted ? existingExam.TotalScore.ToString() : string.Empty);
        }
    }
}