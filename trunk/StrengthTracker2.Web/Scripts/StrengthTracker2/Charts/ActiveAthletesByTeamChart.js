var activeAthletesByTeamChart = {
    holder: $('#activeAthletesByTeamChart'),
    startDateButton: $('#activeAthletesByTeamChartStart'),
    endDateButton: $('#activeAthletesByTeamChartEnd'),
    coachSelect: $('#activeAthletesByTeamChartCoachSelect'),
    data: undefined,
    startDate: undefined,
    startDateGrid: undefined,
    endDate: undefined,
    endDateGrid: undefined,
    chartOptions: undefined,
}
activeAthletesByTeamChart.chartOptions = {
    series: {
        pie: {
            show: true
        }
    },
    //legend: {
    //    position: 'nw',
    //    noColumns: 2,
    //    container: $("#activeAtheleteBySportLegend")
    //},
    shadowSize: 0,
    tooltip: true,
    tooltipOpts: {
        content: '%s: %y'
    },
    colors: ['#4B4B7C', '#3EB15B', '#47BCC7', '#C38FBB'],
};
activeAthletesByTeamChart.setStartDate = function (date) {
    this.startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid = new Date(this.startDate.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid.setDate(this.startDateGrid.getDate() - 15);
};
activeAthletesByTeamChart.setEndDate = function (date) {
    this.endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid = new Date(this.endDate.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid.setDate(this.endDateGrid.getDate() - 15);
};
activeAthletesByTeamChart.loadData = function () {
    var coachId = 0;
    if (activeAthletesByTeamChart.coachSelect[0])
        coachId = activeAthletesByTeamChart.coachSelect[0].value;
    $.ajax({
        url: "../Chart/ActiveAthletesByTeam",
        type: "POST",
        async: true,
        data: {
            coachId: coachId,
            startDate: formatDate(this.startDate),
            endDate: formatDate(this.endDate),
        },
        success: function (resp) {
            if (activeAthletesByTeamChart.holder.length) {
                if (resp.data.length == 0) {
                    resp.data = [
                        { label: "", data: 0 }
                    ];
                }
                $.plot(activeAthletesByTeamChart.holder, resp.data, activeAthletesByTeamChart.chartOptions);
            }
        }, error: function () {
            console.log(resp.Message);
        }
    });
}
activeAthletesByTeamChart.initDateButtons = function () {
    this.startDateButton.click(function () {
        openStartDateModal(activeAthletesByTeamChart.startDate, function (newDate) {
            activeAthletesByTeamChart.setStartDate(newDate);
            activeAthletesByTeamChart.loadData();
        });
    });
    this.endDateButton.click(function () {
        openEndDateModal(activeAthletesByTeamChart.endDate, function (newDate) {
            activeAthletesByTeamChart.setEndDate(newDate);
            activeAthletesByTeamChart.loadData();
        });
    });
};
activeAthletesByTeamChart.init = function () {
    activeAthletesByTeamChart.setStartDate(getStartDateDefault());
    activeAthletesByTeamChart.setEndDate(getEndDateDefault());
    activeAthletesByTeamChart.initDateButtons();
    activeAthletesByTeamChart.coachSelect.on('change', function () {
        activeAthletesByTeamChart.loadData();
    })
    initCoachSelect('activeAthletesByTeamChartCoachSelect');
}
activeAthletesByTeamChart.init();
activeAthletesByTeamChart.loadData();
