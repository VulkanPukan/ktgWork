var athletesStatusChart = {
    holder: $('#athletesStatusChart'),
    startDateButton: $('#athletesStatusChartStart'),
    endDateButton: $('#athletesStatusChartEnd'),
    activated: undefined,
    deactivated: undefined,
    difference: undefined,
    data: undefined,
    startDate: undefined,
    startDateGrid: undefined,
    endDate: undefined,
    endDateGrid: undefined,
    chartOptions: undefined,
    tickFormatter: function (tick) {
        var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        var date = new Date(tick);
        var result = monthNames[date.getMonth()];
        if (athletesStatusChart.data[0].data && athletesStatusChart.data[0].data.length > 12) {
            result += " " + date.getFullYear() % 100;
        }
        return result;
    }
};
athletesStatusChart.data = [
    {
        label: "Monthly Enrolled (Registered & Activated)",
        data: this.activated,
        bars: {
            show: true,
            lineWidth: 0,
            barWidth: 36 * 24 * 60 * 60 * 250,
            fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
        }
    },
    {
        label: "Deactivated by Month",
        data: this.deactivated,
        bars: {
            show: true,
            lineWidth: 0,
            barWidth: 36 * 24 * 60 * 60 * 250,
            fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
        }
    },
    {
        label: "Cumulative Net",
        data: this.difference,
        yaxis: 2,
        lines: {
            show: true,
            lineWidth: 2,
            fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
        }
    }];
athletesStatusChart.chartOptions = {
    series: {
        shadowSize: 0,
        bars: {
            lineWidth: 2,
            fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
        }
    },
    grid: {
        hoverable: true,
        clickable: false,
        borderWidth: 1,
        tickColor: '#eaeaea',
        borderColor: '#eaeaea',
    },
    legend: {
        position: 'nw',
        noColumns: 3,
        container: $("#athletesStatusLegend")
    },
    tooltip: true,
    tooltipOpts: {
        content: '%x: %y'
    },
    xaxis: {
        mode: "time",
        tickSize: [1, "month"],
        tickLength: 0,
        tickFormatter: athletesStatusChart.tickFormatter,
        font: {
            size: 9,
            lineHeight: 13,
            color: "#000000"
        }
    },
    yaxes: [
        {
            position: "left", ticks: 6, tickDecimals: 0
        },
        {
            position: "right", ticks: 6, tickDecimals: 0
        }
    ],
    colors: ['#3b76a0', '#333333', '#009933'],
};
athletesStatusChart.setStartDate = function (date) {
    this.startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid = new Date(this.startDate.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid.setDate(this.startDateGrid.getDate() - 15);
    this.chartOptions.xaxis.min = this.startDateGrid.getTime();
};
athletesStatusChart.setEndDate = function (date) {
    this.endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid = new Date(this.endDate.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid.setDate(this.endDateGrid.getDate() - 15);
    this.chartOptions.xaxis.max = this.endDateGrid.getTime();
};
athletesStatusChart.loadData = function () {
    $.ajax({
        url: "../Chart/RegisteredSignedupGrid",
        type: "POST",
        async: true,
        data: {
            startDate: formatDate(this.startDate),
            endDate: formatDate(this.endDate),
            step: '0000-01-01'
        },
        success: function (resp) {
            if (athletesStatusChart.holder.length) {
                var activated = resp.data[0].data;
                var deactivated = resp.data[1].data;
                var difference = resp.difference.data;
                athletesStatusChart.data[0].data = activated;
                athletesStatusChart.data[1].data = deactivated;
                athletesStatusChart.data[2].data = difference;

                var minDeativated = 0;
                var maxActivated = 0;
                var maxDifference = 0;
                for (var i = 0; i < deactivated.length; i++) {
                    if (minDeativated > deactivated[i][1])
                        minDeativated = deactivated[i][1];
                    if (maxActivated < activated[i][1])
                        maxActivated = activated[i][1];
                    if (maxDifference < difference[i][1])
                        maxDifference = difference[i][1];
                }
                var min1 = ((minDeativated - minDeativated % 10) / 10 - 1) * 10;
                var max1 = ((maxActivated - maxActivated % 10) / 10 + 1) * 10;
                var max2 = ((maxDifference - maxDifference % 10) / 10 + 1) * 10;

                athletesStatusChart.chartOptions.yaxes[0].min = min1;
                athletesStatusChart.chartOptions.yaxes[0].max = max1;
                athletesStatusChart.chartOptions.yaxes[1].max = max2;
                athletesStatusChart.chartOptions.yaxes[1].min = max2 / max1 * min1;

                $.plot(athletesStatusChart.holder, athletesStatusChart.data,
                       athletesStatusChart.chartOptions);
            }
        }, error: function () {
            console.log(resp.Message);
        }
    });
};
athletesStatusChart.initDateButtons = function () {
    this.startDateButton.click(function () {
        openStartDateModal(athletesStatusChart.startDate, function (newDate) {
            athletesStatusChart.setStartDate(newDate);
            athletesStatusChart.loadData();
        });
    });
    this.endDateButton.click(function () {
        openEndDateModal(athletesStatusChart.endDate, function (newDate) {
            athletesStatusChart.setEndDate(newDate);
            athletesStatusChart.loadData();
        });
    });
};

athletesStatusChart.setStartDate(new Date());
var endDate = new Date();
endDate.setMonth(endDate.getMonth() - 1);
athletesStatusChart.setEndDate(endDate);
athletesStatusChart.initDateButtons();
athletesStatusChart.loadData();