using StrengthTracker2.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Core.Repository.Entities.ProgramManagement;
using StrengthTracker2.Core.Repository.Contracts.Account;
using StrengthTracker2.Core.Repository.Contracts.AthleteManagement;
using StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using ENT = StrengthTracker2.Core.Repository.Entities.ProgramManagement;

using System.Dynamic;
using StructureMap;
using System.Web.Script.Serialization;
using StrengthTracker2.Web.Helpers;
using System.Collections;
using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Core.Repository.Contracts.Workout;
using StrengthTracker2.Core.Repository.Entities.WorkoutManagement;
using StrengthTracker2.Web.Models.Admin;

namespace StrengthTracker2.Web.Controllers
{
    public class AthleteController : Controller
    {
        private IAthleteManagementRepository _iAthleteManagementRepository;
        private IProgramManagementRepository _iProgramManagementRepository;
		private IWorkoutManagement _iWorkoutManagement;
		private IExerciseManagement _iExerciseManagement;
        private IMovementTypeRepository _iMovementTypeReposioty;
        private ISportsManagementRepository _iSportMgmtRepository;
        private IState _iStateRepository;
        private IPositionMgmtRepository _iPositionMgmtRepository;
        private readonly IAccount _iAccount;

        public AthleteController()
        {
            _iMovementTypeReposioty = ObjectFactory.GetInstance<IMovementTypeRepository>();
            _iProgramManagementRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();
            _iExerciseManagement = ObjectFactory.GetInstance<IExerciseManagement>();
            _iAthleteManagementRepository = ObjectFactory.GetInstance<IAthleteManagementRepository>();
            _iAthleteManagementRepository = ObjectFactory.GetInstance<IAthleteManagementRepository>();
			_iWorkoutManagement = ObjectFactory.GetInstance<IWorkoutManagement>();
            _iSportMgmtRepository = ObjectFactory.GetInstance<ISportsManagementRepository>();
            _iStateRepository = ObjectFactory.GetInstance<IState>();
            _iPositionMgmtRepository = ObjectFactory.GetInstance<IPositionMgmtRepository>();
            _iAccount = ObjectFactory.GetInstance<IAccount>();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult DashboardNew()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult ProfileNew()
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
                bdm.label = "";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "Session1", "18030" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Session2", "10824" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Session3", "14428" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Session4", "9030" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Session5", "10830" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Session6", "10836" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Session7", "3612" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Session8", "5418" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Session9", "7724" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Session10", "7228" } };
                bdm.data.Add(array2Db);
                //array2Db = new string[1, 2] { { "Session11", "9010" } };
                //bdm.data.Add(array2Db);
                //array2Db = new string[1, 2] { { "Session12", "13840" } };
                //bdm.data.Add(array2Db);

                List<BarChartDataModel> lstBDM = new List<BarChartDataModel>();
                lstBDM.Add(bdm);
                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }
            else if (chartType == "barMultiple3")
            {
                List<BarChartDataModel> lstBDM = new List<BarChartDataModel>();

                BarChartDataModel bdm = new BarChartDataModel();
                bdm.color = "#5ab1ef";
                bdm.label = "Elite";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "DB Renegade Row", "50" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Seated DB Overhead Press", "56" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hang Clean", "160" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Bend Over Row", "108" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Farmer's Walk", "134" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Pull Ups", "200" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "BB Bench Press", "140" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Back Squat", "187" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hex Bar Deadlift", "250" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hip Thrust", "275" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#d87a80";
                bdm.label = "Good";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "DB Renegade Row", "27" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Seated DB Overhead Press", "35" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hang Clean", "100" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Bend Over Row", "65" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Farmer's Walk", "80" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Pull Ups", "133" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "BB Bench Press", "84" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Back Squat", "107" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hex Bar Deadlift", "150" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hip Thrust", "165" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#b2d767";
                bdm.label = "You";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "DB Renegade Row", "28" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Seated DB Overhead Press", "33" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hang Clean", "87" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Bend Over Row", "95" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Farmer's Walk", "62" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Pull Ups", "136" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "BB Bench Press", "90" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Back Squat", "164" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hex Bar Deadlift", "226" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Hip Thrust", "254" } };
                bdm.data.Add(array2Db);
                lstBDM.Add(bdm);

                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

		public ActionResult Home()
		{
            var user = (User)Session["AuthenticatedUser"];
            ViewBag.UserName = user.FirstName + " " + user.LastName;
            return View();
		}
		public ActionResult WorkoutOverviewPartial()
		{
			return PartialView("_WorkoutOverview");

        }

		public ActionResult WorkoutOverview()
		{
            var user = (User)Session["AuthenticatedUser"];
            ViewBag.UserName = user.FirstName + " " + user.LastName;
            return View();

		}
		public ActionResult ViewWorkout()
		{
            var user = (User)Session["AuthenticatedUser"];
            ViewBag.UserName = user.FirstName + " " + user.LastName;
            return View();
		}

		public ActionResult WorkoutProgress()
		{
			var user = (User)Session["AuthenticatedUser"];
			ViewBag.UserName = user.FirstName + " " + user.LastName;
			return View();
		}

		public ActionResult WorkoutSummary()
		{
            var user = (User)Session["AuthenticatedUser"];
            ViewBag.UserName = user.FirstName + " " + user.LastName;
            return View();
		}
		public ActionResult ViewExercise(int exerciseId)
		{
            var user = (User)Session["AuthenticatedUser"];
            ViewBag.UserName = user.FirstName + " " + user.LastName;
            return View();
		}
		public ActionResult ModifyExercise(int exerciseId)
		{
            var user = (User)Session["AuthenticatedUser"];
            ViewBag.UserName = user.FirstName + " " + user.LastName;
            return View();
		}
		public ActionResult ManageAccount()
		{
            var user = (User)Session["AuthenticatedUser"];
            ViewBag.UserName = user.FirstName + " " + user.LastName;
            return View();
		}

        private string GetTempoText(int proTempo)
        {
            //string tempo = string.Empty;
            //switch (proTempo)
            //{
            //    case 1:
            //        tempo = "3-0-X-1";
            //        break;
            //    case 2:
            //        tempo = "Slow";
            //        break;
            //    case 3:
            //        tempo = "Medium";
            //        break;
            //    case 4:
            //        tempo = "Fast";
            //        break;
            //}
            string tempo = TempoModel.Tempos.FirstOrDefault(k => k.Value == proTempo.ToString()).Key;
            return tempo;
        }
		private AthleteUserModel GetAthleteUserModel(User athlete)
		{
			AthleteUserModel userModel = new AthleteUserModel();
			userModel.UserId = athlete.UserId;
			userModel.UserName = athlete.UserName;
			userModel.FirstName = athlete.FirstName;
			userModel.LastName = athlete.LastName;
			userModel.DOB = athlete.DOB;
            if (athlete.ContactInformation != null)
            {
                userModel.Address1 = athlete.ContactInformation.AddressOne != null ? athlete.ContactInformation.AddressOne : string.Empty;
                userModel.Address2 = athlete.ContactInformation.AddressTwo != null ? athlete.ContactInformation.AddressTwo : string.Empty;
            }
            else
            {
                userModel.Address1 = string.Empty;
                userModel.Address2= string.Empty;
            }
            userModel.Gender = athlete.Gender;
			userModel.PositionId = athlete.PositionID;
			//userModel.Position = athlete.P;
			userModel.ProgramId = athlete.Program.Id;
			userModel.ProgramName = athlete.Program.Description;
			userModel.SportId = athlete.Sport.Id;
			userModel.SportName = athlete.Sport.Name;
			userModel.ImageId = athlete.UserImage.ImageId;
			userModel.ImagePath = athlete.UserImage.ImagePath;
            userModel.TrackId = athlete.TrackID;

            return userModel;
		}

