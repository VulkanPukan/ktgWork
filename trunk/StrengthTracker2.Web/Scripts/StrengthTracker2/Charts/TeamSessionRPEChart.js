var teamSessionRPEChart = {
    holder: $('#teamSessionRPEChart'),
    startDateButton: $('#teamSessionRPEChartStart'),
    endDateButton: $('#teamSessionRPEChartEnd'),
    coachSelect: $('#teamSessionRPEChartCoachSelect'),
    data: undefined,
    startDate: undefined,
    startDateGrid: undefined,
    endDate: undefined,
    endDateGrid: undefined,
    chartOptions: undefined,
}
teamSessionRPEChart.chartOptions = {
    series: {
        lines: {
            show: true,
            fill: false,
            lineWidth: 2,
        },
    },
    legend: {
        position: 'nw',
        noColumns: 3,
        container: $("#teamSessionRPEChartLegend")
    },
    grid: {
        hoverable: true,
        clickable: false,
        borderWidth: 1,
        tickColor: '#eaeaea',
        borderColor: '#eaeaea',
    },
    shadowSize: 0,
    tooltip: true,
    tooltipOpts: {
        content: '%s: %y'
    },
    colors: ['#4B4B7C', '#3EB15B', '#47BCC7', '#C38FBB'],
    yaxes: [
        {
            position: "left", ticks: 6, tickDecimals: 0,
            min: 0
        }
    ],
};
teamSessionRPEChart.setStartDate = function (date) {
    this.startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid = new Date(this.startDate.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid.setDate(this.startDateGrid.getDate() - 15);
};
teamSessionRPEChart.setEndDate = function (date) {
    this.endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid = new Date(this.endDate.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid.setDate(this.endDateGrid.getDate() - 15);
};
teamSessionRPEChart.loadData = function () {
    var coachId = 0;
    if (teamSessionRPEChart.coachSelect[0])
        coachId = teamSessionRPEChart.coachSelect[0].value;
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
            if (teamSessionRPEChart.holder.length) {
                //if (resp.data.length == 0) {
                //    resp.data = [
                //        { label: "", data: 0 }
                //    ];
                //}
                $.plot(teamSessionRPEChart.holder, teamSessionRPEChart.data, teamSessionRPEChart.chartOptions);
            }
        }, error: function () {
            console.log(resp.Message);
        }
    });
}
teamSessionRPEChart.initTestData = function () {
    var line1 = [[1, 5], [2, 5], [4, 7], [6, 7]];
    var line2 = [[1, 7.5], [2, 7.8], [3, 7.2], [5, 8.2], [6, 8.2]];
    var line3 = [[1, 10], [2, 10], [3, 9], [4, 10], [5, 10], [6, 9]];
    this.data = [{ label: "Team avreage", data: line2 },
                 { label: "Individual Hige", data: line3 },
                 { label: "Individual Low", data: line1 },
    ];
},
teamSessionRPEChart.initDateButtons = function () {
    this.startDateButton.click(function () {
        openStartDateModal(teamSessionRPEChart.startDate, function (newDate) {
            teamSessionRPEChart.setStartDate(newDate);
            teamSessionRPEChart.loadData();
        });
    });
    this.endDateButton.click(function () {
        openEndDateModal(teamSessionRPEChart.endDate, function (newDate) {
            teamSessionRPEChart.setEndDate(newDate);
            teamSessionRPEChart.loadData();
        });
    });
};
teamSessionRPEChart.init = function () {
    teamSessionRPEChart.setStartDate(getStartDateDefault());
    teamSessionRPEChart.setEndDate(getEndDateDefault());
    teamSessionRPEChart.initDateButtons();
    teamSessionRPEChart.initTestData();
    teamSessionRPEChart.coachSelect.on('change', function () {
        teamSessionRPEChart.loadData();
    })
}
teamSessionRPEChart.init();
teamSessionRPEChart.loadData();
