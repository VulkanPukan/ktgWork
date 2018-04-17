using StrengthTracker2.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StrengthTracker2.Core.Repository.Contracts.AthleteManagement;
using StructureMap;
using StrengthTracker2.Web.Helpers;
using StrengthTracker2.Core.Repository.Contracts.Account;
using System.Globalization;
using System.Web.Http;
using System.Web.Script.Serialization;
using StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Core.Repository.Entities.WorkoutManagement;
using StrengthTracker2.Core.Functional.Contracts.AthleteManagement;
using StrengthTracker2.Core.Repository.Contracts.Workout;
using StrengthTracker2.Core.Repository.Entities.ProgramManagement;
using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Web.Models.Coach;
using StrengthTracker2.Common;
using MyProgrammer.Core.Services;
using StrengthTracker2.Web.Models.Admin;
using System.Configuration;

namespace StrengthTracker2.Web.Controllers
{
    public class CoachController : Controller
    {
        private IAthleteManagementRepository _iAthleteManagementRepository;
        private readonly IAccount _iAccount;
        private readonly ISportsManagementRepository _iSportMgmtRepository;
        private IPositionMgmtRepository _iPositionMgmtRepository;
        private IState _iStateRepository;
        private readonly IAthleteManagementRepository _iAthleteMgmtRepository;
        private readonly IWorkoutManagement _iWorkoutMgmt;
        private readonly IMapperService _mapper;
        private readonly IState _stateService;
        private readonly IAthleteCalculationService _athleteCalculationService;
        private readonly IProgramManagementRepository _progMgmtRepository;
        private IMailService _mailSrvc = null;
        private IAthleteFilterRepository AthleteFilterRepository = ObjectFactory.GetInstance<IAthleteFilterRepository>();
        private IUserDataVisibilityRepository userDataVisibilityRepository;
        private ICustomerCategoryTypeFilter customerCategoryTypeFilter;


        //
        // GET: /Coach/

