var teamProgramVolumeIntensityChart = {
    holder: $('#teamProgramVolumeIntensityChart'),
    startDateButton: $('#teamProgramVolumeIntensityChartStart'),
    endDateButton: $('#teamProgramVolumeIntensityChartEnd'),
    coachSelect: $('#teamProgramVolumeIntensityChartCoachSelect'),
    data: undefined,
    startDate: undefined,
    startDateGrid: undefined,
    endDate: undefined,
    endDateGrid: undefined,
    chartOptions: undefined,
    yAxisTickFormatter: function (tick) {
        return tick + "%";
    },
    yAxisTickFormatter2: function (tick) {
        if (tick == "0") {
            return tick;
        }
        return tick + ",000";
    }
}
teamProgramVolumeIntensityChart.chartOptions = {
    axisLabels: {
        show: true
    },
    legend: {
        position: 'nw',
        noColumns: 3,
        container: $("#teamProgramVolumeIntensityChartLegend")
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
            axisLabel: 'Volume Lifted, Lbs',
            position: "right",
            tickFormatter: teamProgramVolumeIntensityChart.yAxisTickFormatter2,
            min: 0,
            max: 70
        },
        {
            position: "left",
            tickFormatter: teamProgramVolumeIntensityChart.yAxisTickFormatter,
            min: 60,
            max: 85
        }
    ],
};
teamProgramVolumeIntensityChart.setStartDate = function (date) {
    this.startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid = new Date(this.startDate.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid.setDate(this.startDateGrid.getDate() - 15);
};
teamProgramVolumeIntensityChart.setEndDate = function (date) {
    this.endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid = new Date(this.endDate.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid.setDate(this.endDateGrid.getDate() - 15);
};
teamProgramVolumeIntensityChart.loadData = function () {
    var coachId = 0;
    if (teamProgramVolumeIntensityChart.coachSelect[0])
        coachId = teamProgramVolumeIntensityChart.coachSelect[0].value;
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
            if (teamProgramVolumeIntensityChart.holder.length) {
                if (resp.data.length == 0) {
                    resp.data = [
                        { label: "", data: 0 }
                    ];
                }
                $.plot(teamProgramVolumeIntensityChart.holder, teamProgramVolumeIntensityChart.data, teamProgramVolumeIntensityChart.chartOptions);
            }
        }, error: function () {
            console.log(resp.Message);
        }
    });
}
teamProgramVolumeIntensityChart.initTestData = function () {
    var line1 = [[1, 5], [2, 5], [4, 7], [6, 7]];
    this.data = [
    {
        label: "Volume",
        data: [[0, 35], [1, 50], [2, 60], [3, 45], [4, 55], [5, 65], [6, 35], [7, 40], [8, 45],
               [9, 40], [10, 50], [11, 60], [12, 38], [13, 42], [14, 48], [15, 34], [16, 42], [17, 50]],
        bars: {
            show: true,
            barWidth: 0.3,
            fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
        }
    },
    {
        label: "Intensity",
        data: [[0, 65], [2, 65], [3, 70], [5, 70], [6, 75], [8, 75],
               [9, 70], [11, 70], [12, 75], [14, 75], [15, 80], [17, 80]],
        yaxis: 2,
        lines: {
            show: true,
            lineWidth: 2,
            fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
        }
    }];
},
teamProgramVolumeIntensityChart.initDateButtons = function () {
    this.startDateButton.click(function () {
        openStartDateModal(teamProgramVolumeIntensityChart.startDate, function (newDate) {
            teamProgramVolumeIntensityChart.setStartDate(newDate);
            teamProgramVolumeIntensityChart.loadData();
        });
    });
    this.endDateButton.click(function () {
        openEndDateModal(teamProgramVolumeIntensityChart.endDate, function (newDate) {
            teamProgramVolumeIntensityChart.setEndDate(newDate);
            teamProgramVolumeIntensityChart.loadData();
        });
    });
};
teamProgramVolumeIntensityChart.init = function () {
    teamProgramVolumeIntensityChart.setStartDate(getStartDateDefault());
    teamProgramVolumeIntensityChart.setEndDate(getEndDateDefault());
    teamProgramVolumeIntensityChart.initDateButtons();
    teamProgramVolumeIntensityChart.initTestData();
    teamProgramVolumeIntensityChart.coachSelect.on('change', function () {
        teamProgramVolumeIntensityChart.loadData();
    })
}
teamProgramVolumeIntensityChart.init();
teamProgramVolumeIntensityChart.loadData();
