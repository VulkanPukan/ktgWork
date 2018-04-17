var sampleData = [
    {
        id: 1,
        teamName: "Female Volleyball Sophomore B July 2017",
        athleteName: "Sally Moon",
        age: 16,
        position: 1,                                //M dropdown
        program: "Volleyball Explosive Strength",   //???
        height: 73,                                 //Number
        weight: 165,                                //Number
        standingReach: 95,                          //Number
        squatJump: 17,                              //Number
        counterMovementJump: 24,                    //Number
        standingLongJump: 104,                      //Number
        peakPowerGenerated: 3963,                   //Calculated
        onermBarbellbackSquat: 150,                 //Number
        onermTrapbarDeadlift: 185,                  //Number
        onermBarbellHipThrust: 212.5,               //Number
        onermBarbellStanding: 40,                   //Number
        onermBarbellBenchPress: 100,                //Number
        onermChinUps: 140,                          //Number
        onermBarbellBentOverRow: 100,               //Number
    },
    {
        id: 1,
        teamName: "Female Volleyball Sophomore B July 2017",
        athleteName: "Sally Moon",
        age: 16,
        position: 1,                                //M dropdown
        program: "Volleyball Explosive Strength",   //???
        height: 73,                                 //Number
        weight: 165,                                //Number
        standingReach: 95,                          //Number
        squatJump: 17,                              //Number
        counterMovementJump: 24,                    //Number
        standingLongJump: 104,                      //Number
        peakPowerGenerated: 3963,                   //Calculated
        onermBarbellbackSquat: 150,                 //Number
        onermTrapbarDeadlift: 185,                  //Number
        onermBarbellHipThrust: 212.5,               //Number
        onermBarbellStanding: 40,                   //Number
        onermBarbellBenchPress: 100,                //Number
        onermChinUps: 140,                          //Number
        onermBarbellBentOverRow: 100,               //Number
    },
    {
        id: 1,
        teamName: "Female Volleyball Sophomore B July 2017",
        athleteName: "Sally Moon",
        age: 16,
        position: 1,                                //M dropdown
        program: "Volleyball Explosive Strength",   //???
        height: 73,                                 //Number
        weight: 165,                                //Number
        standingReach: 95,                          //Number
        squatJump: 17,                              //Number
        counterMovementJump: 24,                    //Number
        standingLongJump: 104,                      //Number
        peakPowerGenerated: 3963,                   //Calculated
        onermBarbellbackSquat: 150,                 //Number
        onermTrapbarDeadlift: 185,                  //Number
        onermBarbellHipThrust: 212.5,               //Number
        onermBarbellStanding: 40,                   //Number
        onermBarbellBenchPress: 100,                //Number
        onermChinUps: 140,                          //Number
        onermBarbellBentOverRow: 100,               //Number
    }, ];
var assessmentCoachSelect = $('#assessmentCoachSelect');
var assessmentSportSelect = $('#assessmentSportSelect');
var assessmentTeamSelect = $('#assessmentTeamSelect');
var assessmentReportSelect = $('#assessmentReportSelect');
var assessmentApplyFilter = $('#assessmentApplyFilter');
var assessmentGridSelectedRowIndex = -1;