        public CoachController()
        {
            _iAccount = ObjectFactory.GetInstance<IAccount>();
            _iSportMgmtRepository = ObjectFactory.GetInstance<ISportsManagementRepository>();
            _iAthleteManagementRepository = ObjectFactory.GetInstance<IAthleteManagementRepository>();

            _mapper = new MapperService();
            _stateService = ObjectFactory.GetInstance<IState>();
            _athleteCalculationService = ObjectFactory.GetInstance<IAthleteCalculationService>();
            _progMgmtRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();
            _iWorkoutMgmt = ObjectFactory.GetInstance<IWorkoutManagement>();
            userDataVisibilityRepository = ObjectFactory.GetInstance<IUserDataVisibilityRepository>();
            customerCategoryTypeFilter = ObjectFactory.GetInstance<ICustomerCategoryTypeFilter>();
        }
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult GetMaturityCalculation([FromUri]AssesmentProfileViewModel model)
        {
            var user = _iAccount.GetUserInfoByID(model.UserId).Users.FirstOrDefault();
            var assessmentProfile = _mapper.Map<AssesmentProfileViewModel, AssessmentProfile>(model);
            assessmentProfile.Gender = user != null ? user.Gender : Gender.Unspecified;
            assessmentProfile.DOB = user != null ? user.DOB : DateTime.Now;

            var calculatedProfile = _athleteCalculationService.CalculateAssesmentProfile(assessmentProfile);
            var calculatedProfileViewModel = _mapper.Map<AssessmentProfile, AssesmentProfileViewModel>(calculatedProfile);

            return Json(calculatedProfileViewModel, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public void SaveMaturityCalculation([FromBody]AssesmentProfileViewModel model)
        {
            var profile = _mapper.Map<AssesmentProfileViewModel, AssessmentProfile>(model);
            profile.AssessmentDate = DateTime.Now;

            _iAthleteManagementRepository.SaveAssessmentProfile(profile);
        }


        [System.Web.Mvc.HttpPost]
        public JsonResult SaveStrengthTracker(List<AssessmentExerciseModel> model, int UserId, bool completeSession)
        {
            var lstSAQDetail = new List<SAQDetail>();

            var user = _iAccount.GetFullUserInfoByUserID(UserId, true, true, true);
            var localUser = user.Users.FirstOrDefault();

            var saqDetails = _iWorkoutMgmt.GetSAQStivityDetails(true, true, true, true, true, localUser.DOB.GetUserAge(),
               Convert.ToInt32(localUser.Gender));

            var lstSAQExercises = saqDetails.saqstivityexercises;
            var masterSession = _iAthleteManagementRepository.GetUserSAQSMasterSessions(UserId).FirstOrDefault();


            if (model != null)
            {
                foreach (var exer in model)
                {
                    if (exer.ActualScore > 0)
                    {
                        var saqDet = new SAQDetail
                        {
                            Id = exer.Id,
                            UserID = UserId,
                            MovementTypeID = exer.MovementTypeId,
                            ExerciseID = exer.ExerciseId,
                            UOMID = exer.UOMId,
                            ActualScore = exer.ActualScore,
                            SessionNumber = 1, //TODO: temporary value
                            //SAQQuotient = decimal.Parse(exer.SAQQuotient),
                            NormativeData =
                                lstSAQExercises.Where(se => se.AssessmentExerciseID == exer.ExerciseId)
                                    .Select(se => se.NormativeData)
                                    .FirstOrDefault(),
                            SAQStivitySessionMasterID = masterSession == null ? 0 : masterSession.ID // how can I select teh proper mestewr session? Can it be null?
                        };

                        lstSAQDetail.Add(saqDet);
                    }
                }
            }

            decimal quotient = 0;
            if (lstSAQDetail.Any())
            {
                _iWorkoutMgmt.AddUserSAQQDetails(lstSAQDetail);
                if (completeSession)
                {
                    _iWorkoutMgmt.CompleteSAQSession(lstSAQDetail, localUser.UserId);
                }
            }

            if (user.SAQDetails != null)
            {
                quotient = user.SAQDetails.Average(q => q.SAQQuotient) * 100;
            }

            return Json(new IA_ReturnObjectModel()
            {
                Status = ReturnStatus.OK
                ,
                Message = "Strength Tracker data is saved successfully."
                ,
                Quotient = quotient
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult InitialAssessment(int id)
        {
            var user = _iAccount.GetFullUserInfoByUserID(id, true, true, true);

            var profile = _mapper.Map<AssessmentProfile, AssesmentProfileViewModel>(user.Assessments.FirstOrDefault());

            profile.Exercises = new List<AssessmentExerciseModel>();

            var localUser = user.Users.FirstOrDefault();
            var saqDetails = _iWorkoutMgmt.GetSAQStivityDetails(true, true, true, true, true, localUser.DOB.GetUserAge(),
                Convert.ToInt32(localUser.Gender));

            profile.Quotient = "0";

            var masterSession = _iAthleteManagementRepository.GetUserSAQSMasterSessions(id).FirstOrDefault();
            profile.Completed = masterSession != null
                ? masterSession.SAQStivitySessionStatus == SAQSTivitySessionStatus.Complete
                : false;

            if (user.SAQDetails != null)
            {

                foreach (var details in user.SAQDetails)
                {
                    var exerciseModel = new AssessmentExerciseModel()
                    {
                        Id = details.Id,
                        MovementType = saqDetails.movementTypes.FirstOrDefault(m => m.Id == details.MovementTypeID).Name,
                        MovementTypeId = details.MovementTypeID,
                        ExerciseName = saqDetails.exercises.FirstOrDefault(e => e.Id == details.ExerciseID).Name,
                        ExerciseId = details.ExerciseID,
                        UOM = saqDetails.uoms.FirstOrDefault(u => u.Id == details.UOMID).Name,
                        UOMId = details.UOMID,
                        ActualScore = details.ActualScore,
                        SAQQuotient = details.SAQQuotient.ToString()
                    };
                    profile.Exercises.Add(exerciseModel);
                }

                var avg = profile.Exercises.Average(s => decimal.Parse(s.SAQQuotient));
                profile.Quotient = Math.Round(Convert.ToDecimal(avg) * 100, 2).ToString();
            }

            if (profile.Exercises.Count < 11)
            {
                foreach (var assmntExercise in saqDetails.AssessmentExercises)
                {
                    if (saqDetails.SAQDetails == null || !saqDetails.SAQDetails.Any(x => x.ExerciseID == assmntExercise.ID))
                    {
                        var sqDet = new AssessmentExerciseModel
                        {
                            MovementType = saqDetails.movementTypes.FirstOrDefault(m => m.Id == assmntExercise.MovementTypeID).Name,
                            MovementTypeId = assmntExercise.MovementTypeID,
                            ExerciseName = assmntExercise.Title,
                            ExerciseId = assmntExercise.ID,
                            UOM = saqDetails.uoms.FirstOrDefault(u => u.Id == assmntExercise.UOMID).Name,
                            UOMId = assmntExercise.UOMID,
                            ActualScore = 0
                        };
                        profile.Exercises.Add(sqDet);
                    }
                }
            }
            return View(profile);
        }

        public ActionResult AthleteProfile(int UserId)
        {
            var user = _iAthleteManagementRepository.GetAthleteInfoByID(UserId);
            var model = _mapper.Map<User, AthleteProfileViewModel>(user);
            model = _mapper.Map<Contact, AthleteProfileViewModel>(user.ContactInformation, model);
            model.AvailableSport = _iSportMgmtRepository.ListSports(false);
            model.AthleteDefaultSportId = user.SportID;
            model.States = _stateService.ListStates().OrderBy(s => s.Abbreviation).ToList();
            model.TeamName = model.TeamName;
            model.CoachName = model.CoachID == 0 ? "No Coach" : user.Coach.FirstName + " " + user.Coach.LastName;
            if (model.DOB > DateTime.Now)
            {
                model.DOB = null;
            }
            return View(model);
        }

        public ActionResult Schedule()
        {
            return View();
        }

        public ActionResult Holiday()
        {
            return View();
        }

        public ActionResult Capacity()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult DashboardNew()
        {
            if (Session["AuthenticatedUser"] != null)
            {
                int userId = ((StrengthTracker2.Core.Repository.Entities.Actors.User)(Session["AuthenticatedUser"])).UserId;
                var coachs = _iAccount.ListUsersByType(UserType.Coach);
                var currentCoach = coachs.FirstOrDefault(c => c.UserId == userId);
                var dashboardModel = new CoachDashboardModel();
                if (currentCoach != null)
                {
                    dashboardModel.CurrentCoach = new CoachDashboardModel.CoachListModel()
                    {
                        Id = currentCoach.UserId,
                        Name = String.Format("{0} {1}", currentCoach.FirstName, currentCoach.LastName)
                    };
                    if (currentCoach.IsAthleticDirector || currentCoach.IsStrengthCoach)
                    {
                        dashboardModel.IsCoachListDisabled = false;
                    }
                }
                else
                    dashboardModel.CurrentCoach = new CoachDashboardModel.CoachListModel()
                    {
                        Id = 0,
                        Name = "All coaches"
                    };
                dashboardModel.CoachList.Add(new CoachDashboardModel.CoachListModel()
                {
                    Id = 0,
                    Name = "All coaches"
                });
                foreach (var coach in coachs.OrderBy(c => c.FirstName))
                    dashboardModel.CoachList.Add(new CoachDashboardModel.CoachListModel()
                    {
                        Id = coach.UserId,
                        Name = String.Format("{0} {1}", coach.FirstName, coach.LastName)
                    });
                return View(dashboardModel);
            }
            return View(new CoachDashboardModel());
        }

        public ActionResult AthleteDashboard()
        {
            return View();
        }

        public JsonResult ChartData(string chartType)
        {
            string result = "";
            if (chartType == "bar")
            {
                BarChartDataModel bdm = new BarChartDataModel();
                bdm.color = "#28d8b2";
                //bdm.label = "Sales";

                bdm.data = new List<string[,]>();
                //string[,] array2Db = new string[1, 2] { { "Jan", "27" } };
                //bdm.data.Add(array2Db);
                //array2Db = new string[1, 2] { { "Feb", "82" } };
                //bdm.data.Add(array2Db);
                //array2Db = new string[1, 2] { { "Mar", "56" } };
                //bdm.data.Add(array2Db);
                //array2Db = new string[1, 2] { { "Apr", "14" } };
                //bdm.data.Add(array2Db);
                //array2Db = new string[1, 2] { { "May", "28" } };
                //bdm.data.Add(array2Db);
                //array2Db = new string[1, 2] { { "Jun", "77" } };
                //bdm.data.Add(array2Db);
                //array2Db = new string[1, 2] { { "Jul", "23" } };
                //bdm.data.Add(array2Db);
                //array2Db = new string[1, 2] { { "Aug", "49" } };
                //bdm.data.Add(array2Db);
                //array2Db = new string[1, 2] { { "Sep", "81" } };
                //bdm.data.Add(array2Db);
                //array2Db = new string[1, 2] { { "Oct", "20" } };
                //bdm.data.Add(array2Db);


                string[,] array2Db = new string[1, 2] { { "Baseball", "19" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Basketball", "12" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Cricket", "21" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Soccer", "12" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hockey", "25" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Football", "30" } };
                bdm.data.Add(array2Db);

                List<BarChartDataModel> lstBDM = new List<BarChartDataModel>();
                lstBDM.Add(bdm);
                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }
            else if (chartType == "line")
            {
                List<BarChartDataModel> lstBDM = new List<BarChartDataModel>();

                BarChartDataModel bdm = new BarChartDataModel();
                bdm.color = "#5ab1ef";
                bdm.label = "Completed";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "Jan", "27" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Feb", "188" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Mar", "183" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Apr", "185" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "May", "199" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jun", "190" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jul", "194" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Aug", "194" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Sep", "184" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct", "74" } };
                bdm.data.Add(array2Db);

                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#d87a80";
                bdm.label = "Rescheduled";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "Jan", "111" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Feb", "97" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Mar", "93" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Apr", "110" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "May", "102" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jun", "93" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jul", "92" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Aug", "92" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Sep", "92" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct", "44" } };
                bdm.data.Add(array2Db);

                lstBDM.Add(bdm);

                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }
            else if (chartType == "pie")
            {
                List<PieChartDataModel> lstPDM = new List<PieChartDataModel>();

                PieChartDataModel pdm = new PieChartDataModel();
                pdm.color = "#4acab4";
                pdm.label = "Program1";
                pdm.data = "30";
                lstPDM.Add(pdm);

                pdm = new PieChartDataModel();
                pdm.color = "#ffea88";
                pdm.label = "Program2";
                pdm.data = "40";
                lstPDM.Add(pdm);

                pdm = new PieChartDataModel();
                pdm.color = "#ff8153";
                pdm.label = "Program3";
                pdm.data = "90";
                lstPDM.Add(pdm);

                pdm = new PieChartDataModel();
                pdm.color = "#878bb6";
                pdm.label = "Program4";
                pdm.data = "75";
                lstPDM.Add(pdm);

                pdm = new PieChartDataModel();
                pdm.color = "#b2d767";
                pdm.label = "Program5";
                pdm.data = "120";
                lstPDM.Add(pdm);

                return Json(lstPDM, JsonRequestBehavior.AllowGet);
            }
            else if (chartType == "stacked")
            {
                List<BarChartDataModel> lstBDM = new List<BarChartDataModel>();

                BarChartDataModel bdm = new BarChartDataModel();
                bdm.color = "#4acab4";
                bdm.label = "No. of Atheletes";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "Baseball", "9" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Basketball", "12" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Cricket", "11" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Football", "12" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hockey", "15" } };
                bdm.data.Add(array2Db);

                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#d87a80";
                bdm.label = "No. of Sports";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "Baseball", "4" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Basketball", "3" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Cricket", "9" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Football", "7" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hockey", "8" } };
                bdm.data.Add(array2Db);

                lstBDM.Add(bdm);

                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }
            else if (chartType == "barMultiple")
            {
                List<BarChartDataModel> lstBDM = new List<BarChartDataModel>();

                BarChartDataModel bdm = new BarChartDataModel();
                bdm.color = "#5ab1ef";
                bdm.label = "Elite";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "Hip Thrust", "110" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hex Bar Deadlift", "100" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Back Squat", "70" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "BB Bench Press", "50" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Pull Ups", "50" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Farmer's Walk", "50" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Bent Over Row", "42" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hang Clean", "38" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Seated DB Overhead", "19" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "DB Renegade Row", "18" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#b2d767";
                bdm.label = "You";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "Hip Thrust", "115" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hex Bar Deadlift", "100" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Back Squat", "72" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "BB Bench Press", "40" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Pull Ups", "60" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Farmer's Walk", "24" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Bent Over Row", "41" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hang Clean", "39" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Seated DB Overhead", "19" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "DB Renegade Row", "17" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }
            else if (chartType == "barMultiple2")
            {
                List<BarChartDataModel> lstBDM = new List<BarChartDataModel>();

                BarChartDataModel bdm = new BarChartDataModel();
                bdm.color = "#73B5DE";
                bdm.label = "Elite";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "Hip Thrust", "1.1" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hex Bar Deadlift", "1" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Back Squat", "0.714" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "BB Bench Press", "0.556" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Pull Ups", "0.556" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Farmer's Walk", "0.53" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Bent Over Row", "0.43" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hang Clean", "0.38" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Seated DB Overhead", "0.19" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "DB Renegade Row", "0.155" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#ffaf36";
                bdm.label = "You";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "Hip Thrust", "1.125" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hex Bar Deadlift", "1" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Back Squat", "0.72727277" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "BB Bench Press", "0.397272" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Pull Ups", "0.60227" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Farmer's Walk", "0.272727" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Bent Over Row", "0.42045" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hang Clean", "0.38636" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Seated DB Overhead", "0.147727" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "DB Renegade Row", "0.125" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }
            else if (chartType == "barStackedAtheleteBysport")
            {
                List<BarChartDataModel> lstBDM = new List<BarChartDataModel>();

                BarChartDataModel bdm = new BarChartDataModel();
                bdm.color = "#73B5DE";
                bdm.label = "Baseball";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "Oct-03-2016", "40" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-05-2016", "25" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-07-2016", "30" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-10-2016", "22" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-12-2016", "25" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#ffaf36";
                bdm.label = "Basketball";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "Oct-03-2016", "30" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-05-2016", "20" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-07-2016", "25" } };
                bdm.data.Add(array2Db);
                //array2Db = new string[1, 2] { { "Oct-07-2016", "Football" } };
                //bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-12-2016", "22" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#4acab4";
                bdm.label = "Cricket";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "Oct-03-2016", "20" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-05-2016", "15" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-07-2016", "15" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-10-2016", "10" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-12-2016", "20" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#b2d767";
                bdm.label = "Football";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "Oct-03-2016", "10" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-05-2016", "8" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-07-2016", "12" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-10-2016", "2" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-12-2016", "15" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }
            else if (chartType == "barStackedAtheleteSportVsTeam")
            {
                List<BarChartDataModel> lstBDM = new List<BarChartDataModel>();

                BarChartDataModel bdm = new BarChartDataModel();
                bdm.color = "#73B5DE";
                bdm.label = "Team A";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "Baseball", "40" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Basketball", "20" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Football", "25" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Cricket", "15" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#ff8baf";
                bdm.label = "GV";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "Baseball", "22" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Basketball", "15" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Football", "15" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Cricket", "12" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#45d3f9";
                bdm.label = "BV";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "Baseball", "15" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Basketball", "12" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Football", "10" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Cricket", "8" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#4b3e6f";
                bdm.label = "B Group";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "Baseball", "5" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Basketball", "10" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Football", "3" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Cricket", "5" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }
            else if (chartType == "barStackedTeamSessionRPE")
            {
                List<BarChartDataModel> lstBDM = new List<BarChartDataModel>();

                BarChartDataModel bdm = new BarChartDataModel();
                bdm.color = "#cfdd86";
                bdm.label = "Individual High";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "1", "40" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "2", "30" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "3", "25" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "4", "30" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "5", "25" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#9c7670";
                bdm.label = "Team Average";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "1", "30" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "2", "25" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "3", "15" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "4", "20" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "5", "15" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#b7e5d7";
                bdm.label = "Individual Low";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "1", "25" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "2", "22" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "3", "5" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "4", "15" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "5", "12" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }
            else if (chartType == "barStackedFloatTeamVolume")
            {
                List<BarChartDataModel> lstBDM = new List<BarChartDataModel>();

                BarChartDataModel bdm = new BarChartDataModel();
                bdm.color = "#cfdd86";
                bdm.label = "";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "Oct-03-2016", "28" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-05-2016", "30" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-07-2016", "32" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-10-2016", "35" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-12-2016", "40" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct-14-2016", "50" } };
                bdm.data.Add(array2Db);

                lstBDM.Add(bdm);


                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Authorize]
        public JsonResult SaveAthlete([FromBody]AthleteProfileViewModel model)
        {
            ReturnObjectModel ro = new ReturnObjectModel();
            ro.Message = "User Update failed";
            ro.Status = ReturnStatus.Err;

            var athlete = _iAthleteManagementRepository.GetAthleteInfoByID(model.UserId);
            athlete = _mapper.Map<AthleteProfileViewModel, User>(model, athlete);
            athlete.ContactInformation = _mapper.Map<AthleteProfileViewModel, Contact>(model);
            athlete.TeamID = model.TeamID;
            athlete.CoachID = model.CoachID;
            UserDetails userDetails = new UserDetails();

            athlete.SportID = model.AthleteDefaultSportId;

            userDetails.Users.Add(athlete);
            userDetails.Contacts.Add(athlete.ContactInformation);
            userDetails.UserImages.Add(athlete.UserImage);

            if (athlete.UserId > 0)
            {
                var result = _iAccount.SaveUserInfo(userDetails);
                if (result)
                {
                    ro.Status = ReturnStatus.OK;
                    ro.Message = "User saved successfully";
                }
            }
            else
            {
                var result = _iAccount.AddUser(userDetails);
                if (result != null)
                {
                    ro.Status = ReturnStatus.OK;
                    ro.Message = "User added successfully";
                }
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAthletes([DataSourceRequest]DataSourceRequest request)
        {
            var viewModels = new List<AthleteViewModel>();

            //var list = _iAthleteManagementRepository.ListAllAthletes();
            var currentUser = (User)Session["AuthenticatedUser"];
            var customerCategoryType = ((StrengthTracker2.Core.DataTypes.CustomerCategoryType)(Session["CustomerType"]));
            var dataVisibility = userDataVisibilityRepository.GetUserDataVisibilityListByUserId(currentUser.UserId);
            dataVisibility = customerCategoryTypeFilter.FilterUserDataVisibilty(dataVisibility, customerCategoryType, currentUser.UserId);
            var list = AthleteFilterRepository.GetFilteredAthleteByDataVisibility(currentUser.UserId, dataVisibility);
            foreach (var item in list)
            {
                var userDetails = _iAccount.GetFullUserInfoByUserID(item.UserId, true, true, true);
                var lstSAQSSessionMaster = _iAthleteManagementRepository.GetUserSAQSMasterSessions(item.UserId);

                var target = new AthleteViewModel();
                target.UserID = item.UserId;
                target.Age = Helpers.Common.CalculateAge(item.DOB);
                target.AthleteName = string.Concat(item.FirstName, " ", item.LastName);
                target.Email = item.UserName;
                target.UserImagePath = item.UserImage.ImagePath;
                target.IsActive = item.IsAccountEnabled;
                target.School = item.SchoolName;
                target.Phone = item.ContactInformation.PhoneNumber;
                target.Sport = item.Sport.Name;
                target.Program = item.Program.Title;
                target.IsActive = item.IsAccountEnabled;
                if (item.IsAccountEnabled)
                {
                    target.PendingStatus = "Active";
                }
                else
                {
                    target.PendingStatus = "Deactivated";

                    if (item.IsPending)
                    {
                        target.PendingStatus = "Pending";
                    }

                }
                target.StatusDisplayIcon = item.IsAccountEnabled == true ? "<i class='fa fa-circle' aria-hidden='true' style='color:green;'></i>"
                                                                        : "<i class='fa fa-circle' aria-hidden='true'></i>";
                target.DisplayActiveText = item.IsAccountEnabled == true ? "Deactivate" : "Activate";

                var userRegistration = _iAccount.GetRegistrationForUser(item.UserId);
                if (userRegistration != null)
                    target.LocationId = userRegistration.LocationId;
                //if (userDetails != null && userDetails.RegistrationInfo != null)
                //    target.LocationId = userDetails.RegistrationInfo.LocationId;

                target.HasHistoricalAssessment = lstSAQSSessionMaster != null && lstSAQSSessionMaster.Count > 0;

                if (lstSAQSSessionMaster != null && lstSAQSSessionMaster.Count > 0)
                {
                    var ia = lstSAQSSessionMaster
                        .Where(i => i.SAQStivityMasterSessionNum == 1 && i.SAQStivitySessionStatus == SAQSTivitySessionStatus.Complete);

                    target.IsInitialAssessmentComplete = ia != null && ia.Count() != 0;
                }

                viewModels.Add(target);
            }

            return Json(viewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #region GetUserDetailByID

        public JsonResult GetLoggedInUserProfile()
        {
            int userId = ((StrengthTracker2.Core.Repository.Entities.Actors.User)(Session["AuthenticatedUser"])).UserId;
            return GetUserInfoByID(userId);
        }

        public JsonResult GetUserInfoByID(int userId)
        {
            var userDetails = _iAccount.GetUserInfoByID(userId);
            Contact contact = null;
            UserImage image = null;
            UserViewModel target = null;
            if (userDetails != null)
            {
                foreach (var source in userDetails.Users)
                {
                    target = new UserViewModel()
                    {
                        UserId = source.UserId,
                        FirstName = source.FirstName,
                        LastName = source.LastName,
                        IsActive = source.IsAccountEnabled,
                        IsDeleted = source.IsDeleted,
                        UserName = source.UserName,
                        DOB = source.DOB.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        Password = source.Password,
                        GenderId = (int)source.Gender,
                        Gender = source.Gender,
                        UserType = (UserType)(source.UserType)
                    };
                    DateTime parsedDate;
                    if (DateTime.TryParseExact(target.DOB.ToString(), "MM-dd-yyyy", null, DateTimeStyles.None, out parsedDate))
                    {
                        target.DOB = parsedDate.ToString();
                    }

                    contact = userDetails.Contacts.Where(ui => ui.UserID == source.UserId).FirstOrDefault();
                    target.ContactInformation = new Core.Functional.DBEntities.Actors.Contact();
                    if (contact != null)
                    {

                        target.ContactInformation.UserID = contact.UserID;
                        target.ContactInformation.ID = contact.ID;
                        target.Email = contact.Email;
                        target.ContactInformation.Email = contact.Email;
                        target.ContactInformation.AddressOne = contact.AddressOne;
                        target.ContactInformation.AddressTwo = contact.AddressTwo;
                        target.ContactInformation.City = contact.City;
                        target.ContactInformation.Zip = contact.Zip;
                        target.Phone = contact.PhoneNumber;
                        target.AlternatePhone = contact.AlternatePhoneNumber;
                    }
                    image = userDetails.UserImages.Where(ui => ui.UserId == source.UserId).FirstOrDefault();
                    target.UserImage = new Core.Functional.DBEntities.Actors.UserImage();
                    if (image != null)
                    {

                        target.UserImage.ImageId = image.ImageId;
                        target.UserImage.ImagePath = image.ImagePath;
                        target.UserImage.UserId = image.UserId;
                        target.ProfilePicture = image.ImagePath;
                    }
                }
            }

            return Json(target == null ? new UserViewModel() : target, JsonRequestBehavior.AllowGet);
        }
        #endregion 

        public JsonResult GetAthleteInfoByID(int athleteID)
        {
            User athlete = _iAthleteManagementRepository.GetAthleteInfoByID(athleteID);

            var target = new AthleteViewModel()
            {
                UserID = athlete.UserId,
                FirstName = athlete.FirstName,
                LastName = athlete.LastName,
                AthleteName = athlete.UserName,
                DOB = athlete.DOB,
                Gender = (int)athlete.Gender,
                SportID = athlete.SportID,
                PositionID = athlete.PositionID,
                School = athlete.SchoolName,
                Experienced = (athlete.YearsOfPlayOrgSport.HasValue && athlete.YearsOfPlayOrgSport.Value > 0) ? true : false,
                TravelTeamName = athlete.TravelTeamName,
                TravelTeamLevel = athlete.PlayingLevel,
                UserType = Convert.ToInt32(athlete.UserType)
            };
            if (athlete.ContactInformation != null)
            {
                target.ContactID = athlete.ContactInformation.ID;
                target.Email = athlete.ContactInformation.Email;
                target.SecondaryEmail = athlete.ContactInformation.SecondaryEmail;
                target.AddressOne = athlete.ContactInformation.AddressOne;
                target.AddressTwo = athlete.ContactInformation.AddressTwo;
                target.City = athlete.ContactInformation.City;
                target.ZipCode = athlete.ContactInformation.Zip;
                target.StateID = athlete.ContactInformation.StateID;
                target.Phone = athlete.ContactInformation.PhoneNumber;
                target.AlternatePhone = athlete.ContactInformation.AlternatePhoneNumber;
            }

            if (athlete.UserImage != null)
            {

                target.UserImageId = athlete.UserImage.ImageId;
                target.UserImagePath = athlete.UserImage.ImagePath;
            }

            target.Positions = new List<SelectListViewModel>();

            SelectListViewModel posSel = new SelectListViewModel();
            posSel.Value = "0";
            posSel.Text = "--Select Position--";
            posSel.Selected = false;
            target.Positions.Add(posSel);
            foreach (var position in _iPositionMgmtRepository.ListPositions())
            {
                posSel = new SelectListViewModel();
                posSel.Value = position.ID.ToString();
                posSel.Text = position.Name;
                posSel.FilterKey = position.SportID.ToString();
                posSel.Selected = (target.PositionID == position.ID) ? true : false;
                target.Positions.Add(posSel);
            }

            target.Sports = new List<SelectListViewModel>();

            SelectListViewModel posSport = new SelectListViewModel();
            posSport.Value = "0";
            posSport.Text = "--Select Sport--";
            posSport.Selected = false;
            target.Sports.Add(posSport);

            foreach (var sport in _iSportMgmtRepository.ListSports(true))
            {
                posSport = new SelectListViewModel();
                posSport.Value = sport.Id.ToString();
                posSport.Text = sport.Name;
                posSport.Selected = (target.SportID == sport.Id) ? true : false;
                target.Sports.Add(posSport);
            }

            target.States = new List<SelectListViewModel>();
            SelectListViewModel posState = new SelectListViewModel();
            posState.Value = "0";
            posState.Text = "--Select State--";
            posState.Selected = false;
            target.States.Add(posState);
            foreach (var state in _iStateRepository.ListStates())
            {
                posState = new SelectListViewModel();
                posState.Value = state.ID.ToString();
                posState.Text = state.Name;
                posState.Selected = (target.SportID == state.ID) ? true : false;
                target.States.Add(posState);
            }

            return Json(athlete == null ? new AthleteViewModel() : target, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult SessionLogin(int userId, string monthName, int dayNumber)
        {
            string result = string.Empty;
            if (userId > 0 && monthName != null)
            {
                int month = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;
                if (dayNumber > 0 && month > 0)
                {
                    StrengthTracker2.Core.Repository.Entities.Actors.User user = _iAccount.GetUserDetailsbyIDandDOB(userId, dayNumber, month);
                    result = (user != null) ? "Valid" : "Invalid";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHistoricalAssessmentForUser(int userID, int sessionID, [DataSourceRequest]DataSourceRequest request)
        {
            List<AssessmentExerciseModel> lstResult = new List<AssessmentExerciseModel>();

            StrengthTracker2.Core.Repository.Entities.Actors.UserDetails userDetail = _iAccount.GetFullUserInfoByUserID(userID, false, false, false);

            if (userDetail != null)
            {
                StrengthTracker2.Core.Repository.Entities.Actors.User user = userDetail.Users[0];

                DateTime today = DateTime.Today;
                int ageYears = today.Year - user.DOB.Year;
                if (user.DOB > today.AddYears(-ageYears)) ageYears--;


                SAQStivityDetail saqDetail = _iWorkoutMgmt.GetSAQStivityDetails(true, true, true, true, true, ageYears, Convert.ToInt32(user.Gender));

                List<AssessmentExercises> lstAssessmentExercises = saqDetail.AssessmentExercises;
                List<MovementType> lstMovementTypes = saqDetail.movementTypes;
                List<UOM> lstUOMs = saqDetail.uoms;

                List<SAQDetail> lstSAQDetails = _iWorkoutMgmt.GetSAQSdetailsBySessionMasterID(sessionID, userID);

                if (lstSAQDetails != null && lstSAQDetails.Count > 0)
                {
                    List<SAQDetail> lstSAQDetail = lstSAQDetails.Where(s => s.SAQStivitySessionMasterID == sessionID).ToList();

                    var avg = lstSAQDetail.Average(s => s.SAQQuotient);
                    decimal saqQuotient = Math.Round(Convert.ToDecimal(avg) * 100, 2);

                    if (lstSAQDetail.Count < 11)
                    {
                        foreach (var assmntExercise in lstAssessmentExercises)
                        {
                            int doesExerciseExist = lstSAQDetail.Where(x => x.ExerciseID == assmntExercise.ID).Count();

                            if (doesExerciseExist <= 0)
                            {
                                SAQDetail sqDet = new SAQDetail();
                                sqDet.MovementTypeID = assmntExercise.MovementTypeID;
                                sqDet.ExerciseID = assmntExercise.ID;
                                sqDet.UOMID = assmntExercise.UOMID;
                                sqDet.ActualScore = 0;

                                lstSAQDetail.Add(sqDet);
                            }
                        }
                    }

                    lstSAQDetail = lstSAQDetail.OrderBy(x => x.ExerciseID).ToList();

                    decimal saqsQuotient = 0;

                    if (lstSAQDetail != null && lstSAQDetail.Count > 0)
                    {
                        var averageSAQS = lstSAQDetail.Average(s => s.SAQQuotient);
                        saqsQuotient = Convert.ToDecimal(averageSAQS) * 100;
                    }

                    foreach (SAQDetail sq in lstSAQDetail)
                    {
                        var am = new AssessmentExerciseModel
                        {
                            MovementType =
                                Convert.ToString(
                                    lstMovementTypes.Where(m => m.Id == sq.MovementTypeID).Select(m => m.Name).FirstOrDefault()),
                            ExerciseName =
                                Convert.ToString(
                                    lstAssessmentExercises.Where(m => m.ID == sq.ExerciseID)
                                        .Select(m => m.Title)
                                        .FirstOrDefault()),
                            UOM =
                                Convert.ToString(lstUOMs.Where(m => m.Id == sq.UOMID).Select(m => m.Name).FirstOrDefault()),
                            ActualScore = sq.ActualScore,
                            SAQQuotient = Convert.ToString(sq.SAQQuotient),
                            AverageSAQSQuotient = Convert.ToString(saqsQuotient)
                        };


                        lstResult.Add(am);
                    }
                }
            }

            return Json(lstResult.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHistoricalAssessmentSessionNumbers(int userID)
        {
            List<SAQStivityAssessmentModel> lstAssessment = null;
            List<SAQStivitySessionMaster> lstSAQSSessionMaster = _iAthleteManagementRepository.GetUserSAQSMasterSessions(userID);

            if (lstSAQSSessionMaster != null && lstSAQSSessionMaster.Count > 0)
            {
                lstAssessment = new List<SAQStivityAssessmentModel>();

                foreach (SAQStivitySessionMaster sm in lstSAQSSessionMaster)
                {
                    if (sm.SAQStivityMasterSessionNum == 1)
                    {
                        string freeEval = "Free Evaluation";
                        lstAssessment.Add(new SAQStivityAssessmentModel() { AssessmentID = sm.ID, AssessmentName = freeEval });
                    }
                    else
                    {
                        string saqName = "Assessment" + Convert.ToString(sm.SAQStivityMasterSessionNum - 1);
                        lstAssessment.Add(new SAQStivityAssessmentModel() { AssessmentID = sm.ID, AssessmentName = saqName });
                    }
                }
            }

            return Json(lstAssessment, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeAthleteStatus(int userID, bool userStatus, int programID, DateTime activationDate)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            ro.Status = ReturnStatus.Err;
            ro.Message = "User status change failed";

            bool result = _iAccount.ChangeAthleteStatus(userID, userStatus, programID, activationDate);

            if (result)
            {
                var userDetails = _iAccount.GetUserInfoByID(userID);

                var user = userDetails.Users.FirstOrDefault();
                var contact = userDetails.Contacts.FirstOrDefault();

                string customerName = Convert.ToString(Session["CustomerName"]);

                //string link = Helpers.Common.WebsiteRoot + "Account/ResetPassword?q=" + HttpUtility.UrlEncode(SecurityUtility.EncryptText(Convert.ToString(user.UserId) + "|" + customerName));
                string link = Helpers.Common.WebsiteRoot + "Account/ResetPassword?q=" + HttpUtility.UrlEncode(Convert.ToString(user.UserId) + "|" + customerName);

                ChangePasswordMailModel cpm = new ChangePasswordMailModel();

                cpm.AthleteName = user.FirstName + " " + user.LastName;
                cpm.Link = link;
                cpm.UserName = user.UserName;
                cpm.DWLogin = ConfigurationManager.AppSettings["PersonalTrainerLoginURL"] + customerName;

                _mailSrvc = ObjectFactory.GetInstance<IMailService>();

                _mailSrvc.SendMail(cpm,
                "ActivateAthlete", contact.Email, "",
                "Activate Account", Server.MapPath("~/MailTemplates"), null);

                ro.Status = ReturnStatus.OK;
                ro.Message = "User activated successfully";
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetActiveProgramList(int userId = 0)
        {
            var user = (User)Session["AuthenticatedUser"];
            var list = _progMgmtRepository.ListPrograms();
            if (list != null)
                list = list.Where(p => p.IsActive == true).ToList();
            if (user.IsIndividualAthlete)
                list = list.Where(p => p.CustomerCategoryType == CustomerCategoryType.TKG || p.OwnerUserId == user.UserId).ToList();
            if (user.UserType == UserType.TKGAdmin && userId != 0)
            {
                var athlete = _iAthleteManagementRepository.GetAthleteInfoByID(userId);
                if(athlete.IsIndividualAthlete)
                    list = list.Where(p => p.CustomerCategoryType == CustomerCategoryType.TKG || p.OwnerUserId == userId).ToList();
            }

            return (Json(list, JsonRequestBehavior.AllowGet));
        }

        public ActionResult Tabular()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult GetDataForGridActiveAthletes(string coachId)
        {
            var athletes = _iAthleteManagementRepository.ListAllAthletes();
            if (Convert.ToInt32(coachId) != 0)
                athletes = athletes.Where(a => a.CoachID == Convert.ToInt32(coachId)).ToList();
            var sexGroups = athletes.GroupBy(a => a.Gender);
            var data = new List<DashboardGridDataModel>();
            foreach (var sexGroup in sexGroups)
            {
                var gridDataModel = new DashboardGridDataModel();
                gridDataModel.label = sexGroup.Key == Gender.Male ? "Boys " : "Girls ";
                // gridDataModel.label += athletes.Where(a => a.TeamID == teamId).First().Team.Name;
                gridDataModel.data = new List<long[]>();
                var athleteListInTeam = sexGroup.ToList();
                var sportInTeamGroup = athleteListInTeam.GroupBy(a => a.SportID);
                foreach (var item in sportInTeamGroup.OrderBy(v => v.Key))
                {
                    gridDataModel.data.Add(new long[] { item.Key, item.Count() });
                }
                data.Add(gridDataModel);
            }
            var sportGroups = athletes.GroupBy(a => a.SportID);
            var axisValues = new List<string[]>();
            foreach (var sportGroup in sportGroups.OrderBy(v => v.Key))
            {
                axisValues.Add(new string[] { sportGroup.Key.ToString(), sportGroup.First().Sport.Name });
            }

            //Removing spaces between sportIds to avoid alot of empty space in grid
            //first colect data
            var newSportIdDic = new Dictionary<string, int>();
            var counter = 1;
            foreach (var value in axisValues.OrderBy(v => Convert.ToInt32(v[0])))
            {
                newSportIdDic.Add(value[0], counter);
                counter++;
            }
            //replace ids in axisValues
            foreach (var value in axisValues)
            {
                value[0] = newSportIdDic[value[0]].ToString();
            }
            //replcae ids in data
            foreach (var dataModel in data)
            {
                foreach (var item in dataModel.data)
                {
                    item[0] = newSportIdDic[item[0].ToString()];
                }
            }
            return Json(new { data = data, axis = axisValues });
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult GetAthleteBySportData(string coachId)
        {
            var athletes = _iAthleteManagementRepository.ListAllAthletes();
            if (Convert.ToInt32(coachId) != 0)
                athletes = athletes.Where(a => a.CoachID == Convert.ToInt32(coachId)).ToList();
            var teamGroups = athletes.GroupBy(a => a.SportID);
            var data = new List<DashboardGridDataModel>();
            foreach (var group in teamGroups)
            {
                var sportID = group.Key;
                var gridDataModel = new DashboardGridDataModel();
                gridDataModel.label = athletes.Where(a => a.SportID == sportID).First().Sport.Name;
                gridDataModel.data = new List<long[]>();
                var athleteListwithSport = group.ToList();
                var sportInGroup = athleteListwithSport.GroupBy(a => a.SportID);
                foreach (var item in sportInGroup.OrderBy(v => v.Key))
                {
                    gridDataModel.data.Add(new long[] { item.Key, item.Count() });
                }
                data.Add(gridDataModel);
            }
            var sportGroups = athletes.GroupBy(a => a.SportID);
            var axisValues = new List<string[]>();
            foreach (var sportGroup in sportGroups.OrderBy(v => v.Key))
            {
                axisValues.Add(new string[] { sportGroup.Key.ToString(), sportGroup.First().Sport.Name });
            }

            //Removing spaces between sportIds to avoid alot of empty space in grid
            //first colect data
            var newSportIdDic = new Dictionary<string, int>();
            var counter = 1;
            foreach (var value in axisValues.OrderBy(v => Convert.ToInt32(v[0])))
            {
                newSportIdDic.Add(value[0], counter);
                counter++;
            }
            //replace ids in axisValues
            foreach (var value in axisValues)
            {
                value[0] = newSportIdDic[value[0]].ToString();
            }
            //replcae ids in data
            foreach (var dataModel in data)
            {
                foreach (var item in dataModel.data)
                {
                    item[0] = newSportIdDic[item[0].ToString()];
                }
            }
            return Json(new { data = data, axis = axisValues });
        }
    }
}
