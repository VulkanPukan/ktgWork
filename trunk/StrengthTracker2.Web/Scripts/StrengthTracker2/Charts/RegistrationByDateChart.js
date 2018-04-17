var registrationByDateChart = {
    holder: $('#registrationByDateChart'),
    startDateButton: $('#registrationByDateChartStart'),
    endDateButton: $('#registrationByDateChartEnd'),
    registrations: undefined,
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
        if (registrationByDateChart.data[0].data.length > 12) {
            result += " " + date.getFullYear() % 100;
        }
        return result;
    }
};
registrationByDateChart.data = [
        {
            label: "Enrollment",
            data: this.customers,
            bars: {
                show: true,
                lineWidth: 0,
                barWidth: 36 * 24 * 60 * 60 * 250,
                fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
            }
        }];
registrationByDateChart.chartOptions = {
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
        noColumns: 2,
        container: $("#registrationByDateLegend")
    },
    tooltip: true,
    tooltipOpts: {
        content: '%x: %y'
    },
    xaxis: {
        mode: "time",
        tickSize: [1, "month"],
        tickLength: 0,
        tickFormatter: registrationByDateChart.tickFormatter,
        font: {
            size: 9,
            lineHeight: 13,
            color: "#000000"
        }
    },
    yaxes: [
        {
            position: "left", ticks: 6, tickDecimals: 0
        }
    ],
    colors: ['#009933', '#333333', '#00FF00'],
};
registrationByDateChart.setStartDate = function (date) {
    this.startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid = new Date(this.startDate.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid.setDate(this.startDateGrid.getDate() - 15);
    this.chartOptions.xaxis.min = this.startDateGrid.getTime();
}
registrationByDateChart.setEndDate = function (date) {
    this.endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid = new Date(this.endDate.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid.setDate(this.endDateGrid.getDate() - 15);
    this.chartOptions.xaxis.max = this.endDateGrid.getTime();
}
registrationByDateChart.initTestData = function () {
    this.registrations = [[1482796800000, 15], [1485475200000, 25], [1487894400000, 12], [1490572800000, 15],
                 [1493164800000, 18], [1495843200000, 9], [1498435200000, 18], [1501113600000, 23],
                 [1503792000000, 16], [1506384000000, 4], [1509062400000, 9], [1511654400000, 17]];
}
registrationByDateChart.loadData = function () {
    $.ajax({
        url: "../Chart/RegistrationByDateGrid",
        type: "POST",
        async: true,
        data: {
            startDate: formatDate(this.startDate),
            endDate: formatDate(this.endDate),
            step: '0000-01-01'
        },
        success: function (resp) {
            if (registrationByDateChart.holder.length) {
                //registrationByDateChart.data[0].data = registrationByDateChart.registrations;
                if (resp.data.length == 0) {
                    resp.data = [{ data: [], label: "" }];
                }
                registrationByDateChart.data[0].data = resp.data[0].data;
                $.plot(registrationByDateChart.holder, registrationByDateChart.data,
                       registrationByDateChart.chartOptions);
            }
        }, error: function () {
            console.log(resp.Message);
        }
    });
}
registrationByDateChart.initDateButtons = function () {
    this.startDateButton.click(function () {
        openStartDateModal(registrationByDateChart.startDate, function (newDate) {
            registrationByDateChart.setStartDate(newDate);
            registrationByDateChart.loadData();
        });
    });
    this.endDateButton.click(function () {
        openEndDateModal(registrationByDateChart.endDate, function (newDate) {
            registrationByDateChart.setEndDate(newDate);
            registrationByDateChart.loadData();
        });
    });
}

registrationByDateChart.initTestData();
registrationByDateChart.setStartDate(getStartDateDefault());
registrationByDateChart.setEndDate(getEndDateDefault());
registrationByDateChart.initDateButtons();
registrationByDateChart.loadData();