var assessmentGridDataSource = new kendo.data.DataSource({
    transport: {
        read: {
            url: "../Assessment/GetAssessmentGridData",
            type: "post",
            dataType: "json",
            data: function () {
                return {
                    coachId: assessmentCoachSelect.val(),
                    sportIds: assessmentSportSelect.val(),
                    teamIds: assessmentTeamSelect.val(),
                    reportId: assessmentReportSelect.val()
                };
            }
        },
        update: {
            url: "../Assessment/UpdateAssessment",
            type: "post"
        }
    },
    schema: {
        model: {
            id: "id",
            fields: {
                id: { editable: false, type: "number" },
                teamName: { editable: false },
                athleteName: { editable: false },
                age: { editable: false, type: "number" },
                position: { editable: false },
                program: { editable: false },
                height: { editable: true, nullable: "0", validation: { required: true } },
                weight: { editable: true, nullable: "0", validation: { required: true } },
                standingReach: { editable: true, nullable: "0", validation: { required: true } },
                squatJump: { editable: true, nullable: "0", validation: { required: true } },
                counterMovementJump: { editable: true, nullable: "0", validation: { required: true } },
                standingLongJump: { editable: true, nullable: "0", validation: { required: true } },
                peakPowerGenerated: { editable: false, nullable: "0", validation: { required: true } },
                onermBarbellbackSquat: { editable: true, nullable: "0", validation: { required: true } },
                onermTrapbarDeadlift: { editable: true, nullable: "0", validation: { required: true } },
                onermBarbellHipThrust: { editable: true, nullable: "0", validation: { required: true } },
                onermBarbellStanding: { editable: true, nullable: "0", validation: { required: true } },
                onermBarbellBenchPress: { editable: true, nullable: "0", validation: { required: true } },
                onermChinUps: { editable: true, nullable: "0", validation: { required: true } },
                onermBarbellBentOverRow: { editable: true, nullable: "0", validation: { required: true } },
            }
        }
    }
});
var positionDropdownDataSource = new kendo.data.DataSource({
    data:
    [
    { kkey: "M", vvalue: "1" },
    { kkey: "L", vvalue: "2" },
    { kkey: "P", vvalue: "3" },
    { kkey: "X", vvalue: "4" }
    ]
});
$("#assessmentGrid").kendoGrid({
    dataBound: function () {},
    selectable: true,
    change: function (e) {
        var seletedRowIndex = getSelectedRowIndex();
        if (assessmentGridSelectedRowIndex == seletedRowIndex)
            return;
        else
            assessmentGridSelectedRowIndex = seletedRowIndex;
        assessmentKendoGrid.dataSource.sync();
       
        var dataItem = assessmentKendoGrid.dataSource.data()[seletedRowIndex];
        var temp = assessmentKendoGrid.table.find('tr[data-uid="' + dataItem.uid + '"]');
        assessmentKendoGrid.select(temp);
        editRow(seletedRowIndex + 1);
    },
    dataSource: assessmentGridDataSource,
    columns: [
        { field: "teamName", title: "Team Name" },
        { field: "athleteName", title: "Athlete Name" },
        { field: "age", title: "Age" },
        { field: "position", title: "Position" },
        { field: "program", title: "Program" },
        { field: "height", title: "Height (inches)" },
        { field: "weight", title: "Weight (lbs)" },
        { field: "standingReach", title: "Standing Reach (inches)" },
        { field: "squatJump", title: "Squat Jump (inches)" },
        { field: "counterMovementJump", title: "Counter-movement Jump (inches)" },
        { field: "standingLongJump", title: "Standing Long Jump (inches)" },
        { field: "peakPowerGenerated", title: "Peak Power Generated (watts)" },
        { field: "onermBarbellbackSquat", title: "1RM Barbell Back Squat (lbs)" },
        { field: "onermTrapbarDeadlift", title: "1RM Trapbar Deadlift (lbs)" },
        { field: "onermBarbellHipThrust", title: "1RM Barbell Hip Thrust (lbs)" },
        { field: "onermBarbellStanding", title: "1RM Barbell Standing Overhead Shoulder Press (lbs)" },
        { field: "onermBarbellBenchPress", title: "1RM Barbell Bench Press (lbs)" },
        { field: "onermChinUps", title: "1RM  Chin Ups/ Cable Pulldown (lbs)" },
        { field: "onermBarbellBentOverRow", title: "1RM  Barbell Bent Over Row (lbs)" }],
    editable: "inline"
});
var assessmentKendoGrid = $('#assessmentGrid').data('kendoGrid');

