function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}
function getStartDateDefault() {
    var now = new Date();
    var startDate = new Date(now.getFullYear(), 0, 1);
    return startDate;
}
function getEndDateDefault() {
    var now = new Date();
    var endDate = new Date(now.getFullYear(), 12, 0);
    return endDate;
}
function initStartDate(startDate, startDateGrid) {
    var now = new Date();
    startDate = new Date(now.getFullYear(), 0, 1);
    startDateGrid = new Date(startDate.getFullYear(), 0, 1);
    startDateGrid.setDate(startDateGrid.getDate() - 15);
}
function initEndDate(endDate, endDateGrid) {
    var now = new Date();
    endDate = new Date(now.getFullYear(), 12, 0);
    endDateGrid = new Date(endDate.getFullYear(), 12, 0);
    endDateGrid.setDate(endDateGrid.getDate() - 15);
}

function openStartDateModal(initDate, callback) {
    $("#startDatepicker").datepicker("setDate", initDate);
    $("#startDateSave").off();
    $("#startDateSave").click(function () {
        var dateString = $("#startDatepicker").val();
        var date = new Date(dateString);
        callback(date);
        $("#startDateModal").modal('hide');
    });
    $("#startDateModal").modal('show');
}
function openEndDateModal(initDate, callback) {
    $("#endDatepicker").datepicker("setDate", initDate);
    $("#endDateSave").off();
    $("#endDateSave").click(function () {
        var dateString = $("#endDatepicker").val();
        var date = new Date(dateString);
        callback(date);
        $("#endDateModal").modal('hide');
    });
    $("#endDateModal").modal('show');
}

function initModalDatePickers() {
    $("#startDatepicker").datepicker();
    $("#endDatepicker").datepicker();
}

function initCoachSelect(selectId) {
    $.ajax({
        url: "../Chart/GetCoachListForUser",
        type: "POST",
        data: JSON.stringify({}),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            console.log("GetCoachListForUser success!");
            $.each(result, function (i, item) {
                $('#' + selectId).append($('<option>', {
                    value: item.Id,
                    text: item.Name
                }));
            });
        },
        error: function (req, status, errorObj) {
            console.log("GetCoachListForUser error!");
        }
    });
}
