using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.IO;
using StrengthTracker2.Core.DataTypes;
using System.Globalization;
using System.Dynamic;
using System.Configuration;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using StructureMap;

using MyProgrammer.Core.Services;

using StrengthTracker2.Core.Repository.Contracts.Account;
using StrengthTracker2.Core.Repository.Contracts.Customers;
using StrengthTracker2.Core.Repository.Contracts.AthleteManagement;
using StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using StrengthTracker2.Core.Repository.Contracts.Security;
using StrengthTracker2.Core.Repository.Contracts.TKGMaster;
using ENT = StrengthTracker2.Core.Repository.Entities.ProgramManagement;
using StrengthTracker2.Core.Repository.Entities.Security;
using StrengthTracker2.Core.Repository.Entities.Customers;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Core.Repository.Entities.TKGMaster;
using StrengthTracker2.Core.Repository.Contracts.Teams;

using StrengthTracker2.Web.Models;
using StrengthTracker2.Web.Models.Admin;
using StrengthTracker2.Web.Models.Coach;
using StrengthTracker2.Web.Helpers;

namespace StrengthTracker2.Web.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        private IAthleteManagementRepository _iAthleteManagementRepository;
        private IProgramManagementRepository _iProgramMgmtRepository;
        private ISportsManagementRepository _iSportMgmtRepository;
        private IPositionMgmtRepository _iPositionMgmtRepository;
        private readonly ICustomerLocationMgmtRepository _iCustomerLocationMgmtRepository = ObjectFactory.GetInstance<ICustomerLocationMgmtRepository>();
        private ILocationContactManagementRepository _ilocationCtcMgmtRepository;
        private ILocationPricingManagementRepository _iLocationPricingMgmtRepository;
        private ICustomerManagementRepository _iCustomerManagementRepository;
        private ICustomerMasterMgmtRepository _iCustomerMgmt;
        private IState _iStateRepository;
        private readonly IAccount _iAccount;
        private readonly IMovementTypeRepository _iMovementTypeReposioty = ObjectFactory.GetInstance<IMovementTypeRepository>();
        private IExerciseManagement _iExerciseManagement;

        private IRoleManagement _iRoleMgmtRepository;
        private ICustomerLocationRoleMgmtRepository _icustomerLocationRoleMgmtRepository;
        private readonly ITeamManagmentRepository _iteamMgmtRepository;
        private IUOMManagementRepository _uomMgmtRep;

        private readonly IUserHistoryRepository userHistoryRepository;
        private readonly IAthleteFilterRepository athleteFilterRepository;
        private readonly IUserDataVisibilityRepository userDataVisibilityRepository;
        private readonly ICustomerCategoryTypeFilter customerCategoryTypeFilter;
        private readonly IMailService _mailService;
        private readonly IMapperService _mapperService = new MapperService();

        static readonly string[] Columns = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };


        public AdminController()
        {
            _iProgramMgmtRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();
            _iExerciseManagement = ObjectFactory.GetInstance<IExerciseManagement>();
            _iRoleMgmtRepository = ObjectFactory.GetInstance<IRoleManagement>();
            _icustomerLocationRoleMgmtRepository = ObjectFactory.GetInstance<ICustomerLocationRoleMgmtRepository>();
            _iAthleteManagementRepository = ObjectFactory.GetInstance<IAthleteManagementRepository>();
            _iAccount = ObjectFactory.GetInstance<IAccount>();
            _iteamMgmtRepository = ObjectFactory.GetInstance<ITeamManagmentRepository>();
            _iSportMgmtRepository = ObjectFactory.GetInstance<ISportsManagementRepository>();
            _iPositionMgmtRepository = ObjectFactory.GetInstance<IPositionMgmtRepository>();
            userHistoryRepository = ObjectFactory.GetInstance<IUserHistoryRepository>();
            athleteFilterRepository = ObjectFactory.GetInstance<IAthleteFilterRepository>();
            userDataVisibilityRepository = ObjectFactory.GetInstance<IUserDataVisibilityRepository>();
            _iCustomerManagementRepository = ObjectFactory.GetInstance<ICustomerManagementRepository>();
            customerCategoryTypeFilter = ObjectFactory.GetInstance<ICustomerCategoryTypeFilter>();
            _iStateRepository = ObjectFactory.GetInstance<IState>();
            _mailService = ObjectFactory.GetInstance<IMailService>();
            _iCustomerMgmt = ObjectFactory.GetInstance<ICustomerMasterMgmtRepository>();
        }

        public ActionResult Program()
        {
            if (Session["AuthenticatedUser"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Sports()
        {
            return View();
        }

        public ActionResult Position()
        {
            return View();
        }

        public ActionResult Users()
        {
            var lstCustomerLocation = _iCustomerLocationMgmtRepository.ListAllLocations();
            ViewBag.LocationList = lstCustomerLocation;

            var lstRole = _iRoleMgmtRepository.ListAllRoles()
                .Where(r => r.IsActive == true);
            ViewBag.RoleList = lstRole;

            var td = _iteamMgmtRepository.ListTeamswithAllDetails();
            if (td.Teams != null && td.Teams.Count > 0)
            {
                ViewBag.TeamList = td.Teams;
            }

            List<SelectListViewModel> lstState = new List<SelectListViewModel>();
            SelectListViewModel posState = new SelectListViewModel();
            posState.Value = "0";
            posState.Text = "Select State";
            posState.Selected = false;
            lstState.Add(posState);
            foreach (var state in _iStateRepository.ListStates())
            {
                posState = new SelectListViewModel();
                posState.Value = state.ID.ToString();
                posState.Text = state.Name;
                lstState.Add(posState);
            }
            ViewBag.StateList = lstState;

            var lstSport = _iSportMgmtRepository.ListSports(true);
            ViewBag.SportsList = lstSport;
            Session["PersonalPrefix"] = Convert.ToString(ConfigurationManager.AppSettings["PersonalPrefix"]) + Convert.ToString(Session["CustomerName"]);
            return View();
        }

        public ActionResult PersonalTrainerProfileSection()
        {
            return View();
        }

        public ActionResult ManageUsers()
        {
            List<SelectListViewModel> lstState = new List<SelectListViewModel>();
            SelectListViewModel posState = new SelectListViewModel();
            posState.Value = "0";
            posState.Text = "Select State";
            posState.Selected = false;
            lstState.Add(posState);
            foreach (var state in _iStateRepository.ListStates())
            {
                posState = new SelectListViewModel();
                posState.Value = state.ID.ToString();
                posState.Text = state.Name;
                lstState.Add(posState);
            }
            ViewBag.StateList = lstState;
            return View();
        }

        public ActionResult Roles()
        {
            if (Session["AuthenticatedUser"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult ManageRoles()
        {
            return View();
        }

        public ActionResult Exercises()
        {
            return View();
        }

        public ActionResult Customers()
        {
            return View();
        }

        public ActionResult AuditLog()
        {
            return View();
        }

        #region Programs

        public ActionResult ProgramExercises(int programID)
        {
            var prog = _iProgramMgmtRepository.GetProgrambyID(programID);
            var lstSessionModel = new List<WrokoutSessionModel>();

            for (int i = 1; i <= prog.Steps; i++)
            {
                for (int j = 1; j <= prog.TotalGroups; j++)
                {
                    var wSession = new WrokoutSessionModel
                    {
                        StepGroup = Convert.ToString(i + "~" + j),
                        StepGroupDisp = Convert.ToString("Step " + i + " - Group " + Columns[j - 1])
                    };


                    lstSessionModel.Add(wSession);
                }
            }
            ViewBag.ProgramName = prog.Title;
            ViewBag.SessionModelList = lstSessionModel;
            ViewBag.IsActiveProgram = prog.IsActive;
            return View();
        }

        [Authorize]
        public ActionResult ManageProgram(string programID)
        {
            List<ENT.Sport> lstSport = _iSportMgmtRepository.ListSports(true);

            ViewBag.SportsList = lstSport;

            ENT.Program prog = new ENT.Program();

            int _programID = programID == null ? 0 : Convert.ToInt32(programID);

            if (_programID > 0)
                prog = _iProgramMgmtRepository.GetProgrambyID(_programID);

            List<ENT.Position> lstPosition = _iPositionMgmtRepository.ListPositionBySportID(prog.SportID == 0 ? lstSport[0].Id : prog.SportID);

            ViewBag.PositionList = lstPosition;

            return View();
        }

        public JsonResult ListPositionsBySportID(int sportID)
        {
            _iPositionMgmtRepository = ObjectFactory.GetInstance<IPositionMgmtRepository>();

            List<ENT.Position> lstPosition = _iPositionMgmtRepository.ListPositionBySportID(sportID);

            return Json(lstPosition, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult AddExerciseToProgram(int programID)
        {
            var model = new AddExerciseToProgramViewModel()
            {
                ListGroups = new List<string>()
            };

            var prog = _iProgramMgmtRepository.GetProgrambyID(programID);
            if (prog != null)
            {
                model.CurrentProgram = prog;
                for (int gi = 1; gi <= prog.TotalGroups; gi++)
                {
                    string group = Columns[gi - 1];
                    //Char c = (Char)((65) + (gi-1));
                    //lstGroup.Add(c.ToString());
                    model.ListGroups.Add(group);
                }

                ENT.Sport sport = _iSportMgmtRepository.ListSports().FirstOrDefault(s => s.Id == prog.SportID);

                ENT.Position position = _iPositionMgmtRepository.ListPositionBySportID(prog.SportID).FirstOrDefault(p => p.ID == prog.PositionID);

                ViewBag.TrainingPhase = prog.TrainingPhase;
                ViewBag.TrainingSeason = prog.TrainingSeasonID == 1 ? "In Season" : "Off Season";
                ViewBag.Sport = sport == null ? "" : sport.Name;
                ViewBag.Position = position == null ? "" : position.Name;
                ViewBag.IsActiveProgram = prog.IsActive;

            }

            model.ListExercises = _iExerciseManagement.ListExercises().OrderBy(e => e.Name).ToList();
            model.ListMovementTypes = _iMovementTypeReposioty.ListAllMovementTypes().OrderBy(m => m.Name).ToList();

            var Formula = new SelectList(Enum.GetValues(typeof(Formula)).Cast<Formula>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");

            ViewBag.Formulas = Formula;

            return View(model);
        }

        public ActionResult GetUOMs()
        {
            _uomMgmtRep = ObjectFactory.GetInstance<IUOMManagementRepository>();

            List<ENT.UOM> lstUOM = _uomMgmtRep.GetAllUOM();

            return Json(lstUOM, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveExercise(ExerciseModel exerciseModel)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            ro.Status = ReturnStatus.Err;
            ro.Message = "Exercise not saved successfully";

            ENT.Exercise exercise = new ENT.Exercise();

            exercise.Id = exerciseModel.Id;
            exercise.Name = exerciseModel.Name;
            if (exerciseModel.Description != null)
                exercise.Description = exerciseModel.Description;
            else
                exercise.Description = "";
            exercise.MovementTypeID = exerciseModel.MovementTypeID;
            exercise.UOMID = exerciseModel.UOMID;
            exercise.BWPercentageforVolume = exerciseModel.BWPercentageforVolume;
            exercise.Formula = exerciseModel.Formula;

            _iExerciseManagement = ObjectFactory.GetInstance<IExerciseManagement>();

            int exerciseID = _iExerciseManagement.SaveExercise(exercise);

            if (exerciseID > 0)
            {
                ro.Status = ReturnStatus.OK;
                ro.Message = "Exercise Saved Successfully|" + exerciseID;
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetExerciseList(int movementTypeID)
        {
            var result = _iExerciseManagement.ListExercises(movementTypeID).OrderBy(e => e.Name).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteExercise(int ProgramId, int programExerciseId, bool isTKGProgram)
        {
            var res = _iProgramMgmtRepository.DeleteProgramExercise(programExerciseId);

            if (res != true)
            {
                return View("Error");
            }

            return Json(Url.Action("ProgramExercises", "Admin", new { programID = ProgramId, p = isTKGProgram }));
        }

        public ActionResult ProgramDWSession(int programID)
        {
            var prog = _iProgramMgmtRepository.GetProgrambyID(programID);

            _iSportMgmtRepository = ObjectFactory.GetInstance<ISportsManagementRepository>();
            _iPositionMgmtRepository = ObjectFactory.GetInstance<IPositionMgmtRepository>();

            var position = _iPositionMgmtRepository.ListPositionBySportID(prog.SportID)
                //.Where(p => p.ID == prog.PositionID)
                .FirstOrDefault();

            var sport = _iSportMgmtRepository.ListSports(true)
                .Where(s => s.Id == prog.SportID)
                .FirstOrDefault();

            var sessionList = new List<string>();
            for (int i = 0; i < prog.TotalSessions; i++)
            {
                sessionList.Add(string.Format("{0}", i + 1));
            }

            var model = new DWSessionViewModel()
            {
                SessionList = sessionList,
                SelectedSession = sessionList.FirstOrDefault(),
                Program = prog.Description,
                Position = position.Name,
                Phase = prog.TrainingPhase,
                Sport = sport.Name
            };
            ViewBag.ProgramName = prog.Title;
            ViewBag.IsActiveProgram = prog.IsActive;
            return View(model);
        }

        private string GenerateExcerciseGridColumnsForSession(int maxSets)
        {
            List<object> cList = new List<object>();
            TemplateColumn c1 = new TemplateColumn();
            c1.field = "LoadOrRM";
            c1.title = "Starting 1RM (Lbs)";
            c1.width = "100px";
            //c1.template = "<div style=\"#=TextAlign#\"><a href=\"Javascript(void);\" class=\"#=StyleUp#;\"><i class=\"fa fa-arrow-up\" style=\"display:#=OrderUp#;color:#=ColorUp#;padding-left:23px;\"></i></a> <a href=\"JavaScript:void(0);\"><i class=\"fa fa-arrow-down\" style=\"display:#=OrderDown#;color:#=ColorDown#;padding-left:8px;\"></i></a><div>";
            cList.Add(c1);


            TemplateColumn c2 = new TemplateColumn();
            c2.field = "Name";
            c2.title = "Exercise";
            //c2.template = "<a onclick=\"NavigatetoAdd(#=MasterExerciseID#);\" style=\"cursor: pointer;\">#=Name#</a>";
            c2.width = "100px";
            cList.Add(c2);

            Column c3 = new Column();
            c3.field = "MovementType";
            c3.title = "Movements";
            cList.Add(c3);

            //Column c4 = new Column();
            //c4.field = "Tempo1";
            //c4.title = "Tempo";
            //cList.Add(c4);

            Column c5 = new Column();
            c5.field = "Rest1";
            c5.title = "Rest Between Paired Exercises (Mins)";
            cList.Add(c5);

            Column c6 = new Column();
            c6.field = "Rest2";
            c6.title = "Rest Between Sets (Mins)";
            cList.Add(c6);

            ColumnTree ci = null;
            Column cj = null;
            Column ck = null;
            Column cl = null;
            for (int i = 0; i < maxSets; i++)
            {
                ci = new ColumnTree();
                ci.columns = new List<Column>();
                ci.field = "Set" + (i + 1);
                ci.title = "Set" + (i + 1);
                cj = new Column();
                cj.field = "LoadOrRM" + (i + 1);
                cj.title = "Target Load or % 1RM";
                ck = new Column();
                ck.field = "RepOrSec" + (i + 1);
                ck.title = "Target Reps or Sec";
                cl = new Column();
                cl.field = "Tempo" + (i + 1);
                cl.title = "Tempo";
                ci.columns.Add(cl);
                ci.columns.Add(cj);
                ci.columns.Add(ck);
                cList.Add(ci);
            }

            var serializer = new JavaScriptSerializer();
            var jsonColumns = serializer.Serialize(cList);

            return "{ \"Columns\" : [" + jsonColumns + "]}";
        }

        public JsonResult SessionChanged(int exerciseSession, int programID)
        {
            var viewModels = new List<object>();
            ENT.ProgramDetail progDet = _iProgramMgmtRepository.GetProgramExercisesWithSchedule(programID);

            int exerciseStep = -1;
            int exerciseGroup = -1;
            if (progDet.SpecialSchedules.Count > 0)
            {
                exerciseStep = progDet.SpecialSchedules.Where(s => s.SessionNum == exerciseSession).FirstOrDefault().Step;
                exerciseGroup = progDet.SpecialSchedules.Where(s => s.SessionNum == exerciseSession).FirstOrDefault().Group;
            }
            else
            {

            }


            progDet.ProgramExercises = progDet.ProgramExercises.Where(p => p.ProgramID == programID && p.ExerciseStepID == exerciseStep && p.ExerciseGroupID == exerciseGroup).ToList();

            ENT.Program prog = new ENT.Program();

            //prog = _iProgramMgmtRepository.GetProgrambyID(programID);

            prog = progDet.Programs[0];

            dynamic expando = new ExpandoObject();
            var expandoObj = expando as IDictionary<String, object>;
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new ExpandoJSONConverter() });

            //int maxSets = (progDet.ProgramExercises.Count>1)?progDet.ProgramExercises.Max(t => t.NumeofSets):0;
            int maxSets = (progDet.ProgramExercises.Count > 0) ? progDet.ProgramExercises.Max(t => t.NumeofSets) : 0;
            string columnsJson = GenerateExcerciseGridColumnsForSession(maxSets);
            string jsonData = string.Empty;

            List<ENT.MovementType> lstMovement = _iMovementTypeReposioty.ListAllMovementTypes();

            if (progDet.ProgramExercises != null && progDet.ProgramExercises.Count > 1)
            {
                progDet.ProgramExercises = progDet.ProgramExercises.OrderBy(p => p.OrderNum).ToList();
            }
            foreach (ENT.ProgramExercise progExercise in progDet.ProgramExercises)
            {
                ProgramExerciseModel proExerModel = new ProgramExerciseModel();
                expando = new ExpandoObject();
                expandoObj = expando as IDictionary<String, object>;

                proExerModel.ExerciseID = Convert.ToString(progExercise.ExerciseID);
                proExerModel.ProgramExerciseID = Convert.ToString(progExercise.ID);
                string exerciseName = "";
                var exercise = _iExerciseManagement.GetExercise(progExercise.ExerciseID);
                //switch (progExercise.ExerciseID)
                //{
                //    case 48:
                //        exerciseName = "Squat, Back";
                //        break;
                //    case 8:
                //        exerciseName = "Bench Press Flat, Dumbbell";
                //        break;
                //    case 9:
                //        exerciseName = "Bench Press, Flat, Barbell";
                //        break;
                //    case 17:
                //        exerciseName = "Push Up";
                //        break;
                //    case 28:
                //        exerciseName = "Box Jumps";
                //        break;
                //    case 39:
                //        exerciseName = "Hops, Double Leg";
                //        break;
                //    case 40:
                //        exerciseName = "Hops, Single Leg";
                //        break;
                //}
                exerciseName = exercise.Name;
                List<ENT.ExerciseTempo> proTempos = progDet.ExerciseTempos.Where(t => t.ProgramExerciseID == progExercise.ID).ToList();
                if (proTempos != null && proTempos.Count > 0)
                {
                    string tempo = string.Empty;
                    foreach (ENT.ExerciseTempo proTempo in proTempos)
                    {
                        tempo = TempoModel.Tempos.FirstOrDefault(k => k.Value == proTempo.Tempo.ToString()).Key;

                        expandoObj["Set" + proTempo.SetNum.ToString()] = proTempo.SetNum.ToString();
                        expandoObj["Tempo" + proTempo.SetNum.ToString()] = tempo;
                        expandoObj["TempoID" + proTempo.SetNum.ToString()] = proTempo.ID.ToString();
                        expandoObj["ProgramExerciseID" + proTempo.SetNum.ToString()] = proTempo.ProgramExerciseID.ToString();
                        //expandoObj["Rest" + proTempo.SetNum.ToString()] = proTempo.RestInterval.ToString();
                        if (progExercise.PrimaryExerciseTarget == 1)
                        {
                            expandoObj["LoadOrRM" + proTempo.SetNum.ToString()] = proTempo.PrimaryTarget;
                            expandoObj["RepOrSec" + proTempo.SetNum.ToString()] = proTempo.PrimaryIntensityTarget;
                        }
                        else
                        {
                            expandoObj["LoadOrRM" + proTempo.SetNum.ToString()] = proTempo.PrimaryIntensityTarget;
                            expandoObj["RepOrSec" + proTempo.SetNum.ToString()] = proTempo.PrimaryTarget;
                        }

                        expandoObj["TimeToComplete" + proTempo.SetNum.ToString()] = proTempo.TimeToComplete.ToString();
                    }
                }

                proExerModel.Name = exerciseName;
                ENT.MovementType movementType = lstMovement.FirstOrDefault(m => m.Id == progExercise.MovementTypeID);

                proExerModel.MovementType = movementType == null ? "" : movementType.Name;

                var exerciseTempo = proTempos.FirstOrDefault();
                proExerModel.LoadOrRM = progExercise.IntensityVol == 2 && exerciseTempo != null ? Convert.ToString(proTempos.FirstOrDefault().TargetReps) : "";
                proExerModel.RepOrSec = progExercise.IntensityVol == 1 && exerciseTempo != null ? Convert.ToString(proTempos.FirstOrDefault().TargetIntensity) : "";
                proExerModel.Rest = proTempos.FirstOrDefault() == null ? "" : Convert.ToString(proTempos.FirstOrDefault().RestInterval); //"60";
                proExerModel.Rest2 = progExercise.RestBetweenSets.ToString();

                proExerModel.TextAlign = "text-align:left;";
                proExerModel.StyleUp = "p-lg";
                proExerModel.OrderUp = "inline";
                proExerModel.OrderDown = "inline";
                proExerModel.ColorUp = "lightgray";
                proExerModel.ColorDown = "darkgray";

                expandoObj["Order"] = progExercise.OrderNum;
                if (progExercise.PairedExercise)
                {
                    expandoObj["Order"] = progExercise.OrderNum + "A";
                }
                expandoObj["Rest1"] = progExercise.RestBetweenPairedExercises.ToString();//.RestInterval.ToString();
                expandoObj["Rest2"] = progExercise.RestBetweenSets.ToString();//.RestInterval.ToString();
                expandoObj["OrderUp"] = proExerModel.OrderUp;
                expandoObj["OrderDown"] = proExerModel.OrderDown;
                expandoObj["StyleUp"] = proExerModel.StyleUp;
                expandoObj["ColorUp"] = proExerModel.ColorUp;
                expandoObj["ColorDown"] = proExerModel.ColorDown;
                expandoObj["TextAlign"] = proExerModel.TextAlign;
                expandoObj["Name"] = proExerModel.Name;
                expandoObj["MovementType"] = proExerModel.MovementType;
                expandoObj["ExerciseID"] = proExerModel.ExerciseID;
                expandoObj["MasterExerciseID"] = proExerModel.ExerciseID;
                expandoObj["ProgramExerciseID"] = proExerModel.ProgramExerciseID;
                jsonData = jsonData + serializer.Serialize(expando) + ",";

                if (progExercise.PairedExercise)
                {
                    jsonData = jsonData + GetPairedExercise(progDet, progExercise.ID);
                }

            }

            jsonData = "{ \"ProgramsList\" : [" + jsonData.TrimEnd(new char[] { ',' }) + "]}";
            viewModels.Add(jsonData);
            viewModels.Add(columnsJson);

            decimal progTimeToComplete = 0M;
            progTimeToComplete = prog.DynamicWarmup + prog.FoamRollOut;
            //decimal exerciseTimeTocomplete = 0M;
            foreach (ENT.ExerciseTempo exTempo in progDet.ExerciseTempos)
            {
                progTimeToComplete = progTimeToComplete + exTempo.TimeToComplete;
                //exerciseTimeTocomplete = exerciseTimeTocomplete + exTempo.TimeToComplete;
            }

            viewModels.Add(progTimeToComplete);

            if (progDet.SpecialSchedules != null && progDet.SpecialSchedules.Count > 0)
            {
                ENT.SpecialScheduleSessions splSchd = progDet.SpecialSchedules.Where(s => s.SessionNum == exerciseSession).FirstOrDefault();

                if (splSchd != null)
                {
                    if (!splSchd.IsAssessmentStep)
                    {
                        string group = Columns[splSchd.Group - 1];

                        viewModels.Add(splSchd.Step);
                        viewModels.Add(group);
                    }
                    else
                    {
                        viewModels.Add("isAssessmentStep=true");
                    }
                }
            }

            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListAllPrograms()
        {
            var programs = _iProgramMgmtRepository.ListPrograms().Select(p => new { ID = p.Id, Text = p.Title });
            return Json(programs, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Dashboard

        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult DashboardNew()
        {
            return View();
        }
        public ActionResult CoachDashboardNew()
        {
            var coachs = _iAccount.ListUsersByType(UserType.Coach);
            var dashboardModel = new CoachDashboardModel();
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
        private List<Permission> GetUserPermissions(int userId)
        {
            //int userId = ((StrengthTracker2.Core.Repository.Entities.Actors.User)(Session["AuthenticatedUser"])).UserId;
            var customerLocationRoleList = _icustomerLocationRoleMgmtRepository.customerLocationRoles(userId);
            var permisionList = _iRoleMgmtRepository.ListPermissions();
            var userPermissionList = new List<Permission>();
            foreach (var customerLocationRole in customerLocationRoleList)
            {
                foreach (var roleId in customerLocationRole.RoleId.Split(','))
                {
                    var roleDetail = _iRoleMgmtRepository.GetRoleInfo(Convert.ToInt32(roleId));
                    var rolePermissionList = roleDetail.RolePermissions.Where(rp => rp.CanView.HasValue && rp.CanView.Value);
                    foreach (var rolePermission in rolePermissionList)
                    {
                        if (userPermissionList.SingleOrDefault(up => up.PermissionID == rolePermission.PermissionID) == null)
                        {
                            userPermissionList.Add(permisionList.First(p => p.PermissionID == rolePermission.PermissionID));
                        }
                    }
                }
            }
            return userPermissionList;
        }




        [HttpPost]
        public ActionResult GetCustomersChartData()
        {
            int intCurrentYear = DateTime.Now.Year;

            var startDateTime = DateTime.ParseExact("01/01/" + intCurrentYear, "MM/dd/yyyy", null);
            var endDateTime = DateTime.ParseExact("12/31/" + intCurrentYear, "MM/dd/yyyy", null);

            _iCustomerManagementRepository = ObjectFactory.GetInstance<ICustomerManagementRepository>();

            var customerDetails = _iCustomerManagementRepository.GetCustomerByCreateDate(startDateTime, endDateTime);

            List<Customer> lstCustomerForProfit = new List<Customer>();
            List<Customer> lstNonprofitCustomer = new List<Customer>();

            List<string> lstMonth = new List<string> { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };

            foreach (CustomerDetails custDet in customerDetails)
            {
                if (custDet.CustomerPricings != null && custDet.CustomerPricings.Count() > 0)
                {
                    CustomerPricing custPricing = custDet.CustomerPricings.FirstOrDefault();

                    if (custPricing.TypeOfCustomer == CustomerType.ForProfit)
                    {
                        lstCustomerForProfit.Add(custDet.Customer);
                    }
                    else
                    {
                        lstNonprofitCustomer.Add(custDet.Customer);
                    }
                }
            }

            var profitMonthwiseTotal = from p in lstCustomerForProfit
                                       group p by p.CreateDate.ToString("MM") into grp
                                       select new { Month = grp.Key, Count = grp.Count() };

            var nonprofitMonthwiseTotal = from p in lstNonprofitCustomer
                                          group p by p.CreateDate.ToString("MM") into grp
                                          select new { Month = grp.Key, Count = grp.Count() };

            var profitGroupedByMonth = profitMonthwiseTotal.ToList();
            var nonprofitGroupedByMonth = nonprofitMonthwiseTotal.ToList();

            var profitGridDateModel = new DashboardGridDataModel();
            profitGridDateModel.label = "Profit";
            profitGridDateModel.data = new List<long[]>();
            //foreach (var item in lstCustomerForProfit)
            //    profitGridDateModel.data.Add(new long[] { (long)item.CreateDate.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, 1 });
            foreach (var item in lstMonth)
            {
                int itemCount = 0;
                DateTime firstOfMonth = DateTime.Parse(item + "/01/" + intCurrentYear);
                var monthData = profitGroupedByMonth.FirstOrDefault(m => m.Month == item);

                if (monthData != null)
                {
                    itemCount = monthData.Count;
                }

                profitGridDateModel.data.Add(new long[] { (long)firstOfMonth.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, itemCount });
            }

            var nonprofitGridDataModel = new DashboardGridDataModel
            {
                label = "Non-profit",
                data = new List<long[]>()
            };
            //foreach (var item in lstNonprofitCustomer)
            //    nonprofitGridDataModel.data.Add(new long[] { (long)item.CreateDate.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, 1 });
            foreach (var item in lstMonth)
            {
                int itemCount = 0;
                DateTime firstOfMonth = DateTime.Parse(item + "/01/" + intCurrentYear);
                var monthData = nonprofitGroupedByMonth.FirstOrDefault(m => m.Month == item);

                if (monthData != null)
                {
                    itemCount = monthData.Count;
                }

                nonprofitGridDataModel.data.Add(new long[] { (long)firstOfMonth.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, itemCount });
            }

            var data = new List<DashboardGridDataModel>
            {
                profitGridDateModel,
                nonprofitGridDataModel
            };

            return Json(data);
        }

        public ActionResult GetAthleteGenderStats()
        {
            var list = _iAthleteManagementRepository.ListAllAthletes()
                .Where(x => x.IsDeleted == false
                    && x.IsPending == false
                    && x.IsAccountEnabled).ToList();

            var male = new
            {
                label = "Male",
                value = list.Count(g => g.Gender == Gender.Male)
            };

            var female = new
            {
                label = "Female",
                value = list.Count(g => g.Gender == Gender.Female)
            };

            var result = new object[] { male, female };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAthleteSportStats()
        {
            var query = _iAthleteManagementRepository.ListAllAthletes()
                .Where(x => x.IsDeleted == false
                            && x.IsPending == false
                            && x.IsAccountEnabled)
                .GroupBy(g => g.SportID)
                .Select(g => new { SportId = g.Key, Count = g.Count() });

            var result = (from item in query
                          let sport = _iSportMgmtRepository.ListSports(true).FirstOrDefault(s => s.Id == item.SportId)
                          select new { label = sport == null ? "" : sport.Name, value = item.Count }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAthleteAgeStats()
        {

            var query = _iAthleteManagementRepository.ListAllAthletes()
                .Where(x => x.IsDeleted == false
                            && x.IsPending == false
                            && x.IsAccountEnabled)
                .GroupBy(g => (DateTime.Today - g.DOB))
                .Select(g => new { Age = g.Key, Count = g.Count() });

            var result = new List<object[]>();
            foreach (var item in query)
            {

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Athletes

        public ActionResult Athletes()
        {
            CustomerMaster customerMaster = (CustomerMaster)(Session["CustomerObj"]);
            ViewBag.RegistrationLink = customerMaster == null ? "" : customerMaster.RegistrationURL;

            List<User> lstActiveAthlete = _iAccount.ListAllActiveUsers();

            User authenticatedUser = (User)(Session["AuthenticatedUser"]);

            if (authenticatedUser.IsIndividualAthlete)
            {
                ViewBag.IsIndividualAthlete = true;
                lstActiveAthlete = lstActiveAthlete.Where(a => a.UserId == authenticatedUser.UserId).ToList();
                ViewBag.ShowReginLink = "display:none;";
            }
            else
            {
                lstActiveAthlete = lstActiveAthlete.Where(a => a.UserType == UserType.Athlete).ToList();
                ViewBag.ShowReginLink = "";
            }

            ViewBag.ActiveAthletes = lstActiveAthlete.Count; //_iAccount.ListAllActiveUsers().Where(a=> a.UserType == UserType.Athlete).Count();
            return View();
        }

        public JsonResult GetAthletes([DataSourceRequest]DataSourceRequest request)
        {
            var viewModels = new List<AthleteViewModel>();
            IEnumerable<User> list = new List<User>();

            var authenticatedUser = (User)(Session["AuthenticatedUser"]);
            var customerCategoryType = ((CustomerCategoryType)(Session["CustomerType"]));
            var locations = _iCustomerLocationMgmtRepository.ListAllLocations();

            bool canSeeAllData = customerCategoryType == CustomerCategoryType.PersonalTrainer;

            if (!canSeeAllData)
            {
                canSeeAllData = authenticatedUser.IsAthleticDirector;
            }

            if (authenticatedUser.IsIndividualAthlete)
            {
                canSeeAllData = true;
            }

            if (!canSeeAllData)
            {
                var dataVisibility = userDataVisibilityRepository.GetUserDataVisibilityListByUserId(authenticatedUser.UserId);
                dataVisibility = customerCategoryTypeFilter.FilterUserDataVisibilty(dataVisibility, customerCategoryType, authenticatedUser.UserId);
                list = athleteFilterRepository.GetFilteredAthleteByDataVisibility(authenticatedUser.UserId, dataVisibility);
            }
            else
            {
                list = _iAthleteManagementRepository.ListAllAthletes();
            }

            //var list = _iAthleteManagementRepository.ListAllAthletes();

            //UserDetails authenticatedUserDetails = (UserDetails)(Session["AuthenticatedUserDetails"]);
            //if (authenticatedUser != null)
            //{
            //    if(authenticatedUserDetails.UserSportTeamLinks != null && authenticatedUserDetails.UserSportTeamLinks.Count > 0)
            //    {
            //        List<int> objectIDs = authenticatedUserDetails.UserSportTeamLinks.Select(u => u.ObjectID).ToList();

            //        if (authenticatedUser.CoachType == Convert.ToInt32(CoachType.Sport))
            //        {
            //            list = list.Where(l => objectIDs.Contains(l.SportID)).ToList();
            //        }
            //        else
            //        {
            //            list = list.Where(l => objectIDs.Contains(l.TeamID)).ToList();
            //        }
            //    }
            //}

            if (authenticatedUser.IsIndividualAthlete)
            {
                list = list.Where(a => a.UserId == authenticatedUser.UserId).ToList();
            }

            foreach (var item in list)
            {
                //var userDetails = _iAccount.GetFullUserInfoByUserID(item.UserId, true, true, true);
                var target = new AthleteViewModel
                {
                    UserID = item.UserId,
                    Age = GetAthleteAge(item.DOB),
                    AthleteName = string.Concat(item.FirstName, " ", item.LastName),
                    Email = item.UserName,
                    UserImagePath = item.UserImage.ImagePath ?? "../images/noimage.jpg",
                    IsActive = item.IsAccountEnabled,
                    ActivationDate = item.IsAccountEnabled ? item.EnablementDate.ToShortDateString() : String.Empty,
                    School = item.SchoolName,
                    Phone = item.ContactInformation.PhoneNumber,
                    Sport = item.Sport.Name,
                    Program = item.Program.Title,
                    LocationId = 0,
                    TeamID = item.TeamID,
                    TeamName = item.TeamID == 0 ? "No Team" : item.Team.Name,
                    CoachID = item.CoachID,
                    CoachName = item.CoachID == 0 ? "No Coach" : item.Coach.FirstName + " " + item.Coach.LastName,
                    IsIndividualAthlete = item.IsIndividualAthlete
                };

                target.PendingStatus = item.IsAccountEnabled
                    ? "Active" : item.IsPending ? "Pending" : "Deactivated";

                target.StatusDisplayIcon = item.IsAccountEnabled ? "<i class='fa fa-circle' aria-hidden='true' style='color:green;'></i>"
                                                                        : "<i class='fa fa-circle' aria-hidden='true'></i>";
                target.DisplayActiveText = item.IsAccountEnabled ? "Deactivate" : "Activate";

                var userRegistration = _iAccount.GetRegistrationForUser(item.UserId);
                if (userRegistration != null)
                {
                    target.LocationId = userRegistration.LocationId;
                    target.Location = locations.FirstOrDefault(l => l.ID == userRegistration.LocationId) == null ? "" : locations.FirstOrDefault(l => l.ID == userRegistration.LocationId).LocationName;
                }

                //if (userDetails != null && userDetails.RegistrationInfo != null)
                //    target.LocationId = userDetails.RegistrationInfo.LocationId;

                viewModels.Add(target);
            }

            return Json(viewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private int GetAthleteAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
            return (a - b) / 10000;
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

            var athleteModel = new AthleteViewModel();
            if (System.Web.HttpContext.Current.Request.Form["athleteViewModel"] != null)
            {
                athleteModel = new JavaScriptSerializer().Deserialize<AthleteViewModel>(System.Web.HttpContext.Current.Request.Form["athleteViewModel"]);
                athleteModel.RegisteredSport = athleteModel.RegisteredSport != null ? athleteModel.RegisteredSport.Trim() : "";
            }

            var athlete = _iAthleteManagementRepository.GetAthleteInfoByID(athleteModel.UserID) ?? new User();
            _mapperService.Map(athleteModel, athlete);

            athlete.Gender = (Gender)athleteModel.Gender;
            athlete.SchoolName = athleteModel.School;
            athlete.PlayingLevel = athleteModel.TravelTeamLevel;
            athlete.YearsOfPlayOrgSport = Convert.ToInt32(athleteModel.Experienced);
            athlete.UserType = (UserType)(athleteModel.UserType);

            if (athlete.ContactInformation != null)
            {
                _mapperService.Map(athleteModel, athlete.ContactInformation);

                athlete.ContactInformation.ID = athleteModel.ContactID;
                athlete.ContactInformation.Zip = athleteModel.ZipCode;
                athlete.ContactInformation.PhoneNumber = athleteModel.Phone;
                athlete.ContactInformation.AlternatePhoneNumber = athleteModel.AlternatePhone;
                athlete.ContactInformation.SecondaryEmail = athleteModel.SecondaryEmail;
            }
            if (athlete.UserImage != null)
            {
                athlete.UserImage.UserId = athleteModel.UserID;
                athlete.UserImage.ImageId = athleteModel.UserImageId;
                athlete.UserImage.ImagePath = athleteModel.UserImagePath;
            }

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                if (athleteModel.UserImagePath != null)
                {
                    string fileFullPathName = Server.MapPath("~/Images/user/") + Session["CustomerName"] + "\\" + athleteModel.UserImagePath;

                    if (!Directory.Exists(Server.MapPath("~/Images/user/") + Session["CustomerName"]))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Images/user/") + Session["CustomerName"]);
                    }

                    var pic = System.Web.HttpContext.Current.Request.Files[0];

                    if (pic.ContentLength > 0)
                    {
                        pic.SaveAs(fileFullPathName);
                    }

                    athlete.UserImage.ImagePath = "../Images/user/" + Session["CustomerName"] + "\\" + athleteModel.UserImagePath;
                }
            }
            var userDetails = new UserDetails();
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

        public JsonResult GetAthleteInfoByID(int athleteID)
        {
            User athlete = _iAthleteManagementRepository.GetAthleteInfoByID(athleteID);

            var target = _mapperService.Map<User, AthleteViewModel>(athlete);

            if (athlete.ContactInformation != null)
            {
                target.ContactID = athlete.ContactInformation.ID;
                target.Email = athlete.ContactInformation.Email;
                target.SecondaryEmail = athlete.ContactInformation.SecondaryEmail;
                target.AddressOne = athlete.ContactInformation.AddressOne;
                target.AddressTwo = athlete.ContactInformation.AddressTwo;
                target.City = athlete.ContactInformation.City;
                target.ZipCode = athlete.ContactInformation.Zip;
                target.Country = athlete.ContactInformation.Country;
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

            target.Positions = _iPositionMgmtRepository.ListPositions()
                .Select(g => new SelectListViewModel()
                {
                    Value = g.ID.ToString(),
                    Text = g.Name,
                    FilterKey = g.SportID.ToString(),
                    Selected = target.PositionID == g.ID
                })
                .ToList();

            SelectListViewModel posSel = new SelectListViewModel
            {
                Value = "0",
                Text = "--Select Position--",
                Selected = false
            };
            target.Positions.Add(posSel);

            target.Sports = _iSportMgmtRepository.ListSports()
                .Select(g => new SelectListViewModel()
                {
                    Value = g.Id.ToString(),
                    Text = g.Name,
                    Selected = (target.SportID == g.Id)
                })
                .ToList();

            var posSport = new SelectListViewModel
            {
                Value = "0",
                Text = "--Select Sport--",
                Selected = false
            };
            target.Sports.Add(posSport);

            target.States = new List<SelectListViewModel>();
            SelectListViewModel posState = new SelectListViewModel
            {
                Value = "0",
                Text = "--Select State--",
                Selected = false
            };
            target.States.Add(posState);

            foreach (var state in _iStateRepository.ListStates())
            {
                posState = new SelectListViewModel();
                posState.Value = state.ID.ToString();
                posState.Text = state.Name;
                posState.Selected = target.SportID == state.ID;
                target.States.Add(posState);
            }

            var userRegistration = _iAccount.GetRegistrationForUser(athlete.UserId);
            if (userRegistration != null)
            {
                target.LocationId = userRegistration.LocationId;
                target.Location = _iCustomerLocationMgmtRepository.GetCustomerLocation(target.LocationId).LocationName;
            }

            return Json(athlete == null ? new AthleteViewModel() : target, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteAthleteById(int id)
        {
            try
            {
                if (_iAthleteManagementRepository.DeleteAthleteById(id))
                    return Json(new
                    {
                        success = true,
                        message = "Athlete deleted successfully."
                    }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new
                    {
                        success = false,
                        message = "Unable to delete Athlete"
                    }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult DeleteMultipleAthlete(string IDs)
        {
            ReturnObjectModel ro = new ReturnObjectModel();
            ro.Message = "Delete failed";
            ro.Status = ReturnStatus.Err;
            bool deleteStatus = false;
            try
            {
                _iAthleteManagementRepository = ObjectFactory.GetInstance<IAthleteManagementRepository>();
                List<string> lstID = IDs.Split(',').ToList();
                foreach (string strID in lstID)
                {
                    if (strID != "")
                    {
                        int id = Convert.ToInt32(strID);
                        if (_iAthleteManagementRepository.DeleteAthleteById(id))
                        {
                            deleteStatus = true;
                        }
                    }
                }

                if (deleteStatus)
                {
                    ro.Status = ReturnStatus.OK;
                    ro.Message = "Athlete(s) deleted successful";
                }

            }
            catch (Exception ex)
            {
                ro.Message = "Error: " + ex.Message + ". StackTrace: " + ex.StackTrace;
                ro.Status = ReturnStatus.Err;
            }
            return Json(ro, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateAthleteById(int id)
        {
            try
            {
                if (_iAthleteManagementRepository.UpdateAthleteById(id))
                    return Json(new
                    {
                        success = true,
                        message = "Athlete Status Updated successfully."
                    }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new
                    {
                        success = false,
                        message = "Unable to Update Athlete Status."
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SetAthleteListByIds(bool status, string ids)
        {
            try
            {
                var result = true;
                List<string> idList = ids.Split(',').ToList();
                foreach (var id in idList)
                {
                    if (String.IsNullOrWhiteSpace(id))
                        continue;
                    var idInt = Convert.ToInt32(id);
                    if (idInt == 0)
                        continue;
                    var temp = _iAthleteManagementRepository.SetAthleteStatusById(Convert.ToInt32(id), status);
                    result = result && temp;
                }
                if (result)
                    return Json(new
                    {
                        success = true,
                        message = "Athletes Status Updated successfully."
                    }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new
                    {
                        success = false,
                        message = "Unable to Update Athletes Status."
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SendRegistrationMail(string emailList)
        {
            var user = (UserDetails)Session["AuthenticatedUserDetails"];
            var customerObj = (CustomerMaster)(Session["CustomerObj"]);
            RegistrationLinkMailModel rlm = new RegistrationLinkMailModel();
            rlm.CompleteName = user.Users.FirstOrDefault() == null ? "" : user.Users.FirstOrDefault().FirstName + " " + user.Users.FirstOrDefault().LastName;
            rlm.UserID = user.Users.FirstOrDefault() == null ? 0 : user.Users.FirstOrDefault().UserId;
            rlm.RegistrationLink = customerObj.RegistrationURL;

            try
            {
                _mailService.SendMail(rlm
                    , "RegistrationLink"
                    , emailList
                    , user.Contacts == null ? "" : user.Contacts.FirstOrDefault() == null ? "" : user.Contacts.FirstOrDefault().Email
                    , "Registration Link"
                    , Server.MapPath("~/MailTemplates"));

                var ro = new ReturnObjectModel()
                {
                    Status = ReturnStatus.OK,
                    Message = "Message sent successfully."
                };
                return Json(ro, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                var ro = new ReturnObjectModel()
                {
                    Status = ReturnStatus.Err,
                    Message = "Error when send email(s)."
                };
                return Json(ro, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Locations()
        {
            var list = _iCustomerManagementRepository.ListAllCustomers();

            var lstCustomer = new List<Customer>();

            foreach (var source in list)
            {
                lstCustomer.Add(source.Customer);
            }

            if (Session["CustomerObj"] != null)
            {
                CustomerMaster customer = (CustomerMaster)(Session["CustomerObj"]);

                if (customer.CustomerName != null && customer.CustomerName != "")
                {
                    if (customer.IsSuperAdmin == false)
                    {
                        lstCustomer = lstCustomer.Where(c => c.CustomerId == customer.CustomerIDinTKG).ToList();
                    }
                }
            }

            ViewBag.CustomerList = lstCustomer;

            return View();
        }

        public JsonResult ChartData(string chartType)
        {
            string result = "";
            if (chartType == "bar")
            {
                BarChartDataModel bdm = new BarChartDataModel();
                bdm.color = "#28d8b2";
                bdm.label = "Sales";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "Jan", "27" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Feb", "82" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Mar", "56" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Apr", "14" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "May", "28" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jun", "77" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jul", "23" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Aug", "49" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Sep", "81" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct", "20" } };
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
                bdm.label = "Registered";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "Jan", "27" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Feb", "20" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Mar", "10" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Apr", "25" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "May", "18" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jun", "30" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jul", "25" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Sep", "20" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct", "12" } };
                bdm.data.Add(array2Db);

                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#ffea88";
                bdm.label = "Signedup";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "Jan", "15" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Feb", "12" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Mar", "5" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Apr", "10" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "May", "8" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jun", "17" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jul", "16" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Sep", "14" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct", "4" } };
                bdm.data.Add(array2Db);

                lstBDM.Add(bdm);

                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }
            else if (chartType == "profitvsnonstacked")
            {
                List<BarChartDataModel> lstBDM = new List<BarChartDataModel>();

                BarChartDataModel bdm = new BarChartDataModel();
                bdm.color = "#5ab1ef";
                bdm.label = "Profit";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "Jan", "27" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Feb", "20" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Mar", "10" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Apr", "25" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "May", "18" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jun", "30" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jul", "25" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Sep", "20" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct", "12" } };
                bdm.data.Add(array2Db);

                lstBDM.Add(bdm);

                bdm = new BarChartDataModel();
                bdm.color = "#d87a80";
                bdm.label = "Non-profit";

                bdm.data = new List<string[,]>();
                array2Db = new string[1, 2] { { "Jan", "15" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Feb", "12" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Mar", "5" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Apr", "10" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "May", "8" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jun", "17" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jul", "16" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Sep", "14" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Oct", "4" } };
                bdm.data.Add(array2Db);

                lstBDM.Add(bdm);

                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }
            if (chartType == "locationbar")
            {
                BarChartDataModel bdm = new BarChartDataModel();
                bdm.color = "#ffea88";
                bdm.label = "Location";

                bdm.data = new List<string[,]>();
                string[,] array2Db = new string[1, 2] { { "Washington", "27" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Jersey", "82" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Illinois", "56" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "San Francisco", "14" } };
                bdm.data.Add(array2Db);
                array2Db = new string[1, 2] { { "Pasadena", "28" } };
                bdm.data.Add(array2Db);

                List<BarChartDataModel> lstBDM = new List<BarChartDataModel>();
                lstBDM.Add(bdm);
                return Json(lstBDM, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MovementTypes()
        {
            return View();
        }

        public ActionResult UOM()
        {
            return View();
        }

        public ActionResult AthleteProfile()
        {
            return View();
        }

        public ActionResult AthleteDashboard()
        {
            return View();
        }

        public JsonResult GetPrograms([DataSourceRequest]DataSourceRequest request)
        {
            var viewModels = new List<ProgramListViewModel>();

            var cm = _iCustomerMgmt.GetCustomerInfo(Convert.ToString((Session["CustomerName"])));
            var user = (User)Session["AuthenticatedUser"];
            List<ENT.Program> list = null;

            list = _iProgramMgmtRepository.ListPrograms();
            list = list.Where(p => p.IsActive == true).ToList();

            if (user.IsIndividualAthlete)
            {
                list = list.Where(p => p.OwnerUserId == user.UserId && p.CustomerCategoryType == CustomerCategoryType.Individual).ToList();
            }

            foreach (var item in list)
            {
                var target = new ProgramListViewModel();
                target.ProgramID = item.Id;
                target.Name = item.Title;
                target.Objective = item.Objective;
                target.Description = item.Description;
                target.NumberofSteps = item.Steps;
                target.NumebrofSessions = item.TotalSessions;
                target.NumberofGroups = item.TotalGroups;
                target.GroupSequence = 1;
                target.Activate = item.IsActive == true ? "Deactivate" : "Activate";
                target.AllowDelete = true;
                target.SelfCopy = true;
                if (Session["CustomerObj"] != null)
                {
                    CustomerMaster customer = (CustomerMaster)(Session["CustomerObj"]);

                    if (customer.IsSuperAdmin == false)
                    {
                        target.IsTKGProgram = false;
                    }
                }
                viewModels.Add(target);
            }

            return Json(viewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet); ;
        }

        public JsonResult GetTKGPrograms([DataSourceRequest]DataSourceRequest request)
        {
            var viewModels = new List<ProgramListViewModel>();
            var user = (User)Session["AuthenticatedUser"];
            if (Session["CustomerObj"] != null)
            {
                CustomerMaster customer = (CustomerMaster)(Session["CustomerObj"]);

                if (customer.CustomerName != null && customer.CustomerName != "")
                {
                    if (customer.IsSuperAdmin == false || user.IsIndividualAthlete)
                    {
                        var progList = _iProgramMgmtRepository.ListTKGPrograms();

                        progList = progList.Where(p => p.IsActive == true && p.CustomerCategoryType == CustomerCategoryType.TKG).ToList();

                        foreach (var item in progList)
                        {
                            var target = new ProgramListViewModel();
                            target.ProgramID = item.Id;
                            target.Name = item.Title;
                            target.Objective = item.Objective;
                            target.Description = item.Description;
                            target.NumberofSteps = item.Steps;
                            target.NumebrofSessions = item.TotalSessions;
                            target.NumberofGroups = item.TotalGroups;
                            target.GroupSequence = 1;
                            target.Activate = "";
                            target.IsTKGProgram = true;
                            target.AllowDelete = false;
                            target.SelfCopy = false;
                            viewModels.Add(target);
                        }
                    }
                }
            }

            return Json(viewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet); ;
        }

        public JsonResult GetEditPrograms([DataSourceRequest]DataSourceRequest request)
        {
            var viewModels = new List<ProgramListViewModel>();

            List<ENT.Program> list = null;

            var user = (User)Session["AuthenticatedUser"];

            if (user.IsIndividualAthlete)
            {
                list = _iProgramMgmtRepository.ListPrograms()
                    .Where(p => p.IsActive == false && p.IsDeleted == false && p.OwnerUserId == user.UserId && p.CustomerCategoryType == CustomerCategoryType.Individual)
                    .ToList();
            }
            else
            {
                list = _iProgramMgmtRepository.ListPrograms()
                    .Where(p => p.IsActive == false && p.IsDeleted == false && p.CustomerCategoryType != CustomerCategoryType.Individual)
                    .ToList();
            }
            foreach (var item in list)
            {
                var target = new ProgramListViewModel();
                target.ProgramID = item.Id;
                target.Name = item.Title;
                target.Objective = item.Objective;
                target.Description = item.Description;
                target.NumberofSteps = item.Steps;
                target.NumebrofSessions = item.TotalSessions;
                target.NumberofGroups = item.TotalGroups;
                target.GroupSequence = 1;
                target.Activate = item.IsActive == true ? "Deactivate" : "Activate";
                target.AllowDelete = true;
                target.SelfCopy = true;
                if (Session["CustomerObj"] != null)
                {
                    CustomerMaster customer = (CustomerMaster)(Session["CustomerObj"]);

                    if (customer.IsSuperAdmin == false)
                    {
                        target.IsTKGProgram = false;
                    }
                }
                viewModels.Add(target);
            }

            return Json(viewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CopyProgam(int copyFromProgramID, string newProgramName, string newProgramDesc)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            ro.Message = "Program copy failed";
            ro.Status = ReturnStatus.Err;

            var cm = _iCustomerMgmt.GetCustomerInfo(Convert.ToString((Session["CustomerName"])));
            var customerCategoryType = (CustomerCategoryType)cm.Category;

            var user = (User)Session["AuthenticatedUser"];
            var ownerUserId = user.UserId;

            if (user.IsIndividualAthlete)
            {
                customerCategoryType = CustomerCategoryType.Individual;
            }

            _iProgramMgmtRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();
            bool result = _iProgramMgmtRepository.CopyProgram(copyFromProgramID, newProgramName, newProgramDesc, customerCategoryType, ownerUserId);

            if (result)
            {
                ro.Status = ReturnStatus.OK;
                ro.Message = "Program Copied successfully";
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActivateProgam(int programID, bool activate)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            ro.Message = activate == true ? "Program activation failed" : "Program deactivation failed";

            ro.Status = ReturnStatus.Err;

            _iProgramMgmtRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();

            bool result = _iProgramMgmtRepository.ActivateProgram(programID, activate);

            if (result)
            {
                ro.Status = ReturnStatus.OK;
                ro.Message = activate == true ? "Program activation successful" : "Program deactivation successful";
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProgram(int programID)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            ro.Message = "Program deletion failed";

            ro.Status = ReturnStatus.Err;

            _iProgramMgmtRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();

            bool result = _iProgramMgmtRepository.DeleteProgram(programID);

            if (result)
            {
                ro.Status = ReturnStatus.OK;
                ro.Message = "Program deletion successful";
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProgramByID(int programID)
        {
            ENT.ProgramDetail progDetail = new ENT.ProgramDetail();

            _iProgramMgmtRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();

            //prog = _iProgramMgmtRepository.GetProgrambyID(programID);
            progDetail = _iProgramMgmtRepository.GetProgramInfoWithSchedule(programID);

            //ProgramInfoModel programInfoModel = new ProgramInfoModel();

            //programInfoModel.ProgramID = progDetail.Programs[0].Id;
            //programInfoModel.ProgramName = progDetail.Programs[0].Title;
            //programInfoModel.ProgramDescription = progDetail.Programs[0].Description;
            //programInfoModel. = progDetail.Programs[0].Description;

            return Json(progDetail == null ? new ENT.ProgramDetail() : progDetail, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProgramByIDForAddExercise(int programID, bool isTKGProgram)
        {
            var prog = isTKGProgram
                ? _iProgramMgmtRepository.GetTKGProgrambyID(programID)
                : _iProgramMgmtRepository.GetProgrambyID(programID);

            var lstSport = _iSportMgmtRepository.ListSports(true);

            //List<ENT.Position> lstPosition = _iPositionMgmtRepository.ListPositionBySportID(prog.SportID == 0 ? lstSport[0].Id : prog.SportID);
            //var lstPosition = _iPositionMgmtRepository.ListPositionBySportID(1);

            var sport = lstSport.FirstOrDefault(s => s.Id == prog.SportID);

            string position = "All";//lstPosition.Where(p => p.ID == prog.PositionID && p.SportID == prog.SportID).FirstOrDefault().Name;

            var lstReturnObj = new
            {
                sport.Name,
                position,
                TrainingSeasonID = prog.TrainingSeasonID == 1 ? "Peak Season" : "Off Season",
                TrainingPhase = prog.TrainingPhase == null ? "" : prog.TrainingPhase,
                prog.Title
            };

            return Json(lstReturnObj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveProgramInfo(ENT.Program program, List<ENT.SpecialScheduleSessions> ProgramSchedule)
        {
            ReturnObjectModel ro = new ReturnObjectModel();
            ro.Status = ReturnStatus.Err;
            ro.Message = "Save Program failed";


            var progDet = new ENT.ProgramDetail();
            program.PositionID = program.PositionID == 0 ? 9 : program.PositionID;
            if (program.Description == null)
                program.Description = "";

            var cm = _iCustomerMgmt.GetCustomerInfo(Convert.ToString((Session["CustomerName"])));
            var customerCategoryType = (CustomerCategoryType)cm.Category;

            var user = (User)Session["AuthenticatedUser"];
            program.OwnerUserId = user.UserId;

            if (user.IsIndividualAthlete)
            {
                customerCategoryType = CustomerCategoryType.Individual;
            }

            program.CustomerCategoryType = customerCategoryType;

            progDet.Programs = new List<ENT.Program>();
            progDet.Programs.Add(program);
            progDet.SpecialSchedules = ProgramSchedule;

            int result = _iProgramMgmtRepository.SaveProgramInfo(progDet);
            if (result > 0)
            {
                ro.Message = "Program Info saved successfully";
                ro.Status = program.Id > 0 ? ReturnStatus.OK : ReturnStatus.Redirect;
                ro.RedirectLocation = "Admin/ManageProgram?programID=" + result;
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Assessment()
        {
            return View();
        }

        #region UserManagement
        #region User List
        public JsonResult GetTKGAdminUserList([DataSourceRequest]DataSourceRequest request)
        {
            var list = _iAccount.ListAllTKGAdminUsers();
            var lstResult = new List<UserViewModel>();
            foreach (var source in list.Users)
            {
                UserViewModel target = new UserViewModel()
                {
                    UserId = source.UserId,
                    FirstName = source.FirstName,
                    LastName = source.LastName,
                    UserName = source.UserName,
                    FullName = source.FirstName + " " + source.LastName,
                    // Status=
                    IsActive = source.IsAccountEnabled,

                    IsDeleted = source.IsDeleted
                };
                var contact = list.Contacts.FirstOrDefault(ui => ui.UserID == source.UserId);
                if (contact != null)
                {
                    target.Email = contact.Email;
                }
                var image = list.UserImages.FirstOrDefault(ui => ui.UserId == source.UserId);
                if (image != null)
                {
                    target.ProfilePicture = image.ImagePath;
                }
                var userDataVisibilityCount = userDataVisibilityRepository.GetUserDataVisibilityCountByUserId(source.UserId);
                target.HasUserDataVisibility = userDataVisibilityCount > 0 || target.IsAthleticDirector;

                target.RoleName = _iAccount.GetRoleNameByUserID(target.UserId);

                lstResult.Add(target);
            }

            return Json(lstResult
                    .OrderBy(u => u.FirstName)
                .ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete User

        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            try
            {
                return Json(_iAccount.DeleteUser(id) ? new ReturnObjectModel() { Status = ReturnStatus.OK, Message = "User deleted successfully." }
                    : new ReturnObjectModel() { Status = ReturnStatus.Err, Message = "Unable to delete user" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ReturnObjectModel() { Status = ReturnStatus.Err, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

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
                    string sportIDS = "", teamIDS = "";
                    if (source.CoachType == Convert.ToInt32(CoachType.Sport) || source.CoachType == Convert.ToInt32(CoachType.Team))
                    {
                        List<UserSportTeamLink> lstLnk = _iAccount.GetUserSportTeamLink(source.UserId);

                        if (lstLnk != null && lstLnk.Count > 0)
                        {
                            if (source.CoachType == Convert.ToInt32(CoachType.Sport) || source.CoachType == Convert.ToInt32(CoachType.Team))
                            {
                                List<UserSportTeamLink> lstSportLnk = lstLnk.Where(l => l.RecordType == 1).ToList();
                                if (lstSportLnk != null && lstSportLnk.Count > 0)
                                {
                                    sportIDS = string.Join(",", lstSportLnk.Select(l => l.ObjectID));
                                }

                                List<UserSportTeamLink> lstTeamLnk = lstLnk.Where(l => l.RecordType == 2).ToList();
                                if (lstTeamLnk != null && lstTeamLnk.Count > 0)
                                {
                                    teamIDS = string.Join(",", lstTeamLnk.Select(l => l.ObjectID));
                                }
                            }
                        }
                    }
                    target = new UserViewModel()
                    {
                        UserId = source.UserId,
                        FirstName = source.FirstName,
                        LastName = source.LastName,
                        IsActive = source.IsAccountEnabled,
                        IsDeleted = source.IsDeleted,
                        UserName = source.UserName,
                        DOB = source.DOB.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        Password = _iAccount.GenerateDecryptedString(source.Password),
                        GenderId = (int)source.Gender,
                        Gender = source.Gender,
                        IsAthleticDirector = source.IsAthleticDirector,
                        IsStrengthCoach = source.IsStrengthCoach,
                        UserType = (UserType)(source.UserType),
                        CoachType = source.CoachType,
                        SportIDs = sportIDS,
                        TeamIDs = teamIDS
                    };
                    DateTime parsedDate;
                    if (DateTime.TryParseExact(target.DOB.ToString(), "MM-dd-yyyy", null, DateTimeStyles.None, out parsedDate))
                    {
                        target.DOB = parsedDate.ToString();
                    }

                    contact = userDetails.Contacts.FirstOrDefault(ui => ui.UserID == source.UserId);
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
                        target.ContactInformation.StateID = contact.StateID;
                        target.ContactInformation.Country = contact.Country;
                        target.Phone = contact.PhoneNumber;
                        target.AlternatePhone = contact.AlternatePhoneNumber;
                    }
                    image = userDetails.UserImages.FirstOrDefault(ui => ui.UserId == source.UserId);
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
            return Json(target ?? new UserViewModel(), JsonRequestBehavior.AllowGet);
        }
        #endregion 

        #region UpdateUserID
        public JsonResult UpdateUserInfo(UserViewModel UserModel)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            ro.Message = "User Update failed";
            ro.Status = ReturnStatus.Err;

            var user = new User()
            {
                UserId = UserModel.UserId,
                FirstName = UserModel.FirstName,
                LastName = UserModel.LastName,
                IsDeleted = false,
                UserName = UserModel.UserName,
                DOB = DateTime.ParseExact(UserModel.DOB, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                IsAccountEnabled = true
            };
            var contact = new Contact()
            {
                UserID = user.UserId,
                ID = UserModel.ContactInformation.ID,
                Email = UserModel.Email,
                AddressOne = UserModel.ContactInformation.AddressOne,
                AddressTwo = UserModel.ContactInformation.AddressTwo,
                City = UserModel.ContactInformation.City,
                Zip = UserModel.ContactInformation.Zip
            };
            var image = new UserImage()
            {
                UserId = user.UserId,
                ImageId = UserModel.UserImage.ImageId,
                ImagePath = UserModel.UserImage.ImagePath
            };
            UserDetails userDetails = new UserDetails();
            userDetails.Users.Add(user);
            userDetails.Contacts.Add(contact);
            userDetails.UserImages.Add(image);

            bool result = _iAccount.SaveUserInfo(userDetails);
            if (result)
            {
                ro.Status = ReturnStatus.OK;
                ro.Message = "User saved successfully";
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UpdateUserStatusByID
        public JsonResult UpdateUserStatusById(int userId)
        {
            return Json(_iAccount.UpdateUserStatusById(userId) ? new ReturnObjectModel() { Status = ReturnStatus.OK, Message = "User status updated successfully." }
                : new ReturnObjectModel() { Status = ReturnStatus.Err, Message = "Unable to update user status" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        //[Authorize]
        public JsonResult SaveUser()
        {
            var ro = new ReturnObjectModel
            {
                Message = "User Update failed",
                Status = ReturnStatus.Err
            };

            try
            {
                UserViewModel UserModel = new UserViewModel();
                if (System.Web.HttpContext.Current.Request.Form["userViewModel"] != null)
                {
                    UserModel = new JavaScriptSerializer().Deserialize<UserViewModel>(System.Web.HttpContext.Current.Request.Form["userViewModel"]);
                }

                UserDetails userDetails = _iAccount.GetUserInfoByID(UserModel.UserId);

                if (userDetails == null)
                    userDetails = new UserDetails();

                if (userDetails.Users != null && userDetails.Users.Count == 0)
                    userDetails.Users.Add(new User());

                userDetails.Users.FirstOrDefault().UserId = UserModel.UserId;
                userDetails.Users.FirstOrDefault().FirstName = UserModel.FirstName;
                userDetails.Users.FirstOrDefault().LastName = UserModel.LastName;
                userDetails.Users.FirstOrDefault().IsDeleted = false;
                userDetails.Users.FirstOrDefault().UserName = UserModel.UserName;
                if (!String.IsNullOrWhiteSpace(UserModel.DOB))
                    userDetails.Users.FirstOrDefault().DOB = DateTime.ParseExact(UserModel.DOB, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                userDetails.Users.FirstOrDefault().IsAccountEnabled = true;
                if (String.IsNullOrWhiteSpace(UserModel.Password) == false)
                    userDetails.Users.FirstOrDefault().Password = _iAccount.GenerateEncryptedString(UserModel.Password);
                userDetails.Users.FirstOrDefault().HeardFrom = 1;
                userDetails.Users.FirstOrDefault().PayLater = PayLater.PayLater;
                if (UserModel.Gender != null)
                    userDetails.Users.FirstOrDefault().Gender = (Gender)UserModel.Gender;
                userDetails.Users.FirstOrDefault().UserType = (UserType)(UserModel.UserType);
                userDetails.Users.FirstOrDefault().IsAthleticDirector = UserModel.IsAthleticDirector;
                userDetails.Users.FirstOrDefault().IsStrengthCoach = UserModel.IsStrengthCoach;
                userDetails.Users.FirstOrDefault().CoachType = UserModel.CoachType.HasValue ? UserModel.CoachType.Value : 0;
                //if (UserModel.UserName.ToLower() == "coach")
                //{
                //    userDetails.Users.FirstOrDefault().UserType = Core.DataTypes.UserType.Coach;
                //}
                //else
                //{
                //    userDetails.Users.FirstOrDefault().UserType = Core.DataTypes.UserType.TKGAdmin;
                //}

                if (userDetails.Contacts != null && userDetails.Contacts.Count == 0)
                    userDetails.Contacts.Add(new Contact());

                userDetails.Contacts.FirstOrDefault().UserID = UserModel.UserId;
                userDetails.Contacts.FirstOrDefault().ID = UserModel.ContactInformation.ID;
                userDetails.Contacts.FirstOrDefault().Email = UserModel.ContactInformation.Email;
                userDetails.Contacts.FirstOrDefault().AddressOne = UserModel.ContactInformation.AddressOne;
                userDetails.Contacts.FirstOrDefault().AddressTwo = UserModel.ContactInformation.AddressTwo;
                userDetails.Contacts.FirstOrDefault().City = UserModel.ContactInformation.City;
                userDetails.Contacts.FirstOrDefault().Zip = UserModel.ContactInformation.Zip;
                userDetails.Contacts.FirstOrDefault().StateID = UserModel.ContactInformation.StateID;
                userDetails.Contacts.FirstOrDefault().Country = UserModel.ContactInformation.Country;

                if (!string.IsNullOrEmpty(UserModel.Phone))
                {
                    userDetails.Contacts.FirstOrDefault().PhoneNumber = UserModel.Phone;
                }

                if (UserModel.AlternatePhone != null && UserModel.AlternatePhone != "")
                {
                    userDetails.Contacts.FirstOrDefault().AlternatePhoneNumber = UserModel.AlternatePhone;
                }

                if (userDetails.UserImages != null && userDetails.UserImages.Count == 0)
                    userDetails.UserImages.Add(new UserImage());

                userDetails.UserImages.FirstOrDefault().UserId = UserModel.UserId;
                if (UserModel.UserImage.ImageId > 0)
                {
                    userDetails.UserImages.FirstOrDefault().ImageId = UserModel.UserImage.ImageId;
                }
                if (UserModel.UserImage.ImagePath != "")
                {
                    userDetails.UserImages.FirstOrDefault().ImagePath = UserModel.UserImage.ImagePath;
                }

                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    if (UserModel.UserImage.ImagePath != null)
                    {
                        string fileFullPathName = Server.MapPath("~/Images/user/") + Session["CustomerName"] + "\\" + UserModel.UserImage.ImagePath;

                        if (!Directory.Exists(Server.MapPath("~/Images/user/") + Session["CustomerName"]))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Images/user/") + Session["CustomerName"]);
                        }

                        var pic = System.Web.HttpContext.Current.Request.Files[0];
                        if (pic.ContentLength > 0)
                        {
                            pic.SaveAs(fileFullPathName);
                        }
                        userDetails.UserImages.FirstOrDefault().ImagePath = "../Images/user/" + Session["CustomerName"] + "\\" + UserModel.UserImage.ImagePath;
                    }
                }

                if (userDetails.Users.FirstOrDefault().UserId > 0)
                {
                    bool result = _iAccount.SaveUserInfo(userDetails);
                    if (result)
                    {
                        //TODO: this doesn't have any sence
                        if (UserModel.CoachType == Convert.ToInt32(CoachType.Sport) || UserModel.CoachType == Convert.ToInt32(CoachType.Team))
                        {
                            string sportID = UserModel.SportIDs == null ? "" : UserModel.SportIDs;
                            string teamID = UserModel.TeamIDs == null ? "" : UserModel.TeamIDs;
                        }

                        if (userDetails.Users[0].UserId == ((StrengthTracker2.Core.Repository.Entities.Actors.User)(Session["AuthenticatedUser"])).UserId)
                        {
                            Session["AuthenticatedUserDetails"] = userDetails;
                        }

                        ro.Status = ReturnStatus.OK;
                        ro.Message = "User saved successfully";
                        ro.RedirectLocation = Convert.ToString(userDetails.Users.FirstOrDefault().UserId);
                    }
                }
                else
                {
                    var result = _iAccount.AddUser(userDetails);
                    if (result != null)
                    {
                        if (UserModel.CoachType == Convert.ToInt32(CoachType.Sport) || UserModel.CoachType == Convert.ToInt32(CoachType.Team))
                        {
                            string sportID = UserModel.SportIDs == null ? "" : UserModel.SportIDs;
                            string teamID = UserModel.TeamIDs == null ? "" : UserModel.TeamIDs;
                            _iAccount.SaveUserSportTeamLink(result.UserId, UserModel.CoachType.Value, sportID, teamID);
                        }
                        userDataVisibilityRepository.AddUserDataVisibility(new UserDataVisibility() { ResponsibilityType = UserResponsibilityType.Other, UserId = result.UserId });

                        ro.Status = ReturnStatus.OK;
                        ro.Message = "User added successfully";
                        ro.RedirectLocation = Convert.ToString(result.UserId);
                    }
                }
            }
            catch (Exception ex)
            {
                ro.Message = "Error:" + ex.Message + " stack:" + ex.StackTrace;
                ro.Status = ReturnStatus.Err;
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region location-role mapping



        #region Location List
        [HttpPost]
        public JsonResult GetLocationsList()
        {
            var lstCustomerLocationDetails = _iCustomerLocationMgmtRepository.ListAllLocations()
                .OrderBy(x => x.LocationName)
                .Select(l => new { l.ID, l.LocationName });

            return Json(lstCustomerLocationDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteUserLocation(int mappingId)
        {
            try
            {
                _icustomerLocationRoleMgmtRepository = ObjectFactory.GetInstance<ICustomerLocationRoleMgmtRepository>();
                if (_icustomerLocationRoleMgmtRepository.DeleteCustomerLocationRole(mappingId))
                    return Json(new ReturnObjectModel() { Status = ReturnStatus.OK, Message = "Location deleted successfully." }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new ReturnObjectModel() { Status = ReturnStatus.Err, Message = "Unable to delete location" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ReturnObjectModel() { Status = ReturnStatus.Err, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetUserLocationRoleMapping(int mappingId)
        {
            var roleList = _iRoleMgmtRepository.ListAllRoles();

            List<CustomerLocation> lstCustomerLocationDetails = _iCustomerLocationMgmtRepository.ListAllLocations();

            // customerlocation role
            _icustomerLocationRoleMgmtRepository = ObjectFactory.GetInstance<ICustomerLocationRoleMgmtRepository>();

            List<CustomerLocationRoleViewModel> lstcustomerLocation = new List<CustomerLocationRoleViewModel>();
            CustomerLocationRoleViewModel cust = new CustomerLocationRoleViewModel();
            CustomerLocationRole customerLocationRole = _icustomerLocationRoleMgmtRepository.GetCustomerLocationRole(mappingId);
            var locationlst = lstCustomerLocationDetails.Single(p => p.ID == customerLocationRole.LocationId);


            string[] roles = customerLocationRole.RoleId.Split(';');
            string Roles = string.Empty;
            string RoleId = string.Empty;
            int roleId = 0;
            for (int i = 0; i < roles.Length; i++)
            {
                roleId = Convert.ToInt16(roles[i]);
                var match = roleList.Single(p => p.RoleID == roleId);
                if (Roles == "")
                {
                    Roles = match.RoleName;
                    RoleId = Convert.ToString(match.RoleID);
                }

                else
                {
                    Roles = Roles + ',' + match.RoleName;
                    RoleId = RoleId + ';' + Convert.ToString(match.RoleID);
                }
            }
            // end of role
            CustomerLocationRoleViewModel customerLocation = new CustomerLocationRoleViewModel();
            customerLocation.MappingId = mappingId;
            customerLocation.Location = locationlst.LocationName;
            customerLocation.Roles = Roles;
            customerLocation.RoleId = RoleId;
            lstcustomerLocation.Add(customerLocation);
            return Json(lstcustomerLocation, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetLocationRoleUserList(string id)
        {
            var roleList = _iRoleMgmtRepository.ListAllRoles();
            var customerLocationList = _iCustomerLocationMgmtRepository.ListAllLocations();
            var customerLocationRoleList = _icustomerLocationRoleMgmtRepository.customerLocationRoles(Convert.ToInt16(id));
            var customerLocationRoleViewModelList = new List<CustomerLocationRoleViewModel>();
            foreach (var custLocation in customerLocationRoleList)
            {
                var userRoleList = new List<String>();
                var userRoleIdList = new List<String>();
                foreach (var roleId in custLocation.RoleId.Split(';'))
                {
                    var role = roleList.Single(p => p.RoleID == Convert.ToInt16(roleId));
                    userRoleList.Add(role.RoleName);
                    userRoleIdList.Add(role.RoleID.ToString());
                }
                var location = customerLocationList.Single(p => p.ID == custLocation.LocationId);
                customerLocationRoleViewModelList.Add(new CustomerLocationRoleViewModel()
                {
                    MappingId = custLocation.MappingId,
                    Roles = string.Join(",", userRoleList),
                    RoleId = string.Join(";", userRoleIdList),
                    Location = location.LocationName,
                    LocationId = location.ID
                });
            }
            return Json(customerLocationRoleViewModelList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveLocationRole(string locationID, string UserId, string role)
        {
            int currentUser = 0;
            _icustomerLocationRoleMgmtRepository = ObjectFactory.GetInstance<ICustomerLocationRoleMgmtRepository>();
            if (Session["AuthenticatedUser"] != null)
            {
                User us = (User)Session["AuthenticatedUser"];
                currentUser = us.UserId;
            }
            List<CustomerLocationRole> lstCustomerLocationRole = new List<CustomerLocationRole>();
            // Location already exists for the user by 2/16/2017
            List<CustomerLocationRole> lstCustomerLocationRole1 = _icustomerLocationRoleMgmtRepository.customerLocationRoles(Convert.ToInt16(UserId));
            var RoleDetails = from locationrole in lstCustomerLocationRole1
                              where locationrole.LocationId == Convert.ToInt16(locationID)
                              select new CustomerLocationRole
                              {
                                  RoleId = locationrole.RoleId,
                                  LocationId = locationrole.LocationId,
                                  MappingId = locationrole.MappingId,
                                  UpdatedBy = locationrole.UpdatedBy,
                                  IsDeleted = locationrole.IsDeleted,
                                  CreatedBy = locationrole.CreatedBy,
                                  CreatedDate = locationrole.CreatedDate,
                                  UserId = locationrole.UserId
                              };


            var roles = RoleDetails.ToList();

            CustomerLocationRole customerLocationRole = new CustomerLocationRole();
            int MappingId = 0;
            var roleDetails = RoleDetails.ToList().SingleOrDefault();

            // new roles
            var newRoles = role.Split(';');
            //1;2

            if ((newRoles.Length >= 1) && (roleDetails != null))
            {
                //var finalRoles = roleDetails.RoleId;
                //var det = roleDetails.RoleId;
                //var splitted = det.Split(';');
                //for (int j = 0; j < newRoles.Length; j++)
                //{
                //    var newId = newRoles[j].ToString();
                //    var found = 0;
                //    for (int i = 0; i < splitted.Length; i++)
                //    {
                //        if (newId == splitted[i].ToString())
                //        {
                //            finalRoles = finalRoles + ';' + newId;
                //        }
                //    }
                //    if (found == 0)
                //    {
                //        finalRoles = finalRoles + ';' + newId;
                //    }
                //}
                //customerLocationRole.RoleId = finalRoles;
                customerLocationRole.RoleId = role;
                customerLocationRole.MappingId = roleDetails.MappingId;
                customerLocationRole.UpdatedBy = roleDetails.UpdatedBy;

                customerLocationRole.LocationId = roleDetails.LocationId;
                customerLocationRole.UserId = roleDetails.UserId;
                customerLocationRole.CreatedDate = roleDetails.CreatedDate;
                customerLocationRole.UpdatedDate = DateTime.Now;
                customerLocationRole.CreatedBy = roleDetails.CreatedBy;
                customerLocationRole.UpdatedBy = roleDetails.UpdatedBy;
                customerLocationRole.IsDeleted = false;

                MappingId = _icustomerLocationRoleMgmtRepository.SaveLocationRoleInfo(customerLocationRole);

            }

            // end of newly code
            else
            {
                customerLocationRole.LocationId = Convert.ToInt16(locationID);
                customerLocationRole.UserId = Convert.ToInt16(UserId);
                customerLocationRole.CreatedDate = DateTime.Now;
                customerLocationRole.UpdatedDate = DateTime.Now;
                customerLocationRole.CreatedBy = currentUser;
                customerLocationRole.UpdatedBy = currentUser;
                customerLocationRole.RoleId = role;
                customerLocationRole.IsDeleted = false;
                MappingId = _icustomerLocationRoleMgmtRepository.SaveLocationRoleInfo(customerLocationRole);
            }


            if (MappingId > 0)
            {
                return Json(new
                {
                    Status = ReturnStatus.OK,
                    success = true,
                    Message = "Role assigned successfully"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Status = ReturnStatus.Err,
                    success = false,
                    Message = "Role not assigned successfully"
                }, JsonRequestBehavior.AllowGet);
            }


        }

        #endregion


        #endregion

        /// <summary>
        /// Called from AddExerciseToProgram screen
        /// </summary>
        /// <param name="programID"></param>
        /// <param name="exerciseID"></param>
        /// <returns></returns>
        [Authorize]
        public JsonResult GetProgramExercise(int programID, int exerciseID)
        {
            List<object> lstReturnObj = new List<object>();

            try
            {
                var progDetail = _iProgramMgmtRepository.GetProgramExercises(programID);
                var prog = _iProgramMgmtRepository.GetProgrambyID(programID);
                var lstSport = _iSportMgmtRepository.ListSports();
                var lstPosition = _iPositionMgmtRepository.ListPositionBySportID(prog.SportID == 0 ? lstSport[0].Id : prog.SportID);

                string Sport = lstSport.FirstOrDefault(s => s.Id == prog.SportID).Name;

                string position = "All";//lstPosition.Where(p => p.ID == prog.PositionID && p.SportID == prog.SportID).FirstOrDefault().Name;

                var progExercise = progDetail.ProgramExercises.FirstOrDefault(e => e.ExerciseID == exerciseID);

                var lstExerTempo = new List<ExerciseTempoModel>();
                var lstTempo = progDetail.ExerciseTempos.Where(e => e.ProgramExerciseID == progExercise.ID).ToList();

                if (lstTempo != null && lstTempo.Count > 0)
                {

                    foreach (ENT.ExerciseTempo exerTempo in lstTempo)
                    {
                        ExerciseTempoModel tempo = new ExerciseTempoModel();

                        tempo.ID = exerTempo.ID;
                        tempo.ProgramExerciseID = exerTempo.ProgramExerciseID;

                        tempo.PrimaryTarget = exerTempo.PrimaryTarget;
                        tempo.PairedTarget = exerTempo.PairedTarget;
                        tempo.SetNum = exerTempo.SetNum;
                        tempo.PairedTempo = exerTempo.PairedTempo;
                        tempo.Tempo = exerTempo.Tempo;
                        tempo.PairedTempoName = TempoModel.Tempos.FirstOrDefault(t => t.Value == exerTempo.PairedTempo.ToString()).Key;
                        tempo.TempoName = TempoModel.Tempos.FirstOrDefault(t => t.Value == exerTempo.Tempo.ToString()).Key;

                        tempo.PrimaryIntensityTarget = exerTempo.PrimaryIntensityTarget;
                        tempo.PairedIntensityTarget = exerTempo.PairedIntensityTarget;
                        tempo.TimeToComplete = exerTempo.TimeToComplete;
                        tempo.IsPairedExercise = progExercise.PairedExercise;

                        if (progExercise.PrimaryExerciseUnits == 1)
                        {
                            tempo.PrimaryExerciseUnit = "Volume";

                            tempo.PrimaryExerciseTarget = "# of Reps";
                        }
                        else if (progExercise.PrimaryExerciseUnits == 2)
                        {
                            tempo.PrimaryExerciseUnit = "Volume";

                            tempo.PrimaryExerciseTarget = "Time (Min)";
                        }
                        else
                        {
                            tempo.PrimaryExerciseUnit = "Intensity";

                            tempo.PrimaryExerciseTarget = "Body Weight";

                            if (progExercise.PrimaryExerciseUnits == 1)
                            {
                                tempo.PrimaryExerciseTarget = "% 1RM";
                            }
                            else if (progExercise.PrimaryExerciseUnits == 2)
                            {
                                tempo.PrimaryExerciseTarget = "Load (Lbs)";
                            }

                        }

                        if (progExercise.PairedExerciseUnits == 1)
                        {
                            tempo.PairedExerciseUnit = "Volume";

                            tempo.PairedExerciseTarget = "# of Reps";
                        }
                        else if (progExercise.PairedExerciseUnits == 2)
                        {
                            tempo.PrimaryExerciseUnit = "Volume";

                            tempo.PairedExerciseTarget = "Time (Min)";
                        }
                        else
                        {
                            tempo.PairedExerciseUnit = "Intensity";

                            tempo.PairedExerciseTarget = "Body Weight";

                            if (progExercise.PairedExerciseUnits == 1)
                            {
                                tempo.PairedExerciseTarget = "% 1RM";
                            }
                            else if (progExercise.PairedExerciseUnits == 2)
                            {
                                tempo.PairedExerciseTarget = "Load (Lbs)";
                            }
                        }

                        //For intensity
                        if (progExercise.PrimaryIntensityUnit == 1)
                        {
                            tempo.PrimaryIntensityUnit = "% 1RM";
                        }
                        else if (progExercise.PrimaryIntensityUnit == 2)
                        {
                            tempo.PrimaryIntensityUnit = "Load (Lbs)";
                        }
                        else
                        {
                            tempo.PrimaryIntensityUnit = "Body Weight";
                        }

                        if (progExercise.PairedIntensityUnit == 1)
                        {
                            tempo.PairedIntensityUnit = "% 1RM";
                        }
                        else if (progExercise.PairedIntensityUnit == 2)
                        {
                            tempo.PairedIntensityUnit = "Load (Lbs)";
                        }
                        else
                        {
                            tempo.PairedIntensityUnit = "Body Weight";
                        }

                        lstExerTempo.Add(tempo);
                    }//foreach
                }
                else
                {
                    lstTempo = new List<ENT.ExerciseTempo>();

                    for (int i = 1; i <= progExercise.NumeofSets; i++)
                    {

                        ExerciseTempoModel tempo = new ExerciseTempoModel();

                        tempo.ID = 0;
                        tempo.ProgramExerciseID = progExercise.ID;

                        tempo.PrimaryTarget = 0;
                        tempo.PairedTarget = 0;
                        tempo.SetNum = i;
                        tempo.PairedTempo = 1;
                        tempo.PairedTempoName = TempoModel.Tempos.FirstOrDefault(t => t.Value == "1").Key;
                        tempo.TempoName = TempoModel.Tempos.FirstOrDefault(t => t.Value == "1").Key;
                        tempo.Tempo = 1;
                        tempo.PrimaryIntensityTarget = 0;
                        tempo.PairedIntensityTarget = 0;
                        tempo.TimeToComplete = 0;
                        tempo.IsPairedExercise = progExercise.PairedExercise;

                        if (progExercise.PrimaryExerciseUnits == 1)
                        {
                            tempo.PrimaryExerciseUnit = "Volume";

                            tempo.PrimaryExerciseTarget = "Time (Min)";

                            if (progExercise.PrimaryExerciseUnits == 1)
                            {
                                tempo.PrimaryExerciseTarget = "# of Reps";
                            }
                        }
                        else
                        {
                            tempo.PrimaryExerciseUnit = "Intensity";

                            tempo.PrimaryExerciseTarget = "Body Weight";

                            if (progExercise.PrimaryExerciseUnits == 1)
                            {
                                tempo.PrimaryExerciseTarget = "% 1RM";
                            }
                            else if (progExercise.PrimaryExerciseUnits == 2)
                            {
                                tempo.PrimaryExerciseTarget = "Load (Lbs)";
                            }

                        }

                        if (progExercise.PairedExerciseUnits == 1)
                        {
                            tempo.PairedExerciseUnit = "Volume";

                            tempo.PairedExerciseTarget = "Time (Min)";

                            if (progExercise.PairedExerciseUnits == 1)
                            {
                                tempo.PairedExerciseTarget = "# of Reps";
                            }
                        }
                        else
                        {
                            tempo.PairedExerciseUnit = "Intensity";

                            tempo.PairedExerciseTarget = "Body Weight";

                            if (progExercise.PairedExerciseUnits == 1)
                            {
                                tempo.PairedExerciseTarget = "% 1RM";
                            }
                            else if (progExercise.PairedExerciseUnits == 2)
                            {
                                tempo.PairedExerciseTarget = "Load (Lbs)";
                            }
                        }

                        //For intensity
                        if (progExercise.PrimaryIntensityUnit == 1)
                        {
                            tempo.PrimaryIntensityUnit = "% 1RM";
                        }
                        else if (progExercise.PrimaryIntensityUnit == 2)
                        {
                            tempo.PrimaryIntensityUnit = "Load (Lbs)";
                        }
                        else
                        {
                            tempo.PrimaryIntensityUnit = "Body Weight";
                        }

                        if (progExercise.PairedIntensityUnit == 1)
                        {
                            tempo.PairedIntensityUnit = "% 1RM";
                        }
                        else if (progExercise.PairedIntensityUnit == 2)
                        {
                            tempo.PairedIntensityUnit = "Load (Lbs)";
                        }
                        else
                        {
                            tempo.PairedIntensityUnit = "Body Weight";
                        }

                        lstExerTempo.Add(tempo);
                    }
                }

                //progDetail.ExerciseTempos = lstTempo;

                decimal programTimeToComplete = prog.FoamRollOut + prog.DynamicWarmup;
                decimal setTimeCompletion = 0M;
                decimal exerciseCompleteTotal = 0M;
                foreach (ENT.ExerciseTempo exTemp in progDetail.ExerciseTempos)
                {
                    setTimeCompletion = setTimeCompletion + exTemp.TimeToComplete;
                    exerciseCompleteTotal = exerciseCompleteTotal + exTemp.TimeToComplete;
                }

                programTimeToComplete = programTimeToComplete + setTimeCompletion;

                lstReturnObj.Add(progDetail);
                lstReturnObj.Add(Sport);
                lstReturnObj.Add(position);
                lstReturnObj.Add(prog.TrainingSeasonID == 1 ? "Peak Season" : "Off Season");
                lstReturnObj.Add(prog.TrainingPhase == null ? "" : prog.TrainingPhase);
                lstReturnObj.Add(lstExerTempo);
                lstReturnObj.Add(programTimeToComplete);
                lstReturnObj.Add(prog.FoamRollOut);
                lstReturnObj.Add(prog.DynamicWarmup);
                lstReturnObj.Add(exerciseCompleteTotal);
            }
            catch (Exception ex)
            {
                string exm = ex.Message;
            }
            return Json(lstReturnObj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveProgramExercise(ENT.ProgramExercise ProgramExercise, ENT.ExerciseTempo ExerciseTempo, bool saveExerciseTempo)
        {
            var ro = new ReturnObjectModel();
            ro.Status = ReturnStatus.Err;
            ro.Message = "Save Program Exercise Failed";

            var progDetail = new ENT.ProgramDetail();
            progDetail.ProgramExercises = new List<ENT.ProgramExercise>();

            //if (ProgramExercise.ID <= 0)
            //{
            progDetail.ExerciseTempos = new List<ENT.ExerciseTempo>();

            for (int i = 0; i <= ProgramExercise.NumeofSets - 1; i++)
            {
                ENT.ExerciseTempo exerTempo = new ENT.ExerciseTempo();

                exerTempo.ProgramExerciseID = ExerciseTempo.ProgramExerciseID;
                exerTempo.RestInterval = 0;//not needed due to latest UI 06/02/2017
                exerTempo.SetNum = i + 1;
                exerTempo.TargetIntensity = 0; //not needed due to latest UI 06/02/2017 ExerciseTempo.TargetIntensity;
                exerTempo.TargetReps = 0; //not needed due to latest UI 06/02/2017 ExerciseTempo.TargetReps;
                exerTempo.Tempo = 1;
                exerTempo.PairedTempo = 1;
                exerTempo.TimeToComplete = 0; //not needed due to latest UI 06/02/2017 ExerciseTempo.TimeToComplete;
                exerTempo.PrimaryTarget = ExerciseTempo.PrimaryTarget;
                exerTempo.PairedTarget = ExerciseTempo.PairedTarget;
                exerTempo.PrimaryIntensityTarget = ExerciseTempo.PrimaryIntensityTarget;
                exerTempo.PairedIntensityTarget = ExerciseTempo.PairedIntensityTarget;

                progDetail.ExerciseTempos.Add(exerTempo);
            }
            //}//If ProgramExercise.ID <=0

            if (ProgramExercise.PairedExercise == false)
            {
                ProgramExercise.PairedExerciseID = 0;
            }

            progDetail.ProgramExercises.Add(ProgramExercise);
            int programExerciseID = _iProgramMgmtRepository.SaveProgramExercise(progDetail);

            if (programExerciseID > 0)
            {
                ro.Status = ReturnStatus.Redirect;
                ro.Message = "Program Exercise Saved successfully";
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        private string GenerateExcerciseGridColumns(int maxSets)
        {
            List<object> cList = new List<object>();
            TemplateColumn c1 = new TemplateColumn();
            c1.field = "Order";
            c1.title = "Sequence";
            c1.width = "80px";
            c1.template = @"<div style='text-align: center;'>
    <a href=""javascript:ChangeOrderUp(#=ProgramExerciseID#);"" style='text-decoration: none;'>
        <i class='fa fa-arrow-up' style='display:inline;color:darkgrey;padding-left:2px;'></i></a> 
    <a href=""javascript:ChangeOrderDown(#=ProgramExerciseID#);"" style='text-decoration: none;'>
        <i class='fa fa-arrow-down' style='display:inline;color:darkgrey;padding-left:2px;padding-right:5px;'></i></a>#=Order#<div>";
            cList.Add(c1);

            TemplateColumn c2 = new TemplateColumn();
            c2.field = "Name";
            c2.title = "Exercise";
            c2.template = "<a onclick=\"NavigatetoAdd(#=MasterExerciseID#);\" style=\"cursor: pointer;\">#=Name#</a>";
            c2.width = "100px";
            cList.Add(c2);

            Column c3 = new Column();
            c3.field = "MovementType";
            c3.title = "Movements";
            cList.Add(c3);

            //Column c4 = new Column();
            //c4.field = "Tempo1";
            //c4.title = "Tempo";
            //cList.Add(c4);

            Column c5 = new Column();
            c5.field = "Rest1";
            c5.title = "Rest Between Paired Exercises (Mins)";
            cList.Add(c5);

            Column c6 = new Column();
            c6.field = "Rest2";
            c6.title = "Rest Between Sets (Mins)";
            cList.Add(c6);

            ColumnTree ci = null;
            Column cj = null;
            Column ck = null;
            Column cl = null;
            for (int i = 0; i < maxSets; i++)
            {
                ci = new ColumnTree();
                ci.columns = new List<Column>();
                ci.field = "Set" + (i + 1);
                ci.title = "Set" + (i + 1);
                cj = new Column();
                cj.field = "LoadOrRM" + (i + 1);
                cj.title = "Target Load or % 1RM";
                cj.width = "95px";
                ck = new Column();
                ck.field = "RepOrSec" + (i + 1);
                ck.title = "Target Reps or Sec";
                ck.width = "95px";
                cl = new Column();
                cl.field = "Tempo" + (i + 1);
                cl.title = "Tempo";
                cl.width = "75px";
                ci.columns.Add(cl);
                ci.columns.Add(cj);
                ci.columns.Add(ck);
                cList.Add(ci);
            }

            var serializer = new JavaScriptSerializer();
            var jsonColumns = serializer.Serialize(cList);

            return "{ \"Columns\" : [" + jsonColumns + "]}";
        }
        /// <summary>
        /// Called from ProgramExercises Screen
        /// </summary>
        /// <param name="programID"></param>
        /// <returns></returns>
        public JsonResult GetExerciseListForProgram(string sessionNum, int programID)
        {
            var viewModels = new List<object>();

            var prog = _iProgramMgmtRepository.GetProgrambyID(programID);
            var progDet = _iProgramMgmtRepository.GetProgramExercises(programID);

            int exerciseStep = Convert.ToInt32(sessionNum.Split('~')[0]);
            int exerciseGroup = Convert.ToInt32(sessionNum.Split('~')[1]);

            progDet.ProgramExercises = progDet.ProgramExercises
                .Where(p => p.ProgramID == programID && p.ExerciseStepID == exerciseStep && p.ExerciseGroupID == exerciseGroup)
                .ToList();

            dynamic expando = new ExpandoObject();
            var expandoObj = expando as IDictionary<String, object>;
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new ExpandoJSONConverter() });

            //int maxSets = (progDet.ProgramExercises.Count>1)?progDet.ProgramExercises.Max(t => t.NumeofSets):0;
            int maxSets = (progDet.ProgramExercises.Count > 0) ? progDet.ProgramExercises.Max(t => t.NumeofSets) : 0;

            string columnsJson = GenerateExcerciseGridColumns(maxSets);
            string jsonData = string.Empty;

            List<ENT.MovementType> lstMovement = _iMovementTypeReposioty.ListAllMovementTypes();


            if (progDet.ProgramExercises != null && progDet.ProgramExercises.Count > 1)
            {
                progDet.ProgramExercises = progDet.ProgramExercises.OrderBy(p => p.OrderNum).ToList();
            }

            foreach (ENT.ProgramExercise progExercise in progDet.ProgramExercises)
            {
                ProgramExerciseModel proExerModel = new ProgramExerciseModel();
                expando = new ExpandoObject();
                expandoObj = expando as IDictionary<String, object>;

                proExerModel.ExerciseID = Convert.ToString(progExercise.ExerciseID);
                proExerModel.ProgramExerciseID = Convert.ToString(progExercise.ID);

                var exercise = _iExerciseManagement.GetExercise(progExercise.ExerciseID);

                List<ENT.ExerciseTempo> proTempos = progDet.ExerciseTempos.Where(t => t.ProgramExerciseID == progExercise.ID).ToList();
                if (proTempos != null && proTempos.Count > 0)
                {
                    foreach (ENT.ExerciseTempo proTempo in proTempos)
                    {
                        string tempo = TempoModel.Tempos.FirstOrDefault(k => k.Value == proTempo.Tempo.ToString()).Key;

                        expandoObj["Set" + proTempo.SetNum.ToString()] = proTempo.SetNum.ToString();
                        expandoObj["Tempo" + proTempo.SetNum.ToString()] = tempo;
                        expandoObj["TempoID" + proTempo.SetNum.ToString()] = proTempo.ID.ToString();
                        //expandoObj["ProgramExerciseID" + proTempo.SetNum.ToString()] = proTempo.ProgramExerciseID.ToString();
                        //expandoObj["Rest" + proTempo.SetNum.ToString()] = proTempo.RestInterval.ToString();
                        if (progExercise.PrimaryExerciseTarget == 1)
                        {
                            expandoObj["LoadOrRM" + proTempo.SetNum.ToString()] = proTempo.PrimaryTarget;
                            expandoObj["RepOrSec" + proTempo.SetNum.ToString()] = proTempo.PrimaryIntensityTarget + " % of 1 RM";
                        }
                        else
                        {
                            expandoObj["LoadOrRM" + proTempo.SetNum.ToString()] = proTempo.PrimaryIntensityTarget + " % of 1 RM";
                            expandoObj["RepOrSec" + proTempo.SetNum.ToString()] = proTempo.PrimaryTarget;
                        }


                        expandoObj["TimeToComplete" + proTempo.SetNum.ToString()] = proTempo.TimeToComplete.ToString();

                    }
                }

                proExerModel.Name = exercise.Name;
                ENT.MovementType movementType = lstMovement.FirstOrDefault(m => m.Id == progExercise.MovementTypeID);

                proExerModel.MovementType = movementType == null ? "" : movementType.Name;

                var exerciseTempo = proTempos.FirstOrDefault();
                proExerModel.LoadOrRM = progExercise.IntensityVol == 2 && exerciseTempo != null ? Convert.ToString(proTempos.FirstOrDefault().TargetReps) : "";
                proExerModel.RepOrSec = progExercise.IntensityVol == 1 && exerciseTempo != null ? Convert.ToString(proTempos.FirstOrDefault().TargetIntensity) : "";
                proExerModel.Rest = proTempos.FirstOrDefault() == null ? "" : Convert.ToString(proTempos.FirstOrDefault().RestInterval); //"60";
                proExerModel.Rest2 = progExercise.RestBetweenSets.ToString();

                proExerModel.TextAlign = "text-align:left;";
                proExerModel.StyleUp = "p-lg";
                proExerModel.OrderUp = "inline";
                proExerModel.OrderDown = "inline";
                proExerModel.ColorUp = "lightgray";
                proExerModel.ColorDown = "darkgray";

                expandoObj["Order"] = progExercise.OrderNum;
                if (progExercise.PairedExercise)
                {
                    expandoObj["Order"] = progExercise.OrderNum + "A";
                }
                expandoObj["Rest1"] = progExercise.RestBetweenPairedExercises.ToString();//.RestInterval.ToString();
                expandoObj["Rest2"] = progExercise.RestBetweenSets.ToString();//.RestInterval.ToString();
                expandoObj["OrderUp"] = proExerModel.OrderUp;
                expandoObj["OrderDown"] = proExerModel.OrderDown;
                expandoObj["StyleUp"] = proExerModel.StyleUp;
                expandoObj["ColorUp"] = proExerModel.ColorUp;
                expandoObj["ColorDown"] = proExerModel.ColorDown;
                expandoObj["TextAlign"] = proExerModel.TextAlign;
                expandoObj["Name"] = proExerModel.Name;
                expandoObj["MovementType"] = proExerModel.MovementType;
                expandoObj["ExerciseID"] = proExerModel.ExerciseID;
                expandoObj["MasterExerciseID"] = proExerModel.ExerciseID;
                expandoObj["ProgramExerciseID"] = proExerModel.ProgramExerciseID;
                jsonData = jsonData + serializer.Serialize(expando) + ",";

                if (progExercise.PairedExercise)
                {
                    jsonData = jsonData + GetPairedExercise(progDet, progExercise.ID);
                }

            }

            jsonData = "{ \"ProgramsList\" : [" + jsonData.TrimEnd(new char[] { ',' }) + "]}";
            viewModels.Add(jsonData);
            viewModels.Add(columnsJson);

            decimal progTimeToComplete = 0M;
            progTimeToComplete = prog.DynamicWarmup + prog.FoamRollOut;
            //decimal exerciseTimeTocomplete = 0M;
            foreach (ENT.ExerciseTempo exTempo in progDet.ExerciseTempos)
            {
                progTimeToComplete = progTimeToComplete + exTempo.TimeToComplete;
                //exerciseTimeTocomplete = exerciseTimeTocomplete + exTempo.TimeToComplete;
            }

            viewModels.Add(progTimeToComplete);

            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        private string GetPairedExercise(ENT.ProgramDetail progDet, int primaryProgID)
        {
            string jsonData = string.Empty;

            var viewModels = new List<object>();
            _iProgramMgmtRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();
            dynamic expando = new ExpandoObject();
            var expandoObj = expando as IDictionary<String, object>;
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new ExpandoJSONConverter() });

            //int maxSets = (progDet.ProgramExercises.Count>1)?progDet.ProgramExercises.Max(t => t.NumeofSets):0;
            int maxSets = (progDet.ProgramExercises.Count > 0) ? progDet.ProgramExercises.Max(t => t.NumeofSets) : 0;
            string columnsJson = GenerateExcerciseGridColumns(maxSets);

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
                expando = new ExpandoObject();
                expandoObj = expando as IDictionary<String, object>;

                proExerModel.ExerciseID = Convert.ToString(progExercise.PairedExerciseID);
                proExerModel.ProgramExerciseID = Convert.ToString(progExercise.ID);
                var exercise = _iExerciseManagement.GetExercise(progExercise.PairedExerciseID);
                string exerciseName = exercise != null ? exercise.Name : "";

                //switch (progExercise.PairedExerciseID)
                //{
                //    case 48:
                //        exerciseName = "Squat, Back";
                //        break;
                //    case 8:
                //        exerciseName = "Bench Press Flat, Dumbbell";
                //        break;
                //    case 9:
                //        exerciseName = "Bench Press, Flat, Barbell";
                //        break;
                //    case 17:
                //        exerciseName = "Push Up";
                //        break;
                //    case 28:
                //        exerciseName = "Box Jumps";
                //        break;
                //    case 39:
                //        exerciseName = "Hops, Double Leg";
                //        break;
                //    case 40:
                //        exerciseName = "Hops, Single Leg";
                //        break;
                //}

                exerciseName = exercise.Name;
                List<ENT.ExerciseTempo> proTempos = progDet.ExerciseTempos.Where(t => t.ProgramExerciseID == primaryProgID).ToList();
                if (proTempos != null && proTempos.Count > 0)
                {
                    foreach (ENT.ExerciseTempo proTempo in proTempos)
                    {
                        string tempo = TempoModel.Tempos.FirstOrDefault(k => k.Value == proTempo.Tempo.ToString()).Key;
                        expandoObj["Set" + proTempo.SetNum.ToString()] = proTempo.SetNum.ToString();
                        expandoObj["Tempo" + proTempo.SetNum.ToString()] = tempo;
                        expandoObj["TempoID" + proTempo.SetNum.ToString()] = proTempo.ID.ToString();
                        //expandoObj["ProgramExerciseID" + proTempo.SetNum.ToString()] = proTempo.ProgramExerciseID.ToString();
                        //expandoObj["Rest" + proTempo.SetNum.ToString()] = proTempo.RestInterval.ToString();

                        if (progExercise.PairedExerciseTarget == 1)
                        {
                            expandoObj["LoadOrRM" + proTempo.SetNum.ToString()] = proTempo.PairedTarget;
                            expandoObj["RepOrSec" + proTempo.SetNum.ToString()] = proTempo.PairedIntensityTarget + " % of 1 RM";
                        }
                        else
                        {
                            expandoObj["LoadOrRM" + proTempo.SetNum.ToString()] = proTempo.PairedIntensityTarget + " % of 1 RM";
                            expandoObj["RepOrSec" + proTempo.SetNum.ToString()] = proTempo.PairedTarget;
                        }

                        expandoObj["TimeToComplete" + proTempo.SetNum.ToString()] = proTempo.TimeToComplete.ToString();

                    }
                }

                proExerModel.Name = exerciseName;
                ENT.MovementType movementType = lstMovement.FirstOrDefault(m => m.Id == progExercise.PairedExerciseMovementID);

                proExerModel.MovementType = movementType == null ? "" : movementType.Name;

                var exerciseTempo = proTempos.FirstOrDefault();
                proExerModel.LoadOrRM = progExercise.IntensityVol == 2 && exerciseTempo != null ? Convert.ToString(proTempos.FirstOrDefault().TargetReps) : "";
                proExerModel.RepOrSec = progExercise.IntensityVol == 1 && exerciseTempo != null ? Convert.ToString(proTempos.FirstOrDefault().TargetIntensity) : "";
                proExerModel.Rest = proTempos.FirstOrDefault() == null ? "" : Convert.ToString(proTempos.FirstOrDefault().RestInterval); //"60";

                proExerModel.TextAlign = "text-align:left;";
                proExerModel.StyleUp = "p-lg";
                proExerModel.OrderUp = "inline";
                proExerModel.OrderDown = "inline";
                proExerModel.ColorUp = "lightgray";
                proExerModel.ColorDown = "darkgray";

                expandoObj["Order"] = progExercise.OrderNum + "B";
                expandoObj["Rest1"] = progExercise.RestBetweenPairedExercises.ToString();//.RestInterval.ToString();
                expandoObj["Rest2"] = progExercise.RestBetweenSets.ToString();//.RestInterval.ToString();
                expandoObj["OrderUp"] = proExerModel.OrderUp;
                expandoObj["OrderDown"] = proExerModel.OrderDown;
                expandoObj["StyleUp"] = proExerModel.StyleUp;
                expandoObj["ColorUp"] = proExerModel.ColorUp;
                expandoObj["ColorDown"] = proExerModel.ColorDown;
                expandoObj["TextAlign"] = proExerModel.TextAlign;
                expandoObj["Name"] = proExerModel.Name;
                expandoObj["MovementType"] = proExerModel.MovementType;
                expandoObj["ExerciseID"] = proExerModel.ExerciseID;
                expandoObj["MasterExerciseID"] = progExercise.ExerciseID;
                expandoObj["ProgramExerciseID"] = proExerModel.ProgramExerciseID;
                jsonData = jsonData + serializer.Serialize(expando) + ",";

            }

            return jsonData;
        }

        public JsonResult ChangeProgramExerciseOrder(int ProgramID, int ProgramExerciseID, string OrderSeq)
        {
            bool result = _iProgramMgmtRepository.ChangeExerciseOrder(ProgramID, ProgramExerciseID, OrderSeq);
            ReturnObjectModel ro = new ReturnObjectModel
            {
                Status = result ? ReturnStatus.OK : ReturnStatus.Err,
                Message = result ? "Change Order successful" : "Change order failed"
            };

            return Json(ro, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult SaveExerciseTempo(ENT.ExerciseTempo exerciseTempo)
        public JsonResult SaveExerciseTempo(ExerciseTempoModel exerciseTempo, decimal restSets, int restPairedExercise)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            ENT.ExerciseTempo exerTempo = new ENT.ExerciseTempo();

            exerTempo.ID = exerciseTempo.ID;
            exerTempo.ProgramExerciseID = exerciseTempo.ProgramExerciseID;
            exerTempo.Tempo = exerciseTempo.Tempo;
            exerTempo.PairedTempo = exerciseTempo.PairedTempo;
            exerTempo.SetNum = exerciseTempo.SetNum;
            exerTempo.PrimaryTarget = exerciseTempo.PrimaryTarget;
            exerTempo.PairedTarget = exerciseTempo.PairedTarget;
            exerTempo.PrimaryIntensityTarget = exerciseTempo.PrimaryIntensityTarget;
            exerTempo.PairedIntensityTarget = exerciseTempo.PairedIntensityTarget;

            double timeToComplete = 0D;

            //Formula = (Time to complete 1 Rep)x(# of Reps in that Set) + (Rest Between Sets)
            timeToComplete = (Convert.ToDouble(exerTempo.PrimaryTarget) * 0.1) + Convert.ToDouble(restSets);

            exerTempo.TimeToComplete = Convert.ToDecimal(timeToComplete);

            if (exerciseTempo.PairedTarget != null && exerciseTempo.PairedTarget > 0)
            {
                double pairedTimetoComplete = (Convert.ToDouble(exerTempo.PrimaryTarget) * 0.1) + restPairedExercise + Convert.ToDouble(restSets);

                exerTempo.TimeToComplete = Convert.ToDecimal(pairedTimetoComplete);
            }

            bool result = _iProgramMgmtRepository.SaveProgramExerciseTempo(exerTempo);

            ro.Status = result == true ? ReturnStatus.OK : ReturnStatus.Err;
            ro.Message = result == true ? "Exercise Tempo saved" : "Save failed";

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CopyTKGProgam(int copyFromProgramID, string newProgramName, string newProgramDesc)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            ro.Message = "Program copy failed";
            ro.Status = ReturnStatus.Err;

            var cm = _iCustomerMgmt.GetCustomerInfo(Convert.ToString((Session["CustomerName"])));
            var customerCategoryType = (CustomerCategoryType)cm.Category;

            var user = (User)Session["AuthenticatedUser"];
            var ownerUserId = user.UserId;

            if (user.IsIndividualAthlete)
            {
                customerCategoryType = CustomerCategoryType.Individual;
            }

            _iProgramMgmtRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();
            bool result = _iProgramMgmtRepository.CopyTKGProgram(copyFromProgramID, newProgramName, newProgramDesc, customerCategoryType, ownerUserId);

            if (result)
            {
                ro.Status = ReturnStatus.OK;
                ro.Message = "Program Copied successfully";
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangePassword(int athleteID, string newPassword)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            ro.Message = "Password change failed";
            ro.Status = ReturnStatus.Err;

            bool result = _iAccount.UpdateNewPassword(_iAccount.GenerateEncryptedString(newPassword), athleteID);

            ro.Status = result == true ? ReturnStatus.OK : ReturnStatus.Err;
            ro.Message = result == true ? "Change password successful" : "Password change failed";

            return Json(ro, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Schedules()
        {
            return View();
        }

        public JsonResult ListCoaches()
        {
            //var userDataVisibilityList = userDataVisibilityRepository.GetAllUserDataVisibility();
            //userDataVisibilityList = userDataVisibilityList.Where(i => i.ResponsibilityType != UserResponsibilityType.Other).ToList();

            //string userIDs = string.Join(",", userDataVisibilityList.Select(u => u.UserId));

            //if (userIDs == "") return Json(new List<SelectListViewModel>(), JsonRequestBehavior.AllowGet);

            return Json(_iAccount.ListAllActiveUsers().Where(u => u.UserType != UserType.Athlete)//_iAccount.GetUsersByUserIDs(userIDs)
                    .Select(c => new { c.UserId, c.FirstName, c.LastName })
                    .ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListTeams()
        {
            var lstTeam = _iteamMgmtRepository.ListAllActiveTeams();
            return Json(lstTeam, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompleteReassignment(int reassignType, string userIDs, int objectID)
        {
            var ro = new ReturnObjectModel
            {
                Message = "Reassignment Failed",
                Status = ReturnStatus.Err
            };

            var currentUserId = ((User)(Session["AuthenticatedUser"])).UserId;
            var userIdList = userIDs.TrimEnd(',').Split(',').Select(id => Convert.ToInt32(id)).ToList();

            bool result = _iAccount.CompleteReassignment(reassignType, userIDs, objectID);
            if (result)
            {
                // Moved this line here for recognizing result of operation.
                userHistoryRepository.AddUserHistory(currentUserId, userIdList, reassignType == 2, reassignType == 1, objectID);

                ro.Message = "Reassignment Successful";
                ro.Status = ReturnStatus.OK;
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Teams()
        {
            return View();
        }

        public ActionResult ManageTeams()
        {
            return View();
        }

        public JsonResult GetTeams([DataSourceRequest]DataSourceRequest request)
        {
            List<TeamViewModel> lstResult = new List<TeamViewModel>();
            TeamDetails td = _iteamMgmtRepository.ListTeamswithAllDetails();

            if (td != null)
            {
                foreach (var source in td.Teams)
                {
                    var tvm = new TeamViewModel
                    {
                        ID = source.ID,
                        Name = source.Name,
                        CoachID = source.CoachID,
                        Description = source.Description,
                        Gender = source.Gender,
                        Month = source.Month,
                        Year = source.Year,
                        IsActive = source.IsActive
                    };

                    if (source.CoachID != 0)
                    {
                        var coach = td.Coaches.FirstOrDefault(c => c.UserId == source.CoachID);
                        if (coach != null)
                        {
                            tvm.Coach = coach.FirstName + " " + coach.LastName;
                        }
                    }
                    else
                    {
                        tvm.Coach = "No Coach";
                    }

                    if (source.SportID != 0)
                    {
                        var sport = td.Sports.FirstOrDefault(c => c.Id == source.SportID);
                        if (sport != null)
                        {
                            tvm.Sport = sport.Name;
                        }
                    }
                    else
                    {
                        tvm.Sport = "No Sport";
                    }

                    tvm.IsSystemCreated = source.IsSystemCreated;

                    lstResult.Add(tvm);
                }
            }

            return Json(lstResult
                    .OrderBy(s => s.Sport)
                    .ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteTeam(int id)
        {
            try
            {
                if (_iteamMgmtRepository.DeleteTeam(id))
                    return Json(new ReturnObjectModel() { Status = ReturnStatus.OK, Message = "Team deleted successfully." }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new ReturnObjectModel() { Status = ReturnStatus.Err, Message = "Unable to delete team" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ReturnObjectModel() { Status = ReturnStatus.Err, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateTeamStatus(int id)
        {
            var result = _iteamMgmtRepository.UpdateTeamStatus(id);

            return Json(new ReturnObjectModel()
            {
                Status = result ? ReturnStatus.OK : ReturnStatus.Err
            },
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetActiveSports()
        {
            _iSportMgmtRepository = ObjectFactory.GetInstance<ISportsManagementRepository>();

            List<ENT.Sport> lstSport = _iSportMgmtRepository.ListSports(true);

            return Json(lstSport, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeamInfoByID(int teamID)
        {
            Team teamInfo = _iteamMgmtRepository.ListTeamByID(teamID);

            return Json(teamInfo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveTeamInfo(Team teamInfo)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            ro.Message = "Team Save Failed";
            ro.Status = ReturnStatus.Err;
            teamInfo.IsActive = true;

            int result = _iteamMgmtRepository.SaveTeamInfo(teamInfo);

            if (result > 0)
            {
                ro.Message = "Team Info Saved|" + result;
                ro.Status = ReturnStatus.OK;
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }



        public ActionResult UserHistory(int userID, string filter)
        {
            Session["historyforUserID"] = userID;
            Session["historyforfilter"] = filter;
            return View();
        }

        public JsonResult GetUserHistory([DataSourceRequest]DataSourceRequest request)
        {
            int userID = Convert.ToInt32(Session["historyforUserID"]);
            string filter = Convert.ToString(Session["historyforfilter"]);

            var userDetails = _iAccount.GetUserInfoByID(userID);

            if (filter == null || filter == "")
            {
                filter = "coach";
            }

            List<UserHistoryViewModel> userViewModelHistory = new List<UserHistoryViewModel>();
            List<UserHistory> userHistory = userHistoryRepository.GetUserHistoryByUserId(userID);

            if (userHistory != null && userHistory.Count > 0)
            {
                userHistory = userHistory.Where(uh => uh.Field.ToLower() == filter.ToLower()).ToList();

                foreach (UserHistory uh in userHistory)
                {
                    if (uh != null)
                    {
                        UserHistoryViewModel uhvm = new UserHistoryViewModel();

                        uhvm.AthleteName = userDetails.Users[0].FirstName + " " + userDetails.Users[0].LastName;
                        uhvm.ByUserId = uh.ByUserId;
                        uhvm.CoachId = uh.CoachId;
                        uhvm.Id = uh.Id;
                        uhvm.TeamId = uh.TeamId;
                        uhvm.StartDate = uh.StartDate;
                        uhvm.EndDate = uh.EndDate;
                        uhvm.UpdateField = uh.Field;
                        uhvm.UserId = uh.UserId;

                        if (filter.ToLower() == "coach")
                        {
                            List<User> lstCoach = _iAccount.ListUsersByType(UserType.Coach);

                            User coach = lstCoach.FirstOrDefault(c => c.UserId == uhvm.CoachId);

                            uhvm.FieldName = coach.FirstName + " " + coach.LastName;
                            uhvm.FieldTitle = "Coach";
                        }
                        else
                        {
                            Team team = _iteamMgmtRepository.ListTeamByID(uhvm.TeamId);

                            uhvm.FieldName = team.Name;
                            uhvm.FieldTitle = "Team";
                        }

                        userViewModelHistory.Add(uhvm);
                    }
                }
            }

            return Json(userViewModelHistory.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AthleteAssessment()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            var currentUser = (User)Session["AuthenticatedUser"];
            return View(currentUser);
        }
        [HttpPost]
        public JsonResult SetShowWelcome(bool showWelcome)
        {
            var currentUser = (User)Session["AuthenticatedUser"];
            currentUser.ShowWelcome = showWelcome;
            Session["AuthenticatedUser"] = currentUser;
            return Json(_iAccount.SetShowWelcome(showWelcome, currentUser.UserId));
        }

        public JsonResult GetCustomerInforRegistration()
        {
            CustomerMaster cm = _iCustomerMgmt.GetCustomerInfo(Convert.ToString((Session["CustomerName"])));

            CustomerMasterTextViewModel cmtv = new CustomerMasterTextViewModel();

            cmtv.CustomerMasterID = cm.CustomerID;
            cmtv.CustomerName = cm.CustomerName;
            cmtv.RegistrationAthletePageText = cm.RegistrationAthletePageText;
            cmtv.RegistrationAthletePageTextID = cm.RegistrationAthletePageTextID;

            return Json(cmtv, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    file.SaveAs(path);
                }
            }

            return Json(new { Status = ReturnStatus.OK }, JsonRequestBehavior.AllowGet);
        }
    }
}