        public JsonResult GetAthleteInfoByID()
        {
            int userId = -1;
            if (Session["AuthenticatedUser"] != null)
            {
                userId = ((User)Session["AuthenticatedUser"]).UserId;
            }

            var athlete = _iAthleteManagementRepository.GetAthleteInfoByID(userId);

            var target = new AthleteViewModel()
            {
                UserID = athlete.UserId,
                FirstName = athlete.FirstName,
                LastName = athlete.LastName,
                AthleteName = athlete.UserName,
                DOB = athlete.DOB,
                Gender = (int)athlete.Gender,
                Grade = athlete.Grade,
                SportID = athlete.SportID,
                PositionID = athlete.PositionID,
                School = athlete.SchoolName,
                Experienced = (athlete.YearsOfPlayOrgSport.HasValue && athlete.YearsOfPlayOrgSport.Value > 0),
                TravelTeamName = athlete.TravelTeamName,
                TravelTeamLevel = athlete.PlayingLevel,
                UserType = Convert.ToInt32(athlete.UserType),
                CoachID = athlete.CoachID,
                TeamID = athlete.TeamID
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
            if (athlete.Coach != null)
            {
                target.CoachName = athlete.Coach.FirstName + " " + athlete.Coach.LastName;
            }
            if (athlete.Team != null)
            {
                target.TeamName = athlete.Team.Name;
            }

            target.Positions = new List<SelectListViewModel>();

            var posSel = new SelectListViewModel
            {
                Value = "0",
                Text = "--Select Position--",
                Selected = false
            };
            target.Positions.Add(posSel);
            foreach (var position in _iPositionMgmtRepository.ListPositions())
            {
                posSel = new SelectListViewModel
                {
                    Value = position.ID.ToString(),
                    Text = position.Name,
                    FilterKey = position.SportID.ToString(),
                    Selected = (target.PositionID == position.ID) ? true : false
                };
                target.Positions.Add(posSel);
            }

            target.Sports = new List<SelectListViewModel>();

            var posSport = new SelectListViewModel
            {
                Value = "0",
                Text = "--Select Sport--",
                Selected = false
            };
            target.Sports.Add(posSport);

            foreach (var sport in _iSportMgmtRepository.ListSports(true))
            {
                posSport = new SelectListViewModel
                {
                    Value = sport.Id.ToString(),
                    Text = sport.Name,
                    Selected = (target.SportID == sport.Id) ? true : false
                };
                target.Sports.Add(posSport);
            }

            target.States = new List<SelectListViewModel>();
            var posState = new SelectListViewModel
            {
                Value = "0",
                Text = "--Select State--",
                Selected = false
            };
            target.States.Add(posState);
            foreach (var state in _iStateRepository.ListStates())
            {
                posState = new SelectListViewModel
                {
                    Value = state.ID.ToString(),
                    Text = state.Name,
                    Selected = (target.SportID == state.ID) ? true : false
                };
                target.States.Add(posState);
            }

            return Json(target, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult SaveAthlete()
        {
            var ro = new ReturnObjectModel
            {
                Message = "User Update failed",
                Status = ReturnStatus.Err
            };

            var userModel = new JavaScriptSerializer().Deserialize<AthleteViewModel>(System.Web.HttpContext.Current.Request.Form["athleteViewModel"]);
            var athlete = _iAthleteManagementRepository.GetAthleteInfoByID(userModel.UserID) ?? new User();

            athlete.FirstName = userModel.FirstName;
            athlete.LastName = userModel.LastName;
            athlete.DOB = userModel.DOB;
            athlete.Gender = (Gender)userModel.Gender;
            athlete.Grade = userModel.Grade;
            athlete.SportID = userModel.SportID;
            athlete.PositionID = userModel.PositionID;
            athlete.SchoolName = userModel.School;
            athlete.TravelTeamName = userModel.TravelTeamName;
            athlete.PlayingLevel = userModel.TravelTeamLevel;
            athlete.UserType = (UserType)(userModel.UserType);
            if (athlete.ContactInformation != null)
            {
                athlete.ContactInformation.UserID = userModel.UserID;
                athlete.ContactInformation.ID = userModel.ContactID;
                athlete.ContactInformation.Email = userModel.Email;
                athlete.ContactInformation.AddressOne = userModel.AddressOne;
                athlete.ContactInformation.AddressTwo = userModel.AddressTwo;
                athlete.ContactInformation.City = userModel.City;
                athlete.ContactInformation.Zip = userModel.ZipCode;
                athlete.ContactInformation.PhoneNumber = userModel.Phone;
                athlete.ContactInformation.StateID = userModel.StateID;
                athlete.ContactInformation.AlternatePhoneNumber = userModel.AlternatePhone;
                athlete.ContactInformation.SecondaryEmail = userModel.SecondaryEmail;
            }
            if (athlete.UserImage != null)
            {
                athlete.UserImage.UserId = userModel.UserID;
                athlete.UserImage.ImageId = userModel.UserImageId;
                athlete.UserImage.ImagePath = userModel.UserImagePath;
            }

            UserDetails userDetails = new UserDetails();
            UserImage image = null;

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                if (userModel.UserImagePath != null)
                {
                    string fileFullPathName = string.Empty;

                    fileFullPathName = Server.MapPath("~/Images/user/") + userModel.UserImagePath;
                    var pic = System.Web.HttpContext.Current.Request.Files[0];

                    if (pic.ContentLength > 0)
                    {
                        pic.SaveAs(fileFullPathName);
                    }
                    athlete.UserImage.ImagePath = "../Images/user/" + userModel.UserImagePath;
                }
            }

            userDetails.Users.Add(athlete);
            userDetails.Contacts.Add(athlete.ContactInformation);
            userDetails.UserImages.Add(athlete.UserImage);

            if (athlete.UserId > 0)
            {
                bool result = _iAccount.SaveUserInfo(userDetails);
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



        public string GetClassName(string columnName)
        {
            string className = string.Empty;
            columnName = TrimEndWithNumeric(columnName);
            switch (columnName)
            {
                case "OrderNumber":
                    className = "STCenter";
                    break;
                case "ExerciseName":
                    className = "STLeft";
                    break;
                case "Status":
                    className = "STCenter";
                    break;
                case "MovementTypeName":
                    className = "STLeft";
                    break;
                case "Sets":
                    className = "STCenter";
                    break;
                case "OneRM":
                    className = "STCenter";
                    break;
                case "Dynamic1RM":
                    className = "STCenter";
                    break;
                case "TargetLoad":
                    className = "STCenter";
                    break;
                case "TargetReps":
                    className = "STCenter";
                    break;
                case "ActualLoad":
                    className = "STCenter";
                    break;
                case "ActualReps":
                    className = "STCenter";
                    break;
                default:
                    className = "STCenter";
                    break;
            }
            return className;
        }
        public string GetColumnTitle(string columnName)
		{
			string title = string.Empty;
			columnName = TrimEndWithNumeric(columnName);
			switch (columnName)
			{
			    case "OrderNumber": title = "#";
					break;
				case "ExerciseName":
					title = "Exercise";
					break;
				case "Status":
					title = "Done";
					break;
				case "MovementTypeName":
					title = "Movement";
					break;
				case "OneRM":
					title = "1RM";
					break;
				case "Dynamic1RM":
					title = "D1RM";
					break;
				case "TargetLoad":
					title = "Tgt.Load";
					break;
				case "TargetReps":
					title = "Tgt.Reps";
					break;
				case "ActualLoad":
					title = "Act.Load";
					break;
				case "ActualReps":
					title = "Act.Reps";
					break;
				default:
					title = columnName;
					break;
			}
			return title;
		}
		public string TrimEndWithNumeric(string value)
        {
            int outInt = 0;
            if (value != "Rest1" && value != "Rest2")
            {
                bool isLastCharNumeric = int.TryParse(value[value.Length - 1].ToString(), out outInt);
                if (isLastCharNumeric)
                {
                    //chop off the last char
                    value = value.Remove(value.Length - 1);
                }
            }
            return value;
        }

		public JsonResult SaveModifyExercise(ExerciseSet currentSet, ExerciseSet modifiedSet)
		{

			DWChangedExerciseDetails dwChangedExerciseDetails = new DWChangedExerciseDetails();
			WorkoutSummaryViewModel workoutSummaryViewModel = GetAthleteWorkoutViewModel();
			if (workoutSummaryViewModel != null)
			{
				ProgramDetail programDetail = _iProgramManagementRepository.GetProgramExercises(workoutSummaryViewModel.User.ProgramId);
				if (programDetail != null)
				{
					ProgramExercise currentExerciseModel = programDetail.ProgramExercises.Find(r => r.ID == currentSet.ProgramExerciseId);
					if (currentExerciseModel != null)
					{
						dwChangedExerciseDetails.UserID = workoutSummaryViewModel.User.UserId;
						dwChangedExerciseDetails.ProgramID = currentExerciseModel.ProgramID;
						dwChangedExerciseDetails.TrackID = workoutSummaryViewModel.User.TrackId;
						dwChangedExerciseDetails.SessionID = currentExerciseModel.ExerciseStepID;
						dwChangedExerciseDetails.ExerciseStepID = currentExerciseModel.ExerciseStepID;
						dwChangedExerciseDetails.ExerciseGroupID = currentExerciseModel.ExerciseGroupID;
						dwChangedExerciseDetails.SetNumber = currentSet.SetNumber;
						dwChangedExerciseDetails.OriginalExerciseID = currentSet.ExerciseId;
						dwChangedExerciseDetails.OriginalTargetLoad = currentSet.TargetLoad;
						dwChangedExerciseDetails.OriginalTargetReps = currentSet.TargetReps;
						dwChangedExerciseDetails.OriginalTempo = currentSet.Tempo;
						dwChangedExerciseDetails.ModifiedExerciseID = modifiedSet.ExerciseId;
						dwChangedExerciseDetails.ModifiedTargetLoad = modifiedSet.TargetLoad;
						dwChangedExerciseDetails.ModifiedTargetReps = modifiedSet.TargetReps;
						dwChangedExerciseDetails.ModifiedTempo = modifiedSet.Tempo;
						dwChangedExerciseDetails.ModifiedDate = DateTime.Now;
						_iWorkoutManagement.SaveDWChangedExerciseDetails(dwChangedExerciseDetails);
					}
				}
			}
			
			return Json("Success", JsonRequestBehavior.AllowGet);
		}

		public JsonResult SaveAthleteWorkout(ExerciseSet exerciseSet)
		{

			WorkoutSummaryViewModel workoutSummaryViewModel = GetAthleteWorkoutViewModel();
			List<UserWorkoutDetails> userWorkoutDetails = _iWorkoutManagement.GetUserWorkoutDetails(workoutSummaryViewModel.User.ProgramId, workoutSummaryViewModel.User.UserId);
			UserWorkoutDetails userWorkoutDetailsModel = new UserWorkoutDetails();
			ProgramDetail programDetail = _iProgramManagementRepository.GetProgramExercises(workoutSummaryViewModel.User.ProgramId);
			ExerciseSet es = exerciseSet;

				userWorkoutDetailsModel = userWorkoutDetails.FindAll(s => s.ExerciseID == es.ExerciseId && s.SetNum == es.SetNumber).FirstOrDefault();
				ProgramExercise weModel = programDetail.ProgramExercises.Find(r => r.ID == es.ProgramExerciseId);

				if (userWorkoutDetailsModel != null)
				{
					userWorkoutDetailsModel.ActualLoad = es.ActualLoad;
					userWorkoutDetailsModel.ActualReps = es.ActualReps;
					userWorkoutDetailsModel.Dynamic1RM = es.Dynamic1RM;
					userWorkoutDetailsModel.OneRM = es.OneRM;
					userWorkoutDetailsModel.WorkoutDate = DateTime.Now;
					userWorkoutDetailsModel.ModifiedDate = DateTime.Now;
				}
				else
				{
					userWorkoutDetailsModel = new UserWorkoutDetails();
					userWorkoutDetailsModel.ProgramID = workoutSummaryViewModel.User.ProgramId;
					userWorkoutDetailsModel.UserID = workoutSummaryViewModel.User.UserId;
					userWorkoutDetailsModel.TrackID = workoutSummaryViewModel.User.TrackId;
					if (weModel != null)
					{
						if (weModel.PairedExerciseID == es.ExerciseId)
						{
							userWorkoutDetailsModel.ExerciseID = es.ExerciseId;

						}
						else
						{
							userWorkoutDetailsModel.ExerciseID = weModel.ExerciseID;

						}

						userWorkoutDetailsModel.SessionID = weModel.ExerciseStepID;
						userWorkoutDetailsModel.ExerciseStepID = weModel.ExerciseStepID;
						userWorkoutDetailsModel.ExerciseGroupID = weModel.ExerciseGroupID;
					}

					userWorkoutDetailsModel.ActualLoad = es.ActualLoad;
					userWorkoutDetailsModel.ActualReps = es.ActualReps;
					userWorkoutDetailsModel.Dynamic1RM = es.Dynamic1RM;
					userWorkoutDetailsModel.SetNum = es.SetNumber;
					userWorkoutDetailsModel.OneRM = es.OneRM;
					userWorkoutDetailsModel.WorkoutDate = DateTime.Now;
					userWorkoutDetailsModel.ModifiedDate = DateTime.Now;
					userWorkoutDetailsModel.Status = 0;
			}
			_iWorkoutManagement.UpdateWorkoutDetails(userWorkoutDetailsModel);
			return Json("Success", JsonRequestBehavior.AllowGet);
		}

		public JsonResult SaveAthleteWorkoutDetails(List<ExerciseSet> exerciseSets)
		{
           
            WorkoutSummaryViewModel workoutSummaryViewModel = GetAthleteWorkoutViewModel();
            List<UserWorkoutDetails> userWorkoutDetails = _iWorkoutManagement.GetUserWorkoutDetails(workoutSummaryViewModel.User.ProgramId, workoutSummaryViewModel.User.UserId);
            UserWorkoutDetails userWorkoutDetailsModel = new UserWorkoutDetails();
            ProgramDetail programDetail = _iProgramManagementRepository.GetProgramExercises(workoutSummaryViewModel.User.ProgramId);
            foreach (ExerciseSet es in exerciseSets)
            {
                userWorkoutDetailsModel = userWorkoutDetails.FindAll(s => s.ExerciseID == es.ExerciseId && s.SetNum == es.SetNumber).FirstOrDefault();
                ProgramExercise weModel = programDetail.ProgramExercises.Find(r => r.ID == es.ProgramExerciseId);
				
				if (userWorkoutDetailsModel != null)
                {
                    userWorkoutDetailsModel.ActualLoad = es.ActualLoad;
                    userWorkoutDetailsModel.ActualReps = es.ActualReps;
                    userWorkoutDetailsModel.Dynamic1RM = es.Dynamic1RM;
                    userWorkoutDetailsModel.OneRM = es.OneRM;
                    userWorkoutDetailsModel.WorkoutDate = DateTime.Now;
                    userWorkoutDetailsModel.ModifiedDate = DateTime.Now;
                }
                else
                {
                    userWorkoutDetailsModel = new UserWorkoutDetails();
                    userWorkoutDetailsModel.ProgramID = workoutSummaryViewModel.User.ProgramId;
                    userWorkoutDetailsModel.UserID = workoutSummaryViewModel.User.UserId;
                    userWorkoutDetailsModel.TrackID = workoutSummaryViewModel.User.TrackId;
                    if (weModel != null)
                    {
						if (weModel.PairedExerciseID == es.ExerciseId)
						{
							userWorkoutDetailsModel.ExerciseID = es.ExerciseId;
						
						}
						else
						{
							userWorkoutDetailsModel.ExerciseID = weModel.ExerciseID;
						
						}

						userWorkoutDetailsModel.SessionID = weModel.ExerciseStepID;
						userWorkoutDetailsModel.ExerciseStepID = weModel.ExerciseStepID;
						userWorkoutDetailsModel.ExerciseGroupID = weModel.ExerciseGroupID;
					}

                    userWorkoutDetailsModel.ActualLoad = es.ActualLoad;
                    userWorkoutDetailsModel.ActualReps = es.ActualReps;
					userWorkoutDetailsModel.Dynamic1RM = es.Dynamic1RM;
                    userWorkoutDetailsModel.SetNum = es.SetNumber;
                    userWorkoutDetailsModel.OneRM = es.OneRM;
                    userWorkoutDetailsModel.WorkoutDate = DateTime.Now;
                    userWorkoutDetailsModel.ModifiedDate = DateTime.Now;
                    userWorkoutDetailsModel.Status = 0;
                }

                _iWorkoutManagement.UpdateWorkoutDetails(userWorkoutDetailsModel);
            }
          
            return Json("Success", JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetExerciseList()
		{
			return Json(_iExerciseManagement.ListExercises(), JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetExerciseSetsList()
        {
            dynamic expando = new ExpandoObject();
            var expandoObj = expando as IDictionary<String, object>;
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new ExpandoJSONConverter() });
            string jsonData = string.Empty;
            string jsonColumnData = string.Empty;
            string jsonDataTemp = string.Empty;
            string jsonColumnDataTemp = string.Empty;
            ExerciseSetsResult exerciseSetsResult = new ExerciseSetsResult();
            WorkoutSummaryViewModel workoutSummaryViewModel = GetAthleteWorkoutViewModel();
			List<UserWorkoutDetails> userWorkoutDetails = _iWorkoutManagement.GetUserWorkoutDetails(workoutSummaryViewModel.User.ProgramId, workoutSummaryViewModel.User.UserId);
			int maxSets = 0;
			UserWorkoutDetails userWorkoutDetailsModel = null;
			ProgramDetail programDetail = _iProgramManagementRepository.GetProgramExercises(workoutSummaryViewModel.User.ProgramId);
			if (workoutSummaryViewModel.WorkoutExercises != null && workoutSummaryViewModel.WorkoutExercises.Count > 0)
            {
                maxSets = workoutSummaryViewModel.WorkoutExercises.Max(r => r.ExerciseSets.Max(s => s.SetNumber));
            }
            foreach (WorkoutExerciseModel workoutExerciseModel in workoutSummaryViewModel.WorkoutExercises)
            {
                jsonDataTemp = string.Empty;
                jsonColumnDataTemp = string.Empty;

                jsonColumnData = string.Empty;
                string OrderNumber = workoutExerciseModel.OrderNumber != null ? workoutExerciseModel.OrderNumber : string.Empty;
                string ExerciseName = workoutExerciseModel.ExerciseName != null ? workoutExerciseModel.ExerciseName : string.Empty;
				ProgramExercise pe = programDetail.ProgramExercises.Find(s => s.ID == workoutExerciseModel.ProgramExerciseId);

				List<DWChangedExerciseDetails> dwChangedExerciseDetails = _iWorkoutManagement.GetDWChangedExercises(workoutSummaryViewModel.User.ProgramId, workoutSummaryViewModel.User.UserId);
				List<Exercise> exercisesList = _iExerciseManagement.ListExercises();
				Exercise exercise = null;
				DWChangedExerciseDetails dwChangedExercise = null;
			
				foreach (ExerciseSetModel set in workoutExerciseModel.ExerciseSets)
                {
					if (pe != null && userWorkoutDetails != null)
					{
						userWorkoutDetailsModel = userWorkoutDetails.FindAll(s => s.ExerciseGroupID == pe.ExerciseGroupID && s.ExerciseStepID == pe.ExerciseStepID && (s.ExerciseID == workoutExerciseModel.ExerciseId ) && s.SetNum == set.SetNumber).FirstOrDefault();
					}
                    if (userWorkoutDetailsModel != null && (dwChangedExerciseDetails != null && dwChangedExerciseDetails.Count > 0))
					{
						dwChangedExercise = dwChangedExerciseDetails.Find(s => s.OriginalExerciseID == userWorkoutDetailsModel.ExerciseID && s.ExerciseGroupID == userWorkoutDetailsModel.ExerciseGroupID && s.ExerciseStepID == userWorkoutDetailsModel.ExerciseStepID);
					}

					if (dwChangedExercise != null)
					{
						exercise = exercisesList.Find(s => s.Id == dwChangedExercise.ModifiedExerciseID);
						if (exercise != null)
						{
							//workoutExerciseModel.ExerciseId = exercise.Id;
							//workoutExerciseModel.ExerciseName = exercise.Name;
							expandoObj["ExerciseId"] = exercise.Id;
							expandoObj["ExerciseName"] = exercise.Name;
							expandoObj["ExerciseSetName"] = OrderNumber + "." + exercise.Name;
							expandoObj["SetName"] = OrderNumber + "." + exercise.Name + "." + set.SetNumber;
						}
						else
						{
							expandoObj["ExerciseId"] = workoutExerciseModel.ExerciseId;
							expandoObj["ExerciseName"] = ExerciseName;
							expandoObj["ExerciseSetName"] = OrderNumber + "." + ExerciseName;
							expandoObj["SetName"] = OrderNumber + "." + ExerciseName + "." + set.SetNumber;
						}
					}
					else
					{
						expandoObj["ExerciseId"] = workoutExerciseModel.ExerciseId;
						expandoObj["ExerciseName"] = ExerciseName;
						expandoObj["ExerciseSetName"] = OrderNumber + "." + ExerciseName;
						expandoObj["SetName"] = OrderNumber + "." + ExerciseName + "." + set.SetNumber;
					}
					expandoObj["SetId"] = set.ExerciseTempoId;
                    expandoObj["SetNumber"] = set.SetNumber;
                  

					if (dwChangedExercise != null)
					{
						expandoObj["TargetLoad"] = dwChangedExercise.ModifiedTargetLoad;
						expandoObj["TargetReps"] = dwChangedExercise.ModifiedTargetReps;
						expandoObj["Tempo"] = dwChangedExercise.ModifiedTempo;
					}
					else {
						expandoObj["Tempo"] = set.Tempo;
						expandoObj["TargetLoad"] = set.TargetLoad;
						expandoObj["TargetReps"] = set.TargetReps;
					}
					if (userWorkoutDetailsModel != null)
					{
						expandoObj["OneRM"] = userWorkoutDetailsModel.OneRM;
						expandoObj["Dynamic1RM"] = userWorkoutDetailsModel.Dynamic1RM;
						expandoObj["ActualLoad"] = userWorkoutDetailsModel.ActualLoad;
						expandoObj["ActualReps"] = userWorkoutDetailsModel.ActualReps;
					}
					else
					{
						expandoObj["OneRM"] = set.OneRM;
						expandoObj["Dynamic1RM"] = set.Dynamic1RM;
						expandoObj["ActualLoad"] = set.ActualLoad;
						expandoObj["ActualReps"] = set.ActualReps;
					}
                    expandoObj["Rest"] = set.RestIntervel;
                    expandoObj["OrderNumber"] = OrderNumber;
                 
                    expandoObj["SetStatus"] = false;
                  
                    expandoObj["ProgramExerciseId"] = workoutExerciseModel.ProgramExerciseId;
                   jsonData = jsonData + serializer.Serialize(expando) + ",";
                }
            }

            jsonData = "{\"exerciseSets\":[" + jsonData.TrimEnd(new char[] { ',' }) + "]}";
            exerciseSetsResult.Data = jsonData;
			exerciseSetsResult.UserName = workoutSummaryViewModel.User.FirstName + " " + workoutSummaryViewModel.User.LastName;
			exerciseSetsResult.Exercises = _iExerciseManagement.ListExercises();
			return Json(exerciseSetsResult, JsonRequestBehavior.AllowGet);
        }

        public WorkoutSummaryResult GetDynamicWorkoutSummaryModel(WorkoutSummaryViewModel workoutSummaryViewModel)
        {

            dynamic expando = new ExpandoObject();
            var expandoObj = expando as IDictionary<String, object>;
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new ExpandoJSONConverter() });
            string jsonData = string.Empty;
            string jsonColumnData = string.Empty;
            string jsonDataTemp = string.Empty;
            string jsonColumnDataTemp = string.Empty;
            WorkoutSummaryResult workoutSummaryResult = new WorkoutSummaryResult();
            int maxSets = 0;
			UserWorkoutDetails userWorkoutDetailsModel = null;
			List<UserWorkoutDetails> userWorkoutDetails = _iWorkoutManagement.GetUserWorkoutDetails(workoutSummaryViewModel.User.ProgramId, workoutSummaryViewModel.User.UserId);
			ProgramDetail programDetail = _iProgramManagementRepository.GetProgramExercises(workoutSummaryViewModel.User.ProgramId);
			List<DWChangedExerciseDetails> dwChangedExerciseDetails = _iWorkoutManagement.GetDWChangedExercises(workoutSummaryViewModel.User.ProgramId, workoutSummaryViewModel.User.UserId);
			List<Exercise> exercisesList = _iExerciseManagement.ListExercises();
			Exercise exercise = null;
			DWChangedExerciseDetails dwChangedExercise = null;

			if (workoutSummaryViewModel.WorkoutExercises != null && workoutSummaryViewModel.WorkoutExercises.Count > 0)
            {
                maxSets = workoutSummaryViewModel.WorkoutExercises.Max(r => r.ExerciseSets.Max(s => s.SetNumber));
            }
			
			foreach (WorkoutExerciseModel workoutExerciseModel in workoutSummaryViewModel.WorkoutExercises)
            {
				ProgramExercise pe =  programDetail.ProgramExercises.Find(s => s.ID == workoutExerciseModel.ProgramExerciseId && (s.ExerciseID == workoutExerciseModel.ExerciseId || s.PairedExerciseID ==workoutExerciseModel.ExerciseId));
                if (dwChangedExerciseDetails != null && dwChangedExerciseDetails.Count > 0)
                {
                    dwChangedExercise = dwChangedExerciseDetails.Find(s => s.OriginalExerciseID == workoutExerciseModel.ExerciseId && s.ExerciseGroupID == workoutExerciseModel.GroupId && s.ExerciseStepID == workoutExerciseModel.StepId);
                }

				if (dwChangedExercise != null)
				{
					exercise = exercisesList.Find(s => s.Id == dwChangedExercise.ModifiedExerciseID);
					if (exercise != null)
					{
						workoutExerciseModel.ExerciseId = exercise.Id;
						workoutExerciseModel.ExerciseName = exercise.Name;
					}
				}

				jsonDataTemp = string.Empty;
                jsonColumnDataTemp = string.Empty;

                jsonColumnData = string.Empty;
                expandoObj["OrderNumber"] = workoutExerciseModel.OrderNumber!=null ? workoutExerciseModel.OrderNumber:string.Empty;
                expandoObj["ExerciseName"] = workoutExerciseModel.ExerciseName != null ? workoutExerciseModel.ExerciseName : string.Empty; 
                expandoObj["Status"] = workoutExerciseModel.Status;
                expandoObj["Sets"] = workoutExerciseModel.Sets;
                expandoObj["MovementTypeName"] = workoutExerciseModel.MovementTypeName != null ? workoutExerciseModel.MovementTypeName : string.Empty;
				//expandoObj["MovementTypeId"] = workoutExerciseModel.MovementTypeId;
				//expandoObj["Rest1"] = workoutExerciseModel.Rest1;
				//expandoObj["Rest2"] = workoutExerciseModel.Rest2;
				//expandoObj["ExerciseId"] = workoutExerciseModel.ExerciseId;
				//expandoObj["MasterExerciseId"] = workoutExerciseModel.MasterExerciseId;
				expandoObj["ProgramExerciseId"] = workoutExerciseModel.ProgramExerciseId;
				//expandoObj["GroupId"] = workoutExerciseModel.GroupId;
				//expandoObj["StepId"] = workoutExerciseModel.StepId;
				//expandoObj["ExerciseTimeToComplete"] = workoutExerciseModel.ExerciseTimeToComplete;
				int i = 0;
                for (int j=0; j< maxSets;j++)
                {
                    ExerciseSetModel set = null;
                    if (j<workoutExerciseModel.ExerciseSets.Count)
                    {
                        set = workoutExerciseModel.ExerciseSets[j];
                    }
					
					i = j + 1;
                    if (set != null)
                    {
						if (dwChangedExercise != null)
						{
							set.TargetLoad = dwChangedExercise.ModifiedTargetLoad;
							set.TargetReps = dwChangedExercise.ModifiedTargetReps;
							set.Tempo = dwChangedExercise.ModifiedTempo;
						}

						if (pe != null && userWorkoutDetails != null)
						{
							if (dwChangedExercise != null)
							{
								userWorkoutDetailsModel = userWorkoutDetails.FindAll(s => s.ExerciseGroupID == pe.ExerciseGroupID && s.ExerciseStepID == pe.ExerciseStepID && s.ExerciseID == dwChangedExercise.OriginalExerciseID && s.SetNum == set.SetNumber).FirstOrDefault();
							}
							else
							{
								userWorkoutDetailsModel = userWorkoutDetails.FindAll(s => s.ExerciseGroupID == pe.ExerciseGroupID && s.ExerciseStepID == pe.ExerciseStepID && s.ExerciseID == workoutExerciseModel.ExerciseId && s.SetNum == set.SetNumber).FirstOrDefault();
							}
							
						}

						
						if (userWorkoutDetailsModel != null)
						{
							expandoObj["OneRM" + i] = userWorkoutDetailsModel.OneRM;
							expandoObj["Dynamic1RM" + i] = userWorkoutDetailsModel.Dynamic1RM;
						}
						else
						{
							expandoObj["OneRM" + i] = set.OneRM;
							expandoObj["Dynamic1RM" + i] = set.Dynamic1RM;
						}
                        expandoObj["Tempo" + i] = set.Tempo;
                        expandoObj["TargetLoad" + i] = set.TargetLoad;
                        expandoObj["TargetReps" +i] = set.TargetReps;
						if (userWorkoutDetailsModel != null)
						{
							expandoObj["ActualLoad" + i] = userWorkoutDetailsModel.ActualLoad;
							expandoObj["ActualReps" + i] = userWorkoutDetailsModel.ActualReps;
						}
						else
						{
							expandoObj["ActualLoad" + i] = set.ActualLoad ;
							expandoObj["ActualReps" + i] = set.ActualReps;
						}
						expandoObj["ExerciseTempoId" + i] = set.ExerciseTempoId;
					}
					else
                    {
                        expandoObj["OneRM" + i] = string.Empty;
                        expandoObj["Dynamic1RM" + i] = string.Empty;
                        expandoObj["Tempo" +i] = string.Empty ;
                        expandoObj["TargetLoad" + i] = string.Empty;
                        expandoObj["TargetReps" + i] = string.Empty;
                        expandoObj["ActualLoad" + i] = string.Empty;
                        expandoObj["ActualReps" + i] = string.Empty;
						expandoObj["ExerciseTempoId" + i] = string.Empty;
					}
                }
                

                IDictionary<String, object> expandoObj1 = expando as IDictionary<String, object>;
                foreach (KeyValuePair<String, object> eEntry in expandoObj)
                {
                    jsonColumnDataTemp = jsonColumnDataTemp + "{  \"className\": \"" + GetClassName(eEntry.Key).Trim() + "\", \"title\": \"" + GetColumnTitle(eEntry.Key) + "\", \"data\":\"" +eEntry.Key + "\"},";
                }

                jsonData = jsonData + serializer.Serialize(expando) + ",";
                jsonColumnData = jsonColumnData + jsonColumnDataTemp;
            }

            jsonData = "[" + jsonData.TrimEnd(new char[] { ',' }) + "]";
            jsonColumnData = "[" + jsonColumnDataTemp.TrimEnd(new char[] { ',' }) + "]";

            workoutSummaryResult.Data = jsonData;
            workoutSummaryResult.ColumnData = jsonColumnData;
            return workoutSummaryResult;

        }

        public JsonResult GetUserDetails()
        {
            AthleteUserModel athleteUserModel = new Models.AthleteUserModel();
            User athlete = null;
            var user = (User)Session["AuthenticatedUser"];
            if (user != null)
            {
                athlete = _iAthleteManagementRepository.GetAthleteInfoByID(user.UserId);
                if (athlete != null && athlete.Program != null)
                {
                   
                    athleteUserModel =  GetAthleteUserModel(athlete);
                }
            }

           return Json(athleteUserModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAthleteWorkoutDetails()
        {
		
			WorkoutSummaryViewModel workoutSummaryViewModel = GetAthleteWorkoutViewModel();
            WorkoutSummaryResult workoutSummaryResult = GetDynamicWorkoutSummaryModel(workoutSummaryViewModel);
            if (workoutSummaryViewModel.User != null)
            {
                workoutSummaryResult.UserName = workoutSummaryViewModel.User.FirstName + " " + workoutSummaryViewModel.User.LastName;
            }
			workoutSummaryResult.WorkoutStatusCode = workoutSummaryViewModel.WorkoutStatusCode;
		
			return Json(workoutSummaryResult, JsonRequestBehavior.AllowGet);
        }


		public JsonResult ConfirmWorkoutSession()
		{
			int currentSession = (int)System.Web.HttpContext.Current.Session["CurrentSession"];
			int currentStep= (int)System.Web.HttpContext.Current.Session["CurrentStep"];
			int currentGroup = (int)System.Web.HttpContext.Current.Session["CurrentGroup"];
			
			int programID = 0;
			User athlete = null;
			Program program = null;
			var user = (User)Session["AuthenticatedUser"];
			athlete = _iAthleteManagementRepository.GetAthleteInfoByID(user.UserId);

			if (athlete != null && athlete.Program != null)
			{
				programID = athlete.Program.Id;
				program = _iProgramManagementRepository.GetProgrambyID(programID);

				UserWorkoutStatus userWorkoutStatus = new UserWorkoutStatus();
				userWorkoutStatus.ProgramID = athlete.Program.Id;
				userWorkoutStatus.UserID = athlete.UserId;
				userWorkoutStatus.TrackID = athlete.TrackID;
				userWorkoutStatus.CompletedStep = currentStep;
				userWorkoutStatus.CompletedGroup = currentGroup;
				userWorkoutStatus.SessionCompleted = currentSession;
				userWorkoutStatus.StepGroupSessionCounter = 0;
				userWorkoutStatus.Status = 0;

				bool saveStatus = _iWorkoutManagement.SaveUserWorkoutStatus(userWorkoutStatus);
				if (saveStatus)
				{
					System.Web.HttpContext.Current.Session["CurrentSession"] = currentSession + 1;
				}
			}

			return Json("Success", JsonRequestBehavior.AllowGet);
		}

		private WorkoutSummaryViewModel GetAthleteWorkoutViewModel()
        {
            int programID = 0;
            User athlete = null;
            Program program = null;
            ProgramDetail programDetails = null;
            var user = (User)Session["AuthenticatedUser"];
            List<ENT.MovementType> lstMovement = new List<MovementType>();
            WorkoutSummaryViewModel workoutSummaryViewModel = new WorkoutSummaryViewModel();

			if (System.Web.HttpContext.Current.Session["CurrentSession"] == null)
			{
				System.Web.HttpContext.Current.Session["CurrentSession"] = 1;
			}
			if (System.Web.HttpContext.Current.Session["CurrentGroup"] == null)
			{
				System.Web.HttpContext.Current.Session["CurrentGroup"] =-1;
			}
			if (System.Web.HttpContext.Current.Session["CurrentStep"] == null)
			{
				System.Web.HttpContext.Current.Session["CurrentStep"] = -1;
			}

			int maxSets = 0;
            if (user != null)
            {
                athlete = _iAthleteManagementRepository.GetAthleteInfoByID(user.UserId);

				if (athlete != null && athlete.Program != null)
				{
					//Fill AthleteUser
					workoutSummaryViewModel.User = GetAthleteUserModel(athlete);
					programID = athlete.Program.Id;
					program = _iProgramManagementRepository.GetProgrambyID(programID);
					programDetails = _iProgramManagementRepository.GetProgramExercises(programID);
					maxSets = (programDetails.ProgramExercises.Count > 0) ? programDetails.ProgramExercises.Max(t => t.NumeofSets) : 0;
					lstMovement = _iMovementTypeReposioty.ListAllMovementTypes();

					var viewModels = new List<object>();
					ENT.ProgramDetail progDet = _iProgramManagementRepository.GetProgramExercisesWithSchedule(programID);

					int currentStep = -1;
					int currentGroup = -1;

					var workoutStatus = _iWorkoutManagement.GetUserWorkoutStatus(athlete.Program.Id, athlete.UserId).FirstOrDefault();
					workoutSummaryViewModel.WorkoutStatusCode = 1;
					int currentSession = 1;

					if (workoutStatus != null)
					{
						if (workoutStatus.SessionCompleted < program.TotalSessions)
						{
							currentSession = workoutStatus.SessionCompleted + 1;
							workoutSummaryViewModel.WorkoutStatusCode = 1;
						}
						else
						{
							currentSession = 0;
							workoutSummaryViewModel.WorkoutStatusCode = (int)WorkoutStatusCode.WorkoutSessionsComplete;
						}
					}

					if (workoutSummaryViewModel.WorkoutStatusCode != (int)WorkoutStatusCode.WorkoutSessionsComplete)
					{
						System.Web.HttpContext.Current.Session["CurrentSession"] = currentSession;
					
						if (progDet.SpecialSchedules.Count > 0)
						{
							var spSession = progDet.SpecialSchedules.Where(s => s.SessionNum == currentSession).FirstOrDefault();
							if (spSession != null)
							{
								currentStep = spSession.Step;
								currentGroup = spSession.Group;
								System.Web.HttpContext.Current.Session["CurrentGroup"] = currentGroup;
								System.Web.HttpContext.Current.Session["CurrentStep"] = currentStep;
							}

						}

						programDetails.ProgramExercises = progDet.ProgramExercises.Where(p => p.ProgramID == programID && p.ExerciseStepID == currentStep && p.ExerciseGroupID == currentGroup).ToList();
						if (programDetails.ProgramExercises != null && programDetails.ProgramExercises.Count >= 1)
						{
							programDetails.ProgramExercises = programDetails.ProgramExercises.OrderBy(p => p.OrderNum).ToList();
							workoutSummaryViewModel.WorkoutStatusCode = (int)WorkoutStatusCode.ExercisesFound;
						}
						else
						{
							workoutSummaryViewModel.WorkoutStatusCode = (int)WorkoutStatusCode.ExercisesNotFound;
                            var spSession = progDet.SpecialSchedules.Where(s => s.SessionNum == currentSession).FirstOrDefault();
                            if (spSession != null)
                            {
                                if (spSession.IsAssessmentStep)
                                {
                                    workoutSummaryViewModel.WorkoutStatusCode = (int)WorkoutStatusCode.AssessmentStep;
                                }
                            }
						}

						foreach (ENT.ProgramExercise progExercise in programDetails.ProgramExercises)
						{
							ProgramExerciseModel proExerModel = new ProgramExerciseModel();
							WorkoutExerciseModel workoutExerciseModel = new WorkoutExerciseModel();

							proExerModel.ExerciseID = Convert.ToString(progExercise.ExerciseID);
							proExerModel.ProgramExerciseID = Convert.ToString(progExercise.ID);

							workoutExerciseModel.Sets = progExercise.NumeofSets;
							var exercise = _iExerciseManagement.GetExercise(progExercise.ExerciseID);
							List<ENT.ExerciseTempo> proTempos = programDetails.ExerciseTempos.Where(t => t.ProgramExerciseID == progExercise.ID).ToList();

							if (proTempos != null && proTempos.Count > 0)
							{
								foreach (ENT.ExerciseTempo proTempo in proTempos)
								{
									ExerciseSetModel exerciseSetModel = new ExerciseSetModel();
									exerciseSetModel.SetNumber = proTempo.SetNum;
									exerciseSetModel.Tempo = GetTempoText(proTempo.Tempo);
									exerciseSetModel.ExerciseTempoId = proTempo.ID;
									exerciseSetModel.TargetLoad = proTempo.PrimaryIntensityTarget;
									exerciseSetModel.TargetReps = proTempo.PrimaryTarget;
									exerciseSetModel.TimeToComplete = proTempo.TimeToComplete;
									workoutExerciseModel.ExerciseSets.Add(exerciseSetModel);
									//if (progExercise.PrimaryIntensityUnit == 1)//%RM
									//{
									//    exerciseSetModel.OneRM = proTempo.PrimaryIntensityTarget;
									//}
									//else if (progExercise.PrimaryIntensityUnit == 1)//Load
									//{
									//    exerciseSetModel.TargetLoad = proTempo.PrimaryIntensityTarget;
									//}
									//else if (progExercise.PrimaryIntensityUnit == 3)//BodyWeight
									//{
									//    exerciseSetModel.TargetLoad = proTempo.PrimaryIntensityTarget;
									//}

									//if (progExercise.PrimaryExerciseUnits == 1)//#Reps
									//{
									//    exerciseSetModel.TargetReps = proTempo.PrimaryTarget;
									//}
									//else if (progExercise.PrimaryExerciseUnits == 1)//#Reps
									//{
									//    exerciseSetModel.TargetReps = proTempo.PrimaryTarget;
									//}
								}
								workoutExerciseModel.TargetLoad = proTempos.FirstOrDefault().PrimaryIntensityTarget;
								//workoutExerciseModel.OneRM = proTempos.FirstOrDefault().PrimaryIntensityTarget;
								//workoutExerciseModel.Dynamic1RM = proTempos.FirstOrDefault().PrimaryIntensityTarget;
								workoutExerciseModel.TargetReps = proTempos.FirstOrDefault().PrimaryTarget;
								workoutExerciseModel.Tempo = GetTempoText(proTempos.FirstOrDefault().Tempo);
							}

							proExerModel.Name = exercise.Name;
							ENT.MovementType movementType = lstMovement.Where(m => m.Id == progExercise.MovementTypeID).FirstOrDefault();
							proExerModel.MovementType = movementType == null ? "" : movementType.Name;
							workoutExerciseModel.OrderNumber = progExercise.OrderNum.ToString();
							if (progExercise.PairedExercise)
							{
								workoutExerciseModel.OrderNumber = progExercise.OrderNum + "A";
							}
							workoutExerciseModel.Rest1 = progExercise.RestBetweenPairedExercises;
							workoutExerciseModel.Rest2 = progExercise.RestBetweenSets;
							workoutExerciseModel.ExerciseName = proExerModel.Name;
							workoutExerciseModel.MovementTypeName = proExerModel.MovementType;
							workoutExerciseModel.Status = false;
							workoutExerciseModel.ExerciseId = Convert.ToInt32(proExerModel.ExerciseID);
							workoutExerciseModel.MasterExerciseId = Convert.ToInt32(proExerModel.ExerciseID);
							workoutExerciseModel.ProgramExerciseId = Convert.ToInt32(proExerModel.ProgramExerciseID);
							workoutExerciseModel.GroupId = Convert.ToInt32(progExercise.ExerciseGroupID);
							workoutExerciseModel.StepId = Convert.ToInt32(progExercise.ExerciseStepID);
							workoutSummaryViewModel.WorkoutExercises.Add(workoutExerciseModel);
							if (progExercise.PairedExercise)
							{
								WorkoutExerciseModel pairedWorkoutExerciseModel = ProcessPairedExercise(programDetails, progExercise.ID);
								if (pairedWorkoutExerciseModel != null)
									workoutSummaryViewModel.WorkoutExercises.Add(pairedWorkoutExerciseModel);
							}
						}

						decimal progTimeToComplete = 0M;
						progTimeToComplete = program.DynamicWarmup + program.FoamRollOut;
						foreach (ENT.ExerciseTempo exTempo in programDetails.ExerciseTempos)
						{
							progTimeToComplete = progTimeToComplete + exTempo.TimeToComplete;
						}
					}
				}
            }
            return workoutSummaryViewModel;
        }

        private WorkoutExerciseModel ProcessPairedExercise(ENT.ProgramDetail progDet, int primaryProgID)
		{
			string jsonData = string.Empty;
            WorkoutExerciseModel workoutExerciseModel = new WorkoutExerciseModel();
            var viewModels = new List<object>();
			_iProgramManagementRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();
			_iMovementTypeReposioty = ObjectFactory.GetInstance<IMovementTypeRepository>();
			int maxSets = (progDet.ProgramExercises.Count > 0) ? progDet.ProgramExercises.Max(t => t.NumeofSets) : 0;
			List<ENT.MovementType> lstMovement = _iMovementTypeReposioty.ListAllMovementTypes();
			if (progDet.ProgramExercises != null && progDet.ProgramExercises.Count > 1)
			{
				progDet.ProgramExercises = progDet.ProgramExercises.OrderBy(p => p.OrderNum).ToList();
			}
			foreach (ENT.ProgramExercise progExercise in progDet.ProgramExercises)
			{
				if (progExercise.PairedExerciseID <= 0)
				{
					continue;
				}
				if (progExercise.ID != primaryProgID)
				{
					continue;
				}

				ProgramExerciseModel proExerModel = new ProgramExerciseModel();
               
                proExerModel.ExerciseID = Convert.ToString(progExercise.PairedExerciseID);
				proExerModel.ProgramExerciseID = Convert.ToString(progExercise.ID);
                var exercise = _iExerciseManagement.GetExercise(progExercise.PairedExerciseID);
                string exerciseName = "";
                workoutExerciseModel.Sets = progExercise.NumeofSets;
                         
				List<ENT.ExerciseTempo> proTempos = progDet.ExerciseTempos.Where(t => t.ProgramExerciseID == primaryProgID).ToList();
				if (proTempos != null && proTempos.Count > 0)
				{
					foreach (ENT.ExerciseTempo proTempo in proTempos)
					{
                        ExerciseSetModel exerciseSetModel = new ExerciseSetModel();
                        exerciseSetModel.SetNumber = proTempo.SetNum;
                        exerciseSetModel.Tempo = GetTempoText(proTempo.Tempo);
                        exerciseSetModel.ExerciseTempoId = proTempo.ID;
                        exerciseSetModel.TargetLoad = proTempo.PairedIntensityTarget;
                        exerciseSetModel.TargetReps = proTempo.PairedTarget;
                        exerciseSetModel.TimeToComplete = proTempo.TimeToComplete;
                        workoutExerciseModel.ExerciseSets.Add(exerciseSetModel);
                    }

                    proExerModel.LoadOrRM = Convert.ToString(proTempos.FirstOrDefault().PrimaryIntensityTarget);
                    proExerModel.RepOrSec = Convert.ToString(proTempos.FirstOrDefault().PairedTarget);
                    workoutExerciseModel.TargetLoad = proExerModel.LoadOrRM != null ? Convert.ToInt32(proExerModel.LoadOrRM) : 0;
                    ///workoutExerciseModel.OneRM = proExerModel.LoadOrRM != null ? Convert.ToInt32(proExerModel.LoadOrRM) : 0;
                    //workoutExerciseModel.Dynamic1RM = proExerModel.LoadOrRM != null ? Convert.ToInt32(proExerModel.LoadOrRM) : 0;
                    workoutExerciseModel.TargetReps = proExerModel.RepOrSec != null ? Convert.ToInt32(proExerModel.RepOrSec) : 0;
                    workoutExerciseModel.Tempo = GetTempoText(proTempos.FirstOrDefault().Tempo);
                }

				proExerModel.Name = exerciseName;
				ENT.MovementType movementType = lstMovement.Where(m => m.Id == progExercise.PairedExerciseMovementID).FirstOrDefault();
				proExerModel.MovementType = movementType == null ? "" : movementType.Name;

                workoutExerciseModel.OrderNumber = progExercise.OrderNum + "B";
                workoutExerciseModel.ExerciseName = exercise.Name != null ? exercise.Name : string.Empty;
                workoutExerciseModel.MovementTypeName= proExerModel.MovementType != null ? proExerModel.MovementType : string.Empty;
                workoutExerciseModel.Status = false;
                workoutExerciseModel.ExerciseId = Convert.ToInt32( proExerModel.ExerciseID);
                workoutExerciseModel.MasterExerciseId = Convert.ToInt32(proExerModel.ExerciseID);
                workoutExerciseModel.ProgramExerciseId = Convert.ToInt32(proExerModel.ProgramExerciseID);
                workoutExerciseModel.Rest1 = progExercise.RestBetweenPairedExercises;
                workoutExerciseModel.Rest2 = progExercise.RestBetweenSets;
				workoutExerciseModel.GroupId = Convert.ToInt32(progExercise.ExerciseGroupID);
				workoutExerciseModel.StepId = Convert.ToInt32(progExercise.ExerciseStepID);

			}
			return workoutExerciseModel;
		}
	}
}
