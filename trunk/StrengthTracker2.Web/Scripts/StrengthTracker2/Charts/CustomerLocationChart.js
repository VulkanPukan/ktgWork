var customerLocationChart = {
    holder: $('#customerLocationChart'),
    startDateButton: $('#customerLocationChartStart'),
    endDateButton: $('#customerLocationChartEnd'),
    customers: undefined,
    locations: undefined,
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
        if (customerLocationChart.data[0].data.length > 12) {
            result += " " + date.getFullYear() % 100;
        }
        return result;
    }
};
customerLocationChart.data = [
        {
            label: "Cumulative Customer",
            data: this.customers,
            bars: {
                show: true,
                lineWidth: 0,
                barWidth: 36 * 24 * 60 * 60 * 250,
                fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
            }
        },
        {
            label: "Location Count",
            data: this.locations,
            bars: {
                show: true,
                lineWidth: 0,
                barWidth: 36 * 24 * 60 * 60 * 250,
                fillColor: { colors: [{ opacity: 1 }, { opacity: 1 }] }
            }
        }];
customerLocationChart.chartOptions = {
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
        container: $("#customerLocationLegend")
    },
    tooltip: true,
    tooltipOpts: {
        content: '%x: %y'
    },
    xaxis: {
        mode: "time",
        tickSize: [1, "month"],
        tickLength: 0,
        tickFormatter: customerLocationChart.tickFormatter,
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
    colors: ['#3b76a0', '#333333', '#00FF00'],
};
customerLocationChart.setStartDate = function (date) {
    this.startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid = new Date(this.startDate.getFullYear(), date.getMonth(), date.getDate());
    this.startDateGrid.setDate(this.startDateGrid.getDate() - 15);
    this.chartOptions.xaxis.min = this.startDateGrid.getTime();
}
customerLocationChart.setEndDate = function (date) {
    this.endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid = new Date(this.endDate.getFullYear(), date.getMonth(), date.getDate());
    this.endDateGrid.setDate(this.endDateGrid.getDate() - 15);
    this.chartOptions.xaxis.max = this.endDateGrid.getTime();
}
customerLocationChart.initTestData = function () {
    this.customers = [[1482796800000, 10], [1485475200000, 5], [1487894400000, 2], [1490572800000, 15],
                 [1493164800000, 8], [1495843200000, 9], [1498435200000, 18], [1501113600000, 3],
                 [1503792000000, 6], [1506384000000, 4], [1509062400000, 0], [1511654400000, 7]];
    this.locations = [[1483660800000, 5], [1486339200000, -2], [1488758400000, 12], [1491436800000, 4],
                 [1494028800000, 8], [1496707200000, 3], [1499299200000, 8], [1501977600000, 3],
                 [1504656000000, 7], [1507248000000, 9], [1509926400000, -1], [1512518400000, 9]];
}
customerLocationChart.loadData = function () {
    $.ajax({
        url: "../Chart/ActiveAthletesBySport",
        type: "POST",
        async: true,
        data: {
            startDate: formatDate(this.startDate),
            endDate: formatDate(this.endDate),
            step: '0000-01-01'
        },
        success: function (resp) {
            if (customerLocationChart.holder.length) {
                //customers = resp.data[0].data;
                //locations = resp.data[1].data;
                customerLocationChart.data[0].data = customerLocationChart.customers;
                customerLocationChart.data[1].data = customerLocationChart.locations;

                $.plot(customerLocationChart.holder, customerLocationChart.data,
                       customerLocationChart.chartOptions);
            }
        }, error: function () {
            console.log(resp.Message);
        }
    });
}
customerLocationChart.initDateButtons = function () {
    this.startDateButton.click(function () {
        openStartDateModal(customerLocationChart.startDate, function (newDate) {
            customerLocationChart.setStartDate(newDate);
            customerLocationChart.loadData();
        });
    });
    this.endDateButton.click(function () {
        openEndDateModal(customerLocationChart.endDate, function (newDate) {
            customerLocationChart.setEndDate(newDate);
            customerLocationChart.loadData();
        });
    });
}

customerLocationChart.initTestData();
customerLocationChart.setStartDate(getStartDateDefault());
customerLocationChart.setEndDate(getEndDateDefault());
customerLocationChart.initDateButtons();
customerLocationChart.loadData();
