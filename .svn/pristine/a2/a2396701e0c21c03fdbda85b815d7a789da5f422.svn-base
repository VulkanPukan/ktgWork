var activeAthletesBySportChart = {
    holder: $('#activeAthletesBySportChart'),
    startDateButton: $('#activeAthletesBySportChartStart'),
    endDateButton: $('#activeAthletesBySportChartEnd'),
    coachSelect: $('#activeAthletesBySportCoachSelect'),
    data: undefined,
    startDate: undefined,
    startDateGrid: undefined,
    endDate: undefined,
    endDateGrid: undefined,
    chartOptions: undefined,
    //tickFormatter: function (tick) {
    //    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    //    var result = monthNames[tick-1];
    //    return result;
    //}
    tickFormatter: function (tick) {
        var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        var date = new Date(tick);
        var result = monthNames[date.getMonth()];
        if (activeAthletesBySportChart.data[0].data && activeAthletesBySportChart.data[0].data.length > 12) {
            result += " " + date.getFullYear() % 100;
        }
        return result;
    }
}
activeAthletesBySportChart.chartOptions = {
    xaxis: {
        tickFormatter: activeAthletesBySportChart.tickFormatter,
        mode: "time",
        tickSize: [1, "month"],
        tickLength: 0,
        //tickSize: 1,
        //min: 7.5
    },
    grid: {
        hoverable: true,
        clickable: false,
        borderWidth: 1,
        tickColor: '#eaeaea',
        borderColor: '#eaeaea',
    },
    series: {
        stack: true
    },
    legend: {
        position: 'nw',
        noColumns: 3,
        container: $("#activeAthletesBySportChartLegend")
    },
    shadowSize: 0,
    tooltip: true,
    tooltipOpts: {
        content: '%s: %y'
    },
    colors: ['#4B4B7C', '#3EB15B', '#47BCC7', '#C38FBB', '#3b76a0', '#333333', '#00FF00']
};
activeAthletesBySportChart.setStartDate = function (date) {
    this.startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid = new Date(this.startDate.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid.setDate(this.startDateGrid.getDate() - 15);
};
activeAthletesBySportChart.setEndDate = function (date) {
    this.endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid = new Date(this.endDate.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid.setDate(this.endDateGrid.getDate() - 15);
};
activeAthletesBySportChart.loadData = function () {
    var coachId = 0;
    if (activeAthletesBySportChart.coachSelect)
        coachId = activeAthletesBySportChart.coachSelect[0].value;
    $.ajax({
        url: "../Chart/ActiveAthletesBySport",
        type: "POST",
        async: true,
        data: {
            coachId: coachId,
            startDate: formatDate(this.startDate),
            endDate: formatDate(this.endDate),
        },
        success: function (resp) {
            if (activeAthletesBySportChart.holder.length) {
                if (resp.data.length == 0) {
                    resp.data = [{ data: [], label: "" }];
                }
                for(var i=0;i<resp.data.length; i++){
                    resp.data[i].bars = {
                        show: true,
                        lineWidth: 0,
                        barWidth: 36 * 24 * 60 * 60 * 250,
                        fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
                    }
                }
                $.plot(activeAthletesBySportChart.holder, resp.data, activeAthletesBySportChart.chartOptions);
            }
        }, error: function () {
            console.log(resp.Message);
        }
    });
}
activeAthletesBySportChart.initDateButtons = function () {
    activeAthletesBySportChart.startDateButton.click(function () {
        openStartDateModal(activeAthletesBySportChart.startDate, function (newDate) {
            activeAthletesBySportChart.setStartDate(newDate);
            activeAthletesBySportChart.loadData();
        });
    });
    activeAthletesBySportChart.endDateButton.click(function () {
        openEndDateModal(activeAthletesBySportChart.endDate, function (newDate) {
            activeAthletesBySportChart.setEndDate(newDate);
            activeAthletesBySportChart.loadData();
        });
    });
};
activeAthletesBySportChart.initTestData = function () {
    activeAthletesBySportChart.data = [
        {
            label: "Girls Volleyball",
            data: [[8, 60], [9, 58], [10, 55]],
            bars: {
                show: true,
                barWidth: 0.5,
                fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
            }
        },
        {
            label: "Girls Golf",
            data: [[8, 23], [9, 20], [10, 20]],
            bars: {
                show: true,
                barWidth: 0.5,
                fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
            }
        },
        {
            label: "Boys Volleyball",
            data: [[9, 45], [10, 40], [11, 40]],
            bars: {
                show: true,
                barWidth: 0.5,
                fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
            }
        }];
};

activeAthletesBySportChart.init = function () {
    activeAthletesBySportChart.setStartDate(getStartDateDefault());
    activeAthletesBySportChart.setEndDate(getEndDateDefault());
    activeAthletesBySportChart.initDateButtons();
    activeAthletesBySportChart.initTestData();
    activeAthletesBySportChart.coachSelect.on('change', function () {
        activeAthletesBySportChart.loadData();
    })
    initCoachSelect('activeAthletesBySportCoachSelect');
}
activeAthletesBySportChart.init();
activeAthletesBySportChart.loadData();