function positionDropDownEditor(container, options) {
    $('<input data-text-field="kkey" data-value-field="vvalue" data-bind="value:' + options.field + '" />')
    .appendTo(container)
    .kendoDropDownList({
        dataSource: positionDropdownDataSource,
        dataTextField: "kkey",
        dataValueField: "vvalue"
    });
};
function getSelectedRowIndex() {

    return assessmentKendoGrid.select()[0].rowIndex;
};
function setPeakPowerValue(value) {
    var data = assessmentKendoGrid.dataSource.data()[getSelectedRowIndex()];
    data["peakPowerGenerated"] = "" + value;
    assessmentKendoGrid.select()[0].cells[11].textContent = value;
};
function getWeightValue() {
    var data = assessmentKendoGrid.dataSource.data()[getSelectedRowIndex()];
    return data["weight"]
};
function getSquatJumpValue() {
    var data = assessmentKendoGrid.dataSource.data()[getSelectedRowIndex()];
    return data["squatJump"]
};
function calculatePearPower() {
    var temp1 = 60.7 * getSquatJumpValue() * 2.54;
    var temp2 = 45.3 * getWeightValue() / 2.2;
    var result = temp1 + temp2 - 2055;
    setPeakPowerValue(result);
};
function editRow(index) {
    assessmentKendoGrid.editRow($('#assessmentGrid tr:eq(' + index + ')'));
    $("input[name=weight]").change(function (e) {
        calculatePearPower()
    });
    $("input[name=squatJump]").change(function (e) {
        calculatePearPower()
    });
};

function getTeamSelectValues() {
    var coachId = assessmentCoachSelect.val();
    $.ajax({
        url: "../Assessment/GetTeamsByCoach",
        type: "POST",
        data: JSON.stringify({ coachId: coachId }),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            console.log("getTeamSelectValues success!");
            assessmentTeamSelect.empty();
            assessmentTeamSelect.append($('<option>', {
                value: 0,
                text: ""
            }));
            $.each(result, function (i, item) {
                assessmentTeamSelect.append($('<option>', {
                    value: item.Id,
                    text: item.Name
                }));
            });
            assessmentTeamSelect.prop("disabled", false);
        },
        error: function (req, status, errorObj) {
            console.log("getTeamSelectValues error!");
        }
    });
};
function getSportSelectValues() {
    var coachId = assessmentCoachSelect.val();
    $.ajax({
        url: "../Assessment/GetSportsByCoach",
        type: "POST",
        data: JSON.stringify({ coachId: coachId }),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            console.log("getSportSelectValues success!");
            assessmentSportSelect.empty();
            assessmentSportSelect.append($('<option>', {
                value: 0,
                text: ""
            }));
            $.each(result, function (i, item) {
                assessmentSportSelect.append($('<option>', {
                    value: item.Id,
                    text: item.Name
                }));
            });
            assessmentSportSelect.prop("disabled", false);
        },
        error: function (req, status, errorObj) {
            console.log("getSportSelectValues error!");
        }
    });
};
function getCoachSelectValues() {
    $.ajax({
        url: "../Assessment/GetCoachs",
        type: "POST",
        data: JSON.stringify({}),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            console.log("getCoachSelectValues success!");
            assessmentCoachSelect.append($('<option>', {
                value: 0,
                text: ""
            }));
            $.each(result, function (i, item) {
                assessmentCoachSelect.append($('<option>', {
                    value: item.Id,
                    text: item.Name
                }));
            });
            assessmentCoachSelect.prop("disabled", false);
        },
        error: function (req, status, errorObj) {
            console.log("getCoachSelectValues error!");
        }
    });
};
function initCoachSelect() {
    assessmentCoachSelect.change(function (e) {
        assessmentSportSelect.prop("disabled", true);
        assessmentTeamSelect.prop("disabled", true);
        var coachId = assessmentCoachSelect.val();
        if (coachId == 0)
            return;
        getSportSelectValues();
        getTeamSelectValues();
    });
    getCoachSelectValues();
};

function updateGrid() {
    assessmentKendoGrid.dataSource.read();
    assessmentKendoGrid.refresh();
}
function initApplyFilterButton() {
    assessmentApplyFilter.click(function () {
        var coachId = assessmentCoachSelect.val();
        if (coachId < 0)
            return;
        var sportId = assessmentSportSelect.val();
        if (sportId < 0)
            return;
        var teamId = assessmentTeamSelect.val();
        if (teamId < 0)
            return;
        var reportId = assessmentReportSelect.val();
        if (reportId < 0)
            return;
        updateGrid();
    });
};

initApplyFilterButton();
initCoachSelect();


